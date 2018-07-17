namespace Core.AppService.Pagination
{
    using Core.ObjectModel.Pagination;
    using System.Collections.Generic;

    public interface IPagination
    {
        Pager<T> ToPagedList<T>(int pageIndex, int pageSize, IEnumerable<T> list) where T : class;
    }
}
