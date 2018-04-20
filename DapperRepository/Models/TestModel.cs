using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperRepository.Models
{
    public class TestModel
    {
        [ExplicitKey]
        public string Name { get; set; }
        public string ReallyLongName { get; set; }
    }
}
