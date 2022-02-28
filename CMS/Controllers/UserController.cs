using CMS.Models;
using CSharp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CMS.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
       
        public UserController(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            Ado.connectionString = configuration.GetConnectionString("ConCMS");
            webHostEnvironment = hostEnvironment;
        }
        public IActionResult List()
        {
            var user = BLayer.GetUser();

            return View(user);
        }
        public IActionResult GetUser(int id)
        {
            var user = BLayer.GetUser(id);

            return PartialView("_partialAddUpdateUser",user);
        }
        [HttpPost]
        public IActionResult AddUpdateUser(tbl_User_Master tbl)
        {
            string uniqueFileName = "";
            if (ModelState.IsValid)
            {
                if (tbl.id == 0)
                {
                    if (tbl.SavePhoto != null)
                    {
                        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + tbl.SavePhoto.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            tbl.SavePhoto.CopyTo(fileStream);
                        }
                    }


                    SqlParameter[] parameter = new SqlParameter[]
                   {

                      new SqlParameter("@name", tbl.name),
                      new SqlParameter("@email", tbl.email),
                      new SqlParameter("@mobile", tbl.mobile),
                      new SqlParameter("@village", tbl.village),
                      new SqlParameter("@post", tbl.post),
                      new SqlParameter("@policeStation", tbl.policeStation),
                      new SqlParameter("@district", tbl.district),
                      new SqlParameter("@state", tbl.state),
                      new SqlParameter("@country", tbl.country),
                      new SqlParameter("@photo", uniqueFileName),

                    };
                    int i = (int)Ado.GetScaler("[dbo].[_spInserttbl_user_master]", parameter);
                    if (i > 0)
                    {
                        return RedirectToAction("List");
                    }
                }
                else
                {
                    if (tbl.SavePhoto != null)
                    {
                        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + tbl.SavePhoto.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            tbl.SavePhoto.CopyTo(fileStream);
                        }
                    }
                    else
                    {
                        uniqueFileName = tbl.photo;
                    }


                    SqlParameter[] parameter = new SqlParameter[]
                    {

                      new SqlParameter("@id", tbl.id),
                      new SqlParameter("@name", tbl.name),
                      new SqlParameter("@email", tbl.email),
                      new SqlParameter("@mobile", tbl.mobile),
                      new SqlParameter("@village", tbl.village),
                      new SqlParameter("@post", tbl.post),
                      new SqlParameter("@policeStation", tbl.policeStation),
                      new SqlParameter("@district", tbl.district),
                      new SqlParameter("@state", tbl.state),
                      new SqlParameter("@country", tbl.country),
                      new SqlParameter("@photo", uniqueFileName),

                    };
                    int i = (int)Ado.GetScaler("[dbo].[_spUpdate_tbl_user_master]", parameter);
                    if (i > 0)
                    {
                        return RedirectToAction("List");
                    }
                }
            }
            else
            {
                return Content("error");
            }

            return View();
        }
    }
}
