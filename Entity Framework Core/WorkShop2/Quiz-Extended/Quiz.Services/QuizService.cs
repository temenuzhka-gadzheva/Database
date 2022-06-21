using Microsoft.EntityFrameworkCore;
using Quiz.Data;
using Quiz.Models;
using Quiz.Services.Enums;
using Quiz.Services.Interfaces;
using Quiz.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quiz.Services
{
    public class QuizService : IQuizService
    {
        private readonly ApplicationDbContext db;
        public QuizService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public int Add(string title)
        {
            var quiz = new Quiz.Models.Quiz
            {
                Title = title
            };

            this.db.Quizzes.Add(quiz);
            this.db.SaveChanges();

            return quiz.Id;
        }

        public QuizViewModel GetQuizById(int quizId)
        {
            var quiz = this.db.Quizzes
                .Include(x => x.Questions)
                 .ThenInclude(x => x.Answers)
                .FirstOrDefault(x => x.Id == quizId);

            var quizViewModel = new QuizViewModel
            {
                Id = quiz.Id,
                Title = quiz.Title,
                Questions = quiz.Questions.OrderBy(r => Guid.NewGuid())
                .Select(x => new QuestionViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Answers = x.Answers.OrderBy(r => Guid.NewGuid())
                    .Select(a => new AnswerViewModel
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
          var quizzes = db.Quizzes
                .Select(x => new UserQuizViewModel
                {
                    QuizId = x.Id,
                    Title = x.Title
                })
                .ToList();

            foreach (var quiz in quizzes)
            {
                var questionsCount = db.UserAnswers
                    .Count(ua => ua.IdentityUser.UserName == userName &&
                     ua.Question.QuizId == quiz.QuizId);

                if (questionsCount == 0)
                {
                    quiz.Status = QuizStatus.NotStarted;
                    continue;
                }

                var answeredQuestions = db.UserAnswers
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

        public void StartQuiz(string userName, int quizId)
        {
            // quiz is started
            if (db.UserAnswers.Any(x => x.IdentityUser.UserName == userName
            && x.Question.QuizId == quizId))
            {
                return;
            }
            // generate questions

            var userId = this.db.Users.
                Where(x => x.UserName == userName)
                .Select(x => x.Id)
                .FirstOrDefault();

            var questions = db.Questions
                .Where(x => x.QuizId == quizId)
                .Select(x => new { x.Id})
                .ToList();

            foreach (var question in questions)
            {
                db.UserAnswers.Add(new UserAnswer
                {
                    AnswerId = null,
                    IdentityUserId = userId,
                    QuestionId = question.Id
                });
            }
            db.SaveChanges();
        }
    }
}
