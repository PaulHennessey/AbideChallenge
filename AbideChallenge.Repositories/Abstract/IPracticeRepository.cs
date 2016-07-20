using AbideChallenge.Domain;
using System.Collections.Generic;

namespace AbideChallenge.Repositories.Abstract
{
    public interface IPracticeRepository
    {
        void LoadPractices();        
        List<Practice> GetPractices();
    }
}
