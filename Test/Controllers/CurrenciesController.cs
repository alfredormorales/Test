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
    public class CurrenciesController : Controller
    {
        private readonly testDBContext _context;

        private ILog logger;

        public CurrenciesController(ILog logger) => this.logger = logger;

        public async Task<JsonResult> saveCurrency(Currency currency)
        {
            string Message = "";
            bool Error = false;
            try
            {
                testDBContext db = new testDBContext();
                if (currency.Id == 0)
                {
                    Message = db.Currency.Where(i => currency.Name == i.Name).ToList().Count == 1
                            ? "El Nombre que se especifico para la moneda ya existe" : "";
                    if (Message == "")
                    {
                        db.Currency.Add(currency);
                    }
                    else
                    {
                        Error = true;
                    }
                }
                else
                {
                    Message = db.Currency.Where(i => currency.Name == i.Name && currency.Id != i.Id).ToList().Count == 1
                             ? "El Nombre que se quiere actualizar para la moneda ya existe escriba otro"
                             : "";

                    if (Message == "")
                    {
                        Currency updCurrency = db.Currency.Where(i => i.Id == currency.Id).FirstOrDefault();

                        updCurrency.Name = currency.Name;
                        updCurrency.Status = currency.Status;
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
        public JsonResult getCurrency([FromQuery] string search)
        {
            string Message = "";
            bool Error = false;
            List<Currency> currencies = new List<Currency>();
            try
            {
                testDBContext db = new testDBContext();
                currencies = db.Currency.ToList();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Error = false; Message = "Error del servicio";
            }

            return Json(new { message = Message, success = !Error, list = currencies, });
        }
    }
}
