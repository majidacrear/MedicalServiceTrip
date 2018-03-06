using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class UserRewardPointMap : MSTEntityTypeConfiguration<UserRewardPoint>
    {
        public UserRewardPointMap()
        {
            this.ToTable("UserRewardPoint");
            this.HasKey(e => e.Id);
            this.Property(e => e.RewardPoints).IsRequired();
            this.HasRequired(e => e.Users).WithMany().HasForeignKey(e => e.UserId).WillCascadeOnDelete(false);
        }
    }
}
