using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.Mvp.ViewModels;
using Microsoft.Mvp.Models;
using Microsoft.Mvp.Helpers;
using System.Threading.Tasks;

namespace Microsoft.Mvpui
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContributionsPage : ContentPage
	{

		public ContributionsPage()
		{
			Logger.Log("Page-ContributionsPage");
			InitializeComponent();

			BindingContext = MyProfileViewModel.Instance;

			if (Device.RuntimePlatform == Device.UWP || Device.RuntimePlatform == Device.WinPhone)
				ToolbarAddContribution.Icon = "Assets\\toolbar_add.png";


			if (Device.RuntimePlatform == Device.Android)
			{

				ToolbarItems.Remove(ToolbarAddContribution);
				FloatingActionButtonAdd.IsVisible = true;
				FloatingActionButtonAdd.Clicked += (sender, args) =>
				{
					AddContribution_Clicked(null, null);
				};
			}
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			if (MyProfileViewModel.Instance.List.Count == 0)
				MyProfileViewModel.Instance.RefreshCommand.Execute(null);
		}

		public async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (ListViewContributions.SelectedItem != null)
			{
				var viewModel = new ContributionViewModel();
				viewModel.MyContribution = e.SelectedItem as ContributionModel;
				//TODO: Check state of BugZilla bug : https://bugzilla.xamarin.com/show_bug.cgi?id=52248
				//Temporary fix for UWP - Currently ViewCell disable is not working ( cfr BugZilla link in todo )
				if (viewModel.MyContribution.ContributionEnableEditDelete)
				{
					await Navigation.PushModalAsync(new MVPNavigationPage(
						new ContributionDetail()
						{
							BindingContext = viewModel,
							Title = TranslateServices.GetResourceString(CommonConstants.ContributionDetailTitleForEditing)
						}));
				}

				ListViewContributions.SelectedItem = null;
			}
		}

		public async void OnEdit(object sender, EventArgs eventArgs)
		{
			var mi = ((MenuItem)sender);

			var viewModel = new ContributionViewModel();
			viewModel.MyContribution = mi.CommandParameter as ContributionModel;
			await Navigation.PushModalAsync(new MVPNavigationPage(
				new ContributionDetail()
				{
					BindingContext = viewModel,
					Title = TranslateServices.GetResourceString(CommonConstants.ContributionDetailTitleForEditing)
				}));
		}

		public void OnDelete(object sender, EventArgs eventArgs)
		{
			var mi = ((MenuItem)sender);
			var contribution = mi.CommandParameter as ContributionModel;
			if (contribution == null)
				return;

			MyProfileViewModel.Instance.DeleteCommand.Execute(contribution);
		}
		
		public void OnItemAppearing(object sender, ItemVisibilityEventArgs eventArgs)
		{
			if (MyProfileViewModel.Instance.IsBusy || !MyProfileViewModel.Instance.CanLoadMore)
				return;
			
			var viewCellDetails = eventArgs.Item as ContributionModel;
			var viewCellIndex = MyProfileViewModel.Instance.List.IndexOf(viewCellDetails);
			if (MyProfileViewModel.Instance.List.Count() - 2 <= viewCellIndex)
			{
				MyProfileViewModel.Instance.ExecuteLoadMoreCommand().ContinueWith((results) => { }, TaskScheduler.FromCurrentSynchronizationContext());
			}
		}

		
		bool navigating;
		async void AddContribution_Clicked(object sender, System.EventArgs e)
		{
			if (navigating)
				return;

			navigating = true;

			await Navigation.PushModalAsync(new MVPNavigationPage(new ContributionDetail()));

			navigating = false;
		}

		private void OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			ListViewContributions.SelectedItem = null;
		}
	}
}
