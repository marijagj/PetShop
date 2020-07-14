using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.ViewModels
{
    public class SopstvenikVM
    {
            [Required]
            [Display(Name = "Име и презиме:")]
            public string ImePrezime { get; set; }
            [Required]
            [Display(Name = "Миленик:")]
            public string ImeMilenik { get; set; }
            [Display(Name = "Град:")]
            public string Grad { get; set; }
            [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
                ErrorMessage = "Невалиден Емаил формат")]
            [Display(Name = "Емаил:")]
            public string Email { get; set; }
            [Display(Name = "Фотографија:")]
            public IFormFile ProfilePicture { get; set; }
        }
    }

