using Microsoft.AspNetCore.Mvc;
using NumberConverterApp.Models;
using NumberConverterApp.Services;
using NumberConverterApp.DataB;
using System.Linq;

public class ConversionController : Controller
{
    private readonly AppDbContext _dbContext;
    private readonly ConversionService _conversionService;

    public ConversionController(AppDbContext dbContext, ConversionService conversionService)
    {
        _dbContext = dbContext;
        _conversionService = conversionService;
    }

    // ✅ Displays UI Form for Conversion
    [HttpGet]
    public IActionResult Convert()
    {
        return View();
    }

    // ✅ Handles Number Conversion Request
    [HttpPost]
    public IActionResult ConvertNumber(string inputValue, string fromFormat, string toFormat)
    {
        if (string.IsNullOrEmpty(inputValue))
            return View("Error"); // 🛠️ Show error page if input is missing

        string result = _conversionService.ConvertNumber(inputValue, fromFormat.ToLower(), toFormat.ToLower());
        var historyEntry = new ConversionHistory(inputValue, result, fromFormat, toFormat);
        
        _dbContext.ConversionHistories.Add(historyEntry);
        _dbContext.SaveChanges(); // ✅ Stores history in DB

        return View("Result", historyEntry); // ✅ Passes data to Result.cshtml
    }

    // ✅ Retrieves Conversion History
    [HttpGet("history")]
    public IActionResult History()
    {
        var historyList = _dbContext.ConversionHistories.ToList();
        return View("History", historyList); // ✅ Passes history to UI
    }
}