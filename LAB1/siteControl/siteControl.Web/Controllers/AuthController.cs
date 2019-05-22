using System.Web.Mvc;
using siteControl.Web.Models;
using System.Security.Claims;
using System.Web;

using siteControl.Web.CustomLibraries;
using System.Linq;

namespace siteControl.Web.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
     
        [HttpGet]
        public ActionResult Login()
        { 
            return View();
        }
        [HttpPost]
        public ActionResult Login(User model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new MainDbContext())
            {
                var emailCheck = db.Users.FirstOrDefault(u => u.Email == model.Email);
                var getPassword = db.Users.Where(u => u.Email == model.Email).Select(u => u.Password);
                var materializePassword = getPassword.ToList();
                
                var decryptedPassword =(!materializePassword.Any()) ? null: CustomDecrypt.Decrypt(getPassword.ToList()[0]);
                
                if (model.Email != null && model.Password == decryptedPassword)
                {
                    var getName = db.Users.Where(u => u.Email == model.Email).Select(u => u.Name);
                    var materializeName = getName.ToList();
                    var name = materializeName[0];

                    var getCountry = db.Users.Where(u => u.Email == model.Email).Select(u => u.Country);
                    var materializeCountry = getCountry.ToList();
                    var country = materializeCountry[0];

                    var getEmail = db.Users.Where(u => u.Email == model.Email).Select(u => u.Email);
                    var materializeEmail = getEmail.ToList();
                    var email = materializeEmail[0];

                    var identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, name),
                        new Claim(ClaimTypes.Email, email),
                        new Claim(ClaimTypes.Country, country)
                },
                    authenticationType: "ApplicationCookie");

                    var ctx = Request.GetOwinContext();
                    var authManager = ctx.Authentication;
                    authManager.SignIn(identity);

                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Invalid email or password");
            return View(model);
        }
      
        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Login", "Auth");
        }
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(User model)
        {

            if (ModelState.IsValid)
            {
                var emailExists = EmailExists(model.Email);
                var usernameExists = UsernameExists(model.Name);


                if (emailExists)
                    ModelState.AddModelError("Email", "Email already in use.");
                if (usernameExists)
                    ModelState.AddModelError("Username", "Username already in use.");
                if (!usernameExists && !emailExists) 
                    using (var db = new MainDbContext())
                {
                    var encryptedPassword = CustomEnrypt.Encrypt(model.Password);
                    var user = db.Users.Create();
                    user.Email = model.Email;
                    user.Password = encryptedPassword;
                    user.Country = model.Country;
                    user.Name = model.Name;
                    db.Users.Add(user);
                    db.SaveChanges();

                        return RedirectToAction("Login", "Auth");
                    }
                    
                }
            else
            {
                ModelState.AddModelError("", "Invalid input");
            }
            return View();
        }
        private bool EmailExists(string email)
        {
            using (var db = new MainDbContext())
            {
                var user = db.Users.Where(u => u.Email == email).FirstOrDefault();

                if (user != null)
                    return true;
            }
            return false;
        }

        private bool UsernameExists(string name)
        {
            using (var db = new MainDbContext())
            {
                var user = db.Users.Where(u => u.Name == name).FirstOrDefault();

                if (user != null)
                    return true;
            }
            return false;
        }

    }
}