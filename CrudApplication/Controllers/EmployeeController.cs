using CrudApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApplication.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationContext context;

        public EmployeeController(ApplicationContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var res = context.Employees.ToList();
            return View(res);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee obj)
        {
            if (ModelState.IsValid)
            {
                var emp = new Employee()
                {
                    Name = obj.Name,
                    City = obj.City,
                    State = obj.State,
                    Salary = obj.Salary,

                };
                context.Employees.Add(obj);
                context.SaveChanges();
                TempData["Error"] = " Record Save";
                return RedirectToAction("index");
            }
            else
            {
                TempData["error"] = "empty field con't submit";
                return View(obj);
            }
            
        }
        public IActionResult Edit(int? id)
        {
            var Et = context.Employees.Where(x => x.Id == id).FirstOrDefault();
            return View(Et);
        }
        [HttpPost]
        public IActionResult Edit(Employee a)
        {
            context.Entry(a).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            TempData["Error"] = "Record updated";
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var Dt = context.Employees.Where(x => x.Id == id).FirstOrDefault();
            context.Employees.Remove(Dt);
            context.SaveChanges();
            TempData["Error"] = "Record Deleted";
            return RedirectToAction("index");
        }
    }
}
