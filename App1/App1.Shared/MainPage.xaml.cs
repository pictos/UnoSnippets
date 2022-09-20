using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainPage : Page
{
	ObservableCollection<string> Items { get; } = new(typeof(Colors)
		.GetProperties(BindingFlags.Public | BindingFlags.Static)
		.Select(x => x.Name));

	public MainPage()
	{
		this.InitializeComponent();
		listView.ItemsSource = Items;
	}

	async void OnRefreshRequested(RefreshContainer sender, RefreshRequestedEventArgs args)
	{
		using var deferral = args.GetDeferral();

		await DoSomethingAsync();

		deferral?.Complete();
	}
	
	async Task DoSomethingAsync()
	{
		// Simulate an API call
		await Task.Delay(TimeSpan.FromSeconds(2));
		Items.Insert(0, $"new item {Items.Count + 1}");
	}
}
