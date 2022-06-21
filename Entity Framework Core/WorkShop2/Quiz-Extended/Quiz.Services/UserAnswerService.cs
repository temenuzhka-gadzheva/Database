using Microsoft.EntityFrameworkCore;
using Quiz.Data;
using Quiz.Models;
using Quiz.Services.Interfaces;
using Quiz.Services.Models.Input;
using System.Collections.Generic;
using System.Linq;

namespace Quiz.Services
{
    public class UserAnswerService : IUserAnswerService
    {
        private readonly ApplicationDbContext applicationDbContext;
        public UserAnswerService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public void AddUserAnswer(string userName, int questionId, int answerId)
        {
            var userId = this.applicationDbContext.Users
                .Where(x => x.UserName == userName)
                .Select(x => x.Id).FirstOrDefault();

            var userAnswer = this.applicationDbContext
                .UserAnswers
                .FirstOrDefault(x => x.IdentityUserId == userId
                && x.QuestionId == questionId);

            userAnswer.AnswerId = answerId;
           
            this.applicationDbContext.SaveChanges();

        }

        public void BulkAddUserAnswer(QuizInputModel quizInputModel)
        {
            var userAnswers = new List<UserAnswer>();
            foreach (var item in quizInputModel.Questions)
            {
                var userAnswer = new UserAnswer
                {
                    IdentityUserId = quizInputModel.UserId,
                    AnswerId = item.AnswerId
                };

                userAnswers.Add(userAnswer);
            }
            this.applicationDbContext.AddRange(userAnswers);
            this.applicationDbContext.SaveChanges();
        }

        public int GetUserResult(string userName, int quizId)
        {
            var userId = this.applicationDbContext.Users
               .Where(x => x.UserName == userName)
               .Select(x => x.Id).FirstOrDefault();

            var totaloints = this.applicationDbContext
                .UserAnswers
                .Where(x => x.IdentityUserId == userId 
                && x.Question.QuizId == quizId)
                .Sum(x => x.Answer.Points);

            return totaloints;
         
        }
    }
}
