using BMongo1.Controllers;
using BMongo1.Models;
using BMongo1.Models.BusinessModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;

namespace BMongo1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesAdminController : Controller
    {
        IRepositoryAccount repositoryaccount;
        IRepositoryCategory repositorycategory;
        IRepositoryCart repositorycart;
        IRepositoryProduct repositoryproduct;

        public CategoriesAdminController(IRepositoryAccount repositoryaccount, IRepositoryCategory repositorycategory, IRepositoryCart repositorycart, IRepositoryProduct repositoryproduct, ILogger<HomeController> logger)
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
                var data = repositorycategory.Paging(page, 4, out totalpage);
                ViewBag.totalpage = totalpage;
                ViewBag.page = page;
                return View(data);
            }
            else
            {
                long totalpage;
                var data = repositorycategory.SearchPaging(name, page, 4, out totalpage);
                ViewBag.totalpage = totalpage;
                ViewBag.page = page;
                ViewBag.name = name;
                return View(data);
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/CategoriesAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            TempData["Message"] = "";
            var check = repositorycategory.GetAll();
            var err = true;
            foreach (var c in check)
            {
                if (category.CategoryName == c.CategoryName)
                {
                    ViewBag.errorName = "The category name already exists";
                    err = false;
                }
            }
            if (err == false)
            {
                return View();
            }


            if (ModelState.IsValid)
            {
                var categories = repositorycategory.Insert(category);
                TempData["Message"] = "New category added successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Admin/CategoriesAdmin/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = repositorycategory.GetById(id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/CategoriesAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            TempData["Message"] = "";
            var check = repositorycategory.GetAll();
            var err = true;
            foreach (var c in check)
            {
                if (category.CategoryName == c.CategoryName)
                {
                    ViewBag.errorName = "The category name already exists";
                    err = false;
                }

            }
            if (err == false)
            {
                return View();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var categories = repositorycategory.Update(category);
                    TempData["Message"] = "Category update successful";
                }
                catch (Exception)
                {

                    return NotFound();

                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin/CategoriesAdmin/Delete/?
        public IActionResult Delete(int id)
        {
            var pro = repositoryproduct.GetAll();
            int check = 0;
            foreach (var b in pro)
            {
                if (b.CategoryId == id)
                {
                    check++;
                    TempData["MessageError"] = "There are products in this category!";
                }

            }
            if (check == 0)
            {
                var category = repositorycategory.Delete(id);

                TempData["Message"] = "Category deletion successful";
            }
            return RedirectToAction(nameof(Index));
        }


    }
}
