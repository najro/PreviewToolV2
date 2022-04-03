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

            StringBuilder sb = new StringBuilder();

            foreach (var figure in figures)
            {
                sb.Append($"<div>{figure.Name}</div>");
                // build up content
                foreach (var ext in figure.Extentions.Ext)
                {
                    
                }
            }

            var displayContent = sb.ToString();

            // read template file and replace with content
            var templateFilePath = GetTemplateFilePath();
            var templateHtml = File.ReadAllText(templateFilePath, Encoding.UTF8);
            //previewHtml = previewHtml.Replace("#chartJson#", !string.IsNullOrEmpty(jsonData) ? jsonData : "undefined");

            var viewHtml = templateHtml.Replace("{{DisplayItemInfo}}", displayContent);
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
