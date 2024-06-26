
using App.BLL.DTO;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    
    public class DiscsInWishlistController : Controller
    {
        private readonly IAppBLL _bll;

        public DiscsInWishlistController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: DiscsInWishlist
        public async Task<IActionResult> Index()
        {
            return View(await _bll.DiscsInWishlists.GetAllAsync());
        }

        // GET: DiscsInWishlist/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discsInWishlist = await _bll.DiscsInWishlists.FirstOrDefaultAsync(id.Value);
            if (discsInWishlist == null)
            {
                return NotFound();
            }

            return View(discsInWishlist);
        }

        // GET: DiscsInWishlist/Create
        public async Task<IActionResult> Create()
        {
            ViewData["DiscFromPageId"] = new SelectList(await _bll.DiscFromPages.GetAllAsync(), "Id", "Id");
            ViewData["WishlistId"] = new SelectList(await _bll.Wishlists.GetAllAsync(), "Id", "WishlistName");//useri id tuleb siia
            return View();
        }

        // POST: DiscsInWishlist/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DiscsInWishlist discsInWishlist)
        {
            if (ModelState.IsValid)
            {
                discsInWishlist.Id = Guid.NewGuid();
                _bll.DiscsInWishlists.Add(discsInWishlist);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiscFromPageId"] = new SelectList(await _bll.DiscFromPages.GetAllAsync(), "Id", "Id", discsInWishlist.DiscFromPageId);
            ViewData["WishlistId"] = new SelectList(await _bll.Wishlists.GetAllAsync(), "Id", "WishlistName", discsInWishlist.WishlistId);
            return View(discsInWishlist);
        }

        // GET: DiscsInWishlist/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discsInWishlist = await _bll.DiscsInWishlists.FirstOrDefaultAsync(id.Value);
            if (discsInWishlist == null)
            {
                return NotFound();
            }
            ViewData["DiscFromPageId"] = new SelectList(await _bll.DiscFromPages.GetAllAsync(), "Id", "Id", discsInWishlist.DiscFromPageId);
            ViewData["WishlistId"] = new SelectList(await _bll.Wishlists.GetAllAsync(), "Id", "WishlistName", discsInWishlist.WishlistId);
            return View(discsInWishlist);
        }

        // POST: DiscsInWishlist/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DiscFromPageId,WishlistId")] DiscsInWishlist discsInWishlist)
        {
            if (id != discsInWishlist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.DiscsInWishlists.Update(discsInWishlist);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await DiscsInWishlistExists(discsInWishlist.Id))
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
            ViewData["DiscFromPageId"] = new SelectList(await _bll.DiscFromPages.GetAllAsync(), "Id", "Id", discsInWishlist.DiscFromPageId);
            ViewData["WishlistId"] = new SelectList(await _bll.Wishlists.GetAllAsync(), "Id", "WishlistName", discsInWishlist.WishlistId);
            return View(discsInWishlist);
        }

        // GET: DiscsInWishlist/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discsInWishlist = await _bll.DiscsInWishlists.FirstOrDefaultAsync(id.Value);
            if (discsInWishlist == null)
            {
                return NotFound();
            }

            return View(discsInWishlist);
        }

        // POST: DiscsInWishlist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var discsInWishlist = await _bll.DiscsInWishlists.FirstOrDefaultAsync(id);
            if (discsInWishlist != null)
            {
                await _bll.DiscsInWishlists.RemoveAsync(discsInWishlist);
            }

            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private Task<bool> DiscsInWishlistExists(Guid id)
        {
            return _bll.DiscsInWishlists.ExistsAsync(id);
        }
    }
}
