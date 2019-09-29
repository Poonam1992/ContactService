using System;
using Xunit;
using Moq;
using AutoMapper;
using ContactServiceSolution.Data.Repositories;
using ContactServiceSolution.Data.Entity;
using ContactServiceSolution.Service;

namespace ContactServiceSolution.Test
{
    public class BaseTest
    {
        public IMapper mapper = null;
        public Mock<IRepository<Contact>> contactRepositoryMock = null;
        public void ConfigureContactTestService()
        {
            contactRepositoryMock = new Mock<IRepository<Contact>>();
            var configMapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingEntities>());
            mapper = new Mapper(configMapper);
        }

        public ContactService CreateServiceInstance()
        {
            return new ContactService(contactRepositoryMock.Object, mapper);
        }
    }
}
