using Microsoft.AspNetCore.Mvc;


namespace Quiz.Web.Controllers
{
    public class QuizController : Controller
    {
        public QuizController()
        {

        }
        public IActionResult Test(int id)
        {
            return View();
        }

        public IActionResult Results(int id)
        {
            return View();
        }
    }
}
