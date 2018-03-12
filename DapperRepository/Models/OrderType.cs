using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperRepository.Models
{
    public struct OrderType
    {
        public string ColumnName { get; set; }
        public SortType SortType { get; set; }
        public override string ToString()
        {
            return $"{ColumnName.Trim()} {SortType.ToString()}";
        }
    }
}
