using System.Data.Entity.ModelConfiguration;

namespace Data.Mapping
{
    public partial class MSTEntityTypeConfiguration<T> : EntityTypeConfiguration<T> where T : class
    {
        /// <summary>
        /// Ctor
        /// </summary>
        protected MSTEntityTypeConfiguration()
        {
            PostInitialize();
        }

        /// <summary>
        /// Developers can override this method in custom partial classes
        /// in order to add some custom initialization code to constructors
        /// </summary>
        protected virtual void PostInitialize()
        {

        }
    }
}
