namespace QuanLy_CircleK.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Nhaccs")]
    public partial class NhaCC
    {
        public NhaCC()
        {
            HangHoas = new HashSet<HangHoa>();
        }
        [Key]
        [DisplayName("Mã")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Ma { get; set; }

        [DisplayName("Tên Nhà Cung Cấp")]
        [StringLength(50)]
        public string TenNCC { get; set; }

        [DisplayName("Số Điện Thoại")]
        [StringLength(11)]
        public string SoDienThoai { get; set; }

        public bool Xoa { get; set; }

        public ICollection<HangHoa> HangHoas { get; set; }
    }
}
