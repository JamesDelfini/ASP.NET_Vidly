using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly2.Models;
using Vidly2.ViewModels;

namespace Vidly2.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context;
        
        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        public ActionResult Index()
        {
            /* 
             * Include method is called Eager Loading.
             * To enable to load related objects.
             * Import Namespace using System.Data.Entity;
             * 
             * */
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);
        }

        public ActionResult Show(int? id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                HttpNotFound();

            return View(customer);
        }

        public ActionResult Create()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }
    
        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if(customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                // Using Automapper to maps data automatically.
                // To able to update a specific column, create a new class called Data Transfer Object. Example UpdateCustomerDto

                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.isSubscribeNewsLetter = customer.isSubscribeNewsLetter;

                // Do not use this approach, this will enable security holes.
                // TryUpdateModel(customerInDb, "", new string[] { "Name", "Email" });
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Customer");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel()
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }
    }
}