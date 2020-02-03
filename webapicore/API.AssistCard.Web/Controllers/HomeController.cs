using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using API.AssistCard.Web.Models;
using API.AssistCard.Domain.Interfaces.Services;

namespace API.AssistCard.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IUsuarioService _usuarioService;
        

        public HomeController(IUsuarioService UsuarioService)
        {
            _usuarioService = UsuarioService;            
        }


        public IActionResult Index()
        {

            var Usuarios = _usuarioService.ListarUsuariosAtivos();

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
