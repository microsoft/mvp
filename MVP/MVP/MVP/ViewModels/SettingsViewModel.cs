using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Mvp.ViewModels
{
	public class SettingsViewModel : ViewModelBase
	{
		public string About { get; } = "This is the official Microsoft Most Valuable Professional (MVP) Award program app.\n\n" +
			"Microsoft MVP award is given to exceptional, independent community leaders who share their passion, technical " +
			"expertise, and real-world knowledge of Microsoft products with others.Microsoft MVPs can use this app to access " +
		  "their profile,  contribution data, as well as automating several different tasks creating efficiency in daily tasks.";
		public string Version { get; } = $"Version: {Plugin.VersionTracking.CrossVersionTracking.Current.CurrentVersion}";
		public string Copyright { get; } = $"Copyright {DateTime.Now.Year} Microsoft";

	}
}
