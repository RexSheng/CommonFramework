using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.Core.Localization
{
    public class LocalizationDictionary : ILocalizationDictionary
    {
        private Dictionary<string, Dictionary<string, string>> allDic = new Dictionary<string, Dictionary<string, string>>();
        private readonly ILanguageProvider _languageProvider;

        public LocalizationDictionary(ILanguageProvider languageProvider)
        {
            _languageProvider = languageProvider;
        }

        private LanguageInfo GetContext(CultureInfo cultureInfo = null)
        {
            var lang = cultureInfo == null ? Thread.CurrentThread.CurrentCulture.Name.ToLower() : cultureInfo.Name.ToLower();
            return _languageProvider.GetAll().Where(m => m.LanguageCode.ToLower().Equals(lang)).FirstOrDefault();
        }
        public virtual string Get(string key, CultureInfo cultureInfo = null)
        {
            var context = GetContext(cultureInfo);
            if (cultureInfo == null)
            {
                cultureInfo = Thread.CurrentThread.CurrentCulture;
            }
            return context.Provider.GetString(key, cultureInfo);
        }

        public virtual Dictionary<string, string> GetAll(CultureInfo cultureInfo = null, bool lazyLoading = true)
        {
            var lang = cultureInfo == null ? Thread.CurrentThread.CurrentCulture.Name.ToLower() : cultureInfo.Name.ToLower();
            if (lazyLoading)
            {
                if (allDic.ContainsKey(lang))
                {
                    return allDic[lang];
                }

            }

            UpdateAll(cultureInfo);
            if (allDic.ContainsKey(lang))
            {
                return allDic[lang];
            }

            return new Dictionary<string, string>();
        }

        public virtual void UpdateAll(CultureInfo cultureInfo = null)
        {
            var context = GetContext(cultureInfo);
            allDic[context.LanguageCode] = context.Provider.GetAll(cultureInfo);
        }

        public virtual void Add(string key, string value, CultureInfo cultureInfo = null)
        {
            var context = GetContext(cultureInfo);
            if (cultureInfo == null)
            {
                cultureInfo = Thread.CurrentThread.CurrentCulture;
            }
            context.Provider.Add(key, value, cultureInfo);
        }
    }
}
