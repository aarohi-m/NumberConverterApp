using Microsoft.AspNetCore.Mvc;
using NumberConverterApp.Models;
using NumberConverterApp.Services;

namespace NumberConverterApp.Controllers
{
    public class ConversionController : Controller
    {
        private readonly INumberConverter _converter;

        public ConversionController(INumberConverter converter)
        {
            _converter = converter;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var vm = new ConversionViewModel
            {
                BaseOptions = ConversionViewModel.GetBaseSelectList()
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(ConversionViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.BaseOptions = ConversionViewModel.GetBaseSelectList();
                return View(vm);
            }

            vm.Result = _converter.Convert(vm.FromBase, vm.ToBase, vm.InputValue);
            vm.BaseOptions = ConversionViewModel.GetBaseSelectList();
            return View(vm);
        }
    }
}