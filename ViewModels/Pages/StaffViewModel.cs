using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMR.Models;
using EMR.Interfaces;
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

        private readonly IDatabase<Staff> _database;
        private Staff _staff;

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

        [ObservableProperty]
        private bool isEditing = false;  // 수정 가능 여부

        #endregion

        #region CONSTRUCTOR

        
        // 생성자에서 선택된 Staff 객체를 받아 처리
        public StaffViewModel(IDatabase<Staff> database, Staff staff)
        {
            _database = database;
            _staff = staff;
            SelectedStaff = staff; // Staff 객체를 SelectedStaff에 할당
            SelectStaffCommand = new RelayCommand<Staff>(OnSelectStaff);
            EditStaffCommand = new RelayCommand(OnEditStaff);
            SaveStaffCommand = new RelayCommand(OnSaveStaff);
        }
        public StaffViewModel(IDatabase<Staff> database)
        {
            _database = database;
            SelectStaffCommand = new RelayCommand<Staff>(OnSelectStaff);
            EditStaffCommand = new RelayCommand(OnEditStaff);
            SaveStaffCommand = new RelayCommand(OnSaveStaff);
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
                SelectedName = staff.Name;
                SelectedDepartment = staff.Department;
                SelectedPosition = staff.Position;
                SelectedAge = staff.Age;
                SelectedEmail = staff.Email;
                SelectedPhone = staff.Phone;
                IsEditing = false;  // Staff가 선택되면 수정 불가능 상태로 초기화
            }
        }

        /// <summary>
        /// 수정 버튼 클릭 시 실행되는 명령어
        /// </summary>
        public IRelayCommand EditStaffCommand { get; }

        private void OnEditStaff()
        {
            IsEditing = true;  // 수정 가능한 상태로 변경
        }

        /// <summary>
        /// 저장 버튼 클릭 시 실행되는 명령어
        /// </summary>
        public IRelayCommand SaveStaffCommand { get; }

        // OnSaveStaff()에서 Staff 정보 저장
        public void OnSaveStaff()
        {
            var data = _database?.GetDetail(_staff.Id);
            if (data != null)
            {
                data.Name = _staff.Name;
                data.Department = _staff.Department;
                data.Position = _staff.Position;
                data.Age = _staff.Age;
                data.Email = _staff.Email;
                data.Phone = _staff.Phone;
                _database?.Update(data);
            }
            else
            {
                Debug.WriteLine("ERROR: Staff data not found!");
            }
        }

        [RelayCommand]
        private void DeleteData()
        {
            _database?.Delete(this.SelectedId);
        }

        [RelayCommand]
        private void ReadDetailData()
        {
            var data = _database?.GetDetail(this.SelectedId);

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

            if (_database == null)
            {
                Debug.WriteLine("ERROR: this.database is null!");
                return;
            }
            Debug.WriteLine("this.database is NOT null. Calling Create...");

            // 데이터베이스에 Staff 객체 저장
            _database?.Create(staff);
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
            Staffs = await Task.Run(() => _database.GetAsync());
            isInitialized = true;
        }

        #endregion
    }
}
