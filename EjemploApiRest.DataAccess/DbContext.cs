using EjemploRestApi.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EjemploApiRest.DataAccess
{
    public class DbContext<T> : IDbContext<T> where T : IEntity
    {
        IList<T> _data;

        public DbContext()
        {
            _data = new List<T>();
        }

        public void Delete(int id)
        {
            var e = _data.FirstOrDefault(x => x.Id.Equals(id));
            if (e != null)
                _data.Remove(e);
        }

        public IList<T> GetAll()
        {
            return _data;
        }

        public T GetById(int id)
        {
            return _data.FirstOrDefault(x => x.Id.Equals(id));
        }

        public T Save(T entity)
        {
            if (entity.Id.Equals(0))
                _data.Add(entity);

            return entity;
        }
    }
}
