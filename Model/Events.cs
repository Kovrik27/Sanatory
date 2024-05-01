using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.Model
{
    public class Events
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public TimeOnly Time { get; set; }
        public string Place { get; set; }
        public int DaytimeID { get; set; }
        public Daytime Daytime { get; set; }


    }
}
