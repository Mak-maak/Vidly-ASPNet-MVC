using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Vidly1.Models;
using Vidly1.ViewModels;

namespace Vidly1.Controllers
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

        // GET: Customer
        public ActionResult Index()
        {
            //var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            //return View(/*customers*/);
            if (User.IsInRole(Roles.CanManageEntities))
                return View("List");

            return View("ReadOnlyList");
        }

        [Authorize]
        public ActionResult New()
        {
            if (User.IsInRole(Roles.CanManageEntities))
            {
                var memberships = _context.MembershipTypes.ToList();
                var customerFormViewModel = new CustomerFormViewModel()
                {
                    MembershipTypes = memberships
                };

                return View("CustomerForm", customerFormViewModel);
            }

            return View("ReadOnlyList");
        }

        // POST: Create Customer
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (User.IsInRole(Roles.CanManageEntities))
            {
                if (!ModelState.IsValid)
                {
                    CustomerFormViewModel viewModel = new CustomerFormViewModel(customer)
                    {
                        MembershipTypes = _context.MembershipTypes.ToList()
                    };

                    return View("CustomerForm", viewModel);
                }


                // adding new customer 
                if (customer.Id == 0)
                    _context.Customers.Add(customer);
                else
                {
                    // updating existing customer
                    var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == customer.Id);

                    // TryUpdateModel(customerInDb); alternative approach
                    // use auto mapper instead manually binding each of the properties
                    customerInDb.Name = customer.Name;
                    customerInDb.Birthdate = customer.Birthdate;
                    customerInDb.MembershipTypeId = customer.MembershipTypeId;
                    customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                }

                _context.SaveChanges();

                return RedirectToAction("Index", "Customer");
            }

            return View("ReadOnlyList");
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).Where(w => w.Id == id).FirstOrDefault();

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            if (User.IsInRole(Roles.CanManageEntities))
            {
                var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

                IEnumerable<MembershipType> membershipTypes = _context.MembershipTypes.ToList();
                var viewModel = new CustomerFormViewModel(customer)
                {
                    MembershipTypes = membershipTypes
                };

                return View("CustomerForm", viewModel);
            }

            return View("ReadOnlyList");
        }
    }
}