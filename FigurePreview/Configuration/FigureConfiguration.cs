using System;
using System.IO;
using System.Linq;
using System.Text;
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

            if (FigurePreview.DynamicPathFile.Enable)
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
            }
            catch (Exception exp)
            {
                _figurePreview = null;
                //throw new NotSupportedException($"Problem med XML fil {GetConfigurationFilePath()} : {exp.ToString()}");
            }
        }

        public void SetPublicationDynamicPath()
        {
            try
            {
                var inStream = new FileStream(_figurePreview.DynamicPathFile.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                var deserializer = new XmlSerializer(typeof(PublicationDynamicPath));
                _figurePreview.PublicationDynamicPath = (PublicationDynamicPath)deserializer.Deserialize(inStream);
            }
            catch (Exception exp)
            {
                _figurePreview.PublicationDynamicPath = null;
                //throw new NotSupportedException($"Problem med XML fil {GetConfigurationFilePath()} : {exp.ToString()}");
            }
        }

        // add logic for DynamicPathFile
        public bool IsConfigurationValid(out string errorMessage)
        {
            var validConfiguration = true;
            var error = new StringBuilder();

            if (FigurePreview == null)
            {
                error.Append("Problem med å opprette en konfigurasjon fra XML, sjekk filen!");
            }

            if (string.IsNullOrEmpty(FigurePreview?.StartPath))
            {
                error.Append("startpath er tom i XML fil");
            }

            // Is not in use
            //if (string.IsNullOrEmpty(FigurePreview?.PathRoule))
            //{
            //    error.Append("PathRoule er tom i XML fil");
            //}


            if (FigurePreview?.DynamicPathFile == null)
            {
                error.Append("DynamicPathFile er tom i XML fil");
            }
            else
            {
                if (FigurePreview.DynamicPathFile.Enable)
                {
                    if (string.IsNullOrEmpty(FigurePreview.DynamicPathFile.Text))
                    {
                        error.Append("Det savnes informasjon i DynamicPathFile XML");
                    }

                    if (FigurePreview.PublicationDynamicPath == null)
                    {
                        error.Append("Det savnes informasjon for dynamisk pathDynamicPathFile XML fil");
                    }else if (string.IsNullOrEmpty(FigurePreview.PublicationDynamicPath.Text))
                    {
                        error.Append("Det savnes informasjon i PublicationDynamicPath XML");
                    }

                }
            }

            if (FigurePreview?.Figure == null || !FigurePreview.Figure.Any())
            {
                error.Append("Det savnes Figure elementer i XML fil");
            }
            else
            {
                foreach (var figure in FigurePreview?.Figure)
                {
                    if (string.IsNullOrEmpty(figure.Name))
                    {
                        error.Append("Name er tom i Figure element");
                    }

                    if (string.IsNullOrEmpty(figure.FormatPath))
                    {
                        error.Append("FormatPath er tom i Figure element");
                    }

                    if (figure.Extentions == null)
                    {
                        error.Append("Det savnes Extentions elementer i XML fil");
                    }
                    else if (!figure.Extentions.Ext.Any())
                    {
                        error.Append("Det savnes Ext elementer i XML fil");
                    }
                    else
                    {
                        foreach (var ext in figure.Extentions.Ext.Where(string.IsNullOrWhiteSpace))
                        {
                            error.Append("Ext er tom i Extentions element");
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