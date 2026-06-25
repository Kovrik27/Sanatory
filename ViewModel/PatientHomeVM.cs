using Sanatory.Api;
using Sanatory.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Sanatory.ViewModel
{
    public class PatientHomeVM : BaseVM
    {
        private ObservableCollection<Procedure> procedures;
        private Procedure selectedProcedure;

        public ObservableCollection<Procedure> Procedures
        {
            get => procedures;
            set
            {
                procedures = value;
                Signal();
            }
        }

        public Procedure SelectedProcedure
        {
            get => selectedProcedure;
            set
            {
                selectedProcedure = value;
                Signal();
            }
        }


        //public PatientHomeVM(ObservableCollection<Procedure> procedures)
        //{
        //    Procedures = procedures ?? new ObservableCollection<Procedure>();
        //}
        
        private int patientId;

        public PatientHomeVM(int patientId)
        {
            this.patientId = patientId;
            LoadProcedures();
        }

        private async void LoadProcedures()
        {
            try
            {
                var list = await DB.GetInstance().GetProceduresByGuest(patientId); 
                Procedures = list ?? new ObservableCollection<Procedure>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки процедур: {ex.Message}");
                Procedures = new ObservableCollection<Procedure>();
            }
        }
        
    }
}