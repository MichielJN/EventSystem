using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventSystem.Abstractions;

namespace EventSystem.Data
{
    public interface IBaseRepository<T> :IDisposable where T : TableData, new()
    {
        void SaveEntityWithChildren(T entity, bool recursive = false);
        T? GetEntityWithChildren(int id);
        List<T>? GetEntitiesWithChildren();
        void DeleteEntityWithChildren(T entity);
        void SaveEntity(T entity);
        void DeleteEntity(T entity);
        List<T>? GetEntities();
        T? GetEntity(int id);

    }
}
