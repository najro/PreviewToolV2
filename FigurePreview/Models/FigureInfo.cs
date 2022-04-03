namespace FigurePreview.Models
{
    public class FigureInfo
    {
        public string FigureName { get; set; }
        public string FigureFormatPath { get; set; }
        //public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileExtension { get; set; }
        public bool AllowedExtension { get; set; }
    }
}