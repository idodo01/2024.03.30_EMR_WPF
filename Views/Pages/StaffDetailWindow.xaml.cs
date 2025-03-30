using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EMR.Models;

namespace EMR.Views.Pages
{
    /// <summary>
    /// StaffDetailWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class StaffDetailWindow : Window
    {
        public StaffDetailWindow(Staff staff)
        {
            InitializeComponent();
            DataContext = staff; // 🟢 선택된 Staff 정보를 바인딩
        }
    }
}
