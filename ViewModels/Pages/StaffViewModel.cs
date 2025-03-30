using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMR.Models;
using EMR.interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Wpf.Ui.Controls;

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

        //[RelayCommand]
        //private void UpdateData()
        //{
        //    var data = this.database?.GetDetail(this.SelectedId);

        //    data.Name = this.SelectedName;

        //    this.database?.Update(data);
        //}

        //[RelayCommand]
        //private void DeleteData()
        //{
        //    this.database?.Delete(this.SelectedId);
        //}

        //[RelayCommand]
        //private void ReadDetailData()
        //{
        //    var data = this.database?.GetDetail(this.SelectedId);

        //    this.SelectedName = data.Name;
        //}

        //[RelayCommand]
        //private void CreateNewData()
        //{
        //    Patient patient = new Patient();

        //    patient.Id = (int)this.SelectedId;

        //    patient.Name = this.SelectedName;

        //    this.database?.Create(patient);
        //}

        //[RelayCommand]
        //private void ReadAllData()
        //{
        //    this.Patients = this.database?.Get();
        //}

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
