using SharedTrip.Data;
using SharedTrip.Models;
using SharedTrip.ViewModels.Trips;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedTrip.Services
{
    public class TripsService : ITripsService
    {

        private readonly ApplicationDbContext db;

        public TripsService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public bool AddUserToTrip(string userId, string tripId)
        {
            var userInTrip = db.UserTrips.Any(x => x.UserId == userId && x.TripId == tripId);
            if(userInTrip)
            {
                return false;
            }

            var userTrip = new UserTrip
            {
                UserId = userId,
                TripId = tripId
            };
            db.UserTrips.Add(userTrip);
            db.SaveChanges();
            return true;
        }

        public void Create(AddTripInputModel trip)
        {
            var dbTrip = new Trip()
            {
                StartingPoint = trip.StartPoint,
                Description = trip.Description,
                DepartureTime = DateTime.ParseExact(trip.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                EndPoint = trip.EndPoint,
                ImagePath = trip.ImagePath,
                Seats = (byte)trip.Seats,
            };
            db.Trips.Add(dbTrip);
            db.SaveChanges();
        }

        public IEnumerable<TripViewModel> GetAll()
        {
            var trips = this.db.Trips.Select(x => new TripViewModel
            {
                DepartureTime = x.DepartureTime,
                EndPoint = x.EndPoint,
                StartPoint = x.StartingPoint,
                Id = x.Id,
                Seats = x.Seats,
                UsedSeats = x.UserTrips.Count(),
            }).ToList();
            return trips;
        }

        public TripDetailsViewModel GetDetails(string id)
        {
            var trip = this.db.Trips.Where(x => x.Id == id)
                .Select(x => new TripDetailsViewModel
                {
                    DepartureTime = x.DepartureTime,
                    Description = x.Description,
                    EndPoint = x.EndPoint,
                    Id = x.Id,
                    ImagePath = x.ImagePath,
                    Seats = x.Seats,
                    StartPoint = x.StartingPoint,
                    UsedSeats = x.UserTrips.Count(),
                })
                .FirstOrDefault();
            return trip;
        }

        public bool HasAvailableSeats(string tripId)
        {
            var trip = db.Trips.Where(x => x.Id == tripId)
                .Select(x => new { x.Seats, TakenSeats = x.UserTrips.Count() })
                .FirstOrDefault();
            var availbleSeats = trip.Seats - trip.TakenSeats;
            return availbleSeats > 0;
        }
    }
}
