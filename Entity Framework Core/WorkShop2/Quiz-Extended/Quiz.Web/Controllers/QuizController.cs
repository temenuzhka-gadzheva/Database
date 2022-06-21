using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz.Services.Interfaces;

namespace Quiz.Web.Controllers
{
    [Authorize]
    public class QuizController : Controller
    {
        private readonly IQuizService quizService;
        private readonly IUserAnswerService userAnswer;

        public QuizController(IQuizService quizService,
                              IUserAnswerService userAnswer )
        {
            this.quizService = quizService;
            this.userAnswer = userAnswer;
        }
        public IActionResult Test(int id)
        {
            this.quizService.StartQuiz(this.User?.Identity?.Name,id);
            var viewModel = this.quizService.GetQuizById(id);
            
            return View(viewModel);
        }

        public IActionResult Submit(int id)
        {
            foreach (var item in this.Request.Form)
            {
                var questionId = int.Parse(item.Key.Replace("q_", string.Empty));
                var answerId = int.Parse(item.Value);
                this.userAnswer.AddUserAnswer(this.User?.Identity?.Name, questionId, answerId);
            }

            return this.RedirectToAction("Results", new {id});
        }
        public IActionResult Results(int id)
        {
            var points = this.userAnswer
                .GetUserResult(this.User?.Identity?.Name, id);
            return View(points);
        }
    }
}
