using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.Model
{
    public class Staff
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Lastname { get; set; }
        public string JobTitle { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public List<Days> Days { get; set; } = new();

    }
}
