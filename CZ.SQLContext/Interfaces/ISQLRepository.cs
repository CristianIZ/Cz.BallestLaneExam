using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZ.SQLContext.Interfaces
{
    public interface ISQLRepository<T> where T : class
    {
        Task<T> GetByKey(string key);
        Task Add(T value);
        T MapEntity(DataRow dataRow);
        ICollection<T> ReadEntity(DataTable dataTable);
    }
}
