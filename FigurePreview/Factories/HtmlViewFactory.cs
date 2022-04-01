using FigurePreview.Configuration;
using System.IO;
using System.Text;

namespace FigurePreview.Factories
{
    public class HtmlViewFactory
    {
        private const string HtmlViewFolder = "HtmlView";

        public string CreateHtmlViewForFile(string fileName)
        {
            var figures = FigureConfiguration.Instance.FigurePreview.Figure;

            foreach (var figure in figures)
            {
                // build up content
            }

            var displayContent = "<div>Test</div>";

            // read template file and replace with content
            var templateFilePath = GetTemplateFilePath();
            var templateHtml = File.ReadAllText(templateFilePath, Encoding.UTF8);
            //previewHtml = previewHtml.Replace("#chartJson#", !string.IsNullOrEmpty(jsonData) ? jsonData : "undefined");

            var viewHtml = templateHtml.Replace("{{DisplayItemInfo}}", displayContent);
            var viewPath = GetViewFilePath(fileName);
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
