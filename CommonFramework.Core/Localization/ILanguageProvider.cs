using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.Core.Localization
{
    public interface ILanguageProvider
    {
        ILanguageProvider AddLanguage(string code, string name, ILocalizationSourceProvider provider);

        List<LanguageInfo> GetAll();

        void ChangeLanguage(string code);
    }
}
