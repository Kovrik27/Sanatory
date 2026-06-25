using Microsoft.Extensions.Logging;
using Sanatory.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sanatory.Model
{
    public class Daytime
    {
        public int Id { get; set; } 

        public DateTime? Time { get; set; }

        public List<Events>? Events { get; set; }
    }
}
