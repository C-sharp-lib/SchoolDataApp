using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolMVCApp.Models;

namespace SchoolMVCApp.Controllers
{
    public class ClassesController : Controller
    {
        private readonly SchoolMVCAppDBContext _context;

        public ClassesController(SchoolMVCAppDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var classes = await _context.Classes.ToListAsync();
            return View(classes);
        }

        public async Task<IActionResult> ClassDetails(int id)
        {
            var clas = await _context.Classes.FirstOrDefaultAsync(c => c.ClassId == id);
            if (clas == null)
            {
                return NotFound();
            }
            return View(clas);
        }

        public IActionResult AddClass()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddClass([Bind("ClassId,Title")] Classes clas)
        {
            if (ModelState.IsValid)
            {
                _context.Classes.Add(clas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clas);
        }

        public async Task<IActionResult> EditClass(int id, [Bind("ClassId,Title")] Classes clas)
        {
            if (id != clas.ClassId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassExists(clas.ClassId))
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
            return View(clas);
        }

        public async Task<IActionResult> DeleteClass(int id)
        {
            var clas = await _context.Classes.FirstOrDefaultAsync(m => m.ClassId == id);
            if (clas == null)
            {
                return NotFound();
            }

            return View(clas);
        }
        [HttpPost, ActionName("DeleteClassConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteClassConfirmed(int id)
        {
            var clas = await _context.Classes.FindAsync(id);
            _context.Classes.Remove(clas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassExists(int id)
        {
            return _context.Classes.Any(e => e.ClassId == id);
        }
    }
}
