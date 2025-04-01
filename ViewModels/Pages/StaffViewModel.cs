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
        private int? selectedAge;

        [ObservableProperty]
        private string? selectedEmail;

        [ObservableProperty]
        private string? selectedPhone;

        [ObservableProperty]
        private bool isEditing = false; // 수정 가능 여부

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// 특정 직원 데이터를 초기화하는 생성자
        /// </summary>
        public StaffViewModel(IDatabase<Staff> database, Staff staff)
        {
            _database = database;
            _staff = staff;
            InitializeStaffData(staff);

            SelectStaffCommand = new RelayCommand<Staff>(OnSelectStaff);
            EditStaffCommand = new RelayCommand(OnEditStaff);
            SaveStaffCommand = new RelayCommand(OnSaveStaff);
            DeleteDataCommand = new RelayCommand(OnDeleteData);
            ReadDetailDataCommand = new RelayCommand(OnReadDetailData);
            CreateNewDataCommand = new RelayCommand(OnCreateNewData);
        }

        /// <summary>
        /// 기본 생성자 (직원 리스트를 관리할 때 사용)
        /// </summary>
        public StaffViewModel(IDatabase<Staff> database)
        {
            _database = database;
            SelectStaffCommand = new RelayCommand<Staff>(OnSelectStaff);
            EditStaffCommand = new RelayCommand(OnEditStaff);
            SaveStaffCommand = new RelayCommand(OnSaveStaff);
            DeleteDataCommand = new RelayCommand(OnDeleteData);
            ReadDetailDataCommand = new RelayCommand(OnReadDetailData);
            CreateNewDataCommand = new RelayCommand(OnCreateNewData);


        }

        #endregion

        #region COMMANDS

        /// <summary> 선택된 직원을 업데이트하는 명령 </summary>
        public IRelayCommand<Staff> SelectStaffCommand { get; }

        private void OnSelectStaff(Staff staff)
        {
            if (staff != null)
            {
                InitializeStaffData(staff);
                IsEditing = false; // 초기 상태에서는 편집 비활성화
            }
        }

        /// <summary> 수정 버튼 클릭 시 실행되는 명령어 </summary>
        public IRelayCommand EditStaffCommand { get; }

        private void OnEditStaff()
        {
            IsEditing = true; // 수정 가능 상태 변경
        }

        /// <summary> 저장 버튼 클릭 시 실행되는 명령어 </summary>
        public IRelayCommand SaveStaffCommand { get; }

        public void OnSaveStaff()
        {
            if (SelectedStaff == null) return;

            var data = _database?.GetDetail(SelectedStaff.Id);
            if (data != null)
            {
                data.Name = SelectedName;
                data.Department = SelectedDepartment;
                data.Position = SelectedPosition;
                data.Age = SelectedAge;
                data.Email = SelectedEmail;
                data.Phone = SelectedPhone;

                _database?.Update(data);
            }
            else
            {
                Debug.WriteLine("ERROR: Staff data not found!");
            }
        }

        /// <summary> 직원 삭제 명령 </summary>
        public IRelayCommand DeleteDataCommand { get; }

        private void OnDeleteData()
        {
            if (SelectedId.HasValue)
            {
                _database?.Delete(SelectedId.Value);
                RefreshStaffList();
            }
        }

        /// <summary> 직원 상세 정보 불러오기 </summary>
        public IRelayCommand ReadDetailDataCommand { get; }

        private void OnReadDetailData()
        {
            if (SelectedId.HasValue)
            {
                var data = _database?.GetDetail(SelectedId.Value);
                if (data != null)
                {
                    SelectedName = data.Name;
                    SelectedDepartment = data.Department;
                    SelectedPosition = data.Position;
                    SelectedAge = data.Age;
                    SelectedEmail = data.Email;
                    SelectedPhone = data.Phone;
                }
            }
        }

        /// <summary> 새로운 직원 추가 명령 </summary>
        public IRelayCommand CreateNewDataCommand { get; }

        private void OnCreateNewData()
        {
            var newStaff = new Staff
            {
                Name = SelectedName,
                Department = SelectedDepartment,
                Position = SelectedPosition,
                Age = SelectedAge,
                Email = SelectedEmail,
                Phone = SelectedPhone
            };

            if (_database == null)
            {
                Debug.WriteLine("ERROR: Database is null!");
                return;
            }

            _database.Create(newStaff);
            RefreshStaffList();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// 특정 직원 데이터 초기화
        /// </summary>
        private void InitializeStaffData(Staff staff)
        {
            SelectedStaff = staff;
            SelectedId = staff.Id;
            SelectedName = staff.Name;
            SelectedDepartment = staff.Department;
            SelectedPosition = staff.Position;
            SelectedAge = staff.Age;
            SelectedEmail = staff.Email;
            SelectedPhone = staff.Phone;
        }

        /// <summary>
        /// 직원 목록 새로고침
        /// </summary>
        private async void RefreshStaffList()
        {
            Staffs = await _database.GetAsync();
        }

        public void OnNavigatedTo()
        {
            if (!isInitialized)
                InitializeViewModelAsync();
        }

        public void OnNavigatedFrom()
        {
        }

        public async Task InitializeViewModelAsync()
        {
            Staffs = await Task.Run(() => _database.GetAsync());
            isInitialized = true;
        }


        #endregion
    }
}
