using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ObjectModel.Pagination
{
    public class Pager<T> where T : class
    {
        public int Total { get; set; }

        public List<T> Data { get; set; }
    }
}
