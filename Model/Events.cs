using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sanatory.Model
{
    public class Events
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public int Duration { get; set; }

        public string Place { get; set; } = null!;

        public string? Date { get; set; }
        public virtual List<Daytime> Days { get; set; } = new List<Daytime>();


    }
}
