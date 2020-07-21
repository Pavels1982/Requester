using Requester.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using static Requester.Services.Enums;

namespace Requester.Services
{
    public static class XMLHelper
    {
        private static ValidationStatus ValidationStatus { get; set; } 
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

        }

        public static ObservableCollection<RequestObject> LoadRequests(string fileName)
        {
            ObservableCollection<RequestObject> result = new ObservableCollection<RequestObject>();

            if (ValidateXML(fileName).Equals(ValidationStatus.Passed))
            {
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
                catch
                {

                }
            }
            CreatSchema();
            return result;
        }


        private static ValidationStatus ValidateXML(string fileName)
        {
            ValidationStatus = ValidationStatus.Process;
            
            try
            {
                XmlReaderSettings booksSettings = new XmlReaderSettings();
                booksSettings.Schemas.Add("http://www.contoso.com/books", PathManager.GetXSDFilePath());
                booksSettings.ValidationType = ValidationType.Schema;
                booksSettings.ValidationEventHandler += new ValidationEventHandler(booksSettingsValidationEventHandler);

                XmlReader books = XmlReader.Create(fileName, booksSettings);

                while (books.Read()) { }
            }
            catch
            {
                ValidationStatus = ValidationStatus.Denied;
            }

            if (!ValidationStatus.Equals(ValidationStatus.Denied)) ValidationStatus = ValidationStatus.Passed;
            return ValidationStatus;
        }

        static void booksSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning || e.Severity == XmlSeverityType.Error)
            {
                ValidationStatus = ValidationStatus.Denied;
            }
        }


        private static void CreatSchema()
        {
            XmlReader reader = XmlReader.Create(PathManager.GetConfigFilePath());
 
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            XmlSchemaInference inference = new XmlSchemaInference();
            schemaSet = inference.InferSchema(reader);

            XmlWriter writer;

            foreach (XmlSchema s in schemaSet.Schemas())
            {
                writer = XmlWriter.Create("config.xsd");
                s.Write(writer);
                writer.Close();
            }
            reader.Close();

        }
    }
}
