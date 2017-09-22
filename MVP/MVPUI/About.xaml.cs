using Microsoft.Mvp.Helpers;
using Microsoft.Mvpui.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
