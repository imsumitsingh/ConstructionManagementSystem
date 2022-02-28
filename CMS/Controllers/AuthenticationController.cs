using Microsoft.AspNetCore.Mvc;
using CSharp;
using Microsoft.Extensions.Configuration;
using CMS.Models;
using System.Data.SqlClient;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;


namespace CMS.Controllers
{
    public class AuthenticationController : Controller
    {
        public AuthenticationController(IConfiguration configuration)
        {
            
            Ado.connectionString = configuration.GetConnectionString("ConCMS");
        }
        public IActionResult Login(string ReturnUrl)
        {
            ViewBag.rURL = ReturnUrl;
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(tbl_Login_Info login)
        {

            if (ModelState.IsValid)
            {
                SqlParameter[] parameters = new SqlParameter[] {
                      new SqlParameter("userName",login.Username),
                      new SqlParameter("password",login.Password),
                };
                var count = Ado.GetScaler("spValidateUser", parameters);
                if ((int)count > 0)
                {
                     
                    
                    var claims = new List<Claim>() {
            
                                          
                        new Claim(ClaimTypes.Name,login.Username),
                                        
                };
                   
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                   
                    var principal = new ClaimsPrincipal(identity);
                     
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                    {
                        IsPersistent = login.RememberLogin
                    });
                    ModelState.Clear();
                  
                   return Redirect(login.ReturnUrl);
                }
                else
                {
                    ModelState.Clear();
                    ViewBag.error = "Username or Password is incorrect";
                    return View();
                }
            }
            else
            {
                ViewBag.error = "Username or Password is incorrect";
            }

            return View();
        }
    }
}
