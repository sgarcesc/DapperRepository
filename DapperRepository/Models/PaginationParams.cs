using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperRepository.Models
{
    public struct PaginationParams
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<OrderType> OrderBy { get; set; }
    }
}
