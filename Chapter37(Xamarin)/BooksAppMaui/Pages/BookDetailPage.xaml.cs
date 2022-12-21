using BooksLib.Services;
using BooksLib.ViewModels;

namespace BooksAppMaui.Pages;

public partial class BookDetailPage : ContentPage
{
	public BookDetailPage(BookDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}