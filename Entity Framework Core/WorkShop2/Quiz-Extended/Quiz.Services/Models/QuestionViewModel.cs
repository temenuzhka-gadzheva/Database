using System.Collections.Generic;

namespace Quiz.Services.Models
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual IEnumerable<AnswerViewModel> Answers { get; set; }
    }
}
