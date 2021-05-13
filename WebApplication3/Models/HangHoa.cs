namespace QuanLy_CircleK.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("HangHoas")]
    public partial class HangHoa

    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "Mã Hàng Hóa Phải Ngắn hơn 50 ký tự")]
        [DisplayName("Mã Hàng Hóa")]
        public string MaHangHoa { get; set; }

        [DisplayName("Tên")]
        [Required]
        [StringLength(50)]
        public string TenHH { get; set; }

        [DisplayName("Đơn Giá")]
        public decimal DonGia { get; set; }

        [DisplayName("Đơn Vị")]
        [Required]
        [StringLength(50)]
        public string DonViTinh { get; set; }

        public int NhaCCId { get; set; }

        public bool Xoa { get; set; }

        public virtual NhaCC NhaCCs { get; set; }
    }
}
