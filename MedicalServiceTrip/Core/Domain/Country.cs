using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    /// <summary>
    /// Represents a country
    /// </summary>
    public partial class Country : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether billing is allowed to this country
        /// </summary>
        [JsonIgnore]
        public bool AllowsBilling { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether shipping is allowed to this country
        /// </summary>
        [JsonIgnore]
        public bool AllowsShipping { get; set; }

        /// <summary>
        /// Gets or sets the two letter ISO code
        /// </summary>
        [JsonIgnore]
        public string TwoLetterIsoCode { get; set; }

        /// <summary>
        /// Gets or sets the three letter ISO code
        /// </summary>
        public string ThreeLetterIsoCode { get; set; }

        /// <summary>
        /// Gets or sets the numeric ISO code
        /// </summary>
        [JsonIgnore]
        public int NumericIsoCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether customers in this country must be charged EU VAT
        /// </summary>
        [JsonIgnore]
        public bool SubjectToVat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is published
        /// </summary>
        [JsonIgnore]
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        [JsonIgnore]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is limited/restricted to certain stores
        /// </summary>
        [JsonIgnore]
        public bool LimitedToStores { get; set; }

        
    }
}
