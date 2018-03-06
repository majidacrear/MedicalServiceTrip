﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public partial class Users : BaseEntity
    {
        public string FullName { get; set; }

        public int CountryId { get; set; }

        public string Email { get; set; }

        public string TypeOfProfessional { get; set; }

        public string Specialty { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        [JsonIgnore]
        public string PasswordSalt { get; set; }

        public int PinCode { get; set; }

        public bool IsActive { get; set; }

        public bool IsOrganizationAdmin { get; set; }

        public int? OrganizationId { get; set; }

        [JsonIgnore]
        public string DeviceNumber { get; set; }

        [JsonIgnore]
        /// <summary>
        /// Code Shared by other users
        /// </summary>
        public string RefferalCode { get; set; }

        [JsonIgnore]
        /// <summary>
        /// Code generated by system to be share with new user
        /// </summary>
        public string MyCode { get; set; }

        public Organization Organization { get; set; }

        [JsonIgnore]
        public Country Country { get; set; }
    }
}
