using Microsoft.AspNetCore.Mvc;
using NumberConverterApp.Models;
using NumberConverterApp.Services;
using NumberConverterApp.DataB;
using System.Linq;

namespace NumberConverterApp.Controllers
{

    public class ConversionController
    (AppDbContext dbContext, ConversionService conversionService) : Controller
    {
        [HttpGet]
        public IActionResult Convert()
        {
            return View();
        }

        //  Number Conversion Request
        [HttpPost]
        public IActionResult ConvertNumber(string inputValue, string fromFormat, string toFormat)
        {
            if (string.IsNullOrEmpty(inputValue))
                return View("Error"); //  error page if input is missing

            string result = conversionService.ConvertNumber(inputValue, fromFormat.ToLower(), toFormat.ToLower());
            ConversionHistory historyEntry = new(inputValue, result, fromFormat, toFormat);

            _ = dbContext.ConversionHistories.Add(historyEntry);
            _ = dbContext.SaveChanges(); // Stores history in DB

            return View("Result", historyEntry); //  Passes data to Result.cshtml
        }

        // Retrieves Conversion History
        [HttpGet("history")]
        public IActionResult History()
        {
            List<ConversionHistory> historyList = dbContext.ConversionHistories.ToList();
            return View("History", historyList); // Passes history to UI
        }
    }
}