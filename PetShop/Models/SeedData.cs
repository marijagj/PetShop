using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PetShopContext(
            serviceProvider.GetRequiredService<
            DbContextOptions<PetShopContext>>()))
            {
                if (context.Sopstvenik.Any() || context.Produkt.Any())
                {
                    return; // DB has been seeded
                }
                context.Produkt.AddRange(
                  new Produkt { /*Id = 1, */Ime = "Гумено Топче", NamenetoZa = "Кучиња", Cena = 230 },
                  new Produkt { /*Id = 2, */Ime = "Розево Ремче", NamenetoZa = "Кучиња", Cena = 500 },
                  new Produkt { /*Id = 3, */Ime = "Friskies Junior 2.4Kg", NamenetoZa = "Кучиња", Cena = 400 },
                  new Produkt { /*Id = 4, */Ime = "Песок за мачки", NamenetoZa = "Мачки", Cena = 220 },
                  new Produkt { /*Id = 5, */Ime = "Шампон за мачки", NamenetoZa = "Мачки", Cena = 200 },
                  new Produkt { /*Id = 6, */Ime = "Поилка", NamenetoZa = "Птици", Cena = 190 },
                  new Produkt { /*Id = 7, */Ime = "Храна за тигрици", NamenetoZa = "Птици", Cena = 100 },
                  new Produkt { /*Id = 8, */Ime = "Кафез за хрчак", NamenetoZa = "Глодари", Cena = 1750 },
                  new Produkt { /*Id = 9, */Ime = "Храна за зајак", NamenetoZa = "Глодари", Cena = 140 }
                   );
                context.SaveChanges();
                context.Sopstvenik.AddRange(
                      new Sopstvenik { /*Id = 1, */ImePrezime = "Марија Ѓошева", ImeMilenik = "Астор", Email = "marijagjosheva@yahoo.com", Grad = "Велес" },
                      new Sopstvenik { /*Id = 2, */ImePrezime = "Наташа Чирова", ImeMilenik = "Бади", Email = "natasha1971@yahoo.com", Grad = "Скопје" },
                      new Sopstvenik { /*Id = 3, */ImePrezime = "Валентина Димитровска", ImeMilenik = "Ени", Email = "vdimitrovska@gmail.com", Grad = "Куманово" },
                      new Sopstvenik { /*Id = 4, */ImePrezime = "Александар Јованов", ImeMilenik = "Феликс", Email = "jovanoval@gmail.com", Grad = "Скопје" },
                      new Sopstvenik { /*Id = 5, */ImePrezime = "Стефан Петров", ImeMilenik = "Рики", Email = "petrovs123@yahoo.com", Grad = "Велес" }

                    );
                context.SaveChanges();
                context.Junction.AddRange(
                    new Junction { /*Id = 1, */SopstvenikID = 1, ProduktID = 1 },
                    new Junction { /*Id = 2, */SopstvenikID = 1, ProduktID = 3 },
                    new Junction { /*Id = 3, */SopstvenikID = 2, ProduktID = 3 },
                    new Junction { /*Id = 4, */SopstvenikID = 3, ProduktID = 3 },
                    new Junction { /*Id = 5, */SopstvenikID = 3, ProduktID = 2 },
                    new Junction { /*Id = 6, */SopstvenikID = 4, ProduktID = 4 },
                    new Junction { /*Id = 7, */SopstvenikID = 5, ProduktID = 8 }
                    );
                context.SaveChanges();


            }
        }
    }
}
