using MyWebServer.Controllers;
using MyWebServer.Http;
using SharedTrip.Services;
using SharedTrip.ViewModels.Trips;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {

        private readonly ITripsService tripsService;

        public TripsController(ITripsService tripsService)
        {
            this.tripsService = tripsService;
        }

        public HttpResponse All()
        {
           

            var trips = this.tripsService.GetAll();
            return this.View(trips);
        }

        public HttpResponse Details(string tripId)
        {
            

            var trip = this.tripsService.GetDetails(tripId);
            return this.View(trip);
        }
        public HttpResponse AddUserToTrip(string tripId)
        {
            

            if (!this.tripsService.HasAvailableSeats(tripId))
            {
                return this.Error("No seats available.");
            }

            var userId = this.User.Id;
            if (!this.tripsService.AddUserToTrip(userId, tripId))
            {
                return this.Redirect("/Trips/Details?tripId=" + tripId);
            }

            return this.Redirect("/Trips/All");
        }
        public HttpResponse Add()
        {
         

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddTripInputModel input)
        {
            

            if (string.IsNullOrEmpty(input.StartPoint))
            {
                return this.Error("Start point is required.");
            }

            if (string.IsNullOrEmpty(input.EndPoint))
            {
                return this.Error("End point is required.");
            }

            if (input.Seats < 2 || input.Seats > 6)
            {
                return this.Error("Seats should be between 2 and 6.");
            }

            if (string.IsNullOrEmpty(input.Description) || input.Description.Length > 80)
            {
                return this.Error("Description is required and has max length of 80.");
            }

            if (!DateTime.TryParseExact(
                input.DepartureTime,
                "dd.MM.yyyy HH:mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _))
            {
                return this.Error("Invalid departure time. Please use dd.MM.yyyy HH:mm format.");
            }

            this.tripsService.Create(input);
            return this.Redirect("/Trips/All");
        }
        
    }
}
