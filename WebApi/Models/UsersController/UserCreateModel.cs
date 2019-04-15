using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApi.Attributes;

namespace WebApi.Models.UsersController
{
    public class UserCreateModel
    {
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The password lenghth be from {2} to {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The password lenghth be from {2} to {1} characters long.", MinimumLength = 1)]
        public string UserName { get; set; }
        public string Role { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The first name lenghth be from {2} to {1} characters long.", MinimumLength = 1)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The last name lenghth be from {2} to {1} characters long.", MinimumLength = 1)]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Birthdate(ErrorMessage = "Birthdate out of range")]
        public DateTime Birthdate { get; set; }
    }
}