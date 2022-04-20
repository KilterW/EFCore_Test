using Prism.Ioc;
using Prism.Regions;
using System.Windows;

namespace EFCore_Test.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IRegionManager regionManager, IContainerExtension container)
        {
            InitializeComponent();

            regionManager.RegisterViewWithRegion("ContentRegionDB", typeof(DBFirstView));
            regionManager.RegisterViewWithRegion("ContentRegionCode", typeof(CodeFirstView));
        }
    }
}
