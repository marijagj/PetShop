using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.ViewModels
{
    public class SopstvenikEditVM
    {
        public Sopstvenik Sopstvenik { get; set; }
        public IEnumerable<int> SelectedProducts { get; set; }
        public IEnumerable<SelectListItem> ProduktList { get; set; }
    }
}
