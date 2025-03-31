using EMR.Models;
using System.Windows.Controls;
using EMR.ViewModels.Pages;
using Wpf.Ui.Controls;
using EMR.interfaces;
using EMR.Services;
using Microsoft.EntityFrameworkCore;

namespace EMR.Views.Pages
{
    public partial class StaffPage : INavigableView<StaffViewModel>
    {
        private readonly IDatabase<Staff> database; 

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


        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            // EmrdbContext 생성 (DB 연결) - UseNpgsql 사용
            var options = new DbContextOptionsBuilder<EmrdbContext>()
                .UseNpgsql("Host=localhost;Database=EMRDB;Username=postgres;Password=dntkrlgkxm1!") // DB 연결 문자열
                .Options;

            EmrdbContext dbContext = new EmrdbContext(options);

            // StaffService 생성 (DB 연결)
            IDatabase<Staff> database = new StaffService(dbContext);

            // StaffViewModel 생성 시 database 전달
            StaffViewModel viewModel = new StaffViewModel(database);

            // CreateStaffWindow에 ViewModel 전달
            CreateStaffWindow createWindow = new CreateStaffWindow(viewModel);
            createWindow.ShowDialog();  // 모달 창으로 열기 (사용자가 창을 닫을 때까지 다른 작업을 할 수 없음)
        }
    }
}
