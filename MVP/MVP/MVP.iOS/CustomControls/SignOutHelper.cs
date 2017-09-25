using Microsoft.Mvp.iOS.Renders;
using Microsoft.Mvpui;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Microsoft.Mvp.Interfaces;

[assembly: Dependency(typeof(SignOutHelper))]
namespace Microsoft.Mvp.iOS.Renders
{
    public class SignOutHelper : ISignOutHelper
    {
        public void CloseApp()
        {
            //throw new NotImplementedException();
        }
    }
}
