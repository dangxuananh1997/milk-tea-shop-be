namespace Service.Business.Pagination
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.AppService.Pagination;
    using Core.ObjectModel.Pagination;

    public class PaginationService : IPagination
    {
        public Pager<T> ToPagedList<T>(int pageIndex, int pageSize, IEnumerable<T> list) where T : class
        {
            //int totalpage = (int)Math.Ceiling((double)list.Count() / pageSize);

            Pager<T> result = new Pager<T>()
            {
                Total = list.Count(),
                Data = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList()
            };
            return result;
        }
    }
}
