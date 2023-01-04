using Microsoft.AspNetCore.Mvc;
using PieInfo.Data;
using PieInfo.Models;

namespace PieInfo.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categories = _context.Categories; 
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                TempData["Success"] = "Category Created Successful";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if(id==null || id == 0)
            {   
                return NotFound();
            }
            var category = _context.Categories.Where(x => x.Id == id).FirstOrDefault();
            if(category==null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
                TempData["Success"] = "Category Updated Successful";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var category = _context.Categories.Where(x => x.Id == Id).FirstOrDefault();
            
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteDataById(int Id)
        {
            var deletedata = _context.Categories.Where(x => x.Id == Id).FirstOrDefault();
            if(deletedata==null)
            {
                return NotFound();
            }
            else
            {
                _context.Categories.Remove(deletedata);
                _context.SaveChanges();
                TempData["Success"] = "Category Deleted Successful";
                return RedirectToAction("Index");
            }
               
        }


    }
}
