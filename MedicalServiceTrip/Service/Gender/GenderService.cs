using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Data;
using Core.Domain;

namespace Service.Gender
{
    public partial class GenderService : IGenderService
    {
        #region Fields

        private readonly IRepository<Core.Domain.Gender> _genderRepository;

        #endregion

        public GenderService(IRepository<Core.Domain.Gender> genderRepository)
        {
            this._genderRepository = genderRepository;
        }

        public void AddGender(Core.Domain.Gender gender)
        {
            if (gender == null)
                throw new ArgumentNullException(nameof(gender));
            this._genderRepository.Insert(gender);
        }

        public Core.Domain.Gender GetGenderById(int genderId)
        {
            if (genderId <= 0)
                throw new Exception("Gender Id cannot be less than or equal to zero.");
            return this._genderRepository.GetById(genderId);
        }

        public Core.Domain.Gender GetGenderByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            return this._genderRepository.Table.FirstOrDefault(g=>g.Name.Equals(name));
        }

        public IEnumerable<Core.Domain.Gender> GetGenderList()
        {
            return _genderRepository.Table.ToList();
        }
    }
}
