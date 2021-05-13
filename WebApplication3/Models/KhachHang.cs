namespace QuanLy_CircleK.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHangs")]
    public partial class KhachHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        [DisplayName("Mã Khách hàng")]
        public string MaKhachHang { get; set; }

        [StringLength(50)]
        [DisplayName("Tên khách hàng")] 
        public string TenKhachHang { get; set; }

        [StringLength(11)]
        [DisplayName("Số điện thoại")]
        public string SoDienThoai { get; set; }

        public bool Xoa { get; set; }
    }
}
