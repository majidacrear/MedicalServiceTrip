using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Users
{
    public interface IUserService
    {

        Core.Domain.Users GetUserById(int id);

        int RegisterUser(Core.Domain.Users user);

        Core.Domain.Users VerifyUser(string email, string password,string deviceNumber);

        IEnumerable<Core.Domain.Users> GetUsersByOrganizationId(int organizationId);

        bool ActivateUser(int userId);

        bool DeactivateUser(int userId);
    }
}
