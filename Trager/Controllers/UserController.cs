using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trager.Models;
using Trager.EmailServices;
using Trager.Extensions;
using Trager.Models;
using System.Net.Mail;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Trager.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class UserController : BaseController
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private UserIEmailSender _useremailSender;
        private object url;
        private readonly ApplicationContext _context;
        public UserController(ApplicationContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, UserIEmailSender useremailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _useremailSender = useremailSender;
            _context = context;
        }
        public IActionResult UserLogin(string UserReturnUrl = null)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserLogin(UserLoginModel model)
        {
            var user = new User()
            {
                UserEmail = model.UserEmail,
                UserPassword = model.UserPassword,
                EmailConfirmed = model.EmailConfirmed,
            };
            var UserUser = _context.User.FirstOrDefault(P => P.UserEmail == user.UserEmail && P.UserPassword == user.UserPassword);
            if (UserUser != null)
            {
                HttpContext.Session.SetString("useroturum", JsonConvert.SerializeObject(UserUser));
                return RedirectToAction("UserHome", "User");
            }
            else
            {
                ViewBag.Message = "Geçersiz Kullanıcı Adı yada Parola";
                return View();
            }
        }

        public IActionResult UserRegister()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserRegister(UserRegisterModel model, List<IFormFile> UserAvatar)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User()
            {
                UserName = model.UserName,
                UserLastName = model.UserLastName,
                UserPhone = model.UserPhone,
                UserEmail = model.UserEmail,
                UserPassword = model.UserPassword,
                UserRePassword = model.UserRePassword,
            };
            var kayitvarmı = _context.User.FirstOrDefault(P => P.UserEmail == user.UserEmail && P.UserPhone == user.UserPhone);
            if (kayitvarmı == null)
            {
                foreach (var dosya in UserAvatar)
                {
                    if (dosya.Length > 0)
                    {
                        string CAvatar = "Avatar" +
                            Guid.NewGuid().ToString().Substring(0, 4);
                        string Avatar = CAvatar + "Avatar" +
                            dosya.FileName;
                        var ustklasor = Directory.GetParent(Directory.GetCurrentDirectory());
                        string dosyaAdiOnEk = user.UserName;
                        string dosyaAdi = dosyaAdiOnEk + "_" + dosya.FileName;
                        string dosyaYolu = Path.Combine(ustklasor.FullName + "/Trager", $"wwwroot/UAvatar/{dosyaAdi}");
                        using (var stream = System.IO.File.Create(dosyaYolu))
                        {
                            dosya.CopyTo(stream);

                        }

                    }
                }
                _context.User.Add(user);
                _context.SaveChanges();
                mailgonder(model);
            }

          

            ModelState.AddModelError("", "Bilinmeyen hata oldu lütfen tekrar deneyiniz.");
            return RedirectToAction("UserLogin", "User");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData.Put("message", new AlertMessage()
            {
                Title = "Oturum Kapatıldı.",
                Message = "Hesabınız güvenli bir şekilde kapatıldı.",
                AlertType = "warning"
            });
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Geçersiz token.",
                    Message = "Geçersiz Token",
                    AlertType = "danger"
                });
                return View();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    TempData.Put("message", new AlertMessage()
                    {
                        Title = "Hesabınız onaylandı.",
                        Message = "Hesabınız onaylandı.",
                        AlertType = "success"
                    });
                    return View();
                }
            }
            TempData.Put("message", new AlertMessage()
            {
                Title = "Hesabınızı onaylanmadı.",
                Message = "Hesabınızı onaylanmadı.",
                AlertType = "warning"
            });
            return View();
        }

        public IActionResult UserForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserForgotPassword(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(Email);

            if (user == null)
            {
                return View();
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var url = Url.Action("UserResetPassword", "User", new
            {
                userId = user.Id,
                token = code
            });

            // email
            await _useremailSender.SendEmailAsync(Email, "Reset Password", $"Parolanızı yenilemek için linke <a href='https://localhost:5001{url}'>tıklayınız.</a>");

            return View();
        }

        public IActionResult UserResetPassword(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("User", "UserLogin");
            }

            var model = new UserResetPasswordModel { Token = token };

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserResetPassword(UserResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.UserEmail);
            if (user == null)
            {
                return RedirectToAction("User", "UserLogin");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.UserPassword);

            if (result.Succeeded)
            {
                return RedirectToAction("UserLogin", "User");
            }

            return View(model);
        }

        public IActionResult UserHome()
        {
            CourierListVM veri = new CourierListVM();
            veri.Ils = _context.Il.ToList();
            return View("UserHome", veri);
        }
        public ActionResult ilcegetir(int Id)
        {
            var ilceListe = _context.Ilce.Where(P => P.IlId == Id).ToList();
            ViewBag.Ilceler = ilceListe;
            return PartialView("_ilceliste", ilceListe);
        }

        [HttpPost]
        public ActionResult kuryegetir(int Id)
        {
            CourierListVM veri = new CourierListVM();
            veri.Ils = _context.Il.ToList();
            veri.CourierLocations = _context.CourierLocation.Include(P=> P.Ilce.Il).Include(K=> K.Courier).Where(P => P.IlceId == Id).ToList();
            return View("UserHome", veri);
        }

        public void mailgonder(UserRegisterModel model)
        {
            SmtpClient sc = new SmtpClient();
            sc.Port = 587;
            sc.Host = "smtp.office365.com";
            sc.EnableSsl = true;
            sc.Credentials = new NetworkCredential("Trager240@hotmail.com", "123456Ssc");
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("Trager240@hotmail.com", "üye kayıt");
            mail.To.Add(model.UserEmail);
            mail.Subject = "üye kayıt";
            mail.IsBodyHtml = true;
            mail.Body = $"Lütfen email hesabınızı onaylamak için linke <a href='https://localhost:5001{url}'>tıklayınız.</a>";
            sc.Send(mail);

        }
    }
}