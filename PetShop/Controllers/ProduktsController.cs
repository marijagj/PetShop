using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetShop.Models;
using PetShop.ViewModels;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PetShop.Controllers
{
    [Authorize]
    public class ProduktsController : Controller
    {
        private readonly PetShopContext _context;
        private readonly IHostingEnvironment webHostEnvironment;

        public IHostingEnvironment HostEnvironment { get; }

        public ProduktsController(PetShopContext context, IHostingEnvironment hostingEnvironment, IHostingEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Produkts
        public async Task<IActionResult> Index(string produktVid)
        {
            IQueryable<Produkt> produkti = _context.Produkt.AsQueryable();
            IQueryable<string> vidQuery = _context.Produkt.OrderBy(m => m.NamenetoZa).Select(m => m.NamenetoZa).Distinct();
            if (!string.IsNullOrEmpty(produktVid))
            {
                produkti = produkti.Where(x => x.NamenetoZa == produktVid);
            }
            var searchProduktVM = new SearchProduktVM
            {
                Vidovi = new SelectList(await vidQuery.ToListAsync()),
                Produkts = await produkti.ToListAsync()
            };

            return View(searchProduktVM);
        }

        // GET: Produkts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produkt = await _context.Produkt
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produkt == null)
            {
                return NotFound();
            }

            return View(produkt);
        }
        // GET: Produkts/Edit/5
        [Authorize(Policy = "EditProductPolicy")]
        [HttpGet]
        public ViewResult Edit(int id)
        {
            Produkt product = _context.Produkt.FirstOrDefault(c => c.Id == id);
            EditProduktVM produktEditViewModel = new EditProduktVM
            {
                Id = product.Id,
                Ime = product.Ime,
                NamenetoZa = product.NamenetoZa,
                Cena = product.Cena,
                ExistingPhotoPath = product.ProfilePicture
            };
            return View(produktEditViewModel);
        }

        // POST: Produkts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditProductPolicy")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditProduktVM model)
        {

            if (ModelState.IsValid)
            {
                Produkt product = _context.Produkt.FirstOrDefault(c => c.Id == model.Id);
                product.Ime = model.Ime;
                product.NamenetoZa = model.NamenetoZa;
                product.Cena = model.Cena;
                if (model.ProfilePicture != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath,
                            "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }

                    product.ProfilePicture = UploadedFile(model);
                }

                await _context.SaveChangesAsync(); ;

                return RedirectToAction("index");
            }

            return View(model);
        }

        [Authorize(Policy = "DeleteProductPolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produkt = await _context.Produkt
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produkt == null)
            {
                return NotFound();
            }

            return View(produkt);
        }

        // POST: Produkts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeleteProductPolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produkt = await _context.Produkt.FindAsync(id);
            _context.Produkt.Remove(produkt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProduktExists(int id)
        {
            return _context.Produkt.Any(e => e.Id == id);
        }

        // GET: Produkts/Create
        [Authorize(Policy = "CreateProductPolicy")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produkts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CreateProductPolicy")]
        public async Task<IActionResult> Create(PhotoVM model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);
                Produkt produkt = new Produkt
                {
                    Ime = model.Ime,
                    NamenetoZa = model.NamenetoZa,
                    Cena = model.Cena,
                    ProfilePicture = uniqueFileName,

                };


                _context.Add(produkt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        private string UploadedFile(PhotoVM model)
        {
            string uniqueFileName = null;

            if (model.ProfilePicture != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfilePicture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfilePicture.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public IActionResult Poracaj(int id)
        {
            ViewBag.ID =_context.Produkt.Where(m => m.Id==id).Select(p => p.Ime).SingleOrDefault();
            return View();
        }


    }
}
