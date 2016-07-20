using AbideChallenge.Domain;
using System.Collections.Generic;

namespace AbideChallenge.Questions.Abstract
{
    public interface IQuestionService
    {
        int Question1();
        decimal Question2();
        IEnumerable<Question3Row> Question3();
        IEnumerable<Question4Row> Question4();
        IEnumerable<Question5Row> Question5();
    }
}
