using SharedTrip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedTrip.Models
{
    public class UserTrip
    {
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public string TripId { get; set; }

        public virtual Trip Trip { get; set; }
    }
}
