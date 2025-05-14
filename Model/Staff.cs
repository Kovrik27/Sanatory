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

        public string Lastname { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int JobTitleId { get; set; }

        public JobTitle JobTitle { get; set; }

        public string Phone { get; set; }

        public string Mail { get; set; }

        public List<Day> Days { get; set; } = new();

        public int? ProblemID { get; set; }

        public List<Problem>? Problems { get; set; } = new();

        public int? CabinetID { get; set; }

        public Cabinet? Cabinet { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }


    }
}
