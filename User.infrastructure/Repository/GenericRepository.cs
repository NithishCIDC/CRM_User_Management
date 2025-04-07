using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CRM_User.Domain.Interface;
using CRM_User.infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM_User.infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbcontext;
        public GenericRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task Add(T entity)
        {
            await _dbcontext.Set<T>().AddAsync(entity);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = await _dbcontext.Set<T>().FindAsync(id);
            if(entity != null)
            {
                _dbcontext.Set<T>().Remove(entity);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbcontext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetById(Guid id)
        {
            var response = await _dbcontext.Set<T>().FindAsync(id);
            return response;
        }
        public async Task<T?> GetByEmail(string email)
        {
            var response = await _dbcontext.Set<T>().FirstOrDefaultAsync(e => EF.Property<string>(e, "Email") == email);
            return response;
        }

        public async Task<bool> Update(T entity)
        {
            var user = await _dbcontext.Set<T>().FindAsync(typeof(T).GetProperty("Id")!.GetValue(entity));
            if (user != null)
            {
                _dbcontext.Set<T>().Update(entity);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
