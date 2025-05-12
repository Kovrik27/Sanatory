using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatory.DTO
{
    public class UserOnDTO
    {
        public int UserId { get; set; }
        public int? StaffId { get; set; }
        public int? GuestId { get; set; }
        public int? DoctorId { get; set; }
    }
}
