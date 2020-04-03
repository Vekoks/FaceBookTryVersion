using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetLife.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IMeetLifeDbContext _dbContext;

        private readonly DbSet<T> Dbset;

        public Repository(IMeetLifeDbContext dbContext)
        {
            _dbContext = dbContext;
            this.Dbset = _dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            Dbset.Add(entity);
        }

        public IEnumerable<T> All()
        {
            return Dbset.AsEnumerable();
        }

        public void Delete(object id)
        {
            var entity = this.GetById(id);

            if (entity != null)
            {
                this.Delete(entity);
            }
        }

        public void Delete(T entity)
        {
            var entry = this._dbContext.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.Dbset.Attach(entity);
                this.Dbset.Remove(entity);
            }
        }

        public T GetById(object id)
        {
            return Dbset.Find(id);
        }

        public void Update(T entity)
        {
            var entry = this._dbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.Dbset.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public int SaveChanges()
        {
            return this._dbContext.SaveChanges();
        }
    }
}
