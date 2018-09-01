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
            var viewModel = new NewCustomerViewModel
            {
                MembershipTypes = membershipTypes
            };

            return View(viewModel);
        }
    }
}