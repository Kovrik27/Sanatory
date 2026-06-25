using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.Model
{
    public class Applications
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Count { get; set; } = null!;

        public int? StafId { get; set; }

        public virtual Staff? Staf { get; set; }
    }
}
