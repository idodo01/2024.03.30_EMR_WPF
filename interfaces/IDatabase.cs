using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMR.Models;

namespace EMR.Interfaces
{
    public interface IDatabase<T>
    {
        Task<List<T>>? GetAsync();

        T? GetDetail(int? id);

        void Create(T entity);

        void Update(T entity);

        void Delete(int? id);
    }
}
