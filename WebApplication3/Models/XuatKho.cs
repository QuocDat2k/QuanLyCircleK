namespace QuanLy_CircleK.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Xuat")]
    public partial class XuatKho
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int STT { get; set; }

        [Required]
        [StringLength(50)]
        public string MaPhieuXuat { get; set; }

        [Required]
        [StringLength(20)]
        public string MaHangHoa { get; set; }

        public int SoLuong { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayXuat { get; set; }

        [Required]
        [StringLength(50)]
        public string MaKhachHang { get; set; }
    }
}
