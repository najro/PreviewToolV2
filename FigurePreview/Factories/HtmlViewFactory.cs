using System;
using FigurePreview.Configuration;
using System.IO;
using System.Text;
using FigurePreview.Models;

namespace FigurePreview.Factories
{
    public class HtmlViewFactory
    {
        private const string HtmlViewFolder = "HtmlView";

        public string CreateHtmlViewForFile(FigureItem displayFigureItem)
        {
            var figures = FigureConfiguration.Instance.FigurePreview.Figure;
            var htmlContent = new StringBuilder();


            htmlContent.AppendLine("<div class=\"figures\">");

            foreach (var figure in figures)
            {
                htmlContent.AppendLine("<div class=\"figure\">");

                htmlContent.Append($"<h2>{figure.Name}</h2>");


                if (displayFigureItem.HasNotValidFigureExtensions(figure))
                {
                    htmlContent.Append($"<div class=\"ext-error\">Mappe innholder filer med ikke godkjent format</div>");
                }

                // build up content
                foreach (var ext in figure.Extentions.Ext)
                {
                   

                    if (displayFigureItem.HasExtension(figure, ext))
                    {

                        var figureInfo = displayFigureItem.GetFigureInfoForExtension(figure, ext);

                        htmlContent.Append($"<div class=\"ext-header\">Preview {ext}</div>");
                        htmlContent.Append($"<div class=\"ext-content\">{BuildFigureContentBasedOnExtension(figureInfo)}</div>");
                    }
                    else
                    {
                         htmlContent.Append($"<div class=\"ext-header\">Preview {ext}</div>");
                        htmlContent.Append($"<div class=\"ext-content\">Savnes</div>");
                    }

                }

                htmlContent.AppendLine("</div>");
            }

            htmlContent.AppendLine("</div>");

            var displayContent = htmlContent.ToString();

            // read template file and replace with content
            var templateFilePath = GetTemplateFilePath();
            var templateHtml = File.ReadAllText(templateFilePath, Encoding.UTF8);
            //previewHtml = previewHtml.Replace("#chartJson#", !string.IsNullOrEmpty(jsonData) ? jsonData : "undefined");

            var viewHtml = templateHtml.Replace("{{FiguresInfo}}", displayContent);
            var viewPath = GetViewFilePath(displayFigureItem.Name);
            File.WriteAllText(viewPath, viewHtml, Encoding.UTF8);

          


            return viewPath;
        }

        private string GetTemplateFilePath()
        {
            var templateFilePath = Directory.GetCurrentDirectory() + $"\\{HtmlViewFolder}\\template\\view-template.htm";
            return templateFilePath;
        }

        private string GetViewFilePath(string fileName)
        {
            var viewPath = Directory.GetCurrentDirectory() + $"\\{HtmlViewFolder}\\View\\{fileName}.htm";
            return viewPath;
        }

        private string BuildFigureContentBasedOnExtension(FigureInfo figureInfo)
        {
            var sb = new StringBuilder();

            switch (figureInfo.FileExtension)
            {
                case "png":
                case "jpg":
                case "jpeg":
                case "svg":
                    sb.AppendLine(BuildImageContent(figureInfo));
                    break;
                case "json":
                    sb.AppendLine(BuildJsonContent(figureInfo));
                    break;
                default:
                    sb.Append($"Finnes ikke supportert visning for {figureInfo.FileExtension}");
                    break;

            }

            return sb.ToString();
        }

        private string BuildImageContent(FigureInfo figureInfo)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"<img src=\"{figureInfo.FilePath}\"/>");
            return sb.ToString();
        }

        private string BuildJsonContent(FigureInfo figureInfo)
        {
            var sb = new StringBuilder();

            var jsonFileContent = string.Join("", File.ReadAllLines(figureInfo.FilePath, Encoding.UTF8));

            // replace all not valid chars with reqexp instead of replace

            var chartId = Guid.NewGuid().ToString("N");

            sb.AppendLine($"<div id='{chartId}'></div>");

            sb.AppendLine("<script>");
            sb.AppendLine($" var jsonData{chartId} = {jsonFileContent};");
            sb.AppendLine("document.addEventListener('DOMContentLoaded',");
            sb.AppendLine("function(){");
            sb.AppendLine("Highcharts.setOptions(setOptions);");
            sb.AppendLine($"Highcharts.chart('{chartId}', jsonData{chartId});");
            sb.AppendLine("});");
            sb.AppendLine("</script>");

          

            return sb.ToString();
        }

    }
}
