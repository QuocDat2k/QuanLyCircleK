namespace QuanLy_CircleK.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("nhaps")]
    public partial class NhapKho
    {
        [Key]
        [StringLength(10)]
        public string STT { get; set; }

        [StringLength(50)]
        public string MaPhieuNhap { get; set; }

        [StringLength(10)]
        public string MaHangHoa { get; set; }

        public int? SoLuong { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayNhap { get; set; }

        [StringLength(10)]
        public string MaNCCS { get; set; }
    }
}
