using Edukate.Contexts;
using Edukate.Helpers;
using Edukate.Models;
using Edukate.ViewModels.CourseViewModel;
using Edukate.ViewModels.InstructorViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Edukate.Areas.Admin.Controllers;
[Area("Admin")]


public class CourseController(AppDbContext _context, IWebHostEnvironment _env) : Controller
    {
        private readonly string folderPath = Path.Combine(_env.WebRootPath,"assets", "images");
        private async Task SendInstructorsWithViewBag()
        {
            var instructors = await _context.Instructors.ToListAsync();
            ViewBag.Instructors = instructors;  
        }
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.Select(x => new CourseGetVM()
            {
                Id = x.Id,
                Name = x.Name,
                Rating=x.Rating,
                Image=x.Image,
                InstructorName = x.Instructor.Name,

            }).ToListAsync();
            return View(courses);
        }

        public async Task<IActionResult> Create()
        {
            await SendInstructorsWithViewBag(); 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateVM vm)
        {
            await SendInstructorsWithViewBag();
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            ;

            var existInstructor = await _context.Courses.AnyAsync();
            if (!existInstructor) {
                ModelState.AddModelError("instructir", "instructir not found");
                return View(vm);
            }


            if (!vm.Image.CheckSize(2))
            {
                ModelState.AddModelError("image", "image too large");
                return View(vm);
            }

            if (!vm.Image.CheckType("image"))
            {
                ModelState.AddModelError("image", "file should be an image type");
                return View(vm);
            }

            string uniqueFileName = await vm.Image.FileUploadAsync(folderPath);

            Course newCourse = new()
            {
                Name=vm.Name,
                Rating=vm.Rating,
                Image=uniqueFileName,
                InstructorId=vm.InstructorId
            };

            await _context.Courses.AddAsync(newCourse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            CourseUpdateVM newCourse = new()
            {
                Id = course.Id,
                Name = course.Name,
                Rating = course.Rating,
                InstructorId=course.InstructorId
            };

            await SendInstructorsWithViewBag();

            return View(newCourse); 
        }

        [HttpPost]
        public async Task<IActionResult> Update(CourseUpdateVM vm)
        {
            await SendInstructorsWithViewBag();
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == vm.Id);
            if (course == null)
            {
                return NotFound();
            }

            var existInstructor = await _context.Courses.AnyAsync();
            if (!existInstructor)
            {
                ModelState.AddModelError("instructir", "instructir not found");
                return View(vm);
            }


            if (!vm.Image?.CheckSize(2)?? false)
            {
                ModelState.AddModelError("image", "image too large");
                return View(vm);
            }

            if (!vm.Image?.CheckType("image")?? false)
            {
                ModelState.AddModelError("image", "file should be an image type");
                return View(vm);
            }

            course.Id = vm.Id;
            course.Rating = vm.Rating;
            course.InstructorId = vm.InstructorId;

            if(vm.Image is { }){
                var uniqueName =await vm.Image.FileUploadAsync(folderPath);
                var deletedPath = Path.Combine(folderPath,course.Image);
                FileHelper.DeleteFile(deletedPath);
                course.Image = uniqueName;
            }

            _context.Courses.Update(course);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            var deletedPath = Path.Combine(folderPath, course.Image);
            FileHelper.DeleteFile(deletedPath);

            return RedirectToAction(nameof(Index));
        }
    }
