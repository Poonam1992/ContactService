using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ContactServiceSolution.Service.Domain;
using ContactServiceSolution.Service.CustomException;
using ContactServiceSolution.Data.Repositories;
using ContactServiceSolution.Data.Entity;
using AutoMapper;
using System.Linq;


namespace ContactServiceSolution.Service
{
    public class ContactService : IContact
    {

        private readonly IRepository<Contact> _contactRepository = null;
        private readonly IMapper _mapper;
        public ContactService(IRepository<Contact> contactRepository, IMapper mapper)
        {
            this._contactRepository = contactRepository;
            this._mapper = mapper;
        }

        public async Task<ContactModel> AddContact(ContactModel contact)
        {
            using (var transaction = _contactRepository.BeginRepositoryTransaction())
            {
                try
                {
                    Contact contactEntity = null;
                    var isContactExist = await _contactRepository.CountAsync(x => x.Id == contact.Id, true) > 0;

                    if (isContactExist)
                        throw new ContactAlreadyExistException(string.Format(ErrorMessageConstant._contactAlreadyExistsMsg, contact.Id));

                    contactEntity = _mapper.Map<ContactModel, Contact>(contact);
                    var contactAddResult = _contactRepository.Add(contactEntity);

                    await _contactRepository.SaveChanges();
                    _contactRepository.CommitTransaction(transaction);

                    return _mapper.Map<Contact, ContactModel>(contactAddResult);
                }
                catch (Exception ex)
                {
                    _contactRepository.RollbackTransaction(transaction);
                    throw ;
                }
            }
        }
        public  async Task<bool> DeleteContact(int Id)
        {
            using (var transaction = _contactRepository.BeginRepositoryTransaction())
            {
                try
                {
                    var cartRemoved = await _contactRepository.Remove(Id);
                    await _contactRepository.SaveChanges();
                    _contactRepository.CommitTransaction(transaction);
                    return cartRemoved;
                }
                catch (Exception ex)
                {
                    _contactRepository.RollbackTransaction(transaction);
                    throw;
                }
            }
        }

        public async Task<ContactModel> EditContact(ContactModel contact)
        {
            using (var transaction = _contactRepository.BeginRepositoryTransaction())
            {
                try
                {
                    Contact contactEntity = null;

                    if (contact.Id<=0)
                        throw new ContactNotFoundException(ErrorMessageConstant._contactNotFoundMsg);

                     var isContactExist= await _contactRepository.CountAsync(x=>x.Id==contact.Id,true)>0;

                    if (isContactExist == false)
                        throw new ContactNotFoundException(string.Format(ErrorMessageConstant._contactNotFoundMsg, contact.Id));

                    contactEntity = _mapper.Map<ContactModel, Contact>(contact);
                    var contactAddResult = _contactRepository.Update(contactEntity);
                    await _contactRepository.SaveChanges();

                    _contactRepository.CommitTransaction(transaction);
                    return _mapper.Map<Contact, ContactModel>(contactAddResult);

                }
                catch (Exception ex)
                {
                    _contactRepository.RollbackTransaction(transaction);
                    throw ;
                }
            }
        }

        public async  Task<ContactModel> UpdateContactStatus(ContactPatchStatusDTO contactStatus)
        {
            using (var transaction = _contactRepository.BeginRepositoryTransaction())
            {
                try
                {
                    if (contactStatus.Id <= 0)
                        throw new ContactNotFoundException(ErrorMessageConstant._contactNotFoundMsg);

                    var contactDetails = await _contactRepository.FirstOrDefaultAsync(x => x.Id == contactStatus.Id, true);

                    if (contactDetails == null)
                        throw new ContactNotFoundException(string.Format(ErrorMessageConstant._contactNotFoundMsg, contactStatus.Id));

                    contactDetails.Status = contactStatus.Status;
                    
                    var contactAddResult = _contactRepository.Update(contactDetails);
                    await _contactRepository.SaveChanges();

                    _contactRepository.CommitTransaction(transaction);
                    return _mapper.Map<Contact, ContactModel>(contactAddResult);

                }
                catch (Exception ex)
                {
                    _contactRepository.RollbackTransaction(transaction);
                    throw;
                }
            }
        }

        public List<ContactModel> GetContacts()
        {
            try
            {
                var contactList = _contactRepository.GetAll();

                if (contactList != null && contactList.Any())
                {
                    var result = _mapper.Map<List<Contact>, List<ContactModel>>(contactList.ToList());
                    return result;
                }

                throw new ContactRecordsNotFound(string.Format(ErrorMessageConstant._contactDataNotFoundMsg));
            }
            catch(Exception ex)
            {
                throw;
            }

        }

    }
}
