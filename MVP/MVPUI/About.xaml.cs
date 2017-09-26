using System;

using Xamarin.Forms;

namespace Microsoft.Mvpui
{
    public partial class About : ContentPage
    {
        public About()
        {
            InitializeComponent();
        }

        public async void OnCloseClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(true);
        }
    }
}
