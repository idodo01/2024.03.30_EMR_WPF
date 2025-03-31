using System.Diagnostics;
using System.Windows;
using EMR.Models;
using EMR.ViewModels.Pages;

namespace EMR.Views.Pages
{
    public partial class StaffDetailWindow : Window
    {
        private StaffViewModel _viewModel; // StaffViewModel 변수 추가

        public StaffDetailWindow(StaffViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel; // 뷰모델을 DataContext로 설정
        }

        // 수정 버튼 클릭 시 호출되는 이벤트 핸들러
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // TextBox의 IsReadOnly 속성을 false로 변경하여 수정 가능하도록 설정
            NameTextBox.IsReadOnly = false;
            DepartmentTextBox.IsReadOnly = false;
            PositionTextBox.IsReadOnly = false;
            AgeTextBox.IsReadOnly = false;
            EmailTextBox.IsReadOnly = false;
            PhoneTextBox.IsReadOnly = false;

            // 저장 버튼 활성화
            SaveButton.IsEnabled = true;
        }

        // 저장 버튼 클릭 시 이벤트 핸들러
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // 뷰모델의 저장 메서드 호출
            _viewModel.OnSaveStaff();

            // 저장 후 읽기 전용으로 설정
            NameTextBox.IsReadOnly = true;
            DepartmentTextBox.IsReadOnly = true;
            PositionTextBox.IsReadOnly = true;
            AgeTextBox.IsReadOnly = true;
            EmailTextBox.IsReadOnly = true;
            PhoneTextBox.IsReadOnly = true;

            // 저장 버튼 비활성화
            SaveButton.IsEnabled = false;

            // 저장 완료 메시지 표시
            MessageBox.Show("Staff details updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
