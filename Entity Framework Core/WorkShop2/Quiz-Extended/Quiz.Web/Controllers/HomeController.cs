using Microsoft.AspNetCore.Mvc;
using Quiz.Services.Interfaces;
using Quiz.Web.Models;
using System.Diagnostics;

namespace Quiz.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQuizService quizService;

        public HomeController(IQuizService quizService)
        {
            this.quizService = quizService;
        }

        public IActionResult Index()
        {
            // текущо логнат потребител
            var userName = this.User?.Identity?.Name;
            var userQuizes = this.quizService.GetQuizzesByUserName(userName);
            return View(userQuizes);
        }
    }
}
