using System;
using System.ComponentModel.DataAnnotations;
using WebApi.Attributes;

namespace WebApi.Models.AccountController
{
    public class UserAccountModel
    {
        public Guid Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The username lenghth be from {2} to {1} characters long.", MinimumLength = 1)]
        public string UserName { get; set; }
        public string Role { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The first name lenghth be from {2} to {1} characters long.", MinimumLength = 1)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "The last name lenghth be from {2} to {1} characters long.", MinimumLength = 1)]
        public string LastName { get; set; }
        [Required]
        [Birthdate(ErrorMessage = "Birthdate out of range")]
        public DateTime Birthdate { get; set; }
    }
}