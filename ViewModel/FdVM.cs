using Sanatory.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.ViewModel
{
    public class FdVM : BaseVM
    {
        private ObservableCollection<Feedback> feedbacks;

        public ObservableCollection<Feedback> Feedbacks
        {
            get => feedbacks;
            set
            {
                feedbacks = value;
                Signal();
            }
        }
        public CommandVM GetAll { get; set; }
        public CommandVM GetHappy { get; set; }
        public CommandVM GetBad {  get; set; }

        private bool showAll;
        private bool showHappy;
        private bool showBad;

        public bool ShowAll
        {
            get => showAll;
            set
            {
                showAll = value; Signal();
            }
        }

        public bool ShowHappy
        {
            get => showHappy;
            set
            {
                showHappy = value; Signal();
            }
        }

        public bool ShowBad
        {
            get => showBad;
            set
            {
                showBad = value; Signal();
            }
        }



        //public async void GetAll()
        //{
        //    //var allFeedbacks = await 

        //    if(ShowAll)
        //    {

        //    }

        //    if(ShowHappy)
        //    {

        //    }

        //    if(ShowBad)
        //    {

        //    }


        //}


    }
}
