
namespace Service.Business.Pagination
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.AppService.Pagination;

    public class PaginationService : IPagination
    {
        public List<T> ToPagedList<T>(int pageIndex, int pageSize, IEnumerable<T> list) where T : class
        {
            int totalpage = (int)Math.Ceiling((double)list.Count() / pageSize);
            return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
