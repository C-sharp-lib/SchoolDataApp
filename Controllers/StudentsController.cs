using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolMVCApp.Models;

namespace SchoolMVCApp.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolMVCAppDBContext _context;
        public StudentsController(SchoolMVCAppDBContext context) 
        {
            _context = context;
        }

        public async Task<IActionResult> Index() 
        {
            var students = await _context.Students.ToListAsync();
            return View(students);
        }
        public IActionResult AddStudent() 
        { 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStudent([Bind("StudentId,FirstName,LastName")] Students student)
        {
            if (ModelState.IsValid) 
            { 
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(student);
        }
        
        public async Task<IActionResult> EditStudent(int id) 
        { 
            var student = await _context.Students.FindAsync(id);
            if (student == null) 
            { 
                return NotFound();
            }
            return View(student);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStudent(int id, [Bind("StudentId,FirstName,LastName")] Students student) 
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }
            if (ModelState.IsValid) 
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId)) 
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
            return View(student);
        }
        public async Task<IActionResult> DeleteStudent(int id) 
        {
            var student = await _context.Students.FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        [HttpPost, ActionName("DeleteStudentConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteStudentConfirmed(int id) 
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id) 
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}
