using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.ViewModel
{
    public class PrcAddVM : BaseVM
    {

        public CommandVM Save { get; set; }
        public CommandVM Add { get; set; }

        private Procedure procedure = new();

        public Procedure Procedure
        {
            get => procedure;
            set
            {
                procedure = value;
                Signal();
            }
        }

        private Guests guests = new();

        public Guests Guests
        {
            get => guests;
            set
            {
                guests = value;
                Signal();
            }
        }

        public PrcAddVM()
        {

            Save = new CommandVM(() =>
            {

                if (Procedure.ID == 0)
                    ProceduresRepository.Instance.AddProcedures(Procedure);
                else
                    ProceduresRepository.Instance.UpdateProcedures(Procedure);


                MainWindowVM.Instance.CurrentPage = new Guests();

            });

            Add = new CommandVM(() =>
            {
                //ProceduresRepository.Instance.AddPrc(Guests, Procedure);
                MainWindowVM.Instance.CurrentPage = new Guests();
            });

        }


        internal void SetEditProcedures(Procedure selectedProcedure)
        {
            Procedure = selectedProcedure;
        }

        internal void SetGuests(Guests selectedGuests)
        {
            Guests = selectedGuests;
        }
    }
}
