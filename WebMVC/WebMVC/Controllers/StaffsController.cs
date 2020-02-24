using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebMVC.Models;
using PagedList.Mvc;
using PagedList;

namespace WebMVC.Controllers
{
    public class StaffsController : Controller
    {
        private StaffDbContext db = new StaffDbContext();

        public ActionResult AutoComplete(string term)
        {
            var model = db.Staffs.Where(m => m.Name.StartsWith(term))
                .Take(10)
                .Select(m => new { label = m.Name });
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(string searchName, string searchDepartment, int page = 1)
        {
            var DepartmentList = new List<string>();
            var Departments = from d in db.Staffs orderby d.Department select d.Department;
            DepartmentList.AddRange(Departments);
            ViewBag.searchDepartment = new SelectList(Departments.Distinct());

            var staffs = from m in db.Staffs select m;
            if (searchDepartment != "所有" && !string.IsNullOrEmpty(searchDepartment))
            {
                staffs = staffs.Where(o => o.Department == searchDepartment);
            }
            if (!string.IsNullOrEmpty(searchName))
            {
                staffs = staffs.Where(o => o.Name.Contains(searchName));
            }
            var staffList = staffs.OrderBy(o => o.Name).ToPagedList(page, 3);

            if (Request.IsAjaxRequest())
            {
                return PartialView("P_StaffList", staffList);
            }
            return View(staffList);
        }

        // GET: Staffs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest,"Error url");
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // GET: Staffs/Create
        public ActionResult Create()
        {
            Staff staff = new Staff();
            return View(staff);
        }

        // POST: Staffs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Sex,Department,PhoneNumber,EnterDate")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                if (staff.Sex != "男" && staff.Sex != "女")
                {
                    staff.SexJudge = "性别填写存在错误";
                }
                //电话号码判断
                Regex RegMobilePhone = new Regex("^1[3|5|8][0-9]{9}$");
                if (staff.PhoneNumber.Length != 11 || !RegMobilePhone.IsMatch(staff.PhoneNumber))
                {
                    staff.PhoneNumberJudge = "电话号码填写存在错误,必须以13、15、18开头";
                }
                //页面返回
                if (staff.SexJudge != null || staff.PhoneNumberJudge != null)
                {
                    return View(staff);
                }
                else
                {
                    db.Staffs.Add(staff);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(staff);
        }

        // GET: Staffs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);//相当于一个新的对象了
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Sex,Department,PhoneNumber,EnterDate")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                //性别判断
                if (staff.Sex != "男" && staff.Sex != "女")
                {
                    staff.SexJudge = "性别填写存在错误";
                }
                //电话号码判断
                Regex RegMobilePhone = new Regex("^1[3|5|8][0-9]{9}$");
                if (staff.PhoneNumber.Length != 11 || !RegMobilePhone.IsMatch(staff.PhoneNumber))
                {
                    staff.PhoneNumberJudge = "电话号码填写存在错误";
                }
                //页面返回
                if (staff.SexJudge != null || staff.PhoneNumberJudge != null)
                {
                    return View(staff);
                }
                else
                {
                    db.Entry(staff).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(staff);
        }

        // GET: Staffs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Staff staff = db.Staffs.Find(id);
            db.Staffs.Remove(staff);
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
    }
}
