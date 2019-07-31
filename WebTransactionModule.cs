using System.Diagnostics.CodeAnalysis;
using ADP.TaaS.COD.Controllers;
using ADP.TaaS.COD.Implementation;
using ADP.TaaS.COD.Utility;
using ADP.TLM.Framework.BLL;
using ADP.TLM.Framework.BLL.Profiling;
using Autofac;
using Autofac.Extras.DynamicProxy2;

namespace ADP.TaaS.COD
{
    [ExcludeFromCodeCoverage]
    public class WebTransactionsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProfilingAggregator>().As<IProfilingAggregator>().SingleInstance();

            builder.RegisterType<CodControllerImpl>()
             .As<ICodController>()
             .EnableInterfaceInterceptors()
             .InterceptedBy(
             typeof(ExceptionInterceptor),
             typeof(LoggingInterceptor)).SingleInstance();

            builder.RegisterType<UserControllerImpl>()
                .As<IUserController>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(
                typeof(ExceptionInterceptor),
                typeof(LoggingInterceptor)).SingleInstance();

            builder.RegisterType<VideoControllerImpl>()
                .As<IVideoController>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(
           typeof(ExceptionInterceptor),
           typeof(LoggingInterceptor)).SingleInstance();

            builder.RegisterType<DataImportControllerImpl>()
                .As<IDataImportController>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(
                typeof(ExceptionInterceptor),
                typeof(LoggingInterceptor)).SingleInstance();


            builder.RegisterType<ContentManagerControllerImpl>()
                .As<IContentManagerController>()
                .EnableInterfaceInterceptors()
                .InterceptedBy(
                    typeof(ExceptionInterceptor),
                    typeof(LoggingInterceptor)).SingleInstance();

            //builder.RegisterType<HealthChecksControllerImpl>()
            //    .As<HealthCheckController>()
            //    .EnableInterfaceInterceptors()
            //    .InterceptedBy(
            //    typeof(ExceptionInterceptor),
            //    typeof(LoggingInterceptor))
            //    .SingleInstance();

            builder.RegisterType<GetHeaders>().As<IGetHeaders>();
          //  builder.RegisterType<SetContext>().As<ISetContext>();
            builder.RegisterType<DataImport>().As<IDataImport>();
            builder.RegisterType<RestClient>().As<IRestClient>();
        }
    }
}