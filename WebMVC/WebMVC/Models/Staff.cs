using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models
{
    /// <summary>
    /// 员工
    /// </summary>
    public class Staff: JudgeInfo
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        [Display(Name = "员工姓名")]
        [DataType(DataType.Text)]
        [Required]
        public string Name { get; set; } 
        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        [Required]
        public string Sex { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        [Display(Name="部门")]
        [Required]
        public string Department { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        [Display(Name = "电话号码")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 入职日期
        /// </summary>
        [Display(Name = "入职日期")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}")]
        public DateTime EnterDate { get; set; } = DateTime.Now;
    }

    public class StaffDbContext : DbContext
    {
        public DbSet<Staff> Staffs { get; set; }
    }

    public class JudgeInfo
    {
        /// <summary>
        /// 性别判断信息
        /// </summary>
        public string SexJudge;
        /// <summary>
        /// 电话号码判断信息
        /// </summary>
        public string PhoneNumberJudge;
        /// <summary>
        /// 日期判断
        /// </summary>
        public string DateTimeJudge;
    }
}