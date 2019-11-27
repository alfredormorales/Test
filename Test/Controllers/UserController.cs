using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Test.EFModels;
using Test.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace Test.Controllers
{

    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private ILog logger;
        private User logedUser;

        public UserController(ILog logger) => this.logger = logger;

        [HttpPost]
        public async Task<JsonResult> saveUser(User user)
        {
            string Message = "";
            bool Error = false;
            try
            {
                testDBContext db = new testDBContext();
                if (user.Id == 0)
                {
                    Message = db.User.Where(i => user.UserName == i.UserName).ToList().Count == 1
                            ? "El usuario ya existe." : db.User.Where(i => user.Name == i.Name).ToList().Count == 1
                            ? "El Nombre que se especifico para el usaurio ya existe" : "";
                    if (Message == "")
                    {
                        db.User.Add(user);
                    }
                    else
                    {
                        Error = true;
                    }
                }
                else
                {
                    Message = db.User.Where(i => user.UserName == i.UserName && user.Id != i.Id).ToList().Count == 1
                            ? "El usuario ya esta tomado no se puede cambiar." : db.User.Where(i => user.Name == i.Name && user.Id != i.Id).ToList().Count == 1
                            ? "El Nombre que se quiere actualizar para el usaurio ya existe escriba otro" :
                            db.User.Where(i => i.Id == user.Id).Count() == 0 ? "El Id de Usuario que trata de actualizar no existe," +
                            "por favor insertelo primero" : "";

                    if (Message != "")
                    {
                        User updUser = db.User.Where(i => i.Id == user.Id).FirstOrDefault();

                        updUser.Name = user.Name;
                        updUser.Password = user.Password;
                        updUser.Status = user.Status;
                        updUser.UserName = user.UserName;
                    }
                    else
                    {
                        Error = true;
                    }
                }

                if (!Error)
                    db.SaveChanges();

                return Json(new { success = !Error, message = Message });
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return Json(new { success = false, message = "Error del servicio" });
            }

        }

        [HttpPost]
        public async Task<JsonResult> getUsers(string search)
        {
            return Json("{}");
        }
        [HttpPost]
        public async Task<JsonResult> validateUser(User user)
        {
            string Message = "";
            bool Valid = false;
            try
            {
                var objUser = new User();
                testDBContext db = new testDBContext();

                objUser = db.User.Where(u => u.UserName == user.UserName && u.Password == user.Password).FirstOrDefault(null);

                Valid = objUser == null ? false : true;

                Message = Valid ? "" : "Usuario o contraseña incorrecta";

                //HttpContext.Session.SetString("LogedUser", Valid );

                return Json(new { success = Valid, message = Message, user = objUser });
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return Json(new { success = false, message = "Error del servicio"});
            }
        }
    }
    
}
