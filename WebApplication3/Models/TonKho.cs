namespace QuanLy_CircleK.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tonkho")]
    public partial class TonKho
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int STT { get; set; }

        [Required]
        [StringLength(50)]
        public string MaHangHoa { get; set; }

        public int SoNhap { get; set; }

        public int SoXuat { get; set; }

        public int SoTon { get; set; }
    }
}
