

using Quiz.Services.Models.Input;

namespace Quiz.Services.Interfaces
{
  public  interface IUserAnswerService
    {
        void AddUserAnswer(string userName,int questionId, int answerId);
        void BulkAddUserAnswer(QuizInputModel quizInputModel);
        int GetUserResult(string userName, int quizId);
    }
}
