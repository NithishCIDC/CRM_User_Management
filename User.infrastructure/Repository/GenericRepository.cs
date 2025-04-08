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
    public class GenericRepository<T>(ApplicationDbContext dbcontext) : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbcontext = dbcontext;

        public async Task Add(T entity)
        {
            await _dbcontext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbcontext.Set<T>().Remove(entity);
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbcontext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetById(Guid id)
        {
            return await _dbcontext.Set<T>().FindAsync(id);
        }
        public async Task<T?> GetByEmail(string email)
        {
            return await _dbcontext.Set<T>().FirstOrDefaultAsync(e => EF.Property<string>(e, "Email") == email);
        }

        public void Update(T entity)
        {
            _dbcontext.Set<T>().Update(entity);
        }
    }
}
