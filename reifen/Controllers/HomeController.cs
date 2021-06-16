using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using reifen.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace reifen.Controllers
{
    public class HomeController : Controller
    {
        private readonly ReifenContext context;

        public HomeController(ReifenContext _context)
        {
            context = _context;
        }

        public IActionResult ResetPersonal(int PersonalId)
        {
            var model = context.Personals.Find(PersonalId);
            model.Password = "123";
            context.SaveChanges();
            return RedirectToAction("ListPersonal","Home");
        }

        public IActionResult MyInfo()
        {
            string username = Request.Cookies["username"];
          
            var model = context.Personals.Where(p => p.UserName == username).FirstOrDefault();
            ViewBag.Name = model.Name;
            ViewBag.LastName = model.LastName;
            ViewBag.PersonalId = model.PersonalId;
            ViewBag.UserName = model.UserName;
            return View();
        }

        [HttpPost]
        public IActionResult MyInfo(string name, string lastname,string password,int personalId)
        {

            var model = context.Personals.Find(personalId);
            model.LastName = lastname;
            model.Name = name;
            model.Password = password;
            context.SaveChanges();
            return RedirectToAction("Index","Home");
        }

        public IActionResult DeletePark(int Id)
        {
            var model = context.Reminders.Find(Id);
            context.Reminders.Remove(model);
            context.SaveChanges();
            return RedirectToAction("ListPark", "Home");
        }
        public IActionResult ListPark()
        {
            return View(context.Reminders.OrderByDescending(p=>p.EndDate).ToList());
        }

        public IActionResult CreatePark()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePark(Reminder model)
        {
            if (ModelState.IsValid)
            {
                model.Plate = model.Plate.ToUpper();
                context.Reminders.Add(model);
                context.SaveChanges();
                return RedirectToAction("ListPark", "Home");
            }
            return View(model);
        }

        public IActionResult EditPark(int Id)
        {
            var model = context.Reminders.Find(Id);
            return View(model);
        }

        [HttpPost]
        public IActionResult EditPark(Reminder model)
        {

            if (ModelState.IsValid)
            {
                var data = context.Reminders.Find(model.Id);
                data.City = model.City;
                data.Column = model.Column;
                data.EndDate = model.EndDate;
                data.GSM = model.GSM;
                data.Lastname = model.Lastname;
                data.Name = model.Name;
                data.Plate = model.Plate;
                data.Postal = model.Postal;
                data.Row = model.Row;
                data.Street = model.Street;
                data.WareHouse = model.WareHouse;
                data.Email = model.Email;
                context.SaveChanges();
                return RedirectToAction("ListPark", "Home");
            }
            return View(model);
        }

        public IActionResult AddStock(int productId, int quantity)
        {
            var model = context.Products.Find(productId);
            model.StockQuantity += quantity;
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult MinusStock(int mproductId, int quantity)
        {
            var model = context.Products.Find(mproductId);
            model.StockQuantity -= quantity;
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoadProducts(string productId)
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                // Skiping number of Rows count
                var start = Request.Form["start"].FirstOrDefault();
                // Paging Length 10,20
                var length = Request.Form["length"].FirstOrDefault();
                // Sort Column Name
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                // Sort Column Direction ( asc ,desc)
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                // Search Value from (Search box)
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10,20,50,100)
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all Customer data
                var customerData = (from tempcustomer in context.Products
                                    select new
                                    {
                                        ProductId = tempcustomer.ProductId,
                                        Dimensions = tempcustomer.Dimensions,
                                        EKNetto = tempcustomer.EKNetto,
                                        EKBrutto = tempcustomer.EKBrutto,
                                        VKNetto = tempcustomer.VKNetto,
                                        VKBrutto = tempcustomer.VKBrutto,
                                        StockQuantity = tempcustomer.StockQuantity,
                                        Brand = tempcustomer.Brand.Name
                                    });

                //Sorting
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    //customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection);
                }
                //Search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(m => m.Dimensions.Contains(searchValue) || m.Brand.Contains(searchValue));
                }

                //total number of rows count 
                recordsTotal = customerData.Count();
                //Paging 
                var data = customerData.Skip(skip).Take(pageSize).ToList();

                //Returning Json Data
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }

        }

        public IActionResult CreateProduct()
        {
            ViewBag.Brands = new SelectList(context.Brands.ToList(), "BrandId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Dimensions = product.Dimensions.ToUpper();
                context.Products.Add(product);
                context.SaveChanges();
                return RedirectToAction("ListProduct", "Home");
            }
            ViewBag.Brands = new SelectList(context.Brands.ToList(), "BrandId", "Name");
            return View(product);
        }

        public IActionResult ListProduct()
        {
            var list = context.Products.Include(prop => prop.Brand).ToList();
            return View(list);
        }

        public IActionResult EditProduct(int ProductId)
        {
            var model = context.Products.Where(p => p.ProductId == ProductId).Include(p => p.Brand).FirstOrDefault();
            ViewBag.Brands = new SelectList(context.Brands.ToList(), "BrandId", "Name", model.BrandId);
            return View(model);
        }

        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                var model = context.Products.Where(p => p.ProductId == product.ProductId).FirstOrDefault();
                model.BrandId = product.BrandId;
                model.Dimensions = product.Dimensions;
                model.EKNetto = product.EKNetto;
                model.EKBrutto = product.EKBrutto;
                model.VKNetto = product.VKNetto;
                model.VKBrutto = product.VKBrutto;
                model.StockQuantity = product.StockQuantity;
                context.SaveChanges();
                return RedirectToAction("ListProduct", "Home");
            }
            ViewBag.Brands = new SelectList(context.Brands.ToList(), "BrandId", "Name", product.BrandId);
            return View(product);
        }

        public IActionResult DeleteProduct(int ProductId)
        {
            var model = context.Products.Find(ProductId);
            context.Products.Remove(model);
            context.SaveChanges();
            return RedirectToAction("ListProduct", "Home");
        }

        [HttpPost]
        public IActionResult CheckUser(string UserName)
        {
            string result = string.Empty;
            var model = context.Personals.Where(p => p.UserName == UserName).FirstOrDefault();
            if (model != null)
            {
                result = "Existing";
            }
            else
            {
                result = "Success";
            }
            return Content(result);
        }

        public IActionResult CreatePersonal()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePersonal(Personal personal)
        {
            if (ModelState.IsValid)
            {
                personal.isActive = true;
                context.Personals.Add(personal);
                context.SaveChanges();
                return RedirectToAction("ListPersonal", "Home");
            }
            return View(personal);
        }

        public IActionResult ListPersonal()
        {
            return View(context.Personals.ToList());
        }

        public IActionResult EditPersonal(int PersonalId)
        {
            var model = context.Personals.Find(PersonalId);
            return View(model);
        }

        [HttpPost]
        public IActionResult EditPersonal(Personal personal)
        {
            if (ModelState.IsValid)
            {
                var model = context.Personals.Find(personal.PersonalId);
                model.Email = personal.Email;
                model.LastName = personal.LastName;
                model.Name = personal.Name;
                model.Password = personal.Password;
                model.UserName = personal.UserName;
                model.isActive = personal.isActive;
                context.SaveChanges();

                return RedirectToAction("ListPersonal", "Home");
            }
            return View(personal);
        }




    }
}
