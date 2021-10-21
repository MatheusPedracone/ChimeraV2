using System;
using System.Collections.Generic;
using Chimera_v2.Models.Base;

namespace Chimera_v2.Repository.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);
        T FindById(Guid id);
        List<T> FindAll();
        T Update(T item);
        void Delete(Guid id);
        bool Exists(Guid id);
    }
}