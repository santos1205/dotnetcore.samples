using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MovileWeb.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public bool CheckValidation(Object Object, ref List<ValidationResult> ValidationResults)
        {               
            ValidationContext contexts = new ValidationContext(Object, null, null);
            var isValid = Validator.TryValidateObject(Object, contexts, ValidationResults, true);

            return isValid;
        }
    }
}