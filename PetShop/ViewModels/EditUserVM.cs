using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.ViewModels
{
    public class EditUserVM
    {
        public EditUserVM()
        {
            Claims = new List<string>();
            Roles = new List<string>();
        }
        [Display(Name = "ID:")]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Име:")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Емаил:")]
        public string Email { get; set; }

        public List<string> Claims { get; set; }

        public IList<string> Roles { get; set; }
    }
}
