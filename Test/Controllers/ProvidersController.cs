using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test.EFModels;
using Test.Infrastructure;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvidersController : Controller
    {
        
        private readonly testDBContext _context;

        private ILog logger;

        public ProvidersController(ILog logger) => this.logger = logger;

        public JsonResult saveProvider([FromBody] Provider provider)
        {
            string Message = "";
            bool Error = false;
            try
            {
                testDBContext db = new testDBContext();
                if (provider.Id == 0)
                {
                    Message = db.Provider.Where(i => provider.Name == i.Name).ToList().Count == 1
                            ? "El Nombre que se especifico para el proveedor ya existe" : "";
                    if (Message == "")
                    {
                        db.Provider.Add(provider);
                    }
                    else
                    {
                        Error = true;
                    }
                }
                else
                {
                    Message = db.Provider.Where(i => provider.Name == i.Name && provider.Id != i.Id).ToList().Count == 1
                             ? "El Nombre que se quiere actualizar para el proveedor ya existe escriba otro" 
                             : "";

                    if (Message == "")
                    {
                        Provider updProvider = db.Provider.Where(i => i.Id == provider.Id).FirstOrDefault();

                        updProvider.Name = provider.Name;
                        updProvider.Status = provider.Status;
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

        [HttpGet("[action]")]
        public JsonResult getProvider([FromQuery] string search)
        {
            string Message = "";
            bool Error = false;
            List<Provider> Providers = new List<Provider>();
            try
            {
                testDBContext db = new testDBContext();
                Providers = db.Provider.ToList();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Error = false; Message = "Error del servicio";
            }

            return Json(new { message = Message, success = !Error, list = Providers,  });
        }

    }
}
