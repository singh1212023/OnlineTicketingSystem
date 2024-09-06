using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS.Common.Helpers
{
    public class SortingModel
    {
        public string SortingKey { get; set; }
        public bool IsDescending { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchValue { get; set; }
    }
}
