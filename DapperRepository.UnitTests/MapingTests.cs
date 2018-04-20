using Dapper.FluentMap;
using DapperRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DapperRepository.UnitTests
{
    public class MapingTests
    {
        private const string connectonString = "data source=localhost;initial catalog=dappertests;integrated security=True;MultipleActiveResultSets=True;";
        private readonly IRepository<TestModel> _repository;
        public MapingTests()
        {
            _repository = new Repository<TestModel>(connectonString);
        }


        [Fact]
        public void RepositorySelectMapsSuccessfullyExpectAny()
        {
            var result = _repository.GetAll();

            Assert.True(result.Any());
        }

        [Fact]
        public void RepositoryInsertMapSuccessfulyExpectNew()
        {
            var model = new TestModel { Name = Guid.NewGuid().ToString("n"), ReallyLongName = Guid.NewGuid().ToString("n") };

            var result = _repository.Insert(model);

            Assert.Equal(0, result);
        }
    }
}
