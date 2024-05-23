using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.Model
{
    public class Daytime
    {
        public int ID { get; set; }
        public DateOnly Time { get; set; }
        public int EventID { get; set; } 
        public Events Event { get; set; }

    }
}
