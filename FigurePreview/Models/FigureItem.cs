using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FigurePreview.Configuration;

namespace FigurePreview.Models
{
    public class FigureItem
    {
        private List<FigureInfo> _figureInfoList;

        public FigureItem(string name, FigureInfo figurInfo)
        {
            Name = name;
            _figureInfoList = new List<FigureInfo>();
            AddFigureInfo(figurInfo);
        }

        public string Name
        {
            get;
            set;
        }

        public void AddFigureInfo(FigureInfo figure)
        {
            _figureInfoList.Add(figure);
        }


        public bool ExistsInFigure(Figure figure)
        {
            return true;
        }

        public bool HasNotValidFigureExtensions(Figure currentFigureEntry)
        {
            return GetNotValidFigureExtensions(currentFigureEntry).Count > 0;
        }

        public List<string> GetNotValidFigureExtensions(Figure currentFigureEntry)
        {
            // read all extensions found for this figure item in specific figure configuration folder
            var unvalidExtensions = _figureInfoList
                .Where(x => x.AllowedExtension == false && x.FigureFormatPath == currentFigureEntry.FormatPath)
                .Select(x => x.FileExtension).ToList();

            return unvalidExtensions;
        }

        public bool HasExtension(Figure currentFigureEntry, string extension)
        {
            var hasExtensions = _figureInfoList.Any(x =>
                x.AllowedExtension == true && x.FigureFormatPath == currentFigureEntry.FormatPath &&
                x.FileExtension.ToLower() == extension.ToLower());

            return hasExtensions;
        }

        public FigureInfo GetFigureInfoForExtension(Figure currentFigureEntry, string extension)
        {
            var figureInfo = _figureInfoList.Where(x =>
                x.AllowedExtension == true && x.FigureFormatPath == currentFigureEntry.FormatPath &&
                x.FileExtension.ToLower() == extension.ToLower()).First();

            return figureInfo;
        }
    }
}