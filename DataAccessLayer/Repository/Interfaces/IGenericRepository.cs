using DataAccessLayer.Models.DepartmentModel;
using DataAccessLayer.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        TEntity? GetById(int id);

        IEnumerable<TEntity> GetAll(bool isTracking = false);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity,bool>> expression);
    }
}
