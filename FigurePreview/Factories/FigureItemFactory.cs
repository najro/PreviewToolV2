using FigurePreview.Configuration;
using FigurePreview.Models;
using FigurePreview.Utils.Comparer;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FigurePreview.Factories
{
    public class FigureItemFactory
    {
        public List<FigureItem> GetDisplayItems(string selectedPathFiguresRootFolder)
        {
            List<FigureItem> figureItems = new List<FigureItem>();

            if (string.IsNullOrWhiteSpace(selectedPathFiguresRootFolder))
                return figureItems;

            var figures = FigureConfiguration.Instance.FigurePreview.Figure;

            foreach (var figure in figures)
            {
                string filesToPreviewFolder = $"{selectedPathFiguresRootFolder}\\{figure.FormatPath}";

                if (!Directory.Exists(filesToPreviewFolder))
                {
                    continue;
                }

                // read all files from preview folder to build up unique list of file names
                var files = Directory.GetFiles(filesToPreviewFolder, "*.*", SearchOption.TopDirectoryOnly);

                foreach (var file in files)
                {
                    var fileNameWithoutExt = Path.GetFileNameWithoutExtension(file);

                    if (string.IsNullOrWhiteSpace(fileNameWithoutExt))
                        continue;

                    var fileExtension = Path.GetExtension(file).ToLower().Replace(".", "");

                    if (string.IsNullOrWhiteSpace(fileExtension))
                        continue;

                    var figureInfo = new FigureInfo()
                    {
                        FigureName = figure.Name,
                        FigureFormatPath = figure.FormatPath,
                        FileName = fileNameWithoutExt,
                        FilePath = file,
                        FileExtension = fileExtension,
                        AllowedExtension = figure.Extentions.Ext.Exists(x => x.ToLower() == fileExtension)
                    };

                    if (!figureItems.Exists(x => x.Name == fileNameWithoutExt))
                    {
                        figureItems.Add(new FigureItem(fileNameWithoutExt, figureInfo));
                    }
                    else
                    {
                        var figureItem = figureItems.First(f => f.Name == fileNameWithoutExt);
                        figureItem.AddFigureInfo(figureInfo);
                    }
                }
            }

            //return figureItems.OrderBy(o => o.Name).ToList();
            var figurNameComparer = new FigureNameComparer();
            return figureItems.OrderBy(o => o.Name, figurNameComparer).ToList();
        }
    }
}
