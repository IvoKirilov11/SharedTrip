using MyWebServer.Controllers;
using MyWebServer.Http;
using SharedTrip.Services;
using SharedTrip.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedTrip.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public HttpResponse Login()
        {
            
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputModel input)
        {
          

            var userId = this.usersService.GetUserId(input.Username, input.Password);
            if (userId == null)
            {
                return this.Error("Invalid username or password.");
            }

            this.SignIn(userId);
            return this.Redirect("/Trips/All");
        }

        public HttpResponse Register()
        {
            

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {
            

            if (string.IsNullOrEmpty(input.Username) || input.Username.Length < 5 || input.Username.Length > 20)
            {
                return this.Error("Username should be between 5 and 20 character long.");
            }

            if (string.IsNullOrEmpty(input.Email) || !new EmailAddressAttribute().IsValid(input.Email))
            {
                return this.Error("Invalid email.");
            }

            if (string.IsNullOrEmpty(input.Password) || input.Password.Length < 6 || input.Password.Length > 20)
            {
                return this.Error("Password is required and should be between 6 and 20 characters.");
            }

            if (input.ConfirmPassword != input.Password)
            {
                return this.Error("Passwords do not match.");
            }

            if (!this.usersService.IsEmailAvailable(input.Email))
            {
                return this.Error("Email already taken.");
            }

            if (!this.usersService.IsUsernameAvailable(input.Username))
            {
                return this.Error("Username already taken.");
            }

            this.usersService.Create(input.Username, input.Email, input.Password);
            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            

            this.SignOut();
            return this.Redirect("/");
        }
    }
}
