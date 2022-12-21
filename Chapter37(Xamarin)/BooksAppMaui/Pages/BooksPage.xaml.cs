using BooksLib.ViewModels;

namespace BooksAppMaui.Pages;

public partial class BooksPage : ContentPage
{
	public BooksPage(BookMaterDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	public static BooksPage FromServiceProvider() =>
		MauiProgram.GetRequiredService<BooksPage>();

    private void MyListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
		var viewModel = BindingContext as BookMaterDetailViewModel;
		if (viewModel != null)
		{
			viewModel.GoToDetailCmd.Execute(sender);
		}
    }
}