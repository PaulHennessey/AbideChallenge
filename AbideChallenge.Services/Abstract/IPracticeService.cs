using AbideChallenge.Domain;
using System.Collections.Generic;

namespace AbideChallenge.Services.Abstract
{
    public interface IPracticeService
    {
        void LoadPractices();
        List<Practice> GetPractices();
        List<Practice> GetPracticesByRegion(Region region);
    }
}
