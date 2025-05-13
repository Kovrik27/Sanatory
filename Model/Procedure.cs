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
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int Duration { get; set; }

        public decimal Price { get; set; }

        public DateTime? Date { get; set; } = DateTime.Now;

        public virtual List<Guest> Guests { get; set; } = new List<Guest>();
    }
}
