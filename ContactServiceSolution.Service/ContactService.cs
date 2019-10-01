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
        #region Private Fields
        private readonly IRepository<ContactEntity> _contactRepository = null;
        private readonly IMapper _mapper;
        #endregion

        #region constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contactRepository"></param>
        /// <param name="mapper"></param>
        public ContactService(IRepository<ContactEntity> contactRepository, IMapper mapper)
        {
            this._contactRepository = contactRepository;
            this._mapper = mapper;
        }
        #endregion

        #region Add Contact 
        /// <summary>
        /// Add contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public async Task<ContactModel> AddContact(ContactModel contact)
        {
            using (var transaction = _contactRepository.BeginRepositoryTransaction())
            {
                try
                {
                    ContactEntity contactEntity = null;

                    var isContactExist = await _contactRepository.CountAsync(x => x.Id == contact.Id, true) > 0;

                    if (isContactExist)
                        throw new ContactAlreadyExistException(string.Format(ErrorMessageConstant._contactAlreadyExistsMsg, contact.Id));

                    if(contact.Id >0)
                    {

                    }

                    contactEntity = _mapper.Map<ContactModel, ContactEntity>(contact);
                    var contactAddResult = _contactRepository.Add(contactEntity);

                    await _contactRepository.SaveChanges();
                    _contactRepository.CommitTransaction(transaction);

                    return _mapper.Map<ContactEntity, ContactModel>(contactAddResult);
                }
                catch (Exception ex)
                {
                    _contactRepository.RollbackTransaction(transaction);
                    throw ;
                }
            }
        }
        #endregion

        #region  Delete Contact
        /// <summary>
        /// Delete Contact
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteContact(int Id)
        {
            using (var transaction = _contactRepository.BeginRepositoryTransaction())
            {
                try
                {
                    var isContactExist = await _contactRepository.CountAsync(x => x.Id == Id, true) > 0;

                    if (isContactExist == false)
                        throw new ContactNotFoundException(string.Format(ErrorMessageConstant._contactNotFoundMsg, Id));

                    var contactRemoved = await _contactRepository.Remove(Id);
                    await _contactRepository.SaveChanges();
                    _contactRepository.CommitTransaction(transaction);
                    return contactRemoved;
                }
                catch (Exception ex)
                {
                    _contactRepository.RollbackTransaction(transaction);
                    throw;
                }
            }
        }
        #endregion

        #region Edit Contact
        /// <summary>
        /// Edit Contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public async Task<ContactModel> EditContact(ContactModel contact)
        {
            using (var transaction = _contactRepository.BeginRepositoryTransaction())
            {
                try
                {
                    ContactEntity contactEntity = null;

                    var isContactExist= await _contactRepository.CountAsync(x=>x.Id==contact.Id,true)>0;

                    if (isContactExist == false)
                        throw new ContactNotFoundException(string.Format(ErrorMessageConstant._contactNotFoundMsg, contact.Id));

                    contactEntity = _mapper.Map<ContactModel, ContactEntity>(contact);
                    var contactAddResult = _contactRepository.Update(contactEntity);
                    await _contactRepository.SaveChanges();

                    _contactRepository.CommitTransaction(transaction);
                    return _mapper.Map<ContactEntity, ContactModel>(contactAddResult);

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
                    var contactDetails = await _contactRepository.FirstOrDefaultAsync(x => x.Id == contactStatus.Id, true);

                    if (contactDetails == null)
                        throw new ContactNotFoundException(string.Format(ErrorMessageConstant._contactNotFoundMsg, contactStatus.Id));

                    contactDetails.Status = contactStatus.Status;
                    
                    var contactAddResult = _contactRepository.Update(contactDetails);
                    await _contactRepository.SaveChanges();

                    _contactRepository.CommitTransaction(transaction);
                    return _mapper.Map<ContactEntity, ContactModel>(contactAddResult);

                }
                catch (Exception ex)
                {
                    _contactRepository.RollbackTransaction(transaction);
                    throw;
                }
            }
        }
        #endregion

        #region  Get Contacts
        /// <summary>
        /// Get Contacts
        /// </summary>
        /// <returns></returns>
        public List<ContactModel> GetContacts()
        {
            try
            {
                var contactList = _contactRepository.GetAll();

                if (contactList != null && contactList.Any())
                {
                    var result = _mapper.Map<List<ContactEntity>, List<ContactModel>>(contactList.ToList());
                    return result;
                }

                throw new ContactRecordsNotFound(string.Format(ErrorMessageConstant._contactDataNotFoundMsg));
            }
            catch(Exception ex)
            {
                throw;
            }

        }
        #endregion

    }
}
