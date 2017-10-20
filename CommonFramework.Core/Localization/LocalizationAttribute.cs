using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

/*
* CopyRight ©2017 All Rights Reserved
* 作者:Rex Sheng
*/
namespace CommonFramework.Core.Localization
{
    public class LocalizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // load the culture info from the cookie
            var cookie = filterContext.HttpContext.Request.Cookies["RexSheng.CommonFrameworkLocalization.CurrentUICulture"];
            var langHeader = string.Empty;

            if (cookie != null)
            {
                // set the culture by the cookie content
                langHeader = cookie.Value;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(langHeader);
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(langHeader);
            }
            else
            {
                // set the culture by the location if not speicified
                langHeader = filterContext.HttpContext.Request.UserLanguages[0];
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(langHeader);
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(langHeader);
            }
            // save the location into cookie
            HttpCookie _cookie = new HttpCookie("RexSheng.CommonFrameworkLocalization.CurrentUICulture", Thread.CurrentThread.CurrentUICulture.Name);
            _cookie.Expires = DateTime.Now.AddYears(1);
            filterContext.HttpContext.Response.SetCookie(_cookie);
            base.OnActionExecuting(filterContext);
        }
    }
}
