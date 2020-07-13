using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineShopping.Buisness.DTOs
{
   public class UserForRegisterDto
    {
        public string Email { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must  4and 8")]
        public string Password { get; set; }
        public string City { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string street { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
