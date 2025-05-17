using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ConversionController : ControllerBase
{
    private readonly ConversionService _conversionService;

    public ConversionController()
    {
        _conversionService = new ConversionService();
    }

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

        return Ok(new { original = value, converted = result, from, to });
    }
}