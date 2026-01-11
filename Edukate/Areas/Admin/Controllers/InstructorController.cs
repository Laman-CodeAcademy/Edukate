using Edukate.Contexts;
using Edukate.Models;
using Edukate.ViewModels.InstructorViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Edukate.Areas.Admin.Controllers;
[Area("Admin")]

    public class InstructorController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var instructors = await _context.Instructors.Select(x => new InstructorGetVM()
            {
                Id = x.Id,
                Name = x.Name,
                Position = x.Position,
            }).ToListAsync();
            return View(instructors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(InstructorCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            ;

            Instructor newInstructor = new()
            {
                Name = vm.Name,
                Position = vm.Position,
            };

            await _context.Instructors.AddAsync(newInstructor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }

            InstructorUpdateVM newInstructor = new()
            {
                Id = instructor.Id,
                Name = instructor.Name,
                Position = instructor.Position,
            };

            return View(newInstructor);
        }

        [HttpPost]
        public async Task<IActionResult> Update(InstructorUpdateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var instructor = await _context.Instructors.FirstOrDefaultAsync(x => x.Id == vm.Id);
            if (instructor == null)
            {
                return NotFound();
            }

            instructor.Position = vm.Position;
            instructor.Name = vm.Name;

            _context.Instructors.Update(instructor);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }

            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
