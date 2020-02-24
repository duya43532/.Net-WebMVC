using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVC.Models;
using System.Threading.Tasks;

namespace WebMVC.Controllers
{
    public class HelloController : Controller
    {
        // GET: Hello
        public async Task<ActionResult> Index(string Text = "End")
        {
            HelloViewModel mHelloModel = new HelloViewModel();
            mHelloModel.Text = await Task<string>.Run(() =>
            {
                return GetName();
            });
            mHelloModel.Index = 1;
            mHelloModel.Message = Text;//Server.HtmlEncode(Text);
            return View(mHelloModel);
        }

        private string GetName()
        {
            System.Threading.Thread.Sleep(500);
            return "Xiao Lin";
        }

        public ActionResult Another(int NumTime = 3)
        {
            ViewBag.Message = "Hello World";
            ViewBag.NumTime = NumTime;
            return View();
        }
    }
}