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
        private List<FigureInfo> _figureInfo;

        public FigureItem()
        {
            _figureInfo = new List<FigureInfo>();
        }

        public string Name
        {
            get;
            set;
        }


        public void AddFigureInfo(FigureInfo figure)
        {
            _figureInfo.Add(figure);
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
