using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CRM_User.Domain.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> IsAny(Guid id);
        Task<List<T>> GetAll();
        Task<T?> GetById(Guid id);
        Task<T?> GetByEmail(string email);
        Task Add(T entity);
        void Update(T entity);
        void Delete(T id);
    }
}
