using MyTestProjectDomainLayer.BaseClasses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyTestProjectServiceLayer.BaseClasses
{
	public class BaseService<TEntity> : IBaseService<TEntity>
    {
        private readonly IBaseRepository<TEntity> _service;
        public BaseService(IBaseRepository<TEntity> service)
        {
            _service = service;
        }
        public async Task<ApiResponseWrapper> GetEntityList(TEntity entityObject)
        {
            return await _service.GetEntityList(entityObject);
        }
        public async Task<ApiResponseWrapper> AddEntity(TEntity entityObject)
        {
            return await _service.AddEntity(entityObject);
        }
        public async Task<ApiResponseWrapper> DeleteEntity(TEntity entityObject)
        {
            return await _service.DeleteEntity(entityObject);
        }
        public async Task<ApiResponseWrapper> UpdateEntity(TEntity entityObject)
        {
            return await _service.UpdateEntity(entityObject);
        }
    }
}
