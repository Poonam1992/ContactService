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
            var service = GetContactService();
            var contact = new ContactModel();
            contactRepositoryMock.Setup(x => x.CountAsync(It.IsAny<Expression<Func<ContactEntity, bool>>>(), true)).ReturnsAsync(0);
            contactRepositoryMock.Setup(x => x.Add(It.IsAny<ContactEntity>())).Returns(new ContactEntity());
            contactRepositoryMock.Setup(x => x.SaveChanges()).ReturnsAsync(1);
            var result = await service.AddContact(contact);
            Assert.NotNull(result);
            Assert.IsType<ContactModel>(result);
        }
        #endregion

        #region AddContact_Failure
        [Fact]
        public async void AddContact_Failure()
        {
            ConfigureContactTestService();
            var service = GetContactService();
            var contact = new ContactModel();
            contactRepositoryMock.Setup(x => x.CountAsync(It.IsAny<Expression<Func<ContactEntity, bool>>>(), true)).
                Throws(new ContactAlreadyExistException());
            contactRepositoryMock.Setup(x => x.Add(It.IsAny<ContactEntity>())).Returns(new ContactEntity());
           
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

        #region  Delete Contact Success
        [Fact]
        public async void DeleteContact_Success()
        {
            ConfigureContactTestService();
            var service = GetContactService();
            contactRepositoryMock.Setup(x => x.CountAsync(It.IsAny<Expression<Func<ContactEntity, bool>>>(), true)).ReturnsAsync(1);
            contactRepositoryMock.Setup(x => x.Remove(It.IsAny<int>())).ReturnsAsync(true);
            contactRepositoryMock.Setup(x => x.SaveChanges()).ReturnsAsync(1);
            var result = await service.DeleteContact(1);
            Assert.True(result);
        }
        #endregion

        #region DeleteContact_Failure
        [Fact]
        public async void DeleteContact_Failure()
        {
            ConfigureContactTestService();
            var service = GetContactService();
            var contact = new ContactModel();
            contactRepositoryMock.Setup(x => x.CountAsync(It.IsAny<Expression<Func<ContactEntity, bool>>>(), true)).ReturnsAsync(1);
            contactRepositoryMock.Setup(x => x.Add(It.IsAny<ContactEntity>())).Returns(new ContactEntity());
            contactRepositoryMock.Setup(x => x.Remove(It.IsAny<int>())).ReturnsAsync(true);
            contactRepositoryMock.Setup(x => x.SaveChanges()).ReturnsAsync(1);
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

        #region   UpdateContact Success
        [Fact]
        public async void UpdateContact_Success()
        {
            ConfigureContactTestService();
            var service = GetContactService();
            var contact = new ContactModel();
            contactRepositoryMock.Setup(x => x.CountAsync(It.IsAny<Expression<Func<ContactEntity, bool>>>(), true)).ReturnsAsync(1);
            contactRepositoryMock.Setup(x => x.Update(It.IsAny<ContactEntity>())).Returns(new ContactEntity());
            contactRepositoryMock.Setup(x => x.SaveChanges()).ReturnsAsync(1);
            var result = await service.EditContact(contact);
            Assert.NotNull(result);
            Assert.IsType<ContactModel>(result);
        }
        #endregion

        #region UpdateContact_Failure
        [Fact]
        public async void UpdateContact_Failure()
        {
            ConfigureContactTestService();
            var service = GetContactService();
            var contact = new ContactModel();
            contactRepositoryMock.Setup(x => x.CountAsync(It.IsAny<Expression<Func<ContactEntity, bool>>>(), true)).ReturnsAsync(0);
            contactRepositoryMock.Setup(x => x.Update(It.IsAny<ContactEntity>())).Returns(new ContactEntity());
            contactRepositoryMock.Setup(x => x.SaveChanges()).ReturnsAsync(1);
            try
            {
                var result = await service.EditContact(contact);
            }
            catch (Exception ex)
            {
                Assert.IsType<ContactNotFoundException>(ex);
            }
        }
        #endregion

        #region   GetAllContact Success
        [Fact]
        public  void GetAllContact_Success()
        {
            ConfigureContactTestService();
            var service = GetContactService();
            List<ContactEntity> contacts = new List<ContactEntity>()
            {
                new ContactEntity{ Id=10,FirstName="Andry", LastName="John", Email="Andry@gmail.com",PhoneNumber="545454546"}
            };
            contactRepositoryMock.Setup(x => x.GetAll()).Returns(contacts.AsQueryable());
            var result = service.GetContacts();
            Assert.NotNull(result);
            Assert.IsType<List<ContactModel>>(result);
        }
        #endregion

        #region GetAllContact_Failure
        [Fact]
        public  void GetAllContact_Failure()
        {
            ConfigureContactTestService();
            var service = GetContactService();
            contactRepositoryMock.Setup(x => x.GetAll()).Returns(new List<ContactEntity>().AsQueryable());
            try
            {
                var result =  service.GetContacts();
            }
            catch (Exception ex)
            {
                Assert.IsType<ContactRecordsNotFound>(ex);
            }
        }
        #endregion

        #region   UpdateContactStatus Success
        [Fact]
        public async void UpdateContactStatus_Success()
        {
            ConfigureContactTestService();
            var service = GetContactService();
            var contactStatusDto = new ContactPatchStatusDTO();
            contactRepositoryMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<ContactEntity, bool>>>(), true)).ReturnsAsync(new ContactEntity());
            contactRepositoryMock.Setup(x => x.Update(It.IsAny<ContactEntity>())).Returns(new ContactEntity());
            contactRepositoryMock.Setup(x => x.SaveChanges()).ReturnsAsync(1);
            var result = await service.UpdateContactStatus(contactStatusDto);
            Assert.NotNull(result);
            Assert.IsType<ContactModel>(result);
        }
        #endregion

        #region UpdateContactStatus_Failure
        [Fact]
        public async void UpdateContactStatus_Failure()
        {
            ConfigureContactTestService();
            var service = GetContactService();
            var contactStatusDto = new ContactPatchStatusDTO();
            contactRepositoryMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<ContactEntity, bool>>>(), true)).ReturnsAsync((ContactEntity)null);
            contactRepositoryMock.Setup(x => x.Update(It.IsAny<ContactEntity>())).Returns(new ContactEntity());
            contactRepositoryMock.Setup(x => x.SaveChanges()).ReturnsAsync(1);
            try
            {
                var result = await service.UpdateContactStatus(contactStatusDto);
            }
            catch (Exception ex)
            {
                Assert.IsType<ContactNotFoundException>(ex);
            }
        }
        #endregion

    }

}
