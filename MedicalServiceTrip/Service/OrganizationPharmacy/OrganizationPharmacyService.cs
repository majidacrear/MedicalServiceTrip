using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Data;
using Core.Domain;

namespace Service.OrganizationPharmacy
{
    public class OrganizationPharmacyService : IOrganizationPharmacyService
    {
        #region Fields

        private readonly IRepository<Core.Domain.OrganizationPharmacy> _organizationPharmacy;
        #endregion

        #region Cors
        public OrganizationPharmacyService(IRepository<Core.Domain.OrganizationPharmacy> organizationPharmacy)
        {
            _organizationPharmacy = organizationPharmacy;
        }

        #endregion

        public Core.Domain.OrganizationPharmacy AddOrganizationPharmacy(Core.Domain.OrganizationPharmacy organizationPharmacy)
        {
            if (organizationPharmacy == null)
                throw new ArgumentNullException(nameof(organizationPharmacy));
            if(string.IsNullOrEmpty(organizationPharmacy.MedicineName))
                throw new Exception("MedicineName cannot be empty");
            var pharmacy = this._organizationPharmacy.Table.Where(op => (op.MedicineName.ToLower().Equals(organizationPharmacy.MedicineName.ToLower()) || op.Id == organizationPharmacy.Id) && op.IsDeleted == false).FirstOrDefault();
            if(pharmacy== null)
            {
                this._organizationPharmacy.Insert(organizationPharmacy);
                return organizationPharmacy;
            }
            else
            {
                pharmacy.AvailableStock = organizationPharmacy.AvailableStock;
                _organizationPharmacy.Update(pharmacy);
            }

            return pharmacy;
        }

        public IEnumerable<Core.Domain.OrganizationPharmacy> GetOrganizationPharmacy(int organizationId)
        {
            if (organizationId <= 0)
                throw new Exception("OrganizationId cannot be less than or equal to zero");
            return _organizationPharmacy.Table.Where(op => op.OrganizationId == organizationId && op.IsDeleted == false).ToList();
        }
    }
}
