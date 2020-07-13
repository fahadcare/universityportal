using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultManagmentSystem.Models;

namespace UniversityCourseAndResultManagmentSystem.Controllers
{
    public class StudentController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: /Student/
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.d);
            return View(students.ToList());
        }

        // GET: /Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: /Student/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DeptName");
            return View();
        }

        // POST: /Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="StudentId,StudentName,Email,ContactNo,Date,Address,DepartmentId")] Student student)
        {
            if (ModelState.IsValid)
            {
                student.RegistrationId = RegNo(student);
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DeptName", student.DepartmentId);
            return View(student);
        }

        // GET: /Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DeptName", student.DepartmentId);
            return View(student);
        }

        // POST: /Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="StudentId,StudentName,Email,ContactNo,Date,Address,DepartmentId")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DeptCode", student.DepartmentId);
            return View(student);
        }

        // GET: /Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: /Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private string RegNo(Student student)
        {
            int id = db.Students.Count(s => (s.DepartmentId == student.DepartmentId)
                    && (s.Date.Year == student.Date.Year)) + 1;
            Department aDepartment = db.Departments.Where(d => d.DepartmentId == student.DepartmentId).FirstOrDefault();
            string registrationId = aDepartment.DeptCode + "-" + student.Date.Year + "-";

            string addZero = "";
            int len = 3 - id.ToString().Length;
            for (int i = 0; i < len; i++)
            {
                addZero = "0" + addZero;
            }

            return registrationId + addZero + id;
        }

        public JsonResult IsStudentEmailAvailable(string Email)
        {
            var teach = db.Students.ToList();
            if (!teach.Any(x => x.Email == Email.ToUpper()))
            {
                // This show the error message of validation and stop the submit of the form
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // This will ignore the validation and the submit of the form is gone to take place.
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
