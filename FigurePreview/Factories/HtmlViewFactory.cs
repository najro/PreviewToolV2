using FigurePreview.Configuration;
using System.IO;
using System.Text;
using FigurePreview.Models;

namespace FigurePreview.Factories
{
    public class HtmlViewFactory
    {
        private const string HtmlViewFolder = "HtmlView";

        public string CreateHtmlViewForFile(FigureItem displayItem)
        {
            var figures = FigureConfiguration.Instance.FigurePreview.Figure;

            StringBuilder htmlContent = new StringBuilder();



            htmlContent.AppendLine("<div class=\"figures\">");

            foreach (var figure in figures)
            {
                htmlContent.AppendLine("<div class=\"figure\">");

                htmlContent.Append($"<h2>{figure.Name}</h2>");
                // build up content
                foreach (var ext in figure.Extentions.Ext)
                {
                    htmlContent.Append($"<div class=\"ext-header\">Preview {ext}</div>");
                    htmlContent.Append($"<div class=\"ext-content\">Some cool content</div>");
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
            var viewPath = GetViewFilePath(displayItem.Name);
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
    }
}
