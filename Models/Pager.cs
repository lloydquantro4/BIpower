using System.Collections.Generic;
using System.Linq;

namespace BIpower.Models
{
    public class Pager<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int Total {get;set;}

        public Pager(IEnumerable<T> data, int pageNumber, int pageLength){
            //paging calculator
            Data = data.Skip((pageNumber - 1) * pageLength).Take(pageLength).ToList();
            Total = data.Count();
        }
    }
}