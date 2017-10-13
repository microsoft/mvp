using System;

using Android.App;
using Android.OS;
using Android.Runtime;
using Plugin.CurrentActivity;

namespace Microsoft.Mvp.Droid
{
	//You can specify additional application information in this attribute
    [Application]
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
          :base(handle, transer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            RegisterActivityLifecycleCallbacks(this);
            Acr.UserDialogs.UserDialogs.Init(() => CrossCurrentActivity.Current.Activity);

            //A great place to initialize Xamarin.Insights and Dependency Services!
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Activity contribution, Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity = contribution;
        }

        public void OnActivityDestroyed(Activity contribution)
        {
        }

        public void OnActivityPaused(Activity contribution)
        {
        }

        public void OnActivityResumed(Activity contribution)
        {
            CrossCurrentActivity.Current.Activity = contribution;
        }

        public void OnActivitySaveInstanceState(Activity contribution, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity contribution)
        {
            CrossCurrentActivity.Current.Activity = contribution;
        }

        public void OnActivityStopped(Activity contribution)
        {
        }
    }
}