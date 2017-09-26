using System;
using Microsoft.Mvpui;
using Xamarin.Forms;
using Microsoft.Mvp.Droid;
using Android.Webkit;
using Microsoft.Mvp.Interfaces;

[assembly: Dependency(typeof(SignOutHelper))]
namespace Microsoft.Mvp.Droid
{
    public class SignOutHelper : ISignOutHelper
    {
        public void CloseApp()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}