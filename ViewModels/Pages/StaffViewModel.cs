using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMR.Models;
using EMR.interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Wpf.Ui.Controls;
using System.Diagnostics;

namespace EMR.ViewModels.Pages
{
    public partial class StaffViewModel : ObservableObject, INavigationAware
    {
        #region FIELDS

        private bool isInitialized = false;
        private readonly IDatabase<Staff> database;

        #endregion

        #region PROPERTIES

        [ObservableProperty]
        private IEnumerable<Staff> staffs;

        [ObservableProperty]
        private Staff selectedStaff;

        [ObservableProperty]
        private IEnumerable<string?>? name;

        [ObservableProperty]
        private int? selectedId;

        [ObservableProperty]
        private string? selectedName;

        [ObservableProperty]
        private string? selectedDepartment;

        [ObservableProperty]
        private string? selectedPosition;

        [ObservableProperty] 
        public int? selectedAge;

        [ObservableProperty]
        private string? selectedEmail;

        [ObservableProperty]
        private string? selectedPhone;

        #endregion

        #region CONSTRUCTOR

        public StaffViewModel(IDatabase<Staff> database)
        {
            this.database = database;
            SelectStaffCommand = new RelayCommand<Staff>(OnSelectStaff);
        }

        #endregion

        #region COMMANDS

        /// <summary>
        /// "Select" 버튼 클릭 시 실행되는 명령어
        /// </summary>
        public IRelayCommand<Staff> SelectStaffCommand { get; }
        
        private void OnSelectStaff(Staff staff)
        {
            if (staff != null)
            {
                SelectedStaff = staff;
            }
        }

        [RelayCommand]
        private void UpdateData()
        {
            var data = this.database?.GetDetail(this.SelectedId);

            data.Name = this.SelectedName;

            this.database?.Update(data);
        }

        [RelayCommand]
        private void DeleteData()
        {
            this.database?.Delete(this.SelectedId);
        }

        [RelayCommand]
        private void ReadDetailData()
        {
            var data = this.database?.GetDetail(this.SelectedId);

            this.SelectedName = data.Name;
        }

        [RelayCommand]
        private void CreateNewData()
        {
            // Staff 객체 생성
            Staff staff = new Staff();

            Debug.WriteLine("CREATE");

            // ViewModel에서 값을 가져와 staff 객체에 할당 (Id는 제외)
            staff.Name = this.SelectedName;
            staff.Department = this.SelectedDepartment;
            staff.Position = this.SelectedPosition;
            staff.Age = this.SelectedAge;
            staff.Email = this.SelectedEmail;
            staff.Phone = this.SelectedPhone;

            if (this.database == null)
            {
                Debug.WriteLine("ERROR: this.database is null!");
                return;
            }
            Debug.WriteLine("this.database is NOT null. Calling Create...");

            // 데이터베이스에 Staff 객체 저장
            this.database?.Create(staff);
        }


        [RelayCommand]
        private void ReadAllData()
        {
            this.Staffs = this.database?.Get();
        }

        #endregion


        #region METHODS

        public void OnNavigatedTo()
        {
            if (!isInitialized)
                InitializeViewModelAsync();
        }

        public void OnNavigatedFrom()
        {
        }

        private async Task InitializeViewModelAsync()
        {
            Staffs = await Task.Run(() => database.Get());
            isInitialized = true;
        }

        #endregion
    }
}
