using System;
using System.Collections.Generic;
using System.Text;

namespace ContactServiceSolution.Service.Domain
{
   public  class ContactModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool? Status { get; set; }

    }
}
