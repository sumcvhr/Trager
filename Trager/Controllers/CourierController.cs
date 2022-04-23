using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Trager.Models;
using Trager.EmailServices;
using Trager.Extensions;
using Trager.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace Trager.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class CourierController : BaseController
    {

       

       
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private CourierIEmailSender _courieremailSender;
        private object url;
        private IdentityUser vcourier;
        private readonly ApplicationContext _context;
        public CourierController(ApplicationContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, CourierIEmailSender courieremailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _courieremailSender = courieremailSender;
            _context = context;
        }
        public IActionResult CourierLogin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CourierLogin(CourierLoginModel model, object sender, EventArgs e)
        {
            var courier = new Courier()
            {
                CourierEmail = model.CourierEmail,
                CourierPassword = model.CourierPassword,
                EmailConfirmed = model.EmailConfirmed,
            };

           

            var UserCourier = _context.Courier.FirstOrDefault(P => P.CourierEmail == courier.CourierEmail && P.CourierPassword == courier.CourierPassword);
            if (UserCourier != null)
            {
                HttpContext.Session.SetString("oturum", JsonConvert.SerializeObject(UserCourier));

                //HttpContext.Session.SetString("oturum",JsonConvert.DeSerializeObject(Object"oturum"));

                //    if (courier.EmailConfirmed == true) { 
                //    return RedirectToAction("CourierHome", "Courier");
                //    }
                //    else 
                //    {
                //        ViewBag.Message = "Lütfen Hesabınızı Onaylayınız";
                //        return View();
                //    }
                return RedirectToAction("CourierHome", "Courier");
            }

            else
            {
                ViewBag.Message = "Geçersiz Kullanıcı Adı yada Parola";
                return View();
            }

        }

        [HttpGet]
        public IActionResult CourierRegister()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CourierRegister(CourierRegisterModel model, List<IFormFile> CourierAvatar, int Id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string filePath = "";

            var courier = new Courier()
            {
                CourierName = model.CourierName,
                CourierLastName = model.CourierLastName,
                CourierPhone = model.CourierPhone,
                CourierEmail = model.CourierEmail,
                CourierPassword = model.CourierPassword,
                CourierRePassword = model.CourierRePassword,
                GonderimUcreti = model.GonderimUcreti,
                AvatarAdress = filePath,
            };

            // email

            var kayitvarmı = _context.Courier.FirstOrDefault(P => P.CourierEmail == courier.CourierEmail && P.CourierPhone == courier.CourierPhone);
            if (kayitvarmı == null)
            {


                foreach (var dosya in CourierAvatar)
                {
                    if (dosya.Length > 0)
                    {
                        string CAvatar = "Avatar" +
                            Guid.NewGuid().ToString().Substring(0, 4);
                        string Avatar = CAvatar + "Avatar" +
                            dosya.FileName;
                        var ustklasor = Directory.GetParent(Directory.GetCurrentDirectory());
                        string dosyaAdiOnEk = courier.CourierEmail.Replace('@', '_').Replace('.', '_').ToString();
                        string dosyaAdi = dosyaAdiOnEk + "_" + dosya.FileName;
                        string dosyaYolu = Path.Combine(ustklasor.FullName + "/Trager", $"wwwroot/Avatar/{dosyaAdi}");
                        using (var stream = System.IO.File.Create(dosyaYolu))
                        {
                            dosya.CopyTo(stream);
                            courier.AvatarAdress = "/Avatar/" + dosyaAdi;
                        }

                    }
                }

                _context.Courier.Add(courier);
                _context.SaveChanges();
            }

            ModelState.AddModelError("", "Bilinmeyen hata oldu lütfen tekrar deneyiniz.");
            return RedirectToAction("CourierLogin", "Courier");
        }
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("oturum");
            //await _signInManager.SignOutAsync();
            //TempData.Put("message", new AlertMessage()
            //{
            //    Title = "Oturum Kapatıldı.",
            //    Message = "Hesabınız güvenli bir şekilde kapatıldı.",
            //    AlertType = "warning"
            //});
            return RedirectToAction("Index", "Home");
        }

        //public async Task<IActionResult> ConfirmEmail(CourierHomeVM model)
        //{

        //    if (model.courier.Id == null || model.CourierToken.Token == null)
        //    {
        //        TempData.Put("message", new AlertMessage()
        //        {
        //            Title = "Geçersiz token.",
        //            Message = "Geçersiz Token",
        //            AlertType = "danger"
        //        });
        //        return View();
        //    }
        //    if (model.courier.Id != null)
        //    {
        //        //var result = _context.(courier, token);
        //        //if (result.Succeeded)
        //        //{
        //        //    TempData.Put("message", new AlertMessage()
        //        //    {
        //        //        Title = "Hesabınız onaylandı.",
        //        //        Message = "Hesabınız onaylandı.",
        //        //        AlertType = "success"
        //        //    });
        //            return View();
        //        //}
        //    }
        //    //TempData.Put("message", new AlertMessage()
        //    //{
        //    //    Title = "Hesabınızı onaylanmadı.",
        //    //    Message = "Hesabınızı onaylanmadı.",
        //    //    AlertType = "warning"
        //    ////});
        //    //return View();
        //}
        [HttpGet]
        public IActionResult CourierForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CourierForgotPassword(string CourierEmail)
        {
            if (string.IsNullOrEmpty(CourierEmail))
            {
                return View();
            }

            var courier = await _userManager.FindByEmailAsync(CourierEmail);

            if (courier == null)
            {
                return View();
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(courier);

            var url = Url.Action("CourierResetPassword", "Courier", new
            {
                courierId = courier.Id,
                token = code
            });

            // email
            await _courieremailSender.SendEmailAsync(CourierEmail, "Parolamı Unuttum", $"Parolanızı yenilemek için linke <a href='https://localhost:44362{url}'>tıklayınız.</a>");

            return View();
        }
        [HttpGet]
        public IActionResult CourierResetPassword(string courierId, string token)
        {
            if (courierId == null || token == null)
            {
                return RedirectToAction("Courier", "CourierHome");
            }

            var model = new CourierResetPasswordModel { Token = token };

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CourierResetPassword(CourierResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var courier = await _userManager.FindByEmailAsync(model.CourierEmail);
            if (courier == null)
            {
                return RedirectToAction("Courier", "CourierHome");
            }

            var result = await _userManager.ResetPasswordAsync(courier, model.Token, model.CourierPassword);

            if (result.Succeeded)
            {
                return RedirectToAction("CourierLogin", "Courier");
            }

            return View(model);
        }
        public IActionResult CourierHome(int id)
        {
            CourierHomeVM veri = new CourierHomeVM();
            veri.Il = _context.Il.ToList();
            string oturumBilgi = HttpContext.Session.GetString("oturum");
            veri.courier = JsonConvert.DeserializeObject<Models.Courier>(oturumBilgi);

            veri.comment = _context.CourierComment.Include("User").Where(p => p.CourierId==OturumBlgi.Id).ToList();
            veri.Konum = _context.CourierLocation.Include("Ilce").Include(k => k.Ilce.Il).Where(P => P.CourierId == OturumBlgi.Id).ToList();
            return View("CourierHome", veri);

        }

        [HttpGet]
        public ActionResult ilcegetir(int Id)
        {
            var ilceListe = _context.Ilce.Where(P => P.IlId == Id).ToList();
            ViewBag.Ilceler = ilceListe;
            return PartialView("_ilceliste", ilceListe);
        }
        [HttpGet]
        public IActionResult CourierSettings()
        {

            return View(OturumBlgi);
        }
        [HttpPost]
        public IActionResult CourierSettings(Courier model)
        {
            var GuncellenecekKayit = _context.Courier.FirstOrDefault(P => P.Id == OturumBlgi.Id);

            GuncellenecekKayit.CourierName = model.CourierName;
            GuncellenecekKayit.CourierLastName = model.CourierLastName;
            GuncellenecekKayit.CourierEmail = model.CourierEmail;
            GuncellenecekKayit.CourierPhone = model.CourierPhone;
            GuncellenecekKayit.GonderimUcreti = model.GonderimUcreti;


            _context.SaveChanges();
            HttpContext.Session.SetString("oturum", JsonConvert.SerializeObject(GuncellenecekKayit));
            return RedirectToAction("CourierSettings");
        }
        [HttpPost]
        public IActionResult CourierPasswordSettings(Courier model, string eskiparola)
        {
            var GuncellenecekKayit = _context.Courier.FirstOrDefault(P => P.Id == OturumBlgi.Id);
            if (GuncellenecekKayit.CourierPassword == eskiparola)
            {
                GuncellenecekKayit.CourierPassword = model.CourierPassword;
            }

            _context.SaveChanges();
            return RedirectToAction("CourierSettings");
        }
       


        public void mailgonder(CourierRegisterModel model)
        {

            SmtpClient sc = new SmtpClient();
            sc.Port = 587;
            sc.Host = "smtp.office365.com";
            sc.EnableSsl = true;
            sc.Credentials = new NetworkCredential("Trager240@hotmail.com", "123456Ssc");
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("Trager240@hotmail.com", "üye kayıt");
            mail.To.Add(model.CourierEmail);
            mail.Subject = "üye kayıt";
            mail.IsBodyHtml = true;
            mail.Body = "Hesap Onay Maili"+" Lütfen email hesabınızı onaylamak için linke <a href='https://localhost:5001{url}'>tıklayınız.</a>";
            sc.Send(mail);
        }

        [HttpPost]
        public IActionResult CourierKonum(CourierLocation model)
        {
          
            if (OturumBlgi != null)
            {
                    model.CourierId = OturumBlgi.Id;
                    _context.CourierLocation.Add(model);
                    _context.SaveChanges();
            }
            return RedirectToAction("CourierHome");
        }
        public IActionResult LocationDelete(int Id)
        {
            var SilinecekKayit = _context.CourierLocation.FirstOrDefault(P => P.Id == Id);
            if (SilinecekKayit != null && SilinecekKayit.CourierId == OturumBlgi.Id)
            {
                _context.CourierLocation.Remove(SilinecekKayit);
                _context.SaveChanges();
            }

            return RedirectToAction("CourierHome");
        }

        [HttpPost]
        public IActionResult CourierComment(CourierComment model)
        {
            if (UserOturumBlgi != null)
            {
                model.UserId = UserOturumBlgi.Id;
                _context.CourierComment.Add(model);
                _context.SaveChanges();
            }

            return RedirectToAction("CourierProfile", new { Id = model.CourierId });
        }

        [HttpGet]
        public ActionResult CourierComment()
        {

            var veri = _context.CourierComment.Where(P => P.CourierId == OturumBlgi.Id).FirstOrDefault();
            return PartialView("_CourierComment",veri);
        }

        public IActionResult CourierProfile(int Id)
        {
            CourierHomeVM veri = new CourierHomeVM();

            veri.courier = _context.Courier.FirstOrDefault(P => P.Id == Id);
            veri.Konum = _context.CourierLocation.Where(P => P.CourierId == Id).Include(K => K.Ilce.Il).ToList();
            veri.comment = _context.CourierComment.Where(P => P.CourierId == Id).Include(K => K.User).ToList();
            return View("CourierProfile", veri);
        }

        public IActionResult KodOnayı() 
        {
            return View();
        }

    }
}
