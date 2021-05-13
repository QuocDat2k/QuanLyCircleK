using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using QuanLy_CircleK.Models;

namespace QuanLy_CircleK.Models
{

    [Table("Accouts")]
    public class Account
    {
        [Key]
        public string UserName { get; set; }
        [StringLength(20)]
        public string PassWord { get; set; }
        public string RoleID { get; set; }

    }
}

    
 
