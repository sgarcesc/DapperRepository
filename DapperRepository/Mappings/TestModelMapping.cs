using Dapper.FluentMap.Dommel.Mapping;
using Dapper.FluentMap.Mapping;
using DapperRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DapperRepository.Mappings
{
    public class TestModelMapping : DommelEntityMap<TestModel>
    {
        public TestModelMapping()
        {
            ToTable("TestModels");
            Map(p => p.Name).ToColumn("my_name", caseSensitive: false).IsKey();
            Map(p => p.ReallyLongName).ToColumn("really_long_name", caseSensitive: false);
        }

        protected override RelationshipMap<TestModel, TRelatedEntity> GetRelationshipMap<TRelatedEntity>(PropertyInfo info, RelationshipType relationshipType)
        {
            throw new NotImplementedException();
        }
    }
}
