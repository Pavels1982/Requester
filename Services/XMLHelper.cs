using Requester.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Requester.Services
{
    public static class XMLHelper
    {

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

            if (File.Exists(fileName))
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
            return result;
        }



    }
}
