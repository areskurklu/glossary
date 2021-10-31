using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlossaryWeb.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(string url, int Id, string token);
        Task<IEnumerable<T>> GetAll(string url, string token);
        Task<bool> Create(string url, T objToCreate, string token);
        Task<bool> Update(string url, T objToUpdate, string token);
        Task<bool> Delete(string url, int Id, string token);
    }
}
