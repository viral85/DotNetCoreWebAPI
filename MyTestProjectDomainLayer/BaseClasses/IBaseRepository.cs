using MyTestProjectDomainLayer.BaseClasses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyTestProjectDomainLayer.BaseClasses  
{
	public interface IBaseRepository<TEntity>
    {
        Task<ApiResponseWrapper> GetEntityList(TEntity entityObject);
        Task<ApiResponseWrapper> AddEntity(TEntity entityObject);
        Task<ApiResponseWrapper> DeleteEntity(TEntity entityObject);
        Task<ApiResponseWrapper> UpdateEntity(TEntity entityObject);
    }
}