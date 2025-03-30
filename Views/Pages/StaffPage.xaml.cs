using EMR.Models;
using System.Windows.Controls;
using EMR.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace EMR.Views.Pages
{
    public partial class StaffPage : INavigableView<StaffViewModel>
    {
        public StaffViewModel ViewModel { get; }

        public StaffPage(StaffViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is Staff selectedStaff)
            {
                // 🟢 새로운 창에서 Staff 상세 정보 표시
                StaffDetailWindow detailWindow = new StaffDetailWindow(selectedStaff);
                detailWindow.Show();
            }
        }
    }
}
