using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.Core.Localization
{
    public interface ILocalizationSourceProvider
    {
        string GetString(string key,CultureInfo cultureInfo );
        Dictionary<string, string> GetAll(CultureInfo cultureInfo);
        void Add(string key, string value, CultureInfo cultureInfo);
    }
}
