using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.Mvp.ViewModels;
using Microsoft.Mvp.Models;
using Microsoft.Mvp.Helpers;

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

		public async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (ListViewContributions.SelectedItem != null)
			{
				var viewModel = new ContributionViewModel();
				viewModel.MyContribution = e.SelectedItem as ContributionModel;
				await Navigation.PushModalAsync(new MVPNavigationPage(
					new ContributionDetail()
					{
						BindingContext = viewModel,
						Title = "Edit an activity"
					}));

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
					Title = "Edit an activity"
				}));
		}

		public async void OnDelete(object sender, EventArgs eventArgs)
		{
			var mi = ((MenuItem)sender);
			ContributionModel contribution = mi.CommandParameter as ContributionModel;

			var remove = await DisplayAlert("Delete Activity?", "Are you sure you want to delete this activity?", "Yes, Delete", "Cancel");
			if (!remove)
				return;

			string result = await MvpHelper.MvpService.DeleteContributionModel(Convert.ToInt32(contribution.ContributionId, System.Globalization.CultureInfo.InvariantCulture), LogOnViewModel.StoredToken);
			if (result == CommonConstants.OkResult)
			{
				var modelToDelete = MyProfileViewModel.Instance.List.Where(item => item.ContributionId == contribution.ContributionId).FirstOrDefault();
				MyProfileViewModel.Instance.List.Remove(modelToDelete);
			}
		}

		private bool _allContributionsLoaded = false;
		public async void OnItemAppearing(object sender, ItemVisibilityEventArgs eventArgs)
		{
			if (!_allContributionsLoaded && !ListViewContributions.IsRefreshing)
			{
				ListViewContributions.IsRefreshing = true;
				var viewCellDetails = eventArgs.Item as ContributionModel;
				var viewCellIndex = MyProfileViewModel.Instance.List.IndexOf(viewCellDetails);
				if (MyProfileViewModel.Instance.List.Count() - 2 <= viewCellIndex)
				{
					var contributions = await MvpHelper.MvpService.GetContributions(MyProfileViewModel.Instance.List.Count(), 50, LogOnViewModel.StoredToken);

					if (contributions.Contributions.Count > 0)
					{
						MyProfileViewModel.Instance.List.AddRange(contributions.Contributions);
					}
					else
					{
						_allContributionsLoaded = true;
					}

				}
				ListViewContributions.IsRefreshing = false;
			}
		}

		public async void OnRefreshing(object sender, System.EventArgs e)
		{
			var contributions = await MvpHelper.MvpService.GetContributions(-5, 50, LogOnViewModel.StoredToken);
			MvpHelper.SetContributionInfoToProfileViewModel(contributions);

			ListViewContributions.IsRefreshing = false;
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
