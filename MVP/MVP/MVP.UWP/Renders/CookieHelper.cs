using Microsoft.Mvpui;
using MVP.UWP.Renders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using Xamarin.Forms;

[assembly: Dependency(typeof(CookieHelper))]
namespace MVP.UWP.Renders
{
    public class CookieHelper : ICookieHelper
    {  
        public static CookieContainer GetUriCookieContainer(Uri uri)
        {
            return null;
        }

        public string GetCookie(string url)
        {
            try
            {
                //var container = GetUriCookieContainer(new Uri(url));
                var cookies = GetBrowserCookie(new Uri(url));

                var rpsSec = cookies.FirstOrDefault(s => s.Name == "RPSSecAuth");
                if (rpsSec != null)
                {
                    return rpsSec.Value;
                }
                return "";
            }
            catch
            {
                return "";
            }

        }

        public void ClearCookie()
        {
            Windows.Web.Http.Filters.HttpBaseProtocolFilter myFilter = new Windows.Web.Http.Filters.HttpBaseProtocolFilter();
            var cookieManager = myFilter.CookieManager;
            HttpCookieCollection myCookieJar = cookieManager.GetCookies(new Uri("https://login.live.com"));

            foreach (HttpCookie cookie in myCookieJar)
            {
                cookieManager.DeleteCookie(cookie);
            }
        }

        public string GetCookie(Uri uri)
        {
            return null;
        }

        private HttpCookieCollection GetBrowserCookie(Uri targetUri)
        {
            var httpBaseProtocolFilter = new Windows.Web.Http.Filters.HttpBaseProtocolFilter();
            var cookieManager = httpBaseProtocolFilter.CookieManager;
            var cookieCollection = cookieManager.GetCookies(targetUri);

            return cookieCollection;
        }
    }
}
