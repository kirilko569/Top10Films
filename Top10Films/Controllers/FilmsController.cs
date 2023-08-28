using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Top10Films.Models;

namespace Top10Films.Controllers
{
    public class FilmsController:Controller
    {
        private readonly FilmsContext db;
        IWebHostEnvironment _appEnvironment;
        public FilmsController(FilmsContext context, IWebHostEnvironment appEnvironment)
        {
            db = context;
            _appEnvironment = appEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<FilmsModel> films = await Task.Run(() => db.Film);
            ViewBag.FilmsModel = films;
            return View();
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || db.Film == null)
            {
                return NotFound();
            }

            var student = await db.Film
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Director,Genre,PosterURL,Year,Description")] FilmsModel Film, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                string path = '/' + uploadedFile.FileName; // имя файла

                // Сохраняем файл в папку Files в каталоге wwwroot
                // Для получения полного пути к каталогу wwwroot
                // применяется свойство WebRootPath объекта IWebHostEnvironment
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream); // копируем файл в поток
                }
                Film.PosterURL = uploadedFile.FileName;
            }
            if (ModelState.IsValid) 
            { 
                db.Add(Film);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Film);
            }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Film == null)
            {
                return NotFound();
            }

            var student = await db.Film.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Director,Genre,PosterURL,Year,Description")] FilmsModel Film, IFormFile uploadedFile)
        {
            if (id != Film.Id)
            {
                return NotFound();
            }
            if (uploadedFile != null)
            {
                string path = '/' + uploadedFile.FileName; // имя файла

                // Сохраняем файл в папку Files в каталоге wwwroot
                // Для получения полного пути к каталогу wwwroot
                // применяется свойство WebRootPath объекта IWebHostEnvironment
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream); // копируем файл в поток
                }
                Film.PosterURL = uploadedFile.FileName;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(Film);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(Film.Id))
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
            return View(Film);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Film == null)
            {
                return NotFound();
            }

            var student = await db.Film
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Film == null)
            {
                return Problem("Entity set 'FilmsContext.Film'  is null.");
            }
            var student = await db.Film.FindAsync(id);
            if (student != null)
            {
                db.Film.Remove(student);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(int id)
        {
            return (db.Film?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
