using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Users
{
    public interface IUserRewardPoint
    {
        int AddUserRewardPoint(UserRewardPoint userRewardPoint);

        bool UpdateUserRewardPointAgainstReferralCode(int userId, int points);

        UserRewardPoint GetUserRewardPointByUserId(int userId);
    }
}
