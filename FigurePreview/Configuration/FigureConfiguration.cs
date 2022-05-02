using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

namespace FigurePreview.Configuration
{
    public class FigureConfiguration
    {
        private const string FigureConfigurationFile = "FigurePreview.xml";
        private static FigureConfiguration _figureConfiguration;
        private FigurePreview _figurePreview;

        private FigureConfiguration()
        {
            SetFigurePreview();

            if (FigurePreview.DynamicPathFile.Enabled)
            {
                SetPublicationDynamicPath();
            }
        }

        public static FigureConfiguration Instance
        {
            get
            {
                if (_figureConfiguration == null)
                {
                    _figureConfiguration = new FigureConfiguration();
                }

                return _figureConfiguration;
            }
        }

        private string GetConfigurationFilePath()
        {
            return $"{Directory.GetCurrentDirectory()}\\{FigureConfigurationFile}";
        }

        public void SetFigurePreview()
        {
            try
            {
                var deserializer = new XmlSerializer(typeof(FigurePreview));
                var inStream = new FileStream(GetConfigurationFilePath(), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                _figurePreview = (FigurePreview)deserializer.Deserialize(inStream);
                inStream.Close();
                inStream.Dispose();
            }
            catch
            {
                _figurePreview = null;             
            }
        }

        public void SetPublicationDynamicPath()
        {
            try
            {
                var fileName = _figurePreview.DynamicPathFile.Text; ;

                if (string.IsNullOrEmpty(fileName))
                {
                    _figurePreview.PublicationDynamicPath = null;
                }
                else
                {
                    // try to avoid issues during read XML file from multiple threads. Copy to temp file
                    Thread.Sleep(1000);
                    Random rnd = new Random();
                    var fileDirectory = Path.GetDirectoryName(fileName);
                    var fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
                    var fileExtension = Path.GetExtension(fileName);
                    var tmpFileName = $"{fileDirectory}\\{fileNameWithoutExt}_tmp_{rnd.Next()}{fileExtension}";
                    
                    File.Copy(fileName, tmpFileName);

                    var inStream = new FileStream(tmpFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                    var deserializer = new XmlSerializer(typeof(PublicationDynamicPath));
                    _figurePreview.PublicationDynamicPath = (PublicationDynamicPath)deserializer.Deserialize(inStream);
                    inStream.Dispose();
                    File.Delete(tmpFileName);                    
                }
            }
            catch
            {
                _figurePreview.PublicationDynamicPath = null;                
            }
        }


        public bool IsConfigurationValid(out string errorMessage)
        {
            var validConfiguration = true;
            var error = new StringBuilder();

            if (FigurePreview == null)
            {
                error.Append($"Problem med å opprette en konfigurasjon fra XML ({FigureConfigurationFile})");
            }

            if (string.IsNullOrEmpty(FigurePreview?.StartPath))
            {
                error.Append($"startpath er tom i XML ({FigureConfigurationFile})");
            }

            // Is not in use for now
            //if (string.IsNullOrEmpty(FigurePreview?.PathRoule))
            //{
            //    error.Append("PathRoule er tom i XML fil");
            //}


            if (FigurePreview?.DynamicPathFile == null)
            {
                error.Append($"DynamicPathFile er tom i XML ({FigureConfigurationFile})");
            }
            else
            {
                if (FigurePreview.DynamicPathFile.Enabled)
                {
                    if (string.IsNullOrEmpty(FigurePreview.DynamicPathFile.Text))
                    {
                        error.Append($"Det savnes informasjon i DynamicPathFile XML ({FigureConfigurationFile})");
                    }

                    if (FigurePreview.PublicationDynamicPath == null)
                    {
                        error.Append($"Det savnes informasjon fra fil som er definert i DynamicPathFile i XML ({FigureConfigurationFile})");
                    }
                    else if (string.IsNullOrEmpty(FigurePreview.PublicationDynamicPath.Text))
                    {
                        error.Append($"Det savnes informasjon i XML fil som er referet fra PublicationDynamicPath i XML ({FigureConfigurationFile})");
                    }
                }
            }

            if (FigurePreview?.Figure == null || !FigurePreview.Figure.Any())
            {
                error.Append($"Det savnes Figure elementer i XML ({FigureConfigurationFile})");
            }
            else
            {
                foreach (var figure in FigurePreview?.Figure)
                {
                    if (string.IsNullOrEmpty(figure.Name))
                    {
                        error.Append($"Name er tom i Figure element i XML ({FigureConfigurationFile})");
                    }

                    if (string.IsNullOrEmpty(figure.FormatPath))
                    {
                        error.Append($"FormatPath er tom i Figure element i XML ({FigureConfigurationFile})");
                    }

                    if (figure.Extentions == null)
                    {
                        error.Append($"Det savnes Extentions elementer i XML ({FigureConfigurationFile})");
                    }
                    else if (!figure.Extentions.Ext.Any())
                    {
                        error.Append($"Det savnes Ext elementer i XML ({FigureConfigurationFile})");
                    }
                    else
                    {
                        foreach (var ext in figure.Extentions.Ext.Where(string.IsNullOrWhiteSpace))
                        {
                            error.Append($"Ext er tom i Extentions element i XML ({FigureConfigurationFile})");
                        }
                    }
                }
            }

            errorMessage = error.ToString();

            if (!string.IsNullOrEmpty(errorMessage))
            {
                validConfiguration = false;
            }

            return validConfiguration;
        }

        public FigurePreview FigurePreview
        {
            get
            {
                return _figurePreview;
            }
        }
    }
}