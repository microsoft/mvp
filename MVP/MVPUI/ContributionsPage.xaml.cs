using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MvvmHelpers;
using Microsoft.Mvp.ViewModels;
using Microsoft.Mvp.Models;
using Microsoft.Mvp.Helpers;
using Microsoft.Mvpui.Helpers;

namespace Microsoft.Mvpui
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContributionsPage : ContentPage
    {
       

        public ContributionsPage()
        {
            InitializeComponent();
            

            BindingContext = MyProfileViewModel.Instance;
        }

        public async void OnEdit(object sender, EventArgs eventArgs)
        {
            var mi = ((MenuItem)sender);
            ContributionViewModel.Instance.MyContribution = mi.CommandParameter as ContributionModel;
            await Navigation.PushModalAsync(
                new ContributionDetail()
                {
                    BindingContext = ContributionViewModel.Instance
                });
        }

        public async void OnDelete(object sender, EventArgs eventArgs)
        {
            var mi = ((MenuItem)sender);
            ContributionModel contribution = mi.CommandParameter as ContributionModel;

            string result = await MvpHelper.MvpService.DeleteContributionModel(Convert.ToInt32(contribution.ContributionId, System.Globalization.CultureInfo.InvariantCulture), LogOnViewModel.StoredToken);
            if (result == CommonConstants.OkResult)
            {
                var modelToDelete = MyProfileViewModel.Instance.List.Where(item => item.ContributionId == contribution.ContributionId).FirstOrDefault();
                MyProfileViewModel.Instance.List.Remove(modelToDelete);
            }
        }
    }
}