using Requester.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using static Requester.Services.Enums;

namespace Requester.Services
{
    public static class XMLHelper
    {
        public static ValidationStatus ValidationStatus { get; private set; }

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
          //  CreateSchema();
        }

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



        public static ValidationStatus ValidateXML2(string fileName, string schemaFile)
        {
            ValidationStatus = ValidationStatus.Process;

            try
            {
                XDocument doc = XDocument.Load(fileName);
                XmlSchemaSet schemaSet = new XmlSchemaSet();

                schemaSet.Add(null, schemaFile);

                doc.Validate(schemaSet, (obj, ex) =>
                {
                    ValidationStatus = ValidationStatus.Denied;
                });

            }
            catch (Exception ex)
            {
                ValidationStatus = ValidationStatus.Denied;
            }


            if (!ValidationStatus.Equals(ValidationStatus.Denied)) ValidationStatus = ValidationStatus.Passed;

            return ValidationStatus;
        }


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



        private static void CreateSchema()
        {
            XmlReader reader = XmlReader.Create(PathManager.GetConfigFilePath());

            XmlSchemaInference infer = new XmlSchemaInference();
            XmlSchemaSet schemaSet =
            infer.InferSchema(new XmlTextReader(PathManager.GetConfigFilePath()));

            XmlWriter w = XmlWriter.Create(PathManager.GetXSDFilePath());
            foreach (XmlSchema schema in schemaSet.Schemas())
            {
                schema.Write(w);
            }
            w.Close();

        }
    }
}
