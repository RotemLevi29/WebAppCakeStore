﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppCakeShopeBoutiqe.Data;
using WebAppCakeShopeBoutiqe.Models;

namespace WebAppCakeShopeBoutiqe.Controllers
{
    public class CategoryImagesController : Controller
    {
        private readonly WebAppCakeShopeBoutiqeContext _context;

        public CategoryImagesController(WebAppCakeShopeBoutiqeContext context)
        {
            _context = context;
        }

        // GET: CategoryImages
        public async Task<IActionResult> Index()
        {
            var webAppCakeShopeBoutiqeContext = _context.CategoryImage.Include(c => c.Category);
            return View(await webAppCakeShopeBoutiqeContext.ToListAsync());
        }

        // GET: CategoryImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryImage = await _context.CategoryImage
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryImage == null)
            {
                return NotFound();
            }

            return View(categoryImage);
        }

        // GET: CategoryImages/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryId");
            return View();
        }

        // POST: CategoryImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CategoryId")] CategoryImage categoryImage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryId", categoryImage.CategoryId);
            return View(categoryImage);
        }

        // GET: CategoryImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryImage = await _context.CategoryImage.FindAsync(id);
            if (categoryImage == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryId", categoryImage.CategoryId);
            return View(categoryImage);
        }

        // POST: CategoryImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CategoryId")] CategoryImage categoryImage)
        {
            if (id != categoryImage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryImageExists(categoryImage.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryId", categoryImage.CategoryId);
            return View(categoryImage);
        }

        // GET: CategoryImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryImage = await _context.CategoryImage
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryImage == null)
            {
                return NotFound();
            }

            return View(categoryImage);
        }

        // POST: CategoryImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryImage = await _context.CategoryImage.FindAsync(id);
            _context.CategoryImage.Remove(categoryImage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryImageExists(int id)
        {
            return _context.CategoryImage.Any(e => e.Id == id);
        }
    }
}
