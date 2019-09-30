using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ContactServiceSolution.Service.Domain;


namespace ContactServiceSolution.Service
{
    public interface IContact
    {
        List<ContactModel> GetContacts();

        Task<ContactModel> AddContact(ContactModel contact);

        Task<ContactModel> EditContact(ContactModel contact);

        Task<bool> DeleteContact(int  Id);
        Task<ContactModel> UpdateContactStatus(ContactPatchStatusDTO contactStatus);
    }
}

