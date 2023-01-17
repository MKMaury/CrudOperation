using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PieInfo.Data;
using PieInfo.DataAccessLayer.Infrastructure.IRepository;
using PieInfo.Models;
using PieInfo.Models.ViewModel;

namespace PieInfo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private IUnitOfWork _unitofWork;
        private IWebHostEnvironment _hostEnvironment;
        public ProductController(IUnitOfWork unitofWork, IWebHostEnvironment hostEnvironment)
        {
            _unitofWork = unitofWork;
            _hostEnvironment = hostEnvironment;
        }
        public JsonResult AllProduct()
        {
            //ProductVM productVM = new ProductVM();
            var  products = _unitofWork.Product.GetAll(includePropeties:"Category").OrderByDescending(x=>x.Id);
            return Json (new { data=products });
        }
        public IActionResult Index()
        {
            //ProductVM productVM =new ProductVM();
            //productVM.products = _unitofWork.Product.GetAll();
            return View();
        }
        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitofWork.Product.Add(product);
        //        _unitofWork.Save();
        //        TempData["Success"] = "Product Created Successful";
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}
        [HttpGet]
        public IActionResult CreateUpdate(int id)
        {
            ProductVM vm = new ProductVM()
            {
                Products = new(),
                categories = _unitofWork.Category.GetAll().Select(x => new SelectListItem()
                {
                 Text= x.Name,
                 Value =x.Id.ToString(),
                })
            };
            if (id == null || id == 0)
            {
                return View(vm);
            }
            else
            {
                vm.Products = _unitofWork.Product.GetT(x => x.Id == id);
                if (vm.Products.Id == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(vm);
                }
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(ProductVM vm,IFormFile file)
        {
            if (ModelState.IsValid) 
            {
                string FileName = string.Empty;
                if (file != null)
                {
                    string uploadDir = Path.Combine(_hostEnvironment.WebRootPath, "ProductImage");
                    FileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                    string filePath= Path.Combine(uploadDir, FileName);

                    if(vm.Products.ImageUrl!=null)
                    {
                        var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, vm.Products.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    } 
                    vm.Products.ImageUrl= @"/ProductImage/"+FileName;
                }
                if(vm.Products.Id==0)
                {
                    _unitofWork.Product.Add(vm.Products);
                    TempData["Success"] = "Product Created Done";
                }
                else
                {
                    _unitofWork.Product.Update(vm.Products);
                    TempData["Success"] = "Product Updated Done";
                }
                 _unitofWork.Save();
                return RedirectToAction("Index");
                //if (vm.Products.Id == 0)
                //{
                //    _unitofWork.Product.Add(vm.Products);
                //    _unitofWork.Save();
                //    TempData["success"] = "Created Category done ";
                //    return RedirectToAction("Index");
                //}
                //else
                //{
                //    _unitofWork.Product.Update(vm.Products);
                //    _unitofWork.Save();
                //}


            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _unitofWork.Product.Update(product);
                _unitofWork.Save();
                TempData["Success"] = "Category Updated Successful";
                return RedirectToAction("Index");
            }
            return View();
        }
        //[HttpGet]
        //public IActionResult Delete(int Id)
        //{
        //    if (Id == null || Id == 0)
        //    {
        //        return NotFound();
        //    }
        //    var product = _unitofWork.Product.GetT(x => x.Id == Id);

        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(product);
        //}

        //[HttpPost, ActionName("Delete")]
        [HttpDelete]
        public IActionResult Delete(int Id)
        {
            var deletedata = _unitofWork.Product.GetT(x => x.Id == Id);
            if (deletedata == null)
            {
                 return Json(new { success = false, Error = "Error in Fetching Data" });
            }
            else
            {
               var  oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, deletedata.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
                _unitofWork.Product.Delete(deletedata);
                  //_unitofWork.Product.Delete( oldImagePath);
                _unitofWork.Save();
                return Json(new { success = true, message = "Product has been Deleted" });

            }


        }


    }
}
