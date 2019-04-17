using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.Services;
using Ninject;
using Ninject.Modules;

namespace WebApi.DependencyInjection
{
    /// <summary>
    /// Provides a dependency injection for OAuth provider.
    /// </summary>
    public class BlogDependencyInjection
    {
        /// <summary>
        /// Returns a module for getting interface instances.
        /// </summary>
        /// <returns></returns>
        public static IKernel GetKernel()
        {
            INinjectModule module = new ServiceModule("DefaultConnection");
            var kernel = new StandardKernel(module);
            kernel.Bind<IAccountService>().To<AccountService>();
            return kernel;
        }
    }
}