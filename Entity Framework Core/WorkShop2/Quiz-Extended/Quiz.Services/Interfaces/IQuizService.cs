using Quiz.Services.Models;
using System.Collections.Generic;

namespace Quiz.Services.Interfaces
{
    public interface IQuizService
    {
        int Add(string title);
        QuizViewModel GetQuizById(int quizId);
        IEnumerable<UserQuizViewModel> GetQuizzesByUserName(string userName);
    }
}
