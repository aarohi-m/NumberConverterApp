

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using NumberConverterApp.Models;

[ApiController]
[Route("api/[controller]")]
public class ConversionController : ControllerBase
{
    private readonly ConversionService _conversionService;

    // In-memory list to store conversion history
    private static List<ConversionHistory> conversionHistoryList = new();

    public ConversionController()
    {
        _conversionService = new ConversionService();
    }

    //  API endpoint for number conversions
    [HttpGet("convert")]
    public IActionResult ConvertNumber([FromQuery] string value, [FromQuery] string from, [FromQuery] string to)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
        {
            return BadRequest(new { error = "Missing required parameters." });
        }

        string result = _conversionService.ConvertNumber(value, from.ToLower(), to.ToLower());

        if (result == null)
        {
            return BadRequest(new { error = $"Unsupported or invalid conversion: {from}-to-{to}" });
        }

        //  history entry
        ConversionHistory historyEntry = new ConversionHistory(value, result, from, to, DateTime.Now);
        conversionHistoryList.Add(historyEntry);

        return Ok(new { original = value, converted = result, from, to });
    }

    //  Decimal to Binary Conversion Logic
    [HttpGet("decimal-to-binary")]
    public IActionResult ConvertDecimalToBinary([FromQuery] int decimalValue)
    {
        if (decimalValue < 0)
        {
            return BadRequest(new { error = "Negative values are not supported for binary conversion." });
        }

        string binaryResult = Convert.ToString(decimalValue, 2);

        //  added history entry
        ConversionHistory historyEntry = new(decimalValue.ToString(), binaryResult, "decimal", "binary", DateTime.Now);
        conversionHistoryList.Add(historyEntry);

        return Ok(new { original = decimalValue, converted = binaryResult, from = "decimal", to = "binary" });
    }

    //  Endpoint to retrieve past conversions
    [HttpGet("history")]
    public IActionResult GetConversionHistory()
    {
        return Ok(conversionHistoryList);
    }
}