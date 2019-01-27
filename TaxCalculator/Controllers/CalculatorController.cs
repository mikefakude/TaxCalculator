using System.Web.Mvc;
using TaxCalculator.Interface;
using TaxCalculator.Models;
using TaxCalculator.Repositories;

namespace TaxCalculator.Controllers
{
    public class CalculatorController : Controller
    {
        ICalculatorForTax taxCalculator = new CalculatorForTax();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllAddedValues()
        {
            return Json(taxCalculator.GetAll(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddValues(TaxAmount taxAmount)
        {
            taxAmount = taxCalculator.Save(taxAmount);
            return Json(taxAmount, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditValues(int id, TaxAmount taxAmount)
        {
            taxAmount.Id = id;
            if (taxCalculator.Update(taxAmount))
            {
                return Json(taxCalculator.GetAll(), JsonRequestBehavior.AllowGet);
            }

            return Json(null);
        }

        [HttpDelete]
        public JsonResult DeleteValuesFromTax(int id)
        {
            if (taxCalculator.Delete(id))
            {
                return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
        }
    }
}