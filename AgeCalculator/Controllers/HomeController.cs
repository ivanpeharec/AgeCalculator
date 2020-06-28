using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgeCalculator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // Returns the number of months, days, hours, minutes, seconds from the specified date until now.
        public ActionResult GetElapsedTime(DateTime fromDate)
        {
            // Selecting today's date.
            var toDate = DateTime.Now;

            // Calculate the number of years.
            var years = toDate.Year - fromDate.Year;

            // Check if number of years is too big (e.g. 2016-04-17 - 2020-02-03 is not 4 years!)
            DateTime testDate = fromDate.AddMonths(12 * years);
            if (testDate > toDate)
            {
                years--;
                testDate = fromDate.AddMonths(12 * years);
            }

            // Add months until we go too far.
            var months = 0;
            while (testDate <= toDate)
            {
                months++;
                testDate = fromDate.AddMonths(12 * years + months);
            }
            months--;

            // Subtract to see how many more days, hours, minutes, etc. we need.
            fromDate = fromDate.AddMonths(12 * years + months);
            TimeSpan remainder = toDate - fromDate;

            var days = remainder.Days;
            var hours = remainder.Hours;
            var minutes = remainder.Minutes;
            var seconds = remainder.Seconds;
            
            months += years * 12;
            
            var data = new { status = "ok", months = months, days = days, hours = hours, minutes = minutes, seconds = seconds };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}