using Microsoft.AspNetCore.Mvc;
using PieInfo.Data;
using PieInfo.DataAccessLayer.Infrastructure.IRepository;
using PieInfo.Models;
using PieInfo.Models.ViewModel;

namespace PieInfo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private IUnitOfWork _unitofWork;
        public CategoryController(IUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public IActionResult Index()
        {
            CategoryVM categoryVM=new CategoryVM();
           // categoryVM.categories = _unitofWork.Category.GetAll();
            categoryVM.categories = _unitofWork.Category.GetAll().OrderByDescending(x=>x.Id);
            return View(categoryVM);
        }
        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitofWork.Category.Add(category);
        //        _unitofWork.Save();
        //        TempData["Success"] = "Category Created Successful";
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}
        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            CategoryVM vm=new CategoryVM();
            if (id == null || id == 0)
            {
                return View(vm);
            }
       else 
            {  
               vm.Category = _unitofWork.Category.GetT(x => x.Id == id);
                if (vm.Category == null)
                {
                    return NotFound(vm);
                }
                else
                {
                    return View(vm);
                }
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(CategoryVM vm)
        {
           if(ModelState.IsValid)
            {
                if (vm.Category.Id == 0  )
                {
                    _unitofWork.Category.Add(vm.Category);
                    _unitofWork.Save();
                    TempData["success"] = "Created Category done ";
                    return RedirectToAction("Index");
                }
                else
                {
                    _unitofWork.Category.Update(vm.Category);
                    _unitofWork.Save();
                }
                
                
            }
            return RedirectToAction("Index");
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
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteDataById(int Id)
        {
            var deletedata = _unitofWork.Category.GetT(x => x.Id == Id);
            if (deletedata == null)
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
