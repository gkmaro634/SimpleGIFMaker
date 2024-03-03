using SimpleGIFMaker.ViewModels;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Dispatching;
using Microsoft.Maui.ApplicationModel;
using System.Text.RegularExpressions;

namespace SimpleGIFMaker.Views;

public partial class EditPage : ContentPage
{
	private EditPageViewModel viewModel;
    private string movieFilePath = @"C:\Users\oosot\Documents\workspace\work\SimpleGIFMaker\SimpleGIFMaker\SimpleGIFMaker.Tests\testdata\V_20240229_162423_ES0.mp4";

    public EditPage(EditPageViewModel vm)
	{
		InitializeComponent();
		this.viewModel = vm;
		this.BindingContext = vm;

	}

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
		this.media.Source = movieFilePath;
    }
}