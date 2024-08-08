using BMongo1.Models;
using BMongo1.Models.BusinessModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BMongo1.Controllers
{
    public class ProductController : Controller
    {
        IRepositoryAccount repositoryaccount;
        IRepositoryCategory repositorycategory;
        IRepositoryCart repositorycart;
        IRepositoryProduct repositoryproduct;

        public ProductController(IRepositoryAccount repositoryaccount, IRepositoryCategory repositorycategory, IRepositoryCart repositorycart, IRepositoryProduct repositoryproduct, ILogger<HomeController> logger)
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
                var data = repositoryproduct.Paging(page, 8, out totalpage);
                ViewBag.totalpage = totalpage;
                ViewBag.page = page;
                return View(data);
            }
            else
            {
                long totalpage;
                var data = repositoryproduct.SearchPaging(name, page, 8, out totalpage);
                ViewBag.totalpage = totalpage;
                ViewBag.page = page;
                ViewBag.name = name;
                return View(data);
            }
        }

        public async Task<IActionResult> Details(string id)
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
            var catName = repositoryproduct.GetForeignKey();
            Int32 a = 1;
            List<Product> list = new List<Product>();
            var N = "";
            foreach (var p in catName)
            {
                if (product.CategoryId == p.CategoryId)
                {
                    N = p.CategoryName;
                    if (product._id != p._id)
                    {
                        list.Add(new Product() { _id = p._id, ProductName = p.ProductName, ProductImage = p.ProductImage, ProductStatus = p.ProductStatus, Price = p.Price });
                        a++;
                    }
                    if (a >= 5)
                    {
                        continue;
                    }
                }
                else
                {
                    continue;
                }
            }
            foreach (var p in catName)
            {
            }

            ViewData["catName"] = N;
            ViewData["pro"] = list;
            ViewData["pro_id"] = id;
            ViewData["price"] = product.Price;
            return View(product);
        }
    }
}
