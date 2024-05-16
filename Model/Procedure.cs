using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.Model
{
    public class Procedure
    {
        public int ID {  get; set; }
        public string Title { get; set; } 
        public string Description { get; set; } 
        public int Duration { get; set; }
        public double Price { get; set; }
    }
}
