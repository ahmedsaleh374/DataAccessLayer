using DataAccessLayer.Data.Context;
using DataAccessLayer.Models.Shared;
using DataAccessLayer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.Classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(TEntity entity) => _context.Set<TEntity>().Add(entity);

        public void Delete(TEntity entity) => _context.Set<TEntity>().Remove(entity);

        public IEnumerable<TEntity> GetAll(bool isTracking = false)
        {
            if (isTracking)
                return _context.Set<TEntity>().ToList(); //.Where(x => x.IsDeleted == false).ToList();
            else
                return _context.Set<TEntity>().AsNoTracking().ToList(); //.Where(x => x.IsDeleted == false).AsNoTracking().ToList();
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().Where(expression).AsNoTracking().ToList();
        }

        public TEntity? GetById(int id) => _context.Set<TEntity>().Find(id);//.Where(x =>x.IsDeleted==false).FirstOrDefault(x=>x.Id == id);    


        public void Update(TEntity entity) => _context.Set<TEntity>().Update(entity);
    }
}
