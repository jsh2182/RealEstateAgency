[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(RealEstateAgency.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(RealEstateAgency.App_Start.NinjectWebCommon), "Stop")]

namespace RealEstateAgency.App_Start
{
    using System;
    using System.Web;
    using RealEstateAgency.EFDataService;
    using RealEstateAgency.DataService;


    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static IKernel Kernel
        {
            get
            {
                return bootstrapper.Kernel;
            }
        }

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IAttachmentDataService>().To<EFAttachmentDataService>();
            kernel.Bind<ICallReferDataService>().To<EFCallReferDataService>();
            kernel.Bind<ICallHistoryDataService>().To<EFCallHistoryDataService>();
            kernel.Bind<ICityDataService>().To<EFCityDataService>();
            kernel.Bind<IEstateFileDataService>().To<EFEstateFileDataService>();
            kernel.Bind<IPersonEventDataService>().To<EFEventDataService>();
            kernel.Bind<IFileGroupDataService>().To<EFFileGroupDataService>();
            kernel.Bind<IFileGroupRelationDataService>().To<EFFileGroupRelationDataService>();
            kernel.Bind<IFileReferDataService>().To<EFFileReferDataService>();
            kernel.Bind<IPageListDataService>().To<EFPageListDataService>();
            kernel.Bind<IPageListPermissionDataService>().To<EFPageListPermissionDataService>();
            kernel.Bind<IPersonDataService>().To<EFPersonDataService>();
            kernel.Bind<IPersonGroupDataService>().To<EFPersonGroupDataService>();
            kernel.Bind<IPersonGroupRelationDataService>().To<EFPersonGroupRelationDataService>();
            kernel.Bind<IPersonOtherInfoDataService>().To<EFPersonOtherInfoDataService>();
            kernel.Bind<IProvinceDataService>().To<EFProvinceDataService>();
            kernel.Bind<IZoneDataService>().To<EFZoneDataService>();
            kernel.Bind<IAuthTokenDataService>().To<EFAuthTokenDataService>();
            kernel.Bind<IUserDataService>().To<EFUserDataService>();
            kernel.Bind<IFileRequestDataService>().To<EFFileRequestDataService>();
            kernel.Bind<IEventDetailDataService>().To<EFEventDetailDataService>();
            kernel.Bind<IFollowUpEventsDataService>().To<EFFollowUpEventsDataService>();
        }
    }
}