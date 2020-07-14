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

namespace PetShop.Controllers
{
    [Authorize]
    public class SopstveniksController : Controller
    {
        private readonly PetShopContext _context;
        private readonly IHostingEnvironment webHostEnvironment;

        public SopstveniksController(PetShopContext context, IHostingEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Sopstveniks
        public async Task<IActionResult> IndexSopstvenik()
        {
            return View(await _context.Sopstvenik.Include(m => m.Produkti).ThenInclude(m => m.Produkt).ToListAsync());
        }

        // GET: Sopstveniks/Details/5
        public async Task<IActionResult> DetailsSopstvenik(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sopstvenik = await _context.Sopstvenik.Include(m => m.Produkti).ThenInclude(m => m.Produkt)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sopstvenik == null)
            {
                return NotFound();
            }

            return View(sopstvenik);
        }

        // GET: Sopstveniks/Create
        [Authorize(Policy = "CreateSopstvenikPolicy")]
        public IActionResult CreateSopstvenik()
        {
            return View();
        }

        // POST: Sopstveniks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CreateSopstvenikPolicy")]
        public async Task<IActionResult> CreateSopstvenik(SopstvenikVM model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);
                Sopstvenik sopstvenik = new Sopstvenik
                {
                    ImePrezime = model.ImePrezime,
                    ImeMilenik = model.ImeMilenik,
                    Email = model.Email,
                    Grad = model.Grad,
                    ProfilePicture = uniqueFileName,

                };
                _context.Add(sopstvenik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexSopstvenik));

            }
            return View(model);

        }
        private string UploadedFile(SopstvenikVM model)
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

        // GET: Sopstveniks/Edit/5
        [Authorize(Policy = "EditSopstvenikPolicy")]
        [HttpGet]
        public async Task<IActionResult> EditSopstvenik(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sopstvenik = _context.Sopstvenik.Where(m => m.Id == id).Include(m => m.Produkti).First();
            if (sopstvenik == null)
            {
                return NotFound();
            }
            SopstvenikEditVM viewmodel = new SopstvenikEditVM
            {
                Sopstvenik = sopstvenik,
                ProduktList = new MultiSelectList(_context.Produkt.OrderBy(s => s.Ime), "Id", "Ime"),
                SelectedProducts = sopstvenik.Produkti.Select(sa => sa.ProduktID)
            };

            return View(viewmodel);
        }

        // POST: Sopstveniks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EditSopstvenikPolicy")]
        public async Task<IActionResult> EditSopstvenik(int id, SopstvenikEditVM vm)
        {
            if (id != vm.Sopstvenik.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
               
                try
                {
                    _context.Update(vm.Sopstvenik);
                    await _context.SaveChangesAsync();
                    IEnumerable<int> listProdukt = vm.SelectedProducts;
                    IQueryable<Junction> toBeRemoved = _context.Junction.Where(s => !listProdukt.Contains(s.ProduktID) && s.SopstvenikID == id);
                    _context.Junction.RemoveRange(toBeRemoved);
                    IEnumerable<int> existProdukts = _context.Junction.Where(s => listProdukt.Contains(s.ProduktID) && s.SopstvenikID == id).Select(s => s.ProduktID);
                    IEnumerable<int> newProdukts = listProdukt.Where(s => !existProdukts.Contains(s));
                    foreach (int produktId in newProdukts)
                        _context.Junction.Add(new Junction { ProduktID = produktId, SopstvenikID = id });
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SopstvenikExists(vm.Sopstvenik.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexSopstvenik));
            //}
           // return View(vm);
        }

        // GET: Sopstveniks/Delete/5
        [Authorize(Policy = "DeleteSopstvenikPolicy")]
        public async Task<IActionResult> DeleteSopstvenik(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sopstvenik = await _context.Sopstvenik.Include(m => m.Produkti).ThenInclude(m => m.Produkt)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sopstvenik == null)
            {
                return NotFound();
            }

            return View(sopstvenik);
        }

        // POST: Sopstveniks/Delete/5
        [HttpPost, ActionName("DeleteSopstvenik")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "DeleteSopstvenikPolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sopstvenik = await _context.Sopstvenik.FindAsync(id);
            _context.Sopstvenik.Remove(sopstvenik);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexSopstvenik));
        }

        private bool SopstvenikExists(int id)
        {
            return _context.Sopstvenik.Any(e => e.Id == id);
        }
    }
}

