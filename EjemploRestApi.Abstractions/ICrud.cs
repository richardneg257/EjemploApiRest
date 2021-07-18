using System;
using System.Collections.Generic;

namespace EjemploRestApi.Abstractions
{
    public interface ICrud<T>
    {
        T Save(T entity);
        IList<T> GetAll();
        T GetById(int id);
        void Delete(int id);
    }
}
