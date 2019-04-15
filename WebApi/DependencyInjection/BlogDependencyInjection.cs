using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.Services;
using Ninject;
using Ninject.Modules;

namespace WebApi.DependencyInjection
{
    public class BlogDependencyInjection
    {
        public static IKernel GetKernel()
        {
            INinjectModule module = new ServiceModule("DefaultConnection");
            var kernel = new StandardKernel(module);
            kernel.Bind<IAccountService>().To<AccountService>();
            return kernel;
        }
    }
}