using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.Model
{
    public class Guest
    {
        public int ID { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Pasport { get; set; }

        public string Policy { get; set; }

        public DateTime DataArrival { get; set; } = DateTime.Now;

        public DateTime DataOfDeparture { get; set; } = DateTime.Now;

        public int RoomID {  get; set; }

        public Room Room { get; set; } = new();

        public List<Procedure>? Procedures { get; set; } = new List<Procedure>();
    }
}
