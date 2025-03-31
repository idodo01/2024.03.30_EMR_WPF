using System;
using System.Windows;
using EMR.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace EMR.Views.Pages
{
    /// <summary>
    /// CreateStaffWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CreateStaffWindow : Window, INavigableView<StaffViewModel>
    {
        public StaffViewModel ViewModel { get; }

        // StaffViewModel을 생성자로 받음
        public CreateStaffWindow(StaffViewModel viewModel)
        {
            ViewModel = viewModel; // ViewModel을 설정
            DataContext = this;  // DataContext에 ViewModel을 설정하여 바인딩

            InitializeComponent();
        }
    }
}
