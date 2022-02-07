using MyWebServer;
using MyWebServer.Controllers;
using MyWebServer.Results.Views;
using MyWebServer.Services;
using SharedTrip.Services;
using System.Threading.Tasks;

namespace SharedTrip
{
   

    public class Startup
    {
        public static async Task Main()
            => await HttpServer
                .WithRoutes(routes => routes
                    .MapStaticFiles()
                    .MapControllers())
                .WithServices(services => services
                    .Add<IViewEngine, CompilationViewEngine>()
                    .Add<IUsersService, UsersService>()
                    .Add<ITripsService, TripsService>())
                .Start();

     
    }
}
