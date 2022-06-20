using System.Collections.Generic;

namespace Quiz.Services.Models.Input
{
   public class QuizInputModel
    {
        public string UserId { get; set; }
        public int QuizId { get; set; }
        public virtual ICollection<QuestionInputModel> Questions { get; set; }
    }
}
