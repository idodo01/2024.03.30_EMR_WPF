using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMR.interfaces
{
    public interface IDatabase<T>
    {
        List<T>? Get();

        T? GetDetail(int? id);

        void Create(T entity);

        void Update(T entity);

        void Delete(int? id);
    }
}
