using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLy_CircleK.Models;

namespace QuanLy_CircleK.Controllers
{

    public class HangHoaController : Controller
    {
        private NNTDbContext db = new NNTDbContext();

        // GET: HangHoa
        public ActionResult Index()
        {
            var listHangHoa = db.HangHoas.Where(x => !x.NhaCCs.Xoa && !x.Xoa).ToList();
            return View(listHangHoa);
        }

        // GET: HangHoa/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangHoa hangHoa = db.HangHoas.FirstOrDefault(x => !x.NhaCCs.Xoa && !x.Xoa && x.Id == id);
            if (hangHoa == null)
            {
                return HttpNotFound();
            }
            return View(hangHoa);
        }

        // GET: HangHoa/Create
        [Authorize]
        public ActionResult Create()
        {
            var listNCC = db.NhaCCs.Where(x => !x.Xoa).ToList();
            if(listNCC.Count() == 0)
            {
                return RedirectToAction("Index", "NhaCCs");
            }
            SelectList stlNCC = new SelectList(listNCC, "Ma", "TenNCC");
            ViewBag.Nhacc = stlNCC;
            return View();
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHangHoa,TenHH,DonGia,DonViTinh,NhaCCId")] HangHoa hangHoa)
        {
            if (ModelState.IsValid || hangHoa.DonGia <=0)
            {
                if(hangHoa.NhaCCId == 0)
                {
                    return HttpNotFound();
                }
                db.HangHoas.Add(hangHoa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var listNCC = db.NhaCCs.Where(x => !x.Xoa).ToList();
            if (listNCC.Count() == 0)
            {
                return RedirectToAction("Index", "NhaCCs");
            }
            SelectList stlNCC = new SelectList(listNCC, "Ma", "TenNCC");
            ViewBag.Nhacc = stlNCC;

            return View(hangHoa);
        }

        // GET: HangHoa/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangHoa hangHoa = db.HangHoas.FirstOrDefault(x => !x.NhaCCs.Xoa && !x.Xoa && x.Id == id);
            var listNCC = db.NhaCCs.Where(x => !x.Xoa).ToList();
            SelectList stlNCC = new SelectList(listNCC, "Ma", "TenNCC");
            ViewBag.Nhacc = stlNCC;

            if (hangHoa == null)
            {
                return HttpNotFound();
            }
            return View(hangHoa);
        }

        // POST: HangHoa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MaHangHoa,TenHH,DonGia,DonViTinh,NhaCCId")] HangHoa hangHoa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hangHoa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hangHoa);
        }

        // GET: HangHoa/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangHoa hangHoa = db.HangHoas.FirstOrDefault(x => !x.NhaCCs.Xoa && !x.Xoa && x.Id == id);
            if (hangHoa == null)
            {
                return HttpNotFound();
            }
            return View(hangHoa);
        }

        // POST: HangHoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HangHoa hangHoa = db.HangHoas.FirstOrDefault(x => !x.NhaCCs.Xoa && !x.Xoa && x.Id == id);
            db.Entry(hangHoa).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [Authorize]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return HttpNotFound();
            }
            string _FileName = "a.xls";
            //duong dan luu file
            string _path = Path.Combine(Server.MapPath("~/Uploads/Excels"), _FileName);
            //luu file len server
            file.SaveAs(_path);
            //đọc dữ liệu từ file excel trả về dạng datatable
            DataTable dt = ReadDataFromExcelFile(_path);
            //ghi dữ liệu từ datatable ở bước trên vào sql
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                HangHoa hh = new HangHoa();
                hh.MaHangHoa = dt.Rows[i][0].ToString();
                hh.TenHH = dt.Rows[i][1].ToString();
                hh.DonGia = Convert.ToDecimal(dt.Rows[i][2].ToString());
                hh.DonViTinh = dt.Rows[i][3].ToString();
                hh.NhaCCId = int.Parse(dt.Rows[i][4].ToString());

                db.HangHoas.Add(hh);
                db.SaveChanges();

            }
            return RedirectToAction("Index");
        }
        public DataTable ReadDataFromExcelFile(string filepath)
        {
            string connectionString = "";
            string fileExtention = filepath.Substring(filepath.Length - 4).ToLower();
            if (fileExtention.IndexOf("xlsx") == 0)
            {
                connectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source =" + filepath + ";Extended Properties=\"Excel 12.0 Xml;HDR=NO\"";
            }
            else if (fileExtention.IndexOf(".xls") == 0)
            {
                connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties=Excel 8.0";
            }

            // Tạo đối tượng kết nối
            OleDbConnection oledbConn = new OleDbConnection(connectionString);
            DataTable data = null;
            try
            {
                // Mở kết nối
                oledbConn.Open();

                // Tạo đối tượng OleDBCommand và query data từ sheet có tên "Sheet1"
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn);

                // Tạo đối tượng OleDbDataAdapter để thực thi việc query lấy dữ liệu từ tập tin excel
                OleDbDataAdapter oleda = new OleDbDataAdapter();

                oleda.SelectCommand = cmd;

                // Tạo đối tượng DataSet để hứng dữ liệu từ tập tin excel
                DataSet ds = new DataSet();

                // Đổ đữ liệu từ tập excel vào DataSet
                oleda.Fill(ds);

                data = ds.Tables[0];
            }
            catch
            {
            }
            finally
            {
                // Đóng chuỗi kết nối
                oledbConn.Close();
            }
            return data;
        }
        //copy large data from datatable to sqlserver
        private void CopyDataByBulk(DataTable dt)
        {
            //lay ket noi voi database luu trong file webconfig
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB Context"].ConnectionString);
            SqlBulkCopy bulkcopy = new SqlBulkCopy(con);
            bulkcopy.DestinationTableName = "Ten table";
            bulkcopy.ColumnMappings.Add(0, "ten cot 1");
            bulkcopy.ColumnMappings.Add(1, "ten cot 2");
            bulkcopy.ColumnMappings.Add(2, "ten cot 3");
            con.Open();
            bulkcopy.WriteToServer(dt);
            con.Close();
        }
    }
}
