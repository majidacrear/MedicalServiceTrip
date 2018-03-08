using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.Data;
using Core.Domain;

namespace Service.Users
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly IRepository<Core.Domain.Users> _userRepository;
        private readonly IRepository<Core.Domain.Organization> _organizationRepository;
        private readonly IRepository<Core.Domain.UserRewardPoint> _userRewardPointRepository;
        private readonly IWebHelper _webHelper;
        #endregion

        #region Cors

        public UserService(IRepository<Core.Domain.Users> userRepository,
            IRepository<Core.Domain.Organization> organizationRepository, 
            IRepository<UserRewardPoint> userRewardPointRepository,
            IWebHelper webHelper)
        {
            this._userRepository = userRepository;
            this._organizationRepository = organizationRepository;
            this._userRewardPointRepository = userRewardPointRepository;
            this._webHelper = webHelper;
        }
        #endregion

        public bool ActivateUser(int userId)
        {
            if (userId <= 0)
                throw new Exception("UserId cannot be less than or equal to 0.");
            var user = this._userRepository.GetById(userId);
            if(user!= null)
            {
                user.IsActive = true;
                this._userRepository.Update(user);
                return true;
            }
            return false;
        }

        public bool DeactivateUser(int userId)
        {
            if (userId <= 0)
                throw new Exception("UserId cannot be less than or equal to 0.");
            var user = this._userRepository.GetById(userId);
            if (user != null)
            {
                user.IsActive = false;
                this._userRepository.Update(user);
                return true;
            }
            return false;
        }

        public IEnumerable<Core.Domain.Users> GetUsersByOrganizationId(int organizationId)
        {
            if (organizationId <= 0)
                throw new Exception("organizationId cannot be less than or equal to 0.");
            return this._userRepository.Table.Where(u=>u.OrganizationId == organizationId && u.IsDeleted == false).ToList();            
        }

        public int RegisterUser(Core.Domain.Users user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            // Check Email exist.
            if(_userRepository.Table.Where(u=>u.Email.ToLower().Equals(user.Email) && u.IsDeleted ==false).FirstOrDefault() != null)
            {
                throw new Exception("Email already exist.");
            }

            // Check Organization Exist
            if((user.OrganizationId <=0 || user.OrganizationId == null) && user.Organization != null && !String.IsNullOrEmpty(user.Organization.OrganizationName))
            {
                var organization = _organizationRepository.Table.Where(o => o.OrganizationName.Equals(user.Organization.OrganizationName)).FirstOrDefault();
                if (organization == null)
                {
                    user.Organization.CreatedDate = DateTime.Now;
                    _organizationRepository.Insert(user.Organization);
                    user.OrganizationId = user.Organization.Id;
                }
                else
                {
                    user.Organization = organization;
                    user.OrganizationId = organization.Id;
                }
                user.IsOrganizationAdmin = true;
                user.IsActive = true;
            }
            else if(user.OrganizationId >0)
            {
                user.IsActive = false;
            }
            user.MyCode = GetRefferalCode();            
            user.IsDeleted = false;
            user.CreatedDate = DateTime.Now;
            this._userRepository.Insert(user);

            if (!String.IsNullOrEmpty(user.RefferalCode))
            {
                var refferalUser = this._userRepository.Table.Where(u => u.MyCode.Equals(user.RefferalCode)).FirstOrDefault();
                if (refferalUser != null)
                {
                    var userRewardPoint = this._userRewardPointRepository.Table.Where(r => r.UserId == refferalUser.Id).FirstOrDefault();
                    if (userRewardPoint != null)
                    {
                        // Update reward points of user who shared referral code
                        userRewardPoint.RewardPoints += 100;
                        this._userRewardPointRepository.Update(userRewardPoint);
                    }
                    else
                    {
                        // Add Reward point for existing user who shared refferral code.
                        userRewardPoint = new UserRewardPoint();
                        userRewardPoint.UserId = refferalUser.Id;
                        userRewardPoint.RewardPoints = 100;
                        this._userRewardPointRepository.Insert(userRewardPoint);
                    }

                    // Add Reward point for new user who used refferral code.
                    userRewardPoint = new UserRewardPoint();
                    userRewardPoint.UserId = user.Id;
                    userRewardPoint.RewardPoints = 100;
                    this._userRewardPointRepository.Insert(userRewardPoint);
                }
            }

            return user.Id;
        }

        public Core.Domain.Users VerifyUser(string email, string password, string deviceNumber)
        {
            if(string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrEmpty(deviceNumber))
                throw new ArgumentNullException(nameof(deviceNumber));
            var user = this._userRepository.Table.Where(u => u.Email.ToLower().Trim().Equals(email) && u.IsDeleted == true && u.IsActive == true).FirstOrDefault();
            if(user != null)
            {
                var salt = user.PasswordSalt;
                password = _webHelper.ComputeHash(password, salt);
                if (!user.Password.Equals(password))
                    throw new Exception("Invalid Username or Password");
                else if(user.DeviceNumber.Equals(deviceNumber))
                {
                    user.DeviceNumber = deviceNumber;
                    _userRepository.Update(user);
                }
            }
            return user;

        }

        private string GetRefferalCode()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
    }
}
