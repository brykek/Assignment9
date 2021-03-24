using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment3Movies.Models;

namespace Assignment3Movies.Controllers
{
    public class moviesController : Controller
    {
        private readonly MovieDbContext _context;

        public moviesController(MovieDbContext context)
        {
            _context = context;
        }

        // GET: movies
        public async Task<IActionResult> Index()
        {
            return View(await _context.movies.ToListAsync());
        }

        // GET: movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.movies
                .FirstOrDefaultAsync(m => m.movieID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: movies/Create
        public IActionResult Create()
        {
            //create movie form
            return View();
        }

        // POST: movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("movieID,Category,Title,Year,Director,Rating,Edited,LentTo,Notes")] movie movie)
        {
            //onsubmit of added movie
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //edit movie
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("movieID,Category,Title,Year,Director,Rating,Edited,LentTo,Notes")] movie movie)
        {
            //make changes to editted movie
            if (id != movie.movieID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!movieExists(movie.movieID))
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
            return View(movie);
        }

        // GET: movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            //delete movie
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.movies
                .FirstOrDefaultAsync(m => m.movieID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //remove movie from database
          
            var movie = await _context.movies.FindAsync(id);
            _context.movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool movieExists(int id)
        {
            return _context.movies.Any(e => e.movieID == id);
        }
    }
}
