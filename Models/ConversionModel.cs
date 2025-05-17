using System.ComponentModel.DataAnnotations;

public class ConversionModel
{
    [Required(ErrorMessage = "Please enter a number to convert.")]
    public string Value { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select the base of the input number.")]
    public string FromBase { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select the target base.")]
    public string ToBase { get; set; } = string.Empty;

    public string ConvertedValue { get; set; } = string.Empty;
}