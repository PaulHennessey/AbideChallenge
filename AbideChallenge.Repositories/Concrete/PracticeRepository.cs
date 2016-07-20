using AbideChallenge.Data.Abstract;
using AbideChallenge.Domain;
using AbideChallenge.Repositories.Abstract;
using System.Collections.Generic;
using System.Web;

namespace AbideChallenge.Repositories.Concrete
{
    public class PracticeRepository : IPracticeRepository
    {
        private readonly ICSVReader _csvReader;

        public PracticeRepository()
        {}

        public PracticeRepository(ICSVReader csvReader)
        {
            _csvReader = csvReader;
        }

        public void LoadPractices()
        {
            // Load practices into application cache if necessary.
            if (HttpRuntime.Cache["Practices"] == null)
            {
                _csvReader.LoadPractices();
            }
        }

        public List<Practice> GetPractices()
        {
            // Load practices into application cache if necessary.
            if(HttpRuntime.Cache["Practices"] == null)
            {
                _csvReader.LoadPractices();
            }

            return HttpRuntime.Cache["Practices"] as List<Practice>;
        }
    }
}
