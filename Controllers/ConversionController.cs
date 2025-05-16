using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;


[ApiController]
[Route("api/[controller]")]
public class ConversionController : ControllerBase
{
    [HttpGet("convert")]
    public IActionResult ConvertNumber([FromQuery] string value, [FromQuery] string from, [FromQuery] string to)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
        {
            return BadRequest(new { error = "Missing required parameters." });
        }

        try
        {
            string result = PerformConversion(value, from.ToLower(), to.ToLower());

            if (result == null)
            {
                return BadRequest(new { error = $"Unsupported conversion type: {from}-to-{to}" });
            }

            return Ok(new { original = value, converted = result, from, to });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = $"Conversion failed: Invalid input format for '{from}'. Details: {ex.Message}" });
        }
    }

    private string PerformConversion(string value, string from, string to)
    {
        Console.WriteLine($"Converting {value} from {from} to {to}"); // Debug log

        Dictionary<string, Func<string, string>> conversionMethods = new()
        {
            { "binary-to-decimal", v => Convert.ToInt32(v, 2).ToString() },
            { "octal-to-decimal", v => Convert.ToInt32(v, 8).ToString() },
            { "hexadecimal-to-decimal", v => Convert.ToInt32(v.ToUpper(), 16).ToString() },
            { "decimal-to-binary", v => int.TryParse(v, out var n) ? Convert.ToString(n, 2) : null },
            { "decimal-to-octal", v => int.TryParse(v, out var n) ? Convert.ToString(n, 8) : null },
            { "decimal-to-hexadecimal", v => int.TryParse(v, out var n) ? Convert.ToString(n, 16) : null }
        };

        string key = $"{from}-to-{to}";
        return conversionMethods.ContainsKey(key) ? conversionMethods[key].Invoke(value) : null;
    }
}




