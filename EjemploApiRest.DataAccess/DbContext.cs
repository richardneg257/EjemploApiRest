using EjemploRestApi.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EjemploApiRest.DataAccess
{
    public class DbContext<T> : IDbContext<T> where T : class, IEntity
    {
        private readonly ApiDbContext _contexto;
        DbSet<T> _items;

        public DbContext(ApiDbContext contexto)
        {
            _contexto = contexto;
            _items = contexto.Set<T>();
        }

        public void Delete(int id)
        {

        }

        public IList<T> GetAll()
        {
            return _items.ToList();
        }

        public T GetById(int id)
        {
            return _items.FirstOrDefault(x => x.Equals(id));
        }

        public T Save(T entity)
        {
            _items.Add(entity);
            _contexto.SaveChanges();
            return entity;
        }
    }
}
