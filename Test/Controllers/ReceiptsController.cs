using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async Task<JsonResult> getReceipts(string search)
        {
            string Message = "";
            bool Error = false;
            int idReceipt = 0;
            List<Receipt> receipts = new List<Receipt>();
            try
            {
                testDBContext db = new testDBContext();
                receipts = db.Receipt.ToList();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Error = false; Message = "Error del servicio";
            }

            return Json(new { message = Message, success = !Error, list = receipts, id_reciept = idReceipt });
        }

        [HttpPost]
        public async Task<JsonResult> saveReceipts(Receipt receipt)
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