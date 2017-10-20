using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.Core.Localization
{
    public interface ILocalizationDictionary
    {
        string Get(string key, CultureInfo cultureInfo = null);

        Dictionary<string, string> GetAll(CultureInfo cultureInfo = null, bool lazyLoading = true);

        void UpdateAll(CultureInfo cultureInfo = null);


        void Add(string key, string value, CultureInfo cultureInfo = null);
         
    }
}
