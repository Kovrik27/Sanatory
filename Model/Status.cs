using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.Model
{
    public class Status
    {
        public int Id { get; set; }
        public string Title { get; set; } = "Чистый";
        public int StatusProblemId { get; set; }
        public StatusProblem StatusProblem { get; set; }
    }
}
