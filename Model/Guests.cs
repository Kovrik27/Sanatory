﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.Model
{
    public class Guests
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string LastName { get; set; }
        public int Pasport { get; set; }
        public int Policy { get; set; }
        public int DataArrival { get; set; }
        public int DataOfDeparture { get; set; }

    }
}
