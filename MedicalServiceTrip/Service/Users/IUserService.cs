using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Users
{
    public interface IUserService
    {
        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Core.Domain.Users GetUserById(int id);

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        int RegisterUser(Core.Domain.Users user);

        /// <summary>
        /// Verify User
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="password">password</param>
        /// <param name="deviceNumber">device number while autheticating. It will be replaced in database to make sure that user is logged in only from one device.</param>
        /// <returns></returns>
        Core.Domain.Users VerifyUser(string email, string password,string deviceNumber);

        /// <summary>
        /// Get Users list by organization id
        /// </summary>
        /// <param name="organizationId">organization id</param>
        /// <returns></returns>
        IEnumerable<Core.Domain.Users> GetUsersByOrganizationId(int organizationId);

        /// <summary>
        /// Activate User
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns></returns>
        bool ActivateUser(int userId);

        /// <summary>
        /// Deactivate user
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns></returns>
        bool DeactivateUser(int userId);


        /// <summary>
        /// Change User Pin
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="pinCode">new pin code</param>
        /// <returns></returns>
        bool ChangeUserPin(int userId, int pinCode,string password);

        /// <summary>
        /// Get non organization users
        /// </summary>
        /// <returns></returns>
        IEnumerable<Core.Domain.Users> GetNonOrganizationUsers();
    }
}
