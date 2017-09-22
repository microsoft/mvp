using System;
namespace Microsoft.Mvp.Interfaces
{
    public interface IStatusBar
    {
        void SetLightStatusBar(bool animated);
        void SetDarkStatusBar(bool animated);
    }
}
