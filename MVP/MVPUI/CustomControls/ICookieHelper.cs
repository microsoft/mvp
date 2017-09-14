using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Mvpui
{
    public interface ICookieHelper
    {
        string GetCookie(string url);
        string GetCookie(Uri uri);
        void ClearCookie();
    }
}
