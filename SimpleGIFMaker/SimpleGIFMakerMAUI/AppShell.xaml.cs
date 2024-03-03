using SimpleGIFMaker.Views;

namespace SimpleGIFMaker
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(EditPage), typeof(EditPage));
            Routing.RegisterRoute(nameof(ContentGalleryPage), typeof(ContentGalleryPage));
            Routing.RegisterRoute(nameof(DirectoryExplorerPage), typeof(DirectoryExplorerPage));
        }
    }
}
