using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.ViewModels
{
    public class PhotoVM
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Mаксимум 50 карактери ")]
        [Display(Name = "Име :")]
        public string Ime { get; set; }
        [Required]
        [Display(Name = "Наменето за :")]
        public string NamenetoZa { get; set; }
        [Display(Name = "Цена :")]
        public int Cena { get; set; }
        [Display(Name = "Фотографија :")]
        public IFormFile ProfilePicture { get; set; }
    }
}
