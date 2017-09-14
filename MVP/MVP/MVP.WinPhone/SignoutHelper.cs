using System;
using Microsoft.Mvpui;
using Xamarin.Forms;
using Microsoft.Mvp.WinPhone;

[assembly: Dependency(typeof(SignOutHelper))]
namespace Microsoft.Mvp.WinPhone
{
    public class SignOutHelper : ISignOutHelper
    {
        public void CloseApp()
        {
            Windows.UI.Xaml.Application.Current.Exit();
        }
    }
}