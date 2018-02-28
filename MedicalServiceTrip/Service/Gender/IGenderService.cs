using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Gender
{
    public interface IGenderService
    {
        void AddGender(Core.Domain.Gender gender);

        Core.Domain.Gender GetGenderById(int genderId);

        Core.Domain.Gender GetGenderByName(string name);

        IEnumerable<Core.Domain.Gender> GetGenderList();
    }
}
