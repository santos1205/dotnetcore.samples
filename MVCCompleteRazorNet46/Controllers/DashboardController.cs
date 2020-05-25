using QuestionarioCOrg.Filters;
using QuestionarioCOrg.Service;
using System.Web.Mvc;

namespace QuestionarioCOrg.Controllers
{
    [VerifySession, VerifyUserRoles]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {   
            return View(AdminService.ConsolidarDados());
        }
    }
}