using System; 
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FigurePreview.Utils.Comparer
{
    public class FigureNameComparer : IComparer<string>
    {       
        public int Compare(string first, string second)
        {
            const string regExpPattern = @"([A-za-zÆØÅæøå]*)(\d*)";

            if (first == null || second == null)
            {
                return 0;
            }

            var firstArray = Regex.Split(first, regExpPattern);
            var secondArray = Regex.Split(second, regExpPattern);

            var length = firstArray.Length;
            if (secondArray.Length > length)
            {
                length = secondArray.Length;
            }

            var compareResult = 0;

            for (var i = 0; i < length; i++)
            {                
                if (compareResult != 0)
                {
                    break;
                }
                compareResult = GetNumber(firstArray[i]).CompareTo(GetNumber(secondArray[i]));
                if (compareResult == 0)
                {
                    compareResult = string.Compare(firstArray[i], secondArray[i], StringComparison.CurrentCultureIgnoreCase);
                }
            }

            return compareResult;
        }

        private int GetNumber(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return 0;
            }

            var value = int.TryParse(term, out var number) ? number : 0;
            return value;
        }
    }
}