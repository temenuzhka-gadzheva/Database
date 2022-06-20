using Quiz.Services.Enums;

namespace Quiz.Services.Models
{
  public  class UserQuizViewModel
    {
        public int QuizId { get; set; }
        public string Title { get; set; }
        public QuizStatus Status { get; set; }
    }

    

}
