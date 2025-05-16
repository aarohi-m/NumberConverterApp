using Microsoft.AspNetCore.Mvc;
using System;

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
            int baseFrom = GetBase(from);
            int baseTo = GetBase(to);

            if (baseFrom == -1 || baseTo == -1)
            {
                return BadRequest(new { error = "Unsupported number system." });
            }

            int decimalValue = Convert.ToInt32(value, baseFrom);
            string result = Convert.ToString(decimalValue, baseTo);

            return Ok(new { original = value, converted = result, from, to });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = $"Conversion failed: {ex.Message}" });
        }
    }

    private int GetBase(string format)
    {
        return format.ToLower() switch
        {
            "binary" => 2,
            "octal" => 8,
            "decimal" => 10,
            "hexadecimal" => 16,
            _ => -1
        };
    }
}