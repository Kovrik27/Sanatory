using Sanatory.Api;
using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sanatory.ViewModel
{
    public class PatientWindowVM : BaseVM
    {
        public static PatientWindowVM Instance { get; set; }

        private Page currentPage;

        public Page CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                Signal();
            }
        }

        private Guest guest;
        public Guest Guest
        {
            get => guest;
            set
            {
                guest = value;
                Signal();
            }
        }

        public CommandVM Information {  get; set; }
        public CommandVM Problemss { get; set; }
        public CommandVM Feedbacks { get; set; } 

        public PatientWindowVM()
        {
            Instance = this;

            Information = new CommandVM(() =>
            {
                OpenInformation();
            });

            Problemss = new CommandVM(() =>
            {
                OpenProblems();
            });

            Feedbacks = new CommandVM(() =>
            {
                OpenFeedbacks();
            });
        }

        private void OpenInformation()
        {
            CurrentPage = new Information();
        }

        private void OpenProblems()
        {
            CurrentPage = new ProblemGuests();
        }

        private void OpenFeedbacks()
        {
            CurrentPage = new FeedbacksGuest();
        }

        private ObservableCollection<Procedure> procedures;


        public ObservableCollection<Procedure> Procedures
        {
            get => procedures;
            set
            {
                procedures = value;
                Signal();
            }
        }

        internal async Task SetGuestId(int id)
        {
            Guest = await DB.GetInstance().GetGuestId(id);
            Procedures = new ObservableCollection<Procedure> (await DB.GetInstance().GetProceduresByGuest(Guest.ID));
        }
    }
}

