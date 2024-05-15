using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.Model
{
    public class Problem
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Place {  get; set; }
        public int StaffID { get; set; }
    }
}
