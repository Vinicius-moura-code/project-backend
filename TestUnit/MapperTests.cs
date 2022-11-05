using Application.Mapper;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUnit
{
    [TestFixture]
    public class MapperTests
    {
        [Test]
        public void AutoMapperDtoToModelCliente_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DtoToModelMappingUser>());
            config.AssertConfigurationIsValid();
        }
        [Test]
        public void AutoMapperModelToDto_IsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ModelToDtoMappingUser>());
            config.AssertConfigurationIsValid();
        }

    }
}
