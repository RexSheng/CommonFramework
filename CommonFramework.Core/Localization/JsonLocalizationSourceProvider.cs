using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.Core.Localization
{
    public class JsonLocalizationSourceProvider : ILocalizationSourceProvider
    {
        public void Add(string key, string value, CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetAll(CultureInfo cultureInfo)
        {
            string lang = cultureInfo.Name.ToLower();
            string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data/Global." + lang + ".json");
            string content = File.ReadAllText(file);
            List<JsonLocationInfo> list = JsonConvert.DeserializeObject<List<JsonLocationInfo>>(content);

            return list.ToDictionary(m => m.name, m => m.value); ;
        }

        public string GetString(string key, CultureInfo cultureInfo)
        {
            Dictionary<string, string> dic = GetAll(cultureInfo); ;
            return dic.ContainsKey(key)? dic[key] : key;
        }
    }

    internal class JsonLocationInfo
    {
        public string name { get; set; }

        public string value { get; set; }
    }
}
