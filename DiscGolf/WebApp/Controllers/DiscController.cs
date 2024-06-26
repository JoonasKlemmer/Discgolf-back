using App.BLL.DTO;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

//muutus
namespace WebApp.Controllers
{
    public class DiscController : Controller
    {
        private readonly IAppBLL _bll;

        public DiscController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Disc
        public async Task<IActionResult> Index()
        {
            var res = await _bll.Discs.GetAllDiscs();
            return View(res);
        }

        // GET: Disc/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disc = await _bll.Discs.FirstOrDefaultAsync(id.Value);
            if (disc == null)
            {
                return NotFound();
            }

            return View(disc);
        }

        // GET: Disc/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] =  new SelectList(await _bll.Categories.GetAllAsync(), "Id", "CategoryName");
            ViewData["ManufacturerId"] = new SelectList(await _bll.Manufacturers.GetAllAsync(), "Id", "ManufacturerName");
            return View();
        }

        // POST: Disc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Disc disc)
        {
            
            foreach (var key in ModelState.Keys)
            {
                var state = ModelState[key];
                foreach (var error in state!.Errors)
                {
                    Console.WriteLine($"Error in {key}: {error.ErrorMessage}");
                }
            }

            if (ModelState.IsValid)
            {
                disc.Id = Guid.NewGuid();
                _bll.Discs.Add(disc);

                await _bll.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(await _bll.Categories.GetAllAsync(), "Id", "CategoryName", disc.CategoryId);
            ViewData["ManufacturerId"] = new SelectList(await _bll.Manufacturers.GetAllAsync(), "Id", "ManufacturerName", disc.ManufacturerId);
            return View(disc);
        }

        // GET: Disc/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disc = await _bll.Discs.FirstOrDefaultAsync(id.Value);
            if (disc == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await _bll.Categories.GetAllAsync(), "Id", "CategoryName", disc.CategoryId);
            ViewData["ManufacturerId"] = new SelectList(await _bll.Manufacturers.GetAllAsync(), "Id", "ManufacturerName", disc.ManufacturerId);
            return View(disc);
        }

        // POST: Disc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,  Disc disc)
        {
            if (id != disc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Discs.Update(disc);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await DiscExists(disc.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _bll.Categories.GetAllAsync(), "Id", "CategoryName", disc.CategoryId);
            ViewData["ManufacturerId"] = new SelectList(await _bll.Manufacturers.GetAllAsync(), "Id", "ManufacturerName", disc.ManufacturerId);
            return View(disc);
        }

        // GET: Disc/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disc = await _bll.Discs.FirstOrDefaultAsync(id.Value);
            if (disc == null)
            {
                return NotFound();
            }

            return View(disc);
        }

        // POST: Disc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var disc = await _bll.Discs.FirstOrDefaultAsync(id);
            if (disc != null)
            {
                await _bll.Discs.RemoveAsync(disc);
            }

            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private Task<bool> DiscExists(Guid id)
        {
            return _bll.Discs.ExistsAsync(id);
        }
    }
}
