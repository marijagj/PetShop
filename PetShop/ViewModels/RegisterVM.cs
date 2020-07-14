using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Емаил")]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        [ValidDomain(allowedDomain: "petshop.com",
        ErrorMessage = "Email domain must be petshop.com")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Лозинка")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потврди лозинка")]
        [Compare("Password",
            ErrorMessage = "Лозинките не се совпаѓаат.")]
        public string ConfirmPassword { get; set; }
    }
}
