using Microsoft.AspNetCore.Mvc;
using PieInfo.Data;
using PieInfo.DataAccessLayer.Infrastructure.IRepository;
using PieInfo.Models;

namespace PieInfo.Controllers
{
    public class CategoryController : Controller
    {
        private IUnitOfWork _unitofWork;
        public CategoryController(IUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categories = _unitofWork.Category.GetAll(); 
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
                _unitofWork.Category.Add(category);
                _unitofWork.Save();
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
            var category = _unitofWork.Category.GetT(x => x.Id == id);
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
                _unitofWork.Category.Update(category);
                _unitofWork.Save();
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
            var category = _unitofWork.Category.GetT(x => x.Id == Id);
            
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteDataById(int Id)
        {
            var deletedata =_unitofWork.Category.GetT(x => x.Id == Id);
            if (deletedata==null)
            {
                return NotFound();
            }
            else
            {
                _unitofWork.Category.Delete(deletedata);
                _unitofWork.Save();
                TempData["Success"] = "Category Deleted Successful";
                return RedirectToAction("Index");
            }
               
        }


    }
}
