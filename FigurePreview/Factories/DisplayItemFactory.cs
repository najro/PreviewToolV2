using FigurePreview.Configuration;
using FigurePreview.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FigurePreview.Factories
{
    public class DisplayItemFactory
    {

        public List<FigureItem> GetDisplayItems()
        {
            List<FigureItem> displayItems = new List<FigureItem>();


            // Check 4 areas based on configuration
            string rootFolder =
                "C:\\Users\\pa_xoran\\Downloads\\FigurPreviewTest\\FigurProsjektet\\Lager\\STM\\20212022\\240";

            var figures = FigureConfiguration.Instance.FigurePreview.Figure;

            foreach (var figure in figures)
            {
                string filesToPreviewFolder = $"{rootFolder}\\{figure.FormatPath}";
                // read all files from preview folder to build up unique list of file names
                var files = Directory.GetFiles(filesToPreviewFolder, "*.*", SearchOption.AllDirectories);


                foreach (var file in files)
                {

                    var fileName = Path.GetFileNameWithoutExtension(file);

                    if (string.IsNullOrWhiteSpace(fileName))
                        continue;
















                    var fileExtension = Path.GetExtension(file).ToLower().Replace(".","");

                    if (string.IsNullOrWhiteSpace(fileExtension))
                        continue;

                    if (!figure.Extentions.Ext.Exists(x=> x.ToLower() == fileExtension))
                        continue;


                   

                    if (displayItems.Exists(x => x.Name == fileName))
                        continue;


                    var item = new FigureItem()
                    {
                        Name = fileName
                    };

                    displayItems.Add(item);
                }
            }


            return displayItems.OrderBy(o => o.Name).ToList();
        }
    }
}
