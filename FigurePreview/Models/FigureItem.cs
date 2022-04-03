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

        public bool HasFigureExtension(Figure figure, string extension)
        {
            return true;
        }

        public List<string> MissmatchFigureExtension(Figure figure, string extension)
        {
            
            return new List<string>();
            
        }
    }
}
