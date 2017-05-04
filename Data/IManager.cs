using System.Collections.Generic;

namespace Data
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
        List<T> FindAll(int page = 1,
            int pageSize = 10,
            string where = "",
            string orderBy = "");
    }
}
