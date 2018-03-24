using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.Data;
using Core.Domain;
using System.Data.Entity;

namespace Service.Users
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly IRepository<Core.Domain.Users> _userRepository;
        private readonly IRepository<Core.Domain.Organization> _organizationRepository;
        private readonly IRepository<Core.Domain.UserRewardPoint> _userRewardPointRepository;
        private readonly IWebHelper _webHelper;
        private readonly IRepository<Core.Domain.CheifComplain> _cheifComplainRepository;
        #endregion

        #region Cors

        public UserService(IRepository<Core.Domain.Users> userRepository,
            IRepository<Core.Domain.Organization> organizationRepository, 
            IRepository<UserRewardPoint> userRewardPointRepository,
            IWebHelper webHelper,
            IRepository<Core.Domain.CheifComplain> cheifComplainRepository)
        {
            this._userRepository = userRepository;
            this._organizationRepository = organizationRepository;
            this._userRewardPointRepository = userRewardPointRepository;
            this._webHelper = webHelper;
            this._cheifComplainRepository = cheifComplainRepository;
        }
        #endregion

        public Core.Domain.Users GetUserById(int id)
        {
            if (id <= 0)
                throw new Exception("UserId cannot be less than or equal to 0.");
            return _userRepository.GetById(id);
        }

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
            else
            {
                throw new Exception("Invalid User Id.");
            }
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
            else
            {
                throw new Exception("Invalid User Id.");
            }
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
                    RegisterInitialDataForOrganization(user.OrganizationId.Value);
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
            else if(user.OrganizationId == 0 || user.OrganizationId == null)
            {
                user.IsActive = true;
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
            var user = this._userRepository.Table.Where(u => u.Email.ToLower().Trim().Equals(email) && u.IsDeleted == false).FirstOrDefault();
            if(user != null)
            {
                if (user.IsActive == true)
                {
                    var salt = user.PasswordSalt;
                    password = _webHelper.ComputeHash(password, salt);
                    if (!user.Password.Equals(password))
                        throw new Exception("Oops! Invalid Username or Password");
                    else if (user.DeviceNumber.Equals(deviceNumber))
                    {
                        user.DeviceNumber = deviceNumber;
                        _userRepository.Update(user);
                    }
                }
                else
                {
                    throw new Exception("Oops! Your account is not activated. Please contact organization Administrator to activate your account.");
                }
            }
            else
            {
                throw new Exception("Oops! Invalid Username or Password.");
            }
            return user;

        }

        public bool ChangeUserPin(int userId, int pinCode,string password)
        {
            if (userId <= 0)
                throw new ArgumentNullException(nameof(userId));

            if (pinCode <= 0)
                throw new ArgumentNullException(nameof(pinCode));

            var user = _userRepository.GetById(userId);
            
            if (user != null)
            {
                var salt = user.PasswordSalt;
                password = _webHelper.ComputeHash(password, salt);
                if (!user.Password.Equals(password))
                    throw new Exception("Oops! Invalid Password");
                user.PinCode = pinCode;
                _userRepository.Update(user);
                return true;
            }
            else
            {
                throw new Exception("Invlaid User Id");
            }
        }

        public IEnumerable<Core.Domain.Users> GetNonOrganizationUsers()
        {
            return this._userRepository.Table.Where(u => (u.OrganizationId == 0 || u.OrganizationId == null) && u.IsDeleted == false).ToList();
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

        private void RegisterInitialDataForOrganization(int organizationId)
        {
            Core.Domain.CheifComplain cheifComplain = new Core.Domain.CheifComplain()
            {
                Name = "CC 1",
                CreatedDate = DateTime.Now,
                OrganizationId = organizationId,
                Level = 1
            };

            _cheifComplainRepository.Insert(cheifComplain);

            Core.Domain.CheifComplain diagnostic = new Core.Domain.CheifComplain()
            {
                Name = "DX 1",
                CreatedDate = DateTime.Now,
                OrganizationId = organizationId,
                Level = 2,
                ParentCheifComplainId = cheifComplain.Id
            };

            _cheifComplainRepository.Insert(diagnostic);

            Core.Domain.CheifComplain diagnostic2 = new Core.Domain.CheifComplain()
            {
                Name = "dx 2",
                CreatedDate = DateTime.Now,
                OrganizationId = organizationId,
                Level = 2,
                ParentCheifComplainId = cheifComplain.Id
            };

            _cheifComplainRepository.Insert(diagnostic2);

            Core.Domain.CheifComplain diagnostic3 = new Core.Domain.CheifComplain()
            {
                Name = "dx 3",
                CreatedDate = DateTime.Now,
                OrganizationId = organizationId,
                Level = 2,
                ParentCheifComplainId = cheifComplain.Id
            };

            _cheifComplainRepository.Insert(diagnostic3);

            Core.Domain.CheifComplain redFlag = new Core.Domain.CheifComplain()
            {
                Name = "RF 1",
                CreatedDate = DateTime.Now,
                OrganizationId = organizationId,
                Level = 3,
                ParentCheifComplainId = diagnostic.Id
            };

            _cheifComplainRepository.Insert(redFlag);

            Core.Domain.CheifComplain redFlag2 = new Core.Domain.CheifComplain()
            {
                Name = "RF 2",
                CreatedDate = DateTime.Now,
                OrganizationId = organizationId,
                Level = 3,
                ParentCheifComplainId = diagnostic.Id
            };

            _cheifComplainRepository.Insert(redFlag2);

            Core.Domain.CheifComplain redFlag3 = new Core.Domain.CheifComplain()
            {
                Name = "RF 3",
                CreatedDate = DateTime.Now,
                OrganizationId = organizationId,
                Level = 3,
                ParentCheifComplainId = diagnostic.Id
            };

            _cheifComplainRepository.Insert(redFlag3);

            Core.Domain.CheifComplain redFlag4 = new Core.Domain.CheifComplain()
            {
                Name = "RF 4",
                CreatedDate = DateTime.Now,
                OrganizationId = organizationId,
                Level = 3,
                ParentCheifComplainId = diagnostic2.Id
            };

            _cheifComplainRepository.Insert(redFlag4);

            Core.Domain.CheifComplain redFlag5 = new Core.Domain.CheifComplain()
            {
                Name = "RF 5",
                CreatedDate = DateTime.Now,
                OrganizationId = organizationId,
                Level = 3,
                ParentCheifComplainId = diagnostic2.Id
            };

            _cheifComplainRepository.Insert(redFlag5);

            Core.Domain.CheifComplain redFlag6 = new Core.Domain.CheifComplain()
            {
                Name = "RF 6",
                CreatedDate = DateTime.Now,
                OrganizationId = organizationId,
                Level = 3,
                ParentCheifComplainId = diagnostic3.Id
            };

            _cheifComplainRepository.Insert(redFlag6);
        }
       
    }
}
