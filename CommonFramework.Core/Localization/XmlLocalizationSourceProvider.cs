using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Reflection;
using System.Threading;
using System.Globalization;
using System.IO;
using System.Xml;

/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.Core.Localization
{
    public class XmlLocalizationSourceProvider : ILocalizationSourceProvider
    {
        public string GetString(string key, CultureInfo cultureInfo)
        {
            string lang = cultureInfo.Name.ToLower();
            string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data/Global." + lang + ".xml");

            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            XmlNode rootNode = doc.SelectSingleNode("Resources");
            foreach (var node in rootNode.ChildNodes)
            {
                XmlElement element = (XmlElement)node;
                if (element.GetAttribute("key").ToString() == key)
                {
                    return element.GetAttribute("value").ToString();
                }
            }
            return key;
        }
        public Dictionary<string, string> GetAll(CultureInfo cultureInfo)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string lang = cultureInfo.Name.ToLower();
            string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data/Global." + lang + ".xml");

            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            XmlNode rootNode = doc.SelectSingleNode("Resources");
            foreach (var node in rootNode.ChildNodes)
            {
                XmlElement element = (XmlElement)node;

                dic[element.GetAttribute("key").ToString()] = element.GetAttribute("value").ToString();
            }
            return dic;
        }

        public void Add(string key, string value, CultureInfo cultureInfo)
        {

        }
    }
}
