using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.Models
{
    public class Junction
    {
        public int Id { get; set; }
        public int ProduktID { get; set; }
        public Produkt Produkt { get; set; }
        public int SopstvenikID { get; set; }
        public Sopstvenik Sopstvenik { get; set; }
    }
}
