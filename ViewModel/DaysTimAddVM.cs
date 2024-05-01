﻿using Sanatory.Model;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.ViewModel
{
    public class DaysTimAddVM : BaseVM
    {
        public CommandVM Save { get; set; }

        private Daytime daytime = new();

        public Daytime Daytime
        {
            get => daytime;
            set
            {
                daytime = value;
                Signal();
            }
        }
        public DaysTimAddVM()
        {

            Save = new CommandVM(() =>
            {
                DaystimeRepository.Instance.AddDaytime(Daytime);
                MainWindowVM.Instance.CurrentPage = new Schedule();

            });

        }


        internal void SetEditDaytime(Daytime selectedDayTime)
        {
            Daytime = selectedDayTime;

        }

      
    }
}

