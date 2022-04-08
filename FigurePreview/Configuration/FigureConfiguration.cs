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
            try
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(FigurePreview));
                TextReader textReader = new StreamReader(GetConfigurationFilePath());
                _figurePreview = (FigurePreview)deserializer.Deserialize(textReader);
                textReader.Close();

            }
            catch (Exception exp)
            {
                throw new NotSupportedException($"Problem med XML fil {GetConfigurationFilePath()} : {exp.ToString()}");
            }


            SetPublicationDynamicPath();

        }

        private string GetConfigurationFilePath()
        {
            return $"{Directory.GetCurrentDirectory()}\\{FigureConfigurationFile}";
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

        public void SetPublicationDynamicPath()
        {
            try
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(PublicationDynamicPath));
                TextReader textReader = new StreamReader(_figurePreview.DynamicPathFile.Text);
                _figurePreview.PublicationDynamicPath = (PublicationDynamicPath)deserializer.Deserialize(textReader);
                textReader.Close();
            }
            catch (Exception exp)
            {
                throw new NotSupportedException($"Problem med XML fil {GetConfigurationFilePath()} : {exp.ToString()}");
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

            if (string.IsNullOrEmpty(FigurePreview?.PathRoule))
            {
                error.Append("PathRoule er tom i XML fil");
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