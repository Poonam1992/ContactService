using System;
using Xunit;
using Moq;
using AutoMapper;
using ContactServiceSolution.Data.Repositories;
using ContactServiceSolution.Data.Entity;
using ContactServiceSolution.Service;
using ContactServiceSolution.API.Controllers;

namespace ContactServiceSolution.Test
{
    public class BaseTest
    {
        public IMapper mapper = null;
        public Mock<IRepository<ContactEntity>> contactRepositoryMock = null;
        public void ConfigureContactTestService()
        {
            contactRepositoryMock = new Mock<IRepository<ContactEntity>>();
            var configMapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingEntities>());
            mapper = new Mapper(configMapper);
        }
        public ContactService GetContactService()
        {
            return new ContactService(contactRepositoryMock.Object, mapper);
        }
    }
}
