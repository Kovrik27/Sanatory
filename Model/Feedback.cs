using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.Model
{
    public class Feedback
    {
        public int Id { get; set; }
        public List<User> Users { get; set; } = new List<User>();
        public int Mark { get; set; }
        public string Description { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public string UserLogin => Users?.FirstOrDefault()?.Login ?? "Нет пользователя";
    }
}
