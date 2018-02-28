using Core;
using Core.Data;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Installation
{
    /// <summary>
    /// Code first installation service
    /// </summary>
    public partial class CodeFirstInstallationService : IInstallationService
    {
        #region Fields

        private readonly IWebHelper _webHelper;
        private readonly IRepository<Core.Domain.Country> _countryRepository;
        private readonly IRepository<Core.Domain.Gender> _genderRepository;
        private readonly IRepository<Core.Domain.Organization> _organizationRepository;
        private readonly IRepository<Core.Domain.Users> _userRepository;
        #endregion

        #region Ctor

        public CodeFirstInstallationService(IWebHelper webHelper, 
                                    IRepository<Core.Domain.Country> countryRepository, 
                                    IRepository<Core.Domain.Gender> genderRepository,
                                    IRepository<Core.Domain.Organization> organizationRepository,
                                    IRepository<Core.Domain.Users> userRepository)
        {
            this._webHelper = webHelper;
            this._countryRepository = countryRepository;
            this._genderRepository = genderRepository;
            this._organizationRepository = organizationRepository;
            this._userRepository = userRepository;
        }

        #endregion

        #region Utilities


        protected virtual void InstallUserLogin()
        {
            var salt = _webHelper.RandomString(_webHelper.RandomStringSize);
            var user = new Core.Domain.Users
            {
                CountryId = _countryRepository.Table.Where(c => c.Name.Equals("United States")).FirstOrDefault().Id,
                FullName = "Test User",
                Email = "email@test.com",
                IsActive = true,
                IsOrganizationAdmin = true,
                Specialty = "ABC",
                TypeOfProfessional = "XYZ",
                Password = _webHelper.ComputeHash("Test1234", salt),
                PasswordSalt = salt,
                PinCode = 1234,
                OrganizationId = this._organizationRepository.Table.Where(o => o.OrganizationName.Equals("TestOrganization")).FirstOrDefault().Id,
                DeviceNumber = "ABC"
            };
            this._userRepository.Insert(user);
        }

        protected virtual void InstallDefaultOrganization()
        {
            var organization = new Core.Domain.Organization
            {
                OrganizationName = "TestOrganization"
            };
            this._organizationRepository.Insert(organization);
        }

        protected virtual void InstallCountries()
        {
            var cUsa = new Core.Domain.Country
            {
                Name = "United States",
                AllowsBilling = true,
                AllowsShipping = true,
                TwoLetterIsoCode = "US",
                ThreeLetterIsoCode = "USA",
                NumericIsoCode = 840,
                SubjectToVat = false,
                DisplayOrder = 1,
                Published = true,
            };
            
            var cCanada = new Core.Domain.Country
            {
                Name = "Canada",
                AllowsBilling = true,
                AllowsShipping = true,
                TwoLetterIsoCode = "CA",
                ThreeLetterIsoCode = "CAN",
                NumericIsoCode = 124,
                SubjectToVat = false,
                DisplayOrder = 100,
                Published = true,
            };            
            var countries = new List<Core.Domain.Country>
            {
                cUsa,
                cCanada,
                //other countries
                new Core.Domain.Country
                {
                    Name = "Argentina",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "AR",
                    ThreeLetterIsoCode = "ARG",
                    NumericIsoCode = 32,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Armenia",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "AM",
                    ThreeLetterIsoCode = "ARM",
                    NumericIsoCode = 51,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Aruba",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "AW",
                    ThreeLetterIsoCode = "ABW",
                    NumericIsoCode = 533,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Australia",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "AU",
                    ThreeLetterIsoCode = "AUS",
                    NumericIsoCode = 36,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Austria",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "AT",
                    ThreeLetterIsoCode = "AUT",
                    NumericIsoCode = 40,
                    SubjectToVat = true,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Azerbaijan",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "AZ",
                    ThreeLetterIsoCode = "AZE",
                    NumericIsoCode = 31,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Bahamas",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "BS",
                    ThreeLetterIsoCode = "BHS",
                    NumericIsoCode = 44,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Bangladesh",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "BD",
                    ThreeLetterIsoCode = "BGD",
                    NumericIsoCode = 50,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Belarus",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "BY",
                    ThreeLetterIsoCode = "BLR",
                    NumericIsoCode = 112,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Belgium",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "BE",
                    ThreeLetterIsoCode = "BEL",
                    NumericIsoCode = 56,
                    SubjectToVat = true,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Belize",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "BZ",
                    ThreeLetterIsoCode = "BLZ",
                    NumericIsoCode = 84,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Bermuda",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "BM",
                    ThreeLetterIsoCode = "BMU",
                    NumericIsoCode = 60,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Bolivia",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "BO",
                    ThreeLetterIsoCode = "BOL",
                    NumericIsoCode = 68,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Bosnia and Herzegowina",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "BA",
                    ThreeLetterIsoCode = "BIH",
                    NumericIsoCode = 70,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Brazil",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "BR",
                    ThreeLetterIsoCode = "BRA",
                    NumericIsoCode = 76,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Bulgaria",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "BG",
                    ThreeLetterIsoCode = "BGR",
                    NumericIsoCode = 100,
                    SubjectToVat = true,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Cayman Islands",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "KY",
                    ThreeLetterIsoCode = "CYM",
                    NumericIsoCode = 136,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Chile",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "CL",
                    ThreeLetterIsoCode = "CHL",
                    NumericIsoCode = 152,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "China",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "CN",
                    ThreeLetterIsoCode = "CHN",
                    NumericIsoCode = 156,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Colombia",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "CO",
                    ThreeLetterIsoCode = "COL",
                    NumericIsoCode = 170,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Costa Rica",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "CR",
                    ThreeLetterIsoCode = "CRI",
                    NumericIsoCode = 188,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Croatia",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "HR",
                    ThreeLetterIsoCode = "HRV",
                    NumericIsoCode = 191,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Cuba",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "CU",
                    ThreeLetterIsoCode = "CUB",
                    NumericIsoCode = 192,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Cyprus",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "CY",
                    ThreeLetterIsoCode = "CYP",
                    NumericIsoCode = 196,
                    SubjectToVat = true,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Czech Republic",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "CZ",
                    ThreeLetterIsoCode = "CZE",
                    NumericIsoCode = 203,
                    SubjectToVat = true,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Denmark",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "DK",
                    ThreeLetterIsoCode = "DNK",
                    NumericIsoCode = 208,
                    SubjectToVat = true,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Dominican Republic",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "DO",
                    ThreeLetterIsoCode = "DOM",
                    NumericIsoCode = 214,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "East Timor",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "TL",
                    ThreeLetterIsoCode = "TLS",
                    NumericIsoCode = 626,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Ecuador",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "EC",
                    ThreeLetterIsoCode = "ECU",
                    NumericIsoCode = 218,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Egypt",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "EG",
                    ThreeLetterIsoCode = "EGY",
                    NumericIsoCode = 818,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Finland",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "FI",
                    ThreeLetterIsoCode = "FIN",
                    NumericIsoCode = 246,
                    SubjectToVat = true,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "France",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "FR",
                    ThreeLetterIsoCode = "FRA",
                    NumericIsoCode = 250,
                    SubjectToVat = true,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Georgia",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "GE",
                    ThreeLetterIsoCode = "GEO",
                    NumericIsoCode = 268,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Germany",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "DE",
                    ThreeLetterIsoCode = "DEU",
                    NumericIsoCode = 276,
                    SubjectToVat = true,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Gibraltar",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "GI",
                    ThreeLetterIsoCode = "GIB",
                    NumericIsoCode = 292,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Greece",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "GR",
                    ThreeLetterIsoCode = "GRC",
                    NumericIsoCode = 300,
                    SubjectToVat = true,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Guatemala",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "GT",
                    ThreeLetterIsoCode = "GTM",
                    NumericIsoCode = 320,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Hong Kong",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "HK",
                    ThreeLetterIsoCode = "HKG",
                    NumericIsoCode = 344,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Hungary",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "HU",
                    ThreeLetterIsoCode = "HUN",
                    NumericIsoCode = 348,
                    SubjectToVat = true,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "India",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "IN",
                    ThreeLetterIsoCode = "IND",
                    NumericIsoCode = 356,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Indonesia",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "ID",
                    ThreeLetterIsoCode = "IDN",
                    NumericIsoCode = 360,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Ireland",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "IE",
                    ThreeLetterIsoCode = "IRL",
                    NumericIsoCode = 372,
                    SubjectToVat = true,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Israel",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "IL",
                    ThreeLetterIsoCode = "ISR",
                    NumericIsoCode = 376,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Italy",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "IT",
                    ThreeLetterIsoCode = "ITA",
                    NumericIsoCode = 380,
                    SubjectToVat = true,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Jamaica",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "JM",
                    ThreeLetterIsoCode = "JAM",
                    NumericIsoCode = 388,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Japan",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "JP",
                    ThreeLetterIsoCode = "JPN",
                    NumericIsoCode = 392,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Jordan",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "JO",
                    ThreeLetterIsoCode = "JOR",
                    NumericIsoCode = 400,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Kazakhstan",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "KZ",
                    ThreeLetterIsoCode = "KAZ",
                    NumericIsoCode = 398,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Korea, Democratic People's Republic of",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "KP",
                    ThreeLetterIsoCode = "PRK",
                    NumericIsoCode = 408,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Kuwait",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "KW",
                    ThreeLetterIsoCode = "KWT",
                    NumericIsoCode = 414,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Malaysia",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "MY",
                    ThreeLetterIsoCode = "MYS",
                    NumericIsoCode = 458,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Mexico",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "MX",
                    ThreeLetterIsoCode = "MEX",
                    NumericIsoCode = 484,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Netherlands",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "NL",
                    ThreeLetterIsoCode = "NLD",
                    NumericIsoCode = 528,
                    SubjectToVat = true,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "New Zealand",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "NZ",
                    ThreeLetterIsoCode = "NZL",
                    NumericIsoCode = 554,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Norway",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "NO",
                    ThreeLetterIsoCode = "NOR",
                    NumericIsoCode = 578,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Pakistan",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "PK",
                    ThreeLetterIsoCode = "PAK",
                    NumericIsoCode = 586,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Palestine",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "PS",
                    ThreeLetterIsoCode = "PSE",
                    NumericIsoCode = 275,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Paraguay",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "PY",
                    ThreeLetterIsoCode = "PRY",
                    NumericIsoCode = 600,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Peru",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "PE",
                    ThreeLetterIsoCode = "PER",
                    NumericIsoCode = 604,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Philippines",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "PH",
                    ThreeLetterIsoCode = "PHL",
                    NumericIsoCode = 608,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Poland",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "PL",
                    ThreeLetterIsoCode = "POL",
                    NumericIsoCode = 616,
                    SubjectToVat = true,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Portugal",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "PT",
                    ThreeLetterIsoCode = "PRT",
                    NumericIsoCode = 620,
                    SubjectToVat = true,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Puerto Rico",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "PR",
                    ThreeLetterIsoCode = "PRI",
                    NumericIsoCode = 630,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Qatar",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "QA",
                    ThreeLetterIsoCode = "QAT",
                    NumericIsoCode = 634,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Romania",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "RO",
                    ThreeLetterIsoCode = "ROM",
                    NumericIsoCode = 642,
                    SubjectToVat = true,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Russian Federation",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "RU",
                    ThreeLetterIsoCode = "RUS",
                    NumericIsoCode = 643,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Saudi Arabia",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "SA",
                    ThreeLetterIsoCode = "SAU",
                    NumericIsoCode = 682,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Singapore",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "SG",
                    ThreeLetterIsoCode = "SGP",
                    NumericIsoCode = 702,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Slovakia (Slovak Republic)",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "SK",
                    ThreeLetterIsoCode = "SVK",
                    NumericIsoCode = 703,
                    SubjectToVat = true,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Slovenia",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "SI",
                    ThreeLetterIsoCode = "SVN",
                    NumericIsoCode = 705,
                    SubjectToVat = true,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "South Africa",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "ZA",
                    ThreeLetterIsoCode = "ZAF",
                    NumericIsoCode = 710,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Spain",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "ES",
                    ThreeLetterIsoCode = "ESP",
                    NumericIsoCode = 724,
                    SubjectToVat = true,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Sweden",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "SE",
                    ThreeLetterIsoCode = "SWE",
                    NumericIsoCode = 752,
                    SubjectToVat = true,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Switzerland",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "CH",
                    ThreeLetterIsoCode = "CHE",
                    NumericIsoCode = 756,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Taiwan",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "TW",
                    ThreeLetterIsoCode = "TWN",
                    NumericIsoCode = 158,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Thailand",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "TH",
                    ThreeLetterIsoCode = "THA",
                    NumericIsoCode = 764,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Turkey",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "TR",
                    ThreeLetterIsoCode = "TUR",
                    NumericIsoCode = 792,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Ukraine",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "UA",
                    ThreeLetterIsoCode = "UKR",
                    NumericIsoCode = 804,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "United Arab Emirates",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "AE",
                    ThreeLetterIsoCode = "ARE",
                    NumericIsoCode = 784,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "United Kingdom",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "GB",
                    ThreeLetterIsoCode = "GBR",
                    NumericIsoCode = 826,
                    SubjectToVat = true,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "United States minor outlying islands",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "UM",
                    ThreeLetterIsoCode = "UMI",
                    NumericIsoCode = 581,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Uruguay",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "UY",
                    ThreeLetterIsoCode = "URY",
                    NumericIsoCode = 858,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Uzbekistan",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "UZ",
                    ThreeLetterIsoCode = "UZB",
                    NumericIsoCode = 860,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Venezuela",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "VE",
                    ThreeLetterIsoCode = "VEN",
                    NumericIsoCode = 862,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Serbia",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "RS",
                    ThreeLetterIsoCode = "SRB",
                    NumericIsoCode = 688,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Afghanistan",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "AF",
                    ThreeLetterIsoCode = "AFG",
                    NumericIsoCode = 4,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Albania",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "AL",
                    ThreeLetterIsoCode = "ALB",
                    NumericIsoCode = 8,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Algeria",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "DZ",
                    ThreeLetterIsoCode = "DZA",
                    NumericIsoCode = 12,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "American Samoa",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "AS",
                    ThreeLetterIsoCode = "ASM",
                    NumericIsoCode = 16,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Andorra",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "AD",
                    ThreeLetterIsoCode = "AND",
                    NumericIsoCode = 20,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Angola",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "AO",
                    ThreeLetterIsoCode = "AGO",
                    NumericIsoCode = 24,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Anguilla",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "AI",
                    ThreeLetterIsoCode = "AIA",
                    NumericIsoCode = 660,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Antarctica",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "AQ",
                    ThreeLetterIsoCode = "ATA",
                    NumericIsoCode = 10,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Antigua and Barbuda",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "AG",
                    ThreeLetterIsoCode = "ATG",
                    NumericIsoCode = 28,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Bahrain",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "BH",
                    ThreeLetterIsoCode = "BHR",
                    NumericIsoCode = 48,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Barbados",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "BB",
                    ThreeLetterIsoCode = "BRB",
                    NumericIsoCode = 52,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Benin",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "BJ",
                    ThreeLetterIsoCode = "BEN",
                    NumericIsoCode = 204,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Bhutan",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "BT",
                    ThreeLetterIsoCode = "BTN",
                    NumericIsoCode = 64,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Botswana",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "BW",
                    ThreeLetterIsoCode = "BWA",
                    NumericIsoCode = 72,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Bouvet Island",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "BV",
                    ThreeLetterIsoCode = "BVT",
                    NumericIsoCode = 74,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "British Indian Ocean Territory",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "IO",
                    ThreeLetterIsoCode = "IOT",
                    NumericIsoCode = 86,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Brunei Darussalam",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "BN",
                    ThreeLetterIsoCode = "BRN",
                    NumericIsoCode = 96,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Burkina Faso",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "BF",
                    ThreeLetterIsoCode = "BFA",
                    NumericIsoCode = 854,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Burundi",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "BI",
                    ThreeLetterIsoCode = "BDI",
                    NumericIsoCode = 108,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Cambodia",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "KH",
                    ThreeLetterIsoCode = "KHM",
                    NumericIsoCode = 116,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Cameroon",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "CM",
                    ThreeLetterIsoCode = "CMR",
                    NumericIsoCode = 120,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Cape Verde",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "CV",
                    ThreeLetterIsoCode = "CPV",
                    NumericIsoCode = 132,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Central African Republic",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "CF",
                    ThreeLetterIsoCode = "CAF",
                    NumericIsoCode = 140,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Chad",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "TD",
                    ThreeLetterIsoCode = "TCD",
                    NumericIsoCode = 148,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Christmas Island",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "CX",
                    ThreeLetterIsoCode = "CXR",
                    NumericIsoCode = 162,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Cocos (Keeling) Islands",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "CC",
                    ThreeLetterIsoCode = "CCK",
                    NumericIsoCode = 166,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Comoros",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "KM",
                    ThreeLetterIsoCode = "COM",
                    NumericIsoCode = 174,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Congo",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "CG",
                    ThreeLetterIsoCode = "COG",
                    NumericIsoCode = 178,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Congo (Democratic Republic of the)",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "CD",
                    ThreeLetterIsoCode = "COD",
                    NumericIsoCode = 180,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Cook Islands",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "CK",
                    ThreeLetterIsoCode = "COK",
                    NumericIsoCode = 184,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Cote D'Ivoire",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "CI",
                    ThreeLetterIsoCode = "CIV",
                    NumericIsoCode = 384,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Djibouti",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "DJ",
                    ThreeLetterIsoCode = "DJI",
                    NumericIsoCode = 262,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Dominica",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "DM",
                    ThreeLetterIsoCode = "DMA",
                    NumericIsoCode = 212,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "El Salvador",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "SV",
                    ThreeLetterIsoCode = "SLV",
                    NumericIsoCode = 222,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Equatorial Guinea",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "GQ",
                    ThreeLetterIsoCode = "GNQ",
                    NumericIsoCode = 226,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Eritrea",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "ER",
                    ThreeLetterIsoCode = "ERI",
                    NumericIsoCode = 232,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Estonia",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "EE",
                    ThreeLetterIsoCode = "EST",
                    NumericIsoCode = 233,
                    SubjectToVat = true,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Ethiopia",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "ET",
                    ThreeLetterIsoCode = "ETH",
                    NumericIsoCode = 231,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Falkland Islands (Malvinas)",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "FK",
                    ThreeLetterIsoCode = "FLK",
                    NumericIsoCode = 238,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Faroe Islands",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "FO",
                    ThreeLetterIsoCode = "FRO",
                    NumericIsoCode = 234,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Fiji",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "FJ",
                    ThreeLetterIsoCode = "FJI",
                    NumericIsoCode = 242,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "French Guiana",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "GF",
                    ThreeLetterIsoCode = "GUF",
                    NumericIsoCode = 254,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "French Polynesia",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "PF",
                    ThreeLetterIsoCode = "PYF",
                    NumericIsoCode = 258,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "French Southern Territories",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "TF",
                    ThreeLetterIsoCode = "ATF",
                    NumericIsoCode = 260,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Gabon",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "GA",
                    ThreeLetterIsoCode = "GAB",
                    NumericIsoCode = 266,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Gambia",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "GM",
                    ThreeLetterIsoCode = "GMB",
                    NumericIsoCode = 270,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Ghana",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "GH",
                    ThreeLetterIsoCode = "GHA",
                    NumericIsoCode = 288,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Greenland",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "GL",
                    ThreeLetterIsoCode = "GRL",
                    NumericIsoCode = 304,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Grenada",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "GD",
                    ThreeLetterIsoCode = "GRD",
                    NumericIsoCode = 308,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Guadeloupe",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "GP",
                    ThreeLetterIsoCode = "GLP",
                    NumericIsoCode = 312,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Guam",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "GU",
                    ThreeLetterIsoCode = "GUM",
                    NumericIsoCode = 316,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Guinea",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "GN",
                    ThreeLetterIsoCode = "GIN",
                    NumericIsoCode = 324,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Guinea-bissau",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "GW",
                    ThreeLetterIsoCode = "GNB",
                    NumericIsoCode = 624,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Guyana",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "GY",
                    ThreeLetterIsoCode = "GUY",
                    NumericIsoCode = 328,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Haiti",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "HT",
                    ThreeLetterIsoCode = "HTI",
                    NumericIsoCode = 332,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Heard and Mc Donald Islands",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "HM",
                    ThreeLetterIsoCode = "HMD",
                    NumericIsoCode = 334,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Honduras",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "HN",
                    ThreeLetterIsoCode = "HND",
                    NumericIsoCode = 340,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Iceland",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "IS",
                    ThreeLetterIsoCode = "ISL",
                    NumericIsoCode = 352,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Iran (Islamic Republic of)",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "IR",
                    ThreeLetterIsoCode = "IRN",
                    NumericIsoCode = 364,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Iraq",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "IQ",
                    ThreeLetterIsoCode = "IRQ",
                    NumericIsoCode = 368,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Kenya",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "KE",
                    ThreeLetterIsoCode = "KEN",
                    NumericIsoCode = 404,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Kiribati",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "KI",
                    ThreeLetterIsoCode = "KIR",
                    NumericIsoCode = 296,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Korea",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "KR",
                    ThreeLetterIsoCode = "KOR",
                    NumericIsoCode = 410,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Kyrgyzstan",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "KG",
                    ThreeLetterIsoCode = "KGZ",
                    NumericIsoCode = 417,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Lao People's Democratic Republic",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "LA",
                    ThreeLetterIsoCode = "LAO",
                    NumericIsoCode = 418,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Latvia",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "LV",
                    ThreeLetterIsoCode = "LVA",
                    NumericIsoCode = 428,
                    SubjectToVat = true,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Lebanon",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "LB",
                    ThreeLetterIsoCode = "LBN",
                    NumericIsoCode = 422,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Lesotho",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "LS",
                    ThreeLetterIsoCode = "LSO",
                    NumericIsoCode = 426,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Liberia",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "LR",
                    ThreeLetterIsoCode = "LBR",
                    NumericIsoCode = 430,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Libyan Arab Jamahiriya",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "LY",
                    ThreeLetterIsoCode = "LBY",
                    NumericIsoCode = 434,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Liechtenstein",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "LI",
                    ThreeLetterIsoCode = "LIE",
                    NumericIsoCode = 438,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Lithuania",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "LT",
                    ThreeLetterIsoCode = "LTU",
                    NumericIsoCode = 440,
                    SubjectToVat = true,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Luxembourg",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "LU",
                    ThreeLetterIsoCode = "LUX",
                    NumericIsoCode = 442,
                    SubjectToVat = true,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Macau",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "MO",
                    ThreeLetterIsoCode = "MAC",
                    NumericIsoCode = 446,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Macedonia",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "MK",
                    ThreeLetterIsoCode = "MKD",
                    NumericIsoCode = 807,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Madagascar",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "MG",
                    ThreeLetterIsoCode = "MDG",
                    NumericIsoCode = 450,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Malawi",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "MW",
                    ThreeLetterIsoCode = "MWI",
                    NumericIsoCode = 454,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Maldives",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "MV",
                    ThreeLetterIsoCode = "MDV",
                    NumericIsoCode = 462,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Mali",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "ML",
                    ThreeLetterIsoCode = "MLI",
                    NumericIsoCode = 466,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Malta",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "MT",
                    ThreeLetterIsoCode = "MLT",
                    NumericIsoCode = 470,
                    SubjectToVat = true,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Marshall Islands",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "MH",
                    ThreeLetterIsoCode = "MHL",
                    NumericIsoCode = 584,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Martinique",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "MQ",
                    ThreeLetterIsoCode = "MTQ",
                    NumericIsoCode = 474,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Mauritania",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "MR",
                    ThreeLetterIsoCode = "MRT",
                    NumericIsoCode = 478,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Mauritius",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "MU",
                    ThreeLetterIsoCode = "MUS",
                    NumericIsoCode = 480,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Mayotte",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "YT",
                    ThreeLetterIsoCode = "MYT",
                    NumericIsoCode = 175,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Micronesia",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "FM",
                    ThreeLetterIsoCode = "FSM",
                    NumericIsoCode = 583,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Moldova",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "MD",
                    ThreeLetterIsoCode = "MDA",
                    NumericIsoCode = 498,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Monaco",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "MC",
                    ThreeLetterIsoCode = "MCO",
                    NumericIsoCode = 492,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Mongolia",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "MN",
                    ThreeLetterIsoCode = "MNG",
                    NumericIsoCode = 496,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Montenegro",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "ME",
                    ThreeLetterIsoCode = "MNE",
                    NumericIsoCode = 499,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Montserrat",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "MS",
                    ThreeLetterIsoCode = "MSR",
                    NumericIsoCode = 500,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Morocco",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "MA",
                    ThreeLetterIsoCode = "MAR",
                    NumericIsoCode = 504,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Mozambique",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "MZ",
                    ThreeLetterIsoCode = "MOZ",
                    NumericIsoCode = 508,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Myanmar",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "MM",
                    ThreeLetterIsoCode = "MMR",
                    NumericIsoCode = 104,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Namibia",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "NA",
                    ThreeLetterIsoCode = "NAM",
                    NumericIsoCode = 516,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Nauru",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "NR",
                    ThreeLetterIsoCode = "NRU",
                    NumericIsoCode = 520,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Nepal",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "NP",
                    ThreeLetterIsoCode = "NPL",
                    NumericIsoCode = 524,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Netherlands Antilles",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "AN",
                    ThreeLetterIsoCode = "ANT",
                    NumericIsoCode = 530,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "New Caledonia",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "NC",
                    ThreeLetterIsoCode = "NCL",
                    NumericIsoCode = 540,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Nicaragua",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "NI",
                    ThreeLetterIsoCode = "NIC",
                    NumericIsoCode = 558,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Niger",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "NE",
                    ThreeLetterIsoCode = "NER",
                    NumericIsoCode = 562,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Nigeria",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "NG",
                    ThreeLetterIsoCode = "NGA",
                    NumericIsoCode = 566,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Niue",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "NU",
                    ThreeLetterIsoCode = "NIU",
                    NumericIsoCode = 570,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Norfolk Island",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "NF",
                    ThreeLetterIsoCode = "NFK",
                    NumericIsoCode = 574,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Northern Mariana Islands",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "MP",
                    ThreeLetterIsoCode = "MNP",
                    NumericIsoCode = 580,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Oman",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "OM",
                    ThreeLetterIsoCode = "OMN",
                    NumericIsoCode = 512,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Palau",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "PW",
                    ThreeLetterIsoCode = "PLW",
                    NumericIsoCode = 585,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Panama",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "PA",
                    ThreeLetterIsoCode = "PAN",
                    NumericIsoCode = 591,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Papua New Guinea",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "PG",
                    ThreeLetterIsoCode = "PNG",
                    NumericIsoCode = 598,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Pitcairn",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "PN",
                    ThreeLetterIsoCode = "PCN",
                    NumericIsoCode = 612,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Reunion",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "RE",
                    ThreeLetterIsoCode = "REU",
                    NumericIsoCode = 638,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Rwanda",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "RW",
                    ThreeLetterIsoCode = "RWA",
                    NumericIsoCode = 646,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Saint Kitts and Nevis",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "KN",
                    ThreeLetterIsoCode = "KNA",
                    NumericIsoCode = 659,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Saint Lucia",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "LC",
                    ThreeLetterIsoCode = "LCA",
                    NumericIsoCode = 662,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Saint Vincent and the Grenadines",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "VC",
                    ThreeLetterIsoCode = "VCT",
                    NumericIsoCode = 670,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Samoa",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "WS",
                    ThreeLetterIsoCode = "WSM",
                    NumericIsoCode = 882,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "San Marino",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "SM",
                    ThreeLetterIsoCode = "SMR",
                    NumericIsoCode = 674,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Sao Tome and Principe",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "ST",
                    ThreeLetterIsoCode = "STP",
                    NumericIsoCode = 678,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Senegal",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "SN",
                    ThreeLetterIsoCode = "SEN",
                    NumericIsoCode = 686,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Seychelles",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "SC",
                    ThreeLetterIsoCode = "SYC",
                    NumericIsoCode = 690,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Sierra Leone",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "SL",
                    ThreeLetterIsoCode = "SLE",
                    NumericIsoCode = 694,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Solomon Islands",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "SB",
                    ThreeLetterIsoCode = "SLB",
                    NumericIsoCode = 90,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Somalia",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "SO",
                    ThreeLetterIsoCode = "SOM",
                    NumericIsoCode = 706,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "South Georgia & South Sandwich Islands",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "GS",
                    ThreeLetterIsoCode = "SGS",
                    NumericIsoCode = 239,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "South Sudan",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "SS",
                    ThreeLetterIsoCode = "SSD",
                    NumericIsoCode = 728,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Sri Lanka",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "LK",
                    ThreeLetterIsoCode = "LKA",
                    NumericIsoCode = 144,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "St. Helena",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "SH",
                    ThreeLetterIsoCode = "SHN",
                    NumericIsoCode = 654,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "St. Pierre and Miquelon",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "PM",
                    ThreeLetterIsoCode = "SPM",
                    NumericIsoCode = 666,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Sudan",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "SD",
                    ThreeLetterIsoCode = "SDN",
                    NumericIsoCode = 736,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Suriname",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "SR",
                    ThreeLetterIsoCode = "SUR",
                    NumericIsoCode = 740,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Svalbard and Jan Mayen Islands",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "SJ",
                    ThreeLetterIsoCode = "SJM",
                    NumericIsoCode = 744,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Swaziland",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "SZ",
                    ThreeLetterIsoCode = "SWZ",
                    NumericIsoCode = 748,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Syrian Arab Republic",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "SY",
                    ThreeLetterIsoCode = "SYR",
                    NumericIsoCode = 760,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Tajikistan",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "TJ",
                    ThreeLetterIsoCode = "TJK",
                    NumericIsoCode = 762,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Tanzania",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "TZ",
                    ThreeLetterIsoCode = "TZA",
                    NumericIsoCode = 834,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Togo",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "TG",
                    ThreeLetterIsoCode = "TGO",
                    NumericIsoCode = 768,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Tokelau",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "TK",
                    ThreeLetterIsoCode = "TKL",
                    NumericIsoCode = 772,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Tonga",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "TO",
                    ThreeLetterIsoCode = "TON",
                    NumericIsoCode = 776,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Trinidad and Tobago",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "TT",
                    ThreeLetterIsoCode = "TTO",
                    NumericIsoCode = 780,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Tunisia",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "TN",
                    ThreeLetterIsoCode = "TUN",
                    NumericIsoCode = 788,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Turkmenistan",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "TM",
                    ThreeLetterIsoCode = "TKM",
                    NumericIsoCode = 795,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Turks and Caicos Islands",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "TC",
                    ThreeLetterIsoCode = "TCA",
                    NumericIsoCode = 796,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Tuvalu",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "TV",
                    ThreeLetterIsoCode = "TUV",
                    NumericIsoCode = 798,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Uganda",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "UG",
                    ThreeLetterIsoCode = "UGA",
                    NumericIsoCode = 800,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Vanuatu",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "VU",
                    ThreeLetterIsoCode = "VUT",
                    NumericIsoCode = 548,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Vatican City State (Holy See)",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "VA",
                    ThreeLetterIsoCode = "VAT",
                    NumericIsoCode = 336,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Viet Nam",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "VN",
                    ThreeLetterIsoCode = "VNM",
                    NumericIsoCode = 704,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Virgin Islands (British)",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "VG",
                    ThreeLetterIsoCode = "VGB",
                    NumericIsoCode = 92,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Virgin Islands (U.S.)",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "VI",
                    ThreeLetterIsoCode = "VIR",
                    NumericIsoCode = 850,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Wallis and Futuna Islands",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "WF",
                    ThreeLetterIsoCode = "WLF",
                    NumericIsoCode = 876,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Western Sahara",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "EH",
                    ThreeLetterIsoCode = "ESH",
                    NumericIsoCode = 732,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Yemen",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "YE",
                    ThreeLetterIsoCode = "YEM",
                    NumericIsoCode = 887,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Zambia",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "ZM",
                    ThreeLetterIsoCode = "ZMB",
                    NumericIsoCode = 894,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
                new Core.Domain.Country
                {
                    Name = "Zimbabwe",
                    AllowsBilling = true,
                    AllowsShipping = true,
                    TwoLetterIsoCode = "ZW",
                    ThreeLetterIsoCode = "ZWE",
                    NumericIsoCode = 716,
                    SubjectToVat = false,
                    DisplayOrder = 100,
                    Published = true
                },
            };
            _countryRepository.Insert(countries);
        }

        protected virtual void InstallGender()
        {
            var genderList = new List<Core.Domain.Gender> {
                new Core.Domain.Gender
                {
                    Name = "Male",
                    CreatedDate = DateTime.Now
                },
                new Core.Domain.Gender
                {
                    Name = "Female",
                    CreatedDate = DateTime.Now
                }
            };
            this._genderRepository.Insert(genderList);
        }
        #endregion

        #region Methods

        /// <summary>
        /// Install data
        /// </summary>
        /// <param name="defaultUserEmail">Default user email</param>
        /// <param name="defaultUserPassword">Default user password</param>
        /// <param name="installSampleData">A value indicating whether to install sample data</param>
        public virtual void InstallData(string defaultUserEmail,
            string defaultUserPassword, bool installSampleData = true)
        {
            if (!this.HasData())
            {
                InstallGender();
                InstallCountries();
                InstallDefaultOrganization();
                InstallUserLogin();
            }
        }

        public virtual bool HasData()
        {
            return this._genderRepository.Table.Any();
        }
        #endregion
    }
}
