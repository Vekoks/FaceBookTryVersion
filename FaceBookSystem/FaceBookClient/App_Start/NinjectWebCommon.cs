[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(FaceBookClient.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(FaceBookClient.App_Start.NinjectWebCommon), "Stop")]

namespace FaceBookClient.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using FaceBook.Data;
    using FaceBook.Data.Repository;
    using FaceBook.Services;
    using FaceBook.Services.Contracts;
    using Models;
    using Models.ModelsForLiveInfo;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

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
            kernel.Bind<IFaceBookDbContext>().To<FaceBookDbContext>().InRequestScope();
            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));

            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IUserDetailService>().To<UserDetailService>();
            kernel.Bind<IAllPostInfo>().To<AllPostInf>();
            kernel.Bind<ILikeOnPost>().To<LikeOnPost>();
            kernel.Bind<IAskFriendInfo>().To<AskFriendInfo>();
            kernel.Bind<IUsersInfo>().To<UsersInfo>();
            kernel.Bind<INoSeenMessage>().To<NoSeenMessage>();
            kernel.Bind<IMessageService>().To<MessageService>();
            kernel.Bind<IPostService>().To<PostService>();
            kernel.Bind<ICommentOnThePost>().To<CommentOnThePost>();
            
        }        
    }
}
