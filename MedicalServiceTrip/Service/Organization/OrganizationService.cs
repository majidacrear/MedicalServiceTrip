using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Data;
using Core.Domain;

namespace Service.Organization
{
    public class OrganizationService : IOrganizationService
    {
        #region Fields

        private readonly IRepository<Core.Domain.Organization> _organizationRepository;
        
        #endregion
        
        #region Cors
        public OrganizationService(IRepository<Core.Domain.Organization> organizationRepository)
        {
            this._organizationRepository = organizationRepository;
        }
        #endregion
        public bool CheckOrganizationExist(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("organizationname");
            return this._organizationRepository.Table.Any(o => name.Equals(o.OrganizationName) && o.IsDeleted == false);
        }

        public Core.Domain.Organization GetOrganizationById(int id)
        {
            if (id <= 0)
                throw new ArgumentNullException("organizationid");
            return this._organizationRepository.Table.FirstOrDefault(o => o.Id == id && o.IsDeleted == false);
        }

        public Core.Domain.Organization GetOrganizationByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("organizationname");
            return this._organizationRepository.Table.FirstOrDefault(o => name.Equals(o.OrganizationName) && o.IsDeleted == false);
        }

        public int RegisterOrganization(Core.Domain.Organization organization)
        {
            if (organization == null)
                throw new ArgumentNullException(nameof(organization));
            var organizationObj = GetOrganizationByName(organization.OrganizationName);
            if (organizationObj == null)
            {
                organization.CreatedDate = DateTime.Now;
                this._organizationRepository.Insert(organization);
                organizationObj = organization;
            }
            return organizationObj.Id;
        }

        public IEnumerable<Core.Domain.Organization> GetAllOrganization()
        {
            return this._organizationRepository.Table.Where(o => o.IsDeleted == false).ToList();
        }
    }
}
