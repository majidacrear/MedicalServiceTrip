using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class UserRewardPoint : BaseEntity
    {
        public int UserId { get; set; }

        public int RewardPoints { get; set; }

        public virtual Users Users { get; set; }
    }
}
