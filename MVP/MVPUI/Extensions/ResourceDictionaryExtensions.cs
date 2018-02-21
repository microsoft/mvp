using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Mvpui
{
	internal static class MergeExtensions
	{
		internal static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
		{
			foreach (var item in items) action(item);
		}

		internal static void Merge<TKey, TValue>(this IDictionary<TKey, TValue> targetDictionary,
			IEnumerable<KeyValuePair<TKey, TValue>> sourceItems)
		{
			var targetItems = targetDictionary.ToArray();
			sourceItems.ForEach(i => targetDictionary[i.Key] = i.Value);
			targetItems.ForEach(i => targetDictionary[i.Key] = i.Value); // override merged by local values
		}
	}

	public class ResourceDictionary : Xamarin.Forms.ResourceDictionary
	{
		public ResourceDictionary()
		{
			MergedDictionaries = new ObservableCollection<Xamarin.Forms.ResourceDictionary>();
			MergedDictionaries.CollectionChanged += (sender, args) =>
				(args.NewItems ?? new List<object>()).OfType<Xamarin.Forms.ResourceDictionary>()
				.ForEach(this.Merge);
		}

		public ObservableCollection<Xamarin.Forms.ResourceDictionary> MergedDictionaries { get; }
	}
}