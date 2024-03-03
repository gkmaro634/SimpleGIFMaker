using SimpleGIFMaker.ViewModels;

namespace SimpleGIFMaker.Views;

public partial class DirectoryExplorerPage : ContentPage
{
	private DirectoryExplorerPageViewModel viewModel;

	public DirectoryExplorerPage(DirectoryExplorerPageViewModel vm)
	{
		InitializeComponent();

		this.viewModel = vm;
		this.BindingContext = vm;
	}
}