using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.ViewModels
{
    public class EditProduktVM : PhotoVM
    {
        public int Id { get; set; }
        public string ExistingPhotoPath { get; set; }
    }
}
