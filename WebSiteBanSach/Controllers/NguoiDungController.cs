using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using WebSiteBanSach.Models;

namespace WebSiteBanSach.Controllers
{
    [Authorize]
    public class NguoiDungController : Controller
    {

        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        // GET: /NguoiDung/
       public UserManager<MyFistWed.Models.ApplicationUser> UserManager { get; private set; }
        [HttpGet]
        [AllowAnonymous]
      

        public ActionResult DangKy()
        {

            return View();
        }
        [HttpPost]
       [ValidateAntiForgeryToken]
        public ActionResult DangKy(KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                //Chèn dữ liệu vào bảng khách hàng
                db.KhachHangs.Add(kh);
                //Lưu vào csdl 
                db.SaveChanges();
            }
            return View();
        }

        [HttpGet]
      
         public ActionResult LoginPartial()
         {
             return PartialView();
         }
    
       
         [HttpPost]
         [AllowAnonymous]
        public ActionResult DangNhap(FormCollection f)
        {
            string sTaiKhoan = f["txtTaiKhoan"].ToString();
            string sMatKhau = f.Get("txtMatKhau").ToString();
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);
            if (kh != null)
            {
                ViewBag.ThongBao = "Chúc mừng bạn đăng nhập thành công!";
                Session["TaiKhoan"] = kh;
                return View();
            }
            ViewBag.ThongBao = "Tên tài khoản hoặc mật khẩu không đúng!";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
       public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
          [AllowAnonymous]
          public ActionResult DangNhap()
        {

            return View();
        }
       
        #region helper
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        #endregion
    }
}   