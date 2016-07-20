using AbideChallenge.Domain;
using AbideChallenge.Repositories.Abstract;
using AbideChallenge.Services.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace AbideChallenge.Services.Concrete
{
    public class PracticeService : IPracticeService
    {
        private readonly IPracticeRepository _practiceRepository;

        public PracticeService()
        {}

        public PracticeService(IPracticeRepository practiceRepository)
        {
            _practiceRepository = practiceRepository;
        }

        public void LoadPractices()
        {
            _practiceRepository.GetPractices();
        }

        public List<Practice> GetPractices()
        {
            return _practiceRepository.GetPractices();
        }

        public List<Practice> GetPracticesByRegion(Region region)
        {
            return _practiceRepository
                .GetPractices()
                .Where(p => p.Region == region)
                .ToList();
        }

    }
}
