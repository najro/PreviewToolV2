using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FigurePreview.Helper
{
    public static class HtmlHelper
    {
        public static string GetHtmlViewForFile(string fileName)
        {



            return $"<div>{fileName}</div>";

        }


    }
}
