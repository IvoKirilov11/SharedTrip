using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedTrip.Services
{
    public interface IUsersService
    {
        string GetUserId(string username, string password);

        void Create(string username, string email, string password);

        bool IsUsernameAvailable(string username);

        bool IsEmailAvailable(string email);
    }
}
