using BMongo1.Controllers;
using BMongo1.Models;
using BMongo1.Models.BusinessModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Xml.Linq;

namespace BMongo1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsAdminController : Controller
    {
        IRepositoryAccount repositoryaccount;
        IRepositoryCategory repositorycategory;
        IRepositoryCart repositorycart;
        IRepositoryProduct repositoryproduct;

        public ProductsAdminController(IRepositoryAccount repositoryaccount, IRepositoryCategory repositorycategory, IRepositoryCart repositorycart, IRepositoryProduct repositoryproduct, ILogger<HomeController> logger)
        {
            this.repositoryaccount = repositoryaccount;
            this.repositorycategory = repositorycategory;
            this.repositorycart = repositorycart;
            this.repositoryproduct = repositoryproduct;
        }
        public IActionResult Index(string name, int page = 1)
        {
            page = page < 1 ? 1 : page;
            if (string.IsNullOrEmpty(name))
            {
                long totalpage;
                var data = repositoryproduct.Paging(page, 4, out totalpage);
                ViewBag.totalpage = totalpage;
                ViewBag.page = page;
                return View(data);
            }
            else
            {
                long totalpage;
                var data = repositoryproduct.SearchPaging(name, page, 4, out totalpage);
                ViewBag.totalpage = totalpage;
                ViewBag.page = page;
                ViewBag.name = name;
                return View(data);
            }
        }

        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(repositorycategory.GetAll(), "_id", "CategoryName");
            return View();
        }

        // POST: Admin/ProductsAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            TempData["Message"] = "";

            var check = repositoryproduct.GetById(product._id);

            if (check != null)
            {
                ViewBag.errorId = "The Product ID already exists";
                ViewData["CategoryId"] = new SelectList(repositorycategory.GetAll(), "_id", "CategoryName");
                return View();
            }


            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;

                if (files.Count() > 0 && files[0].Length > 0)
                {
                    var file = files[0];
                    var FileName = file.FileName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        product.ProductImage = FileName;
                    }
                }
                var products = repositoryproduct.Insert(product);
                TempData["Message"] = "New product added successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(repositorycategory.GetAll(), "_id", "CategoryName");
            return View(product);
        }

        // GET: Admin/ProductsAdmin/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = repositoryproduct.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(repositorycategory.GetAll(), "_id", "CategoryName");
            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Product product, string? Image)
        {
            TempData["Message"] = "";


            try
            {
                var files = HttpContext.Request.Form.Files;

                if (files.Count() > 0 && files[0].Length > 0)
                {
                    var file = files[0];
                    var FileName = file.FileName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        product.ProductImage = FileName;
                    }
                }
                else
                {
                    product.ProductImage = Image;
                }

                repositoryproduct.Update(product);
                TempData["Message"] = "Product update successful";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {


                ViewData["CategoryId"] = new SelectList(repositorycategory.GetAll(), "_id", "CategoryName");
                return View(product);

            }


        }

        // GET: Admin/ProductsAdmin/Delete/5
        public IActionResult Delete(string id)
        {
            repositoryproduct.Delete(id);
            TempData["Message"] = "Product deletion successful";
            return RedirectToAction(nameof(Index));
        }

    }
}
