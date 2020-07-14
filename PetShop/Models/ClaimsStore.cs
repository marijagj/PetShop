using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PetShop.Models
{
    public class ClaimsStore
    {
            public static List<Claim> AllClaims = new List<Claim>()
    {
        new Claim("Create Role", "Create Role"),
        new Claim("Edit Role","Edit Role"),
        new Claim("Delete Role","Delete Role"),
        new Claim("Create Product","Create Product"),
        new Claim("Edit Product","Edit Product"),
        new Claim("Delete Product","Delete Product"),
        new Claim("Create Sopstvenik","Create Sopstvenik"),
        new Claim("Edit Sopstvenik","Edit Sopstvenik"),
        new Claim("Delete Sopstvenik","Delete Sopstvenik"),
        new Claim("Poracaj Product","Poracaj Product"),
    };
        }
    }
