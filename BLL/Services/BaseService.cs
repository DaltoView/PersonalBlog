using AutoMapper;
using DAL.Interfaces;
using System;

namespace BLL.Services
{
    /// <summary>
    /// Represents a service with basic automapper and IDisposable implementation.
    /// </summary>
    /// <typeparam name="TBllEntity"></typeparam>
    /// <typeparam name="TDalEntity"></typeparam>
    public abstract class BaseService<TBllEntity, TDalEntity> : IDisposable where TBllEntity : class where TDalEntity : class
    {
        protected IUnitOfWork UnitOfWork { get; private set; }
        protected IMapper mapper;

        public BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            mapper = AutomapperConfigs.AutomapperConfigs.GetBasicMapper<TBllEntity, TDalEntity>();
        }

        private bool _isDisposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    UnitOfWork.Dispose();
                }

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
