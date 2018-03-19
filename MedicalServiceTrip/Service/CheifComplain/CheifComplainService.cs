using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Data;
using Core.Domain;

namespace Service.CheifComplain
{
    public class CheifComplainService : ICheifComplainService
    {
        #region Fields
        private readonly IRepository<Core.Domain.CheifComplain> _cheifComplainRepository;
        #endregion


        #region Cors

        public CheifComplainService(IRepository<Core.Domain.CheifComplain> cheifComplainRepository)
        {
            _cheifComplainRepository = cheifComplainRepository;
        }

        #endregion

        public Core.Domain.CheifComplain AddCheifComplain(Core.Domain.CheifComplain cheifComplain)
        {
            if (cheifComplain == null)
                throw new ArgumentNullException(nameof(cheifComplain));
            _cheifComplainRepository.Insert(cheifComplain);
            return cheifComplain;
        }

        public IEnumerable<Core.Domain.CheifComplain> GetCheifComplainList(int organizationId)
        {
            return _cheifComplainRepository.Table.Where(cc => cc.OrganizationId == organizationId).ToList();
        }
    }
}
