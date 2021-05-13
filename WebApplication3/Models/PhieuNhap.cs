namespace QuanLy_CircleK.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("phieunhap")]
    public partial class PhieuNhap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaPhieuNhap { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayTao { get; set; }

        [Required]
        [StringLength(50)]
        public string MaNCC { get; set; }
    }
}
