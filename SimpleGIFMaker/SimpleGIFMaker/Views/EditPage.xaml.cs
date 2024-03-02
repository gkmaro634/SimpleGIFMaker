using SimpleGIFMaker.ViewModels;

namespace SimpleGIFMaker.Views;

public partial class EditPage : ContentPage
{
	private EditPageViewModel viewModel;

	public EditPage(EditPageViewModel vm)
	{
		InitializeComponent();

		this.viewModel = vm;
		this.BindingContext = vm;
	}
}