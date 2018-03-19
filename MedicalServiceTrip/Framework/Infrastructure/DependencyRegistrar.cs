using Autofac;
using Core;
using Core.Configuration;
using Core.Data;
using Core.Infrastructure;
using Core.Infrastructure.DependencyManagement;
using Data;
using Framework.MVC.Routing;
using Service.CheifComplain;
using Service.Country;
using Service.Email;
using Service.Gender;
using Service.Installation;
using Service.Organization;
using Service.OrganizationPharmacy;
using Service.Patient;
using Service.Storage;
using Service.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Infrastructure
{
    /// <summary>
    /// Dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="config">Config</param>
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, MSTConfig config)
        {
            //web helper
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();

            //data layer
            var dataSettingsManager = new DataSettingsManager();
            var dataProviderSettings = dataSettingsManager.LoadSettings();
            builder.Register(c => dataSettingsManager.LoadSettings()).As<DataSettings>();
            builder.Register(x => new EfDataProviderManager(x.Resolve<DataSettings>())).As<BaseDataProviderManager>().InstancePerDependency();

            builder.Register(x => x.Resolve<BaseDataProviderManager>().LoadDataProvider()).As<IDataProvider>().InstancePerDependency();

            if (dataProviderSettings != null && dataProviderSettings.IsValid())
            {
                var efDataProviderManager = new EfDataProviderManager(dataSettingsManager.LoadSettings());
                var dataProvider = efDataProviderManager.LoadDataProvider();
                dataProvider.InitConnectionFactory();

                builder.Register<IDbContext>(c => new MstDbContext(dataProviderSettings.DataConnectionString)).InstancePerLifetimeScope();
            }
            else
                builder.Register<IDbContext>(c => new MstDbContext(dataSettingsManager.LoadSettings().DataConnectionString)).InstancePerLifetimeScope();



            //repositories
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            builder.RegisterType<RoutePublisher>().As<IRoutePublisher>().SingleInstance();
            //services
            builder.RegisterType<GenderService>().As<IGenderService>().InstancePerLifetimeScope();
            builder.RegisterType<CountryService>().As<ICountryService>().InstancePerLifetimeScope();
            builder.RegisterType<OrganizationService>().As<IOrganizationService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<EmailService>().As<IEmailService>().InstancePerLifetimeScope();
            builder.RegisterType<PatientService>().As<IPatientService>().InstancePerLifetimeScope();
            builder.RegisterType<PatientVisitService>().As<IPatientVisitService>().InstancePerLifetimeScope();
            builder.RegisterType<OrganizationPharmacyService>().As<IOrganizationPharmacyService>().InstancePerLifetimeScope();
            builder.RegisterType<CheifComplainService>().As<ICheifComplainService>().InstancePerLifetimeScope();
            builder.RegisterType<AzureStorage>().As<IStorage>().InstancePerLifetimeScope();
            //installation service

            builder.RegisterType<CodeFirstInstallationService>().As<IInstallationService>().InstancePerLifetimeScope();
            
        }

        /// <summary>
        /// Gets order of this dependency registrar implementation
        /// </summary>
        public int Order
        {
            get { return 0; }
        }
    }
}
