using Microsoft.Mvp.WinPhone;
using Microsoft.Mvpui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Windows.Web.Http;  //NOT: Microsoft.Net.Http

[assembly: Dependency(typeof(CookieHelper))]
namespace Microsoft.Mvp.WinPhone
{
    public class CookieHelper : ICookieHelper
    {

        [DllImport("wininet.dll", SetLastError = true)]
        public static extern bool InternetGetCookieEx(string url, string cookieName, StringBuilder cookieData, ref int size, Int32 dwFlags, IntPtr lpReserved);

        [DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int lpdwBufferLength);

        private const Int32 InternetCookieHttponly = 0x2000;

        public static CookieContainer GetUriCookieContainer(Uri uri)
        {
            CookieContainer cookies = null;
            // Determine the size of the cookie
            int datasize = 8192 * 16;
            StringBuilder cookieData = new StringBuilder(datasize);
            if (!InternetGetCookieEx(uri.ToString(), null, cookieData, ref datasize, InternetCookieHttponly, IntPtr.Zero))
            {
                if (datasize < 0)
                    return null;
                // Allocate stringbuilder large enough to hold the cookie
                cookieData = new StringBuilder(datasize);
                if (!InternetGetCookieEx(uri.ToString(), null, cookieData, ref datasize, InternetCookieHttponly, IntPtr.Zero))
                    return null;
            }
            if (cookieData.Length > 0)
            {
                cookies = new CookieContainer();
                cookies.SetCookies(uri, cookieData.ToString().Replace(';', ','));
            }
            return cookies;
        }
        public string GetCookie(string url)
        {
            try
            {
                var container = GetUriCookieContainer(new Uri(url));
                var cookies = container.GetCookies(new Uri(url));
                return cookies["RPSSecAuth"].Value;
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
            throw new NotImplementedException();
        }
    }
}
