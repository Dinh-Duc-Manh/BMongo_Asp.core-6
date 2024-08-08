using BMongo1.Models;
using Microsoft.AspNetCore.Mvc;
using BMongo1.Models.BusinessModels;

namespace BMongo1.Controllers
{
    public class LoginController : Controller
    {
        IRepositoryAccount repositoryaccount;
        IRepositoryCategory repositorycategory;
        IRepositoryCart repositorycart;
        IRepositoryProduct repositoryproduct;

        public LoginController(IRepositoryAccount repositoryaccount, IRepositoryCategory repositorycategory, IRepositoryCart repositorycart, IRepositoryProduct repositoryproduct, ILogger<HomeController> logger)
        {
            this.repositoryaccount = repositoryaccount;
            this.repositorycategory = repositorycategory;
            this.repositorycart = repositorycart;
            this.repositoryproduct = repositoryproduct;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Login model)
        {
            TempData["MessageError"] = "";

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {

                var checkAccount = repositoryaccount.Login(model.Email, model.Password);

                if (checkAccount != null)
                {
                    HttpContext.Session.Clear();
                    HttpContext.Session.SetInt32("LoginId", checkAccount._id);
                    HttpContext.Session.SetString("LoginName", checkAccount.FullName);
                    HttpContext.Session.SetString("LoginPhone", checkAccount.Phone);
                    HttpContext.Session.SetString("LoginEmail", checkAccount.Email);
                    HttpContext.Session.SetString("LoginAddress", checkAccount.Address);
                    HttpContext.Session.SetString("LoginAvatar", checkAccount.Avatar);
                    HttpContext.Session.SetString("LoginType", checkAccount.AccountType);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    HttpContext.Session.Clear();
                    TempData["MessageError"] = "Login information is incorrect or does not exist";
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Account account)
        {
            TempData["Message"] = "";
            var err = true;
            var check = repositoryaccount.GetAll();
            foreach (var a in check)
            {
                if (a.Email == account.Email)
                {
                    ViewBag.error = "Account Email already exists";
                    err = false;
                }
            }
            if(err == false)
            {
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
                        account.Avatar = FileName;
                    }
                }
                var accounts = repositoryaccount.Insert(account);
                TempData["Message"] = "New account added successfully";
                return RedirectToAction("Index", "Login");
            }
            return View(account);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("Login");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Account(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = repositoryaccount.GetById(id);

            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Account(int id, Account account, string? Image)
        {
            TempData["Message"] = "";

            if (id != account._id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
                            account.Avatar = FileName;
                        }
                    }
                    else
                    {
                        account.Avatar = Image;
                    }

                    repositoryaccount.Update(account);
                    HttpContext.Session.SetString("LoginName", account.FullName);
                    TempData["Message"] = "Product update successful";
                }
                catch (Exception ex)
                {

                    return NotFound();

                }
                return RedirectToAction("Account", "Login");
            }
            return View(account);
        }


    }
}
