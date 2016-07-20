using AbideChallenge.Data.Abstract;
using AbideChallenge.Domain;
using AbideChallenge.Repositories.Abstract;
using System.Collections.Generic;
using System.Web;

namespace AbideChallenge.Repositories.Concrete
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly ICSVReader _csvReader;

        public PrescriptionRepository()
        { }

        public PrescriptionRepository(ICSVReader csvReader)
        {
            _csvReader = csvReader;
        }

       
        public void LoadPrescriptions()
        {
            // Load prescriptions into application cache if necessary.
            if (HttpRuntime.Cache["Prescriptions"] == null)
            {
                _csvReader.LoadPrescriptions();
            }
        }


        public IEnumerable<Prescription> GetPrescriptions()
        {
            // Load prescriptions into application cache if necessary.
            if (HttpRuntime.Cache["Prescriptions"] == null)
            {
                _csvReader.LoadPrescriptions();
            }

            return HttpRuntime.Cache["Prescriptions"] as IEnumerable<Prescription>;
        }
    }
}
