using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace NumberConverterApp.Models
{
    public class ConversionViewModel
    {
        public string InputValue { get; set; } = string.Empty;
        public string FromBase   { get; set; } = string.Empty;
        public string ToBase     { get; set; } = string.Empty;
        public string Result     { get; set; } = string.Empty;

        public List<SelectListItem> BaseOptions { get; set; }
            = GetBaseSelectList();

        public static List<SelectListItem> GetBaseSelectList() => new()
        {
            new SelectListItem("Binary",      "bin"),
            new SelectListItem("Octal",       "oct"),
            new SelectListItem("Decimal",     "dec"),
            new SelectListItem("Hexadecimal", "hex"),
            new SelectListItem("ASCII",       "ascii")
        };
    }
}