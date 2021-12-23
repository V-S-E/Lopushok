using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.ViewModel;

namespace WpfApp1.Interfaces
{
    public static class OrderByMethod
    {
        public static IQueryable<T> OrderByWith<T>(this IQueryable<T> l, SortType<T> t)
        {
            return t.del(l);
        }
    }
}
