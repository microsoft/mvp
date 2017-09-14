
using Xamarin.Forms;
using Microsoft.Mvp.iOS.Renders;
using Foundation;
using Microsoft.Mvpui;
using System;

[assembly: Dependency(typeof(CookieHelper))]
namespace Microsoft.Mvp.iOS.Renders
{
    public class CookieHelper: ICookieHelper
    {

        public string GetCookie(string url)
        {
            return "";
        }
        public void ClearCookie()
        {
            NSHttpCookieStorage CookieStorage = NSHttpCookieStorage.SharedStorage;

            foreach (var cookie in CookieStorage.Cookies)
            {
                CookieStorage.DeleteCookie(cookie);
            }
        }

        public string GetCookie(Uri uri)
        {
            throw new NotImplementedException();
        }
    }
}