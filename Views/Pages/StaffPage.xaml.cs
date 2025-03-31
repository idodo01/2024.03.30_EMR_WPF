using EMR.Models;
using System.Windows.Controls;
using EMR.ViewModels.Pages;
using Wpf.Ui.Controls;
using EMR.Interfaces;
using EMR.Services;
using Microsoft.EntityFrameworkCore;

namespace EMR.Views.Pages
{
    public partial class StaffPage : INavigableView<StaffViewModel>
    {
        private readonly IDatabase<Staff> _database;  // IDatabase<Staff>를 클래스에 선언

        public StaffViewModel ViewModel { get; }

        // StaffPage 생성자에서 _database를 초기화
        public StaffPage(IDatabase<Staff> database)
        {
            _database = database;  // _database 초기화
            ViewModel = new StaffViewModel(_database);  // ViewModel에 _database 전달
            DataContext = ViewModel;

            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is Staff selectedStaff)
            {
                // 선택된 Staff에 대해 StaffViewModel을 생성하고 전달
                var staffViewModel = new StaffViewModel(_database, selectedStaff); // 선택된 Staff를 전달
                StaffDetailWindow detailWindow = new(staffViewModel);
                detailWindow.Show();
            }
        }


        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            // EmrdbContext 생성 (DB 연결) - UseNpgsql 사용
            var options = new DbContextOptionsBuilder<EmrdbContext>()
                .UseNpgsql("Host=localhost;Database=EMRDB;Username=postgres;Password=dntkrlgkxm1!") // DB 연결 문자열
                .Options;

            EmrdbContext dbContext = new(options);

            // StaffService 생성 (DB 연결)
            IDatabase<Staff> database = new StaffService(dbContext);

            // StaffViewModel 생성 시 database 전달
            StaffViewModel viewModel = new StaffViewModel(database);

            // CreateStaffWindow에 ViewModel 전달
            CreateStaffWindow createWindow = new(viewModel);
            createWindow.ShowDialog();  // 모달 창으로 열기 (사용자가 창을 닫을 때까지 다른 작업을 할 수 없음)
        }
    }
}
