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
    public class ReceiptsController : Controller
    {
        private ILog logger;

        public ReceiptsController(ILog logger) => this.logger = logger;

        [HttpGet("[action]")]
        public JsonResult getReceipts([FromQuery] string search)
        {
            string Message = "";
            bool Error = false;
            List<Receipt> receipts = new List<Receipt>();
            try
            {
                testDBContext db = new testDBContext();
                receipts = db.Receipt.Include(r => r.Provider ).Include( r => r.Currency).ToList();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Error = false; Message = "Error del servicio";
            }
            var list = receipts.Select(o => new
            {
                id = o.Id,
                currency = new { o.Currency.Id, o.Currency.Name },
                provider = new { o.Provider.Id, o.Provider.Name },
                date = o.Date,
                status = o.Status,
                comment = o.Comment,
                amount = o.Amount
            });
            return Json(new { message = Message, success = !Error, list = list });
        }

        [HttpPost("[action]")]
        public JsonResult saveReceipts([FromBody] Receipt receipt)
        {
            string Message = "";
            bool Error = false;
            try
            {
                if (receipt.Id == 0)
                {
                    testDBContext db = new testDBContext();
                    db.Receipt.Add(receipt);
                    db.SaveChanges();
                } else
                {

                }

            } catch (Exception ex)
            {
                logger.Error(ex.Message);
                Error = false; Message = "Error del servicio";
            }

            return Json(new { message = Message, success = !Error, id = receipt.Id });
        }

    }
}