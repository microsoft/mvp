using System;
using Microsoft.Mvpui;
using Xamarin.Forms;
using Microsoft.Mvp.Droid;
using Android.Webkit;
using Android.Gms.Common;

[assembly: Dependency(typeof(LocationHelper))]
namespace Microsoft.Mvp.Droid
{
    public class LocationHelper : ILocationHelper

    {
        public bool IsGooglePlayServicesIsInstalled()
        {
            return MainActivity.TestIfGooglePlayServicesIsInstalled();
        } 
    }
}