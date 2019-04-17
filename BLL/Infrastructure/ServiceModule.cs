using DAL.Interfaces;
using DAL.UnitOfWorks;
using Ninject.Modules;

namespace BLL.Infrastructure
{
    /// <summary>
    /// Module for dependency injection.
    /// </summary>
    public class ServiceModule : NinjectModule
    {
        private readonly string _connectionString;

        public ServiceModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(_connectionString);
        }
    }
}
