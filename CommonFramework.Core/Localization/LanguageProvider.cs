using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;

/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.Core.Localization
{
    public class LanguageProvider:ILanguageProvider
    {
        private List<LanguageInfo> _language=new List<LanguageInfo>();
        public ILanguageProvider AddLanguage(string code,string name,ILocalizationSourceProvider provider) {
            _language.Add(new LanguageInfo() { LanguageCode = code, LanguageName = name,Provider=provider });
            return this;
        }

        public List<LanguageInfo> GetAll() {
            return _language;
        }

        public void ChangeLanguage(string code) {
            if (_language.Any(m => m.LanguageCode.Equals(code))) {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(code);
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(code);
            }
        }
    }

    public class LanguageInfo
    {
        public string LanguageCode { get; set; }

        public string LanguageName { get; set; }

        public ILocalizationSourceProvider Provider { get; set; }
    }
}
