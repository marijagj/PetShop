using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.ViewModels
{
    public class ValidDomain:ValidationAttribute
        {
            private readonly string allowedDomain;

            public ValidDomain(string allowedDomain)
            {
                this.allowedDomain = allowedDomain;
            }

            public override bool IsValid(object value)
            {
                string[] strings = value.ToString().Split('@');
                return strings[1].ToUpper() == allowedDomain.ToUpper();
            }
        }

    }

