using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ContactServiceSolution.Service.Domain
{
    public class ContactPatchStatusDTO
    {
        [Required(ErrorMessage = "Provide Contact ID")]
        public int Id { get; set; }
        [Required(ErrorMessage ="Provide status value to update")]
        public bool? Status { get; set; }
    }
}
