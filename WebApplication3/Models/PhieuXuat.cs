namespace QuanLy_CircleK.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("phieuxuat")]
    public partial class PhieuXuat
    {
        [Key]
        [StringLength(10)]
        public string MaPhieuXuat { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayTao { get; set; }

        [Required]
        [StringLength(50)]
        public string MaKhachHang { get; set; }
    }
}
