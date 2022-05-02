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
                    htmlContent.Append(
                        $"<div class=\"ext-error\">Mappe inneholder fil som ikke er godkjent.");

                    htmlContent.Append("<ul>");
                    foreach (var extNotValid in displayFigureItem.GetNotValidFigureExtensions(figure))
                    {
                        htmlContent.Append($"<li>{extNotValid}</ll>");
                    }
                    htmlContent.Append("</ul>");

                    htmlContent.Append("</div>");
                }

                // build up content
                foreach (var ext in figure.Extentions.Ext)
                {
                    if (displayFigureItem.HasExtension(figure, ext))
                    {

                        var figureInfo = displayFigureItem.GetFigureInfoForExtension(figure, ext);
                        htmlContent.Append($"<div class=\"ext-header\">{figureInfo.FileName}.{ext}</div>");
                        htmlContent.Append($"<div class=\"ext-content\">{BuildFigureContentBasedOnExtension(figureInfo)}</div>");
                    }
                }

                htmlContent.AppendLine("</div>");
            }

            htmlContent.AppendLine("</div>");

            var displayContent = htmlContent.ToString();

            // read template file and replace with content
            var templateFilePath = GetTemplateFilePath();
            var viewHtml = File.ReadAllText(templateFilePath, Encoding.UTF8);

            viewHtml = viewHtml.Replace("{{FiguresContent}}", displayContent);
            viewHtml = viewHtml.Replace("{{FiguresGridStyle}}",
                $".figures {{grid-template-columns : repeat({figures.Count}, 1fr);}}");
            var viewPath = GetViewFilePath(displayFigureItem.Name);
            File.WriteAllText(viewPath, viewHtml, Encoding.UTF8);

            return viewPath;
        }

        public string GetDefaultView()
        {
            string defaultView = Directory.GetCurrentDirectory() + $"\\htmlview\\view\\default.htm";
            return defaultView;
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

        private string GetViewDirectoryPath()
        {
            var viewPath = Directory.GetCurrentDirectory() + $"\\{HtmlViewFolder}\\View";
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
                case "gif":
                    sb.AppendLine(BuildImageContent(figureInfo));
                    break;
                case "json":
                    sb.AppendLine(BuildJsonContent(figureInfo));
                    break;
                default:
                    sb.Append($"Savner visning for {figureInfo.FileExtension}");
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

            var chartId = Guid.NewGuid().ToString("N");

            // add placeholder for highchart
            sb.AppendLine($"<div id='{chartId}' class='chartData'></div>");

            // build script that connect json data with highcharts and inject into placeholder
            sb.AppendLine("<script>");

            var jsonDataVariableName = $"jsonData{chartId}";

            sb.AppendLine($" var {jsonDataVariableName} = {jsonFileContent};");

            //sb.AppendLine($"{jsonDataVariableName}.title = null;");
            //sb.AppendLine($"{jsonDataVariableName}.subtitle = null;");

            sb.AppendLine("document.addEventListener('DOMContentLoaded',");
            sb.AppendLine("function(){");
            sb.AppendLine("Highcharts.setOptions(setOptions);");
            sb.AppendLine($"Highcharts.chart('{chartId}', {jsonDataVariableName});");
            sb.AppendLine("});");
            sb.AppendLine("</script>");
            return sb.ToString();
        }

        public void CleanUpViewFiles()
        {
            // Remove all previous preview files
            DirectoryInfo di = new DirectoryInfo($"{GetViewDirectoryPath()}");
            foreach (FileInfo file in di.EnumerateFiles())
            {
                if (file.FullName.Contains("default.htm"))
                {
                    continue;
                }

                file.Delete();
            }
        }
    }
}