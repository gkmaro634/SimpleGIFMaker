using SimpleGIFMaker.ViewModels;

namespace SimpleGIFMaker.Views;

public partial class ContentGalleryPage : ContentPage
{
	private ContentGalleryPageViewModel viewModel;
	public ContentGalleryPage(ContentGalleryPageViewModel vm)
	{
		InitializeComponent();
        this.viewModel = vm;
        this.BindingContext = vm;
    }
}