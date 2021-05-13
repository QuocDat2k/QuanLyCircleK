using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLy_CircleK.Models
{

    [Table("Roles")]
    public class Role
    {
        [Key]
        public string RoleID { get; set; }    
        
      [StringLength(20)]
        public string RoleName { get; set; }


    }
    
}