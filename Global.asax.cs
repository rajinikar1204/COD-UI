using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using ADP.TaaS.COD.BLL;
using Autofac;
using Autofac.Configuration;
using Autofac.Integration.Wcf;
using Autofac.Integration.Web;
using ADP.TLM.Framework;
using ADP.TLM.Framework.Config;
using ADP.TLM.Framework.DAL.DataAccess;
using ADP.TLM.Framework.DAL.DataAccess.Context;
using Autofac.Integration.WebApi;

namespace ADP.TaaS.COD
{
    [ExcludeFromCodeCoverage]
    public class WebApiApplication : System.Web.HttpApplication, IContainerProviderAccessor
    {
        private static IContainerProvider _containerProvider;

        public IContainerProvider ContainerProvider
        {
            get { return _containerProvider; }
        }

        static WebApiApplication()
        {
            log4net.ThreadContext.Properties["ApplicationName"] = Assembly.GetExecutingAssembly().GetName().Name;
        }
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new WebTransactionsModule());
            builder.RegisterModule(new BusinessTransactionModule());
            builder.RegisterModule(new DAL.DataAccessModule());
            builder.RegisterModule(new FrameworkModule());
            builder.RegisterModule(new ConfigurationSettingsReader());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterAssemblyTypes( // register Web API Controllers
                    Assembly.GetExecutingAssembly())
                .Where(t =>
                    !t.IsAbstract && typeof(ApiController).IsAssignableFrom(t))
                .InstancePerMatchingLifetimeScope("AutofacWebRequest");

            var container = builder.Build();

            AutofacHostFactory.Container = container;
            GlobalIocContext.Instance().Delegate = container;

            _containerProvider = new ContainerProvider(container);
            container.Resolve<TLMConfigMgr>();
            TLMConfigMgr.ResetAppSettings();
            ConnectionManager.Factory = container.Resolve<IConnectionFactory>();

            _containerProvider = new ContainerProvider(container);

            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

           // DataAccessContext.Instance().SetClientContext("-1", "TLM");
           //DataAccessContext.Instance().SetCachingContext();
        }
    }
}
