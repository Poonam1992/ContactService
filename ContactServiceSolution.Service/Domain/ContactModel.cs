using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ContactServiceSolution.Service.Domain
{
   public  class ContactModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Please provide email in proper format")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Please provide phone number in proper format")]
        public string PhoneNumber { get; set; }
        public bool? Status { get; set; }

    }
}
