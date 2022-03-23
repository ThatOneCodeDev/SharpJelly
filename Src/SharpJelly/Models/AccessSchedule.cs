using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpJelly.Models
{
    public class AccessSchedule
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? DayOfWeek { get; set; }
        public int StartHour { get; set; }
        public int EndHour { get; set; }
    }
}
