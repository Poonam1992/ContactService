﻿using System;
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
        private readonly IContact _contactService = null;
        private readonly IMapper _mapper = null;
       
        public ContactController(IContact contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> AddContact([FromBody] ContactModel contact)
        {
            try
            {
                var response = await _contactService.AddContact(contact);
                return Ok(response);
            }
            catch (Exception ex)
            {
                //FillErrorInfo(ex);
               // LogException(ex);
                return BadRequest(ex);
            }
        }

        [Route("Update")]
        [HttpPut]
        public async Task<IActionResult> UpdateContact([FromBody]  ContactModel contact)
        {
            try
            {
                var response = await _contactService.EditContact(contact);
                return Ok(response);
            }
            catch (Exception ex)
            {
               // FillErrorInfo(ex);
               // LogException(ex);
                return BadRequest(ex);
            }
        }

        [Route("RemoveContact/{contactId}")]
        [HttpDelete]
        public async Task<IActionResult> RemoveContact(int contactId)
        {
            try
            {
                var response = await _contactService.DeleteContact(contactId);
                return Ok(response);
            }
            catch (Exception ex)
            {
               // FillErrorInfo(ex);
               // LogException(ex);
                return BadRequest(ex);
            }
        }

        [Route("GetAllContacts")]
        [HttpGet]
        public  IActionResult GetAllContacts()
        {
            try
            {
                var response = _contactService.GetContacts();
                return Ok(response);
            }
            catch (Exception ex)
            {
                // FillErrorInfo(ex);
                // LogException(ex);
                return BadRequest(ex);
            }
        }


    }
}
