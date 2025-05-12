using Sanatory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.DTO
{
    public class EventOnDayDTO
    {
        public List<Events> Events { get; set; } = new List<Events>();
        public DateTime Day { get; set; }
    }
}
