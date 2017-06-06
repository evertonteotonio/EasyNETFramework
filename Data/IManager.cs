using System.Collections.Generic;
using ENF.Entity.NotMapped;

namespace ENF.Data
{
    /// <summary>
    /// Interface IManager
    /// </summary>
    /// <typeparam name="T">Entity Type</typeparam>
    public interface IManager<T>
    {
        T Add(T item);
        T Update(T item);
        T Delete(T item);
        T FindById(int id);
        List<T> FindAll(Search search);
    }
}
