namespace QuanLy_CircleK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accouts",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 128),
                        PassWord = c.String(maxLength: 20),
                        RoleID = c.String(),
                    })
                .PrimaryKey(t => t.UserName);
            
            CreateTable(
                "dbo.HangHoas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaHangHoa = c.String(maxLength: 50),
                        TenHH = c.String(nullable: false, maxLength: 50),
                        DonGia = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DonViTinh = c.String(nullable: false, maxLength: 50),
                        NhaCCId = c.Int(nullable: false),
                        Xoa = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nhaccs", t => t.NhaCCId, cascadeDelete: true)
                .Index(t => t.NhaCCId);
            
            CreateTable(
                "dbo.Nhaccs",
                c => new
                    {
                        Ma = c.Int(nullable: false, identity: true),
                        TenNCC = c.String(maxLength: 50),
                        SoDienThoai = c.String(maxLength: 11),
                        Xoa = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Ma);
            
            CreateTable(
                "dbo.KhachHangs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaKhachHang = c.String(maxLength: 50),
                        TenKhachHang = c.String(maxLength: 50),
                        SoDienThoai = c.String(maxLength: 11),
                        Xoa = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.nhaps",
                c => new
                    {
                        STT = c.String(nullable: false, maxLength: 10),
                        MaPhieuNhap = c.String(maxLength: 50),
                        MaHangHoa = c.String(maxLength: 10),
                        SoLuong = c.Int(),
                        NgayNhap = c.DateTime(storeType: "date"),
                        MaNCCS = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.STT);
            
            CreateTable(
                "dbo.phieunhap",
                c => new
                    {
                        MaPhieuNhap = c.Int(nullable: false),
                        NgayTao = c.DateTime(nullable: false, storeType: "date"),
                        MaNCC = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.MaPhieuNhap);
            
            CreateTable(
                "dbo.phieuxuat",
                c => new
                    {
                        MaPhieuXuat = c.String(nullable: false, maxLength: 10),
                        NgayTao = c.DateTime(nullable: false, storeType: "date"),
                        MaKhachHang = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.MaPhieuXuat);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.String(nullable: false, maxLength: 128),
                        RoleName = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.tonkho",
                c => new
                    {
                        STT = c.Int(nullable: false),
                        MaHangHoa = c.String(nullable: false, maxLength: 50),
                        SoNhap = c.Int(nullable: false),
                        SoXuat = c.Int(nullable: false),
                        SoTon = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.STT);
            
            CreateTable(
                "dbo.Xuat",
                c => new
                    {
                        STT = c.Int(nullable: false),
                        MaPhieuXuat = c.String(nullable: false, maxLength: 50),
                        MaHangHoa = c.String(nullable: false, maxLength: 20),
                        SoLuong = c.Int(nullable: false),
                        NgayXuat = c.DateTime(nullable: false, storeType: "date"),
                        MaKhachHang = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.STT);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HangHoas", "NhaCCId", "dbo.Nhaccs");
            DropIndex("dbo.HangHoas", new[] { "NhaCCId" });
            DropTable("dbo.Xuat");
            DropTable("dbo.tonkho");
            DropTable("dbo.Roles");
            DropTable("dbo.phieuxuat");
            DropTable("dbo.phieunhap");
            DropTable("dbo.nhaps");
            DropTable("dbo.KhachHangs");
            DropTable("dbo.Nhaccs");
            DropTable("dbo.HangHoas");
            DropTable("dbo.Accouts");
        }
    }
}
