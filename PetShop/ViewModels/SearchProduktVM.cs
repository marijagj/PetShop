using Microsoft.AspNetCore.Mvc.Rendering;
using PetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.ViewModels
{
    public class SearchProduktVM
    {
        public IList<Produkt> Produkts { get; set; }
        public SelectList Vidovi { get; set; }
        public string ProduktVid { get; set; }
    }
}
