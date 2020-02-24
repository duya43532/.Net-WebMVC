using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebMVC.Models
{
    public class HelloViewModel
    {
        public string Text { get; set; }
        public int Index { get; set; }
        public string Message { get; set; }
    }

    public class HelloDBContext : DbContext
    {
        public DbSet<HelloViewModel> HelloModel { get; set; }
    }
}