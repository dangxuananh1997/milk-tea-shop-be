namespace Core.AppService.Pagination
{
    using System.Collections.Generic;

    public interface IPagination
    {
        List<T> ToPagedList<T>(int pageIndex, int pageSize, IEnumerable<T> list) where T : class;
    }
}
