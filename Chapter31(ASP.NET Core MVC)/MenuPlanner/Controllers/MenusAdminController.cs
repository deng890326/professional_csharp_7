using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MenuPlanner.Models;
using MenuPlanner.Services;

namespace MenuPlanner.Controllers
{
    public class MenusAdminController : Controller
    {
        private readonly IMenuCardsService _service;

        public MenusAdminController(IMenuCardsService service)
        {
            _service = service;
        }

        // GET: MenusAdmin
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetMenusAsync());
        }

        // GET: MenusAdmin/Details/5
        public async Task<IActionResult> Details(int? id = 0)
        {
            if (id == null) return NotFound();
            Menu? menu = await _service.GetMenuByIdAsync(id.Value);
            if (menu == null) return NotFound();

            return View(menu);
        }

        // GET: MenusAdmin/Create
        public async Task<IActionResult> Create()
        {
            var cards = await _service.GetMenuCardsAsync();
            ViewData["MenuCardId"] = new SelectList(cards, "Id", "Name");
            return View();
        }

        // POST: MenusAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text,Price,Active,Order,Type,Day,MenuCardId")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                await _service.AddMenuAsync(menu);
                return RedirectToAction(nameof(Index));
            }
            var cards = await _service.GetMenuCardsAsync();
            ViewData["MenuCardId"] = new SelectList(cards, "Id", "Name", menu.MenuCardId);
            return View(menu);
        }

        // GET: MenusAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _service.GetMenuByIdAsync(id.Value);
            if (menu == null)
            {
                return NotFound();
            }
            var cards = await _service.GetMenuCardsAsync();
            ViewData["MenuCardId"] = new SelectList(cards, "Id", "Name", menu.MenuCardId);
            return View(menu);
        }

        // POST: MenusAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Text,Price,Active,Order,Type,Day,MenuCardId")] Menu menu)
        {
            if (id != menu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateMenuAsync(menu);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.Id))
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
            var cards = await _service.GetMenuCardsAsync();
            ViewData["MenuCardId"] = new SelectList(cards, "Id", "Name", menu.MenuCardId);
            return View(menu);
        }

        // GET: MenusAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _service.GetMenuByIdAsync(id.Value);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: MenusAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.RemoveMenuAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(int id)
        {
          return _service.GetMenuByIdAsync(id) != null;
        }
    }
}
