using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolMVCApp.Models;

namespace SchoolMVCApp.Controllers
{
    public class TeachersController : Controller
    {
        private readonly SchoolMVCAppDBContext _context;

        public TeachersController(SchoolMVCAppDBContext context) 
        { 
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var teachers = await _context.Teachers.ToListAsync();
            return View(teachers);
        }

        public async Task<IActionResult> TeacherDetails(int id) 
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.TeacherId == id);
            if (teacher == null) 
            {
                return NotFound();
            }
            return View(teacher);
        }

        public IActionResult AddTeacher() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTeacher([Bind("TeacherId,FirstName,LastName")] Teacher teacher) 
        {
            if (ModelState.IsValid) 
            { 
                _context.Teachers.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        public async Task<IActionResult> EditTeacher(int id, [Bind("TeacherId,FirstName,LastName")] Teacher teacher) 
        {
            if (id != teacher.TeacherId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.TeacherId))
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
            return View(teacher);
        }

        public async Task<IActionResult> DeleteTeacher(int id) 
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(m => m.TeacherId == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }
        [HttpPost, ActionName("DeleteTeacherConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTeacherConfirmed(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.TeacherId == id);
        }
    }
}
