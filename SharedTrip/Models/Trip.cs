using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedTrip.Models
{
    public class Trip
    {
        // Has an Id – a string, Primary Key
        // Has a StartPoint – a string (required)
        // Has a EndPoint – a string (required)
        // Has a DepartureTime – a datetime(required)
        // Has a Seats – an integer with min value 2 and max value 6 (required)
        // Has a Description – a string with max length 80 (required)
        // Has a ImagePath – a string
        // Has UserTrips collection – a UserTrip type
        public Trip()
        {
            UserTrips = new HashSet<UserTrip>();
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

        [Required]
        public string StartingPoint { get; set; }

        [Required]
        public string EndPoint { get; set; }

        public DateTime DepartureTime { get; set; }

        public byte Seats { get; set; }

        [Required]
        [MaxLength(80)]
        public string Description { get; set; }

        public string ImagePath { get; set; }

        public virtual ICollection<UserTrip> UserTrips { get; set; }
    }
}
