﻿using System.Windows.Media;
using EMR.Models;
using EMR.interfaces;
using Wpf.Ui.Controls;

namespace EMR.ViewModels.Pages
{
    public partial class AdministrationViewModel : ObservableObject, INavigationAware
    {
        #region FIELDS

        private bool isInitialized = false;

        private readonly IDatabase<Patient?>? database;

        #endregion

        #region PROPERTIES

        [ObservableProperty]
        private IEnumerable<Patient?>? patients;

        [ObservableProperty]
        private IEnumerable<string?>? name;

        [ObservableProperty]
        private string? selectedName;

        [ObservableProperty]
        private int? selectedId;

        #endregion

        #region CONSTRUCTOR

        public AdministrationViewModel(IDatabase<Patient?>? database)
        {
            this.database = database;
        }

        #endregion

        #region COMMANDS

        [RelayCommand]
        private void OnSelectName()
        {
            var selectedData = this.selectedName;
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
            Patient patient = new Patient();

            patient.Id = (int)this.SelectedId;

            patient.Name = this.SelectedName;

            this.database?.Create(patient);
        }

        [RelayCommand]
        private void ReadAllData()
        {
            this.Patients = this.database?.Get();
        }

        #endregion

        #region METHOS
        public void OnNavigatedTo()
        {
            if (!isInitialized)
                InitializeViewModelAsync();
        }

        public void OnNavigatedFrom() { }


        [RelayCommand]
        private void OnSelectedName()
        {
            var selectedData = this.SelectedName;
        }


        private async Task InitializeViewModelAsync()
        {

            // 비동기로 데이터를 가져오기
            this.Patients = await Task.Run(() => this.database?.Get());

            // 가져온 데이터를 가지고 필요한 작업 수행
            if (this.patients != null)
            {
                this.Name = this.Patients?.Select(c => c.Name).ToList();
            }

            isInitialized = true;
        }

        #endregion
    }
}
