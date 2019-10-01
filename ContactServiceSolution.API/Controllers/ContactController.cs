using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContactServiceSolution.Service.Domain;
using ContactServiceSolution.Service;
using AutoMapper;

namespace ContactServiceSolution.API.Controllers
{
    [Route("api/Contact")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        #region Private Fields
        private readonly IContact _contactService = null;
        private readonly IMapper _mapper = null;
        #endregion Private Fields

        #region Constructor
        /// <summary>
        /// Contact controller Constructor
        /// </summary>
        /// <param name="contactService"></param>
        /// <param name="mapper"></param>
        public ContactController(IContact contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }
        #endregion Constructor

        #region Add Contact
        /// <summary>
        /// Add contact to contact table
        /// </summary>
        /// <param name="contact"></param>

        /// <returns></returns>
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> AddContact([FromBody] ContactModel contact)
        {
                var response = await _contactService.AddContact(contact);
                return Ok(response);
        }
        #endregion Add Contact

        #region Update Contact
        /// <summary>
        /// Update contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [Route("Update")]
        [HttpPut]
        public async Task<IActionResult> UpdateContact([FromBody]  ContactModel contact)
        {
                var response = await _contactService.EditContact(contact);
                return Ok(response);
        }
        #endregion

        #region Update Contact Status
        /// <summary>
        /// Update status of the contact
        /// </summary>
        /// <param name="contactStataus"></param>
        /// <returns></returns>
        [Route("UpdateContactStatus")]
        [HttpPatch]
        public async Task<IActionResult> UpdateContactStatus([FromBody]  ContactPatchStatusDTO contactStataus)
        {
                var response = await _contactService.UpdateContactStatus(contactStataus);
                return Ok(response);
        }
        #endregion

        #region Remove Contact
        /// <summary>
        /// Remove contact from Table
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        [Route("RemoveContact/{contactId}")]
        [HttpDelete]
        public async Task<IActionResult> RemoveContact(int contactId)
        {
                var response = await _contactService.DeleteContact(contactId);
                return Ok(response);
        }
        #endregion

        #region Get all Contacts
        /// <summary>
        /// Get all contact information
        /// </summary>
        /// <returns></returns>
        [Route("GetAllContacts")]
        [HttpGet]
        public  IActionResult GetAllContacts()
        {
                var response = _contactService.GetContacts();
                return Ok(response);
        }
        #endregion
    }
}
