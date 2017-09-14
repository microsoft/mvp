using System;
using Microsoft.Mvpui;
using Xamarin.Forms;
using Microsoft.Mvp.Droid;
using Android.Webkit;

[assembly: Dependency(typeof(CookieHelper))]
namespace Microsoft.Mvp.Droid
{
    public class CookieHelper : ICookieHelper
    {
        public string GetCookie(string url)
        {
            CookieManager cookManager = CookieManager.Instance;
            string cookie = cookManager.GetCookie(url);
            if (cookie != null)
            {
                String[] temps = cookie.Split(';');
                foreach (var temp in temps)
                { 
                    if (temp.Contains("WLSSC"))
                    { 
                        return temp.Substring(temp.IndexOf("=")+1); 
                    }
                }

            }

            return "";
        }

        public void ClearCookie()
        {
            CookieManager cookManager = CookieManager.Instance;
            cookManager.SetCookie("https://mvpstaging.com/Account/SignIn", "");
            cookManager.RemoveAllCookie(); 
        }

        public string GetCookie(Uri uri)
        {
            throw new NotImplementedException();
        }
    }
}