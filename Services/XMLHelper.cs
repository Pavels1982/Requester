using Requester.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using static Requester.Services.Enums;

namespace Requester.Services
{
    public static class XMLHelper
    {
        #region Properties
        /// <summary>
        /// Gets or sets статус валидации XML-данных.
        /// </summary>
        public static ValidationStatus ValidationStatus { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Метод зохранения коллекции объктов-запросов в XML-файл.
        /// </summary>
        /// <param name="collection">Коллекция объектов-запросов.</param>
        /// <param name="fileName">Имя выходного XML-файла.</param>
        public static void SaveRequests(ObservableCollection<RequestObject> collection,  string fileName)
        {
            XDocument xdoc = new XDocument();

            XElement Requests = new XElement("Requests");

            foreach (var item in collection)
            {
                XElement Request = new XElement("Request");
                XAttribute RequestUrlAttr = new XAttribute("url", item.Request.Url);
                XElement RequestTimeOutElem = new XElement("timeout", item.Request.TimeOut);
                XElement RequestIntervalElem = new XElement("interval", item.Request.Interval);

                Request.Add(RequestUrlAttr);
                Request.Add(RequestTimeOutElem);
                Request.Add(RequestIntervalElem);
                Requests.Add(Request);
            }

            xdoc.Add(Requests);
            xdoc.Save(fileName);
            //  CreateSchema(PathManager.GetXSDFilePath());
        }

        /// <summary>
        /// Метод десериализации XML-данных в коллекцию объектов-запросов.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>Коллекция объектов-запросов.</returns>
        public static ObservableCollection<RequestObject> LoadRequests(string fileName)
        {
            ObservableCollection<RequestObject> result = new ObservableCollection<RequestObject>();
                try
                {
                    XDocument xdoc = XDocument.Load(fileName);
                    XElement root = xdoc.Element("Requests");

                    foreach (var el in root.Elements("Request").ToList())
                    {
                        Request request = new Request(el.Attribute("url").Value, Int32.Parse(el.Element("timeout").Value), Int32.Parse(el.Element("interval").Value));
                        result.Add(new RequestObject(request));
                    }
                }
                catch (Exception ex)
                {

                }
             return result;
        }

        /// <summary>
        /// Метод валидации XML-данных по XSD-схеме.
        /// </summary>
        /// <param name="fileName">XML-файл.</param>
        /// <param name="schemaFile">XSD-файл.</param>
        /// <returns></returns>
        public static ValidationStatus ValidateXML(string fileName, string schemaFile)
        {
            ValidationStatus = ValidationStatus.Process;
            try
            {
                XmlSchemaSet schema = new XmlSchemaSet();
                schema.Add(null, schemaFile);

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessIdentityConstraints;
                settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
                settings.Schemas = schema;

                settings.ValidationEventHandler += ValidationCallBack;

                XmlReader reader = XmlReader.Create(fileName, settings);

                while (reader.Read()) ;
            }
            catch (Exception ex)
            {
                ValidationStatus = ValidationStatus.Denied;
            }

            if (!ValidationStatus.Equals(ValidationStatus.Denied)) ValidationStatus = ValidationStatus.Passed;
            return ValidationStatus;
        }

        private static void ValidationCallBack(object sender, ValidationEventArgs e)
        {
            ValidationStatus = ValidationStatus.Denied;
         //   Console.WriteLine($"Validation Error:\n   {e.Message}\n");
        }

        /// <summary>
        /// Метод создания XSD-файла.
        /// </summary>
        /// <param name="fileName">XML-файл.</param>
        private static void CreateSchema(string fileName)
        {
            XmlReader reader = XmlReader.Create(PathManager.GetConfigFilePath());

            XmlSchemaInference infer = new XmlSchemaInference();
            XmlSchemaSet schemaSet =
            infer.InferSchema(new XmlTextReader(PathManager.GetConfigFilePath()));

            XmlWriter w = XmlWriter.Create(fileName);
            foreach (XmlSchema schema in schemaSet.Schemas())
            {
                schema.Write(w);
            }
            w.Close();

        }
        #endregion
    }
}
