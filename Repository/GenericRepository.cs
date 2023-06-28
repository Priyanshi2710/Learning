using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationContext context;
        internal DbSet<T> dbSet;

        public GenericRepository(ApplicationContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();

        }
        public virtual async Task<IEnumerable<T>> All()
        {
            return await dbSet.ToListAsync();


        }

        public virtual async Task<T> GetById(int id)
        {
            var data = await dbSet.FindAsync(id);
            return data;
        }

        public virtual async Task<bool> Add(T entity)
        {
            await dbSet.AddAsync(entity);
            context.SaveChanges();

            return true;

        }

        public virtual async Task<bool> Delete(T entity)
        {
            dbSet.Remove(entity);
            context.SaveChanges();
            return true;
           
        }

        public virtual async Task<bool> Update(T entity)
        {
            dbSet.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;

        }
    }

}



