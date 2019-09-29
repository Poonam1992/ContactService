using System;
using Xunit;
using Moq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using ContactServiceSolution.Data.Repositories;
using ContactServiceSolution.Data.Entity;
using ContactServiceSolution.Service.Domain;
using ContactServiceSolution.Service.CustomException;

namespace ContactServiceSolution.Test
{
   public class ContactServiceUnitTest:BaseTest

    {

        #region  AddContact Success
        [Fact]
        public async void AddContact_Success()
        {
            ConfigureContactTestService();
            var service = CreateServiceInstance();
            var contact = new ContactModel();
            contactRepositoryMock.Setup(x => x.CountAsync(It.IsAny<Expression<Func<Contact, bool>>>(), true)).ReturnsAsync(0);
            contactRepositoryMock.Setup(x => x.Add(It.IsAny<Contact>())).Returns(new Contact());
            contactRepositoryMock.Setup(x => x.SaveChanges()).ReturnsAsync(1);
            var result = await service.AddContact(contact);
            Assert.NotNull(result);
        }
        #endregion

        #region AddProduct_Failure
        [Fact]
        public async void AddProduct_Failure()
        {
            ConfigureContactTestService();
            var service = CreateServiceInstance();
            var contact = new ContactModel();
            contactRepositoryMock.Setup(x => x.CountAsync(It.IsAny<Expression<Func<Contact, bool>>>(), true)).
                Throws(new ContactAlreadyExistException());
            contactRepositoryMock.Setup(x => x.Add(It.IsAny<Contact>())).Returns(new Contact());
           
            try
            {
                var result = await service.AddContact(contact);
            }
            catch (Exception ex)
            {
                Assert.IsType<ContactAlreadyExistException>(ex);
            }
        }
        #endregion
    }
}
