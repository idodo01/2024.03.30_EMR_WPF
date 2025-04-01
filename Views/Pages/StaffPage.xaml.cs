using EMR.Models;
using System.Windows.Controls;
using EMR.ViewModels.Pages;
using Wpf.Ui.Controls;
using EMR.Interfaces;
using EMR.Services;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;


namespace EMR.Views.Pages
{
    public partial class StaffPage : INavigableView<StaffViewModel>
    {
        private readonly IDatabase<Staff> _database;

        public StaffViewModel ViewModel { get; }

        public StaffPage(IDatabase<Staff> database)
        {
            _database = database;
            ViewModel = new StaffViewModel(_database);
            DataContext = ViewModel;

            InitializeComponent();
        }

        // 더블 클릭 시 상세 창 열기
        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (((ListBox)sender).SelectedItem is Staff selectedStaff)
            {
                var staffViewModel = new StaffViewModel(_database, selectedStaff);
                StaffDetailWindow detailWindow = new(staffViewModel);
                detailWindow.Show();
            }
        }

        // 선택된 Staff 삭제 버튼
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedStaff != null)
            {
                var result = System.Windows.MessageBox.Show(
                    $"정말 {ViewModel.SelectedStaff.Name} 직원을 삭제하시겠습니까?",
                    "삭제 확인", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Warning
                );

                if (result == System.Windows.MessageBoxResult.Yes)
                {
                    _database.Delete(ViewModel.SelectedStaff.Id);
                    await ViewModel.InitializeViewModelAsync(); // UI 업데이트
                }
            }
            else
            {
                System.Windows.MessageBox.Show("삭제할 직원을 선택하세요.", "알림",System.Windows.MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var options = new DbContextOptionsBuilder<EmrdbContext>()
                .UseNpgsql("Host=localhost;Database=EMRDB;Username=postgres;Password=dntkrlgkxm1!")
                .Options;

            EmrdbContext dbContext = new(options);
            IDatabase<Staff> database = new StaffService(dbContext);
            StaffViewModel viewModel = new StaffViewModel(database);

            CreateStaffWindow createWindow = new(viewModel);
            createWindow.ShowDialog();

            await ViewModel.InitializeViewModelAsync(); // UI 업데이트
        }
    }
}
