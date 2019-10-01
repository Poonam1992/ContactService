using System;
using System.Collections.Generic;

namespace ContactServiceSolution.API.Models
{
    public partial class ContactEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool? Status { get; set; }
    }
}
