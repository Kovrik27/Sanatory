using Sanatory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.DTO
{
    public class GuestWithProceduresDTO
    {
        public Guest Guest { get; set; }
        public List<int> ProcedureIds { get; set; }
    }

    public class GuestProcedureDTO
    {
        public int GuestId { get; set; }
        public int ProcedureId { get; set; }
    }
}
