using EMR.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace EMR.Views.Pages
{
    public partial class AdministrationPage : INavigableView<AdministrationViewModel>
    {
        public AdministrationViewModel ViewModel { get; }

        public AdministrationPage(AdministrationViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
