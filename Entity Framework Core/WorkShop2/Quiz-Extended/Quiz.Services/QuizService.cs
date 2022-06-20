using Microsoft.EntityFrameworkCore;
using Quiz.Data;
using Quiz.Services.Enums;
using Quiz.Services.Interfaces;
using Quiz.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace Quiz.Services
{
    public class QuizService : IQuizService
    {
        private readonly ApplicationDbContext applicationDbContext;
        public QuizService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public int Add(string title)
        {
            var quiz = new Quiz.Models.Quiz
            {
                Title = title
            };

            this.applicationDbContext.Quizzes.Add(quiz);
            this.applicationDbContext.SaveChanges();

            return quiz.Id;
        }

        public QuizViewModel GetQuizById(int quizId)
        {
            var quiz = this.applicationDbContext.Quizzes
                .Include(x => x.Questions.ToList())
                 .ThenInclude(x => x.Answers.ToList())
                .FirstOrDefault(x => x.Id == quizId);

            var quizViewModel = new QuizViewModel
            {
                Id = quiz.Id,
                Title = quiz.Title,
                Questions = quiz.Questions
                .Select(x => new QuestionViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Answers = x.Answers.Select(a => new AnswerViewModel
                    {
                        Id = a.Id,
                        Title = a.Title
                    })
                })
            };

            return quizViewModel;

        }

        public IEnumerable<UserQuizViewModel> GetQuizzesByUserName(string userName)
        {
          var quizzes = applicationDbContext.Quizzes
                .Select(x => new UserQuizViewModel
                {
                    QuizId = x.Id,
                    Title = x.Title
                })
                .ToList();

            foreach (var quiz in quizzes)
            {
                var questionsCount = applicationDbContext.UserAnswers
                    .Count(ua => ua.IdentityUser.UserName == userName &&
                     ua.Question.QuizId == quiz.QuizId);

                if (questionsCount == 0)
                {
                    quiz.Status = QuizStatus.NotStarted;
                    continue;
                }

                var answeredQuestions = applicationDbContext.UserAnswers
                    .Count(ua => ua.IdentityUser.UserName == userName
                    && ua.Question.QuizId == quiz.QuizId
                    && ua.AnswerId.HasValue);

                if (questionsCount == answeredQuestions)
                {
                    quiz.Status = QuizStatus.Finished;
                }
                else
                {
                    quiz.Status = QuizStatus.InProgress;
                   
                }

            }
           
            return quizzes;
        }
    }
}
