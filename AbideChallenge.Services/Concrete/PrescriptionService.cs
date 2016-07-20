using AbideChallenge.Domain;
using AbideChallenge.Repositories.Abstract;
using AbideChallenge.Services.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace AbideChallenge.Services.Concrete
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly IPracticeRepository _practiceRepository;

        public PrescriptionService()
        { }

        public PrescriptionService(IPrescriptionRepository prescriptionRepository, IPracticeRepository practiceRepository)
        {
            _prescriptionRepository = prescriptionRepository;
            _practiceRepository = practiceRepository;
        }

        public void LoadPrescriptions()
        {
            _prescriptionRepository.LoadPrescriptions();
        }

        public IEnumerable<Prescription> GetPrescriptionsByBNFName(string bnfName)
        {
            return _prescriptionRepository
                .GetPrescriptions()
                .Where(p => p.BNFName == bnfName);            
        }


        public IEnumerable<Question3Row> Question3()
        {
            IEnumerable<Practice> allPractices = _practiceRepository.GetPractices();
            IEnumerable<Prescription> allPrescriptions = _prescriptionRepository.GetPrescriptions();

            var simpleResults = from practice in allPractices
                         join prescription in allPrescriptions on practice.PracticeCode equals prescription.Practice
                         select new Question3Row
                         {
                             PostCode = practice.PostCode,
                             Spend = prescription.ACTCost
                         };

            var groupedResults = from result in simpleResults
                          group result by result.PostCode into g
                          select new Question3Row
                          {
                              PostCode = g.Key,                              
                              Spend = g.Sum(x => x.Spend)
                          };

            var orderedResults = groupedResults.OrderByDescending(r => r.Spend).Take(5);

            return orderedResults;
        }


        /// <summary>
        /// Note that there are lots of prescriptions with invalid practice codes. For the purposes 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Question4Row> Question4()
        {
            IEnumerable<Practice> allPractices = _practiceRepository.GetPractices();
            IEnumerable<Prescription> allPrescriptions = _prescriptionRepository.GetPrescriptions();

            // Lots of things contain Flucloxacillin. We want them all apart from the ones 
            // that contain Co - Fluampicil. CHECK FOR SPELLING VARIANTS OF THIS - I THINK THIS IS WHAT BECKY WAS REFERRING TO.
            //Flucloxacillin Sodium
            //    Flucloxacillin Magnesium
            //    Co - Fluampicil(Flucloxacillin / Ampicillin)

            // Get all the prescriptions for Flucloxacillin, apart from those for Co-Fluampicil.
            var selectedPrescriptions = allPrescriptions.Where(
                                    p => p.BNFName.Contains("Flucloxacillin") && 
                                    !p.BNFName.Contains("Co-Fluampicil"));


            var notFound = selectedPrescriptions.Where(p => !allPractices.Any(r => r.PracticeCode == p.Practice));

            int count1 = selectedPrescriptions.Count();

            // Get the national mean cost
            decimal nationalMean = selectedPrescriptions.Sum(p => p.ACTCost) / selectedPrescriptions.Count();


            // Join the prescriptions with the practices to get the regions. Note that there are lots of prescriptions with invalid practice codes. 
            // For the purposes of this question they are ignored. Perhaps they should be thrown away before I calculate the national mean? Or maybe 
            // they should be distributed across the regions?       
            var simpleResults = from prescription in selectedPrescriptions
                                join practice in allPractices on prescription.Practice equals practice.PracticeCode
                                select new 
                                {
                                    Region = practice.Region,
                                    Price = prescription.ACTCost
                                };

            int count2 = simpleResults.Count();

            decimal nationalMean2 = simpleResults.Sum(p => p.Price) / simpleResults.Count();

            // Now group them by region and work out the averages
            var groupedResults = from result in simpleResults
                                 group result by result.Region into g
                                 select new Question4Row
                                 {
                                     Region = g.Key,
                                     AveragePrice = g.Sum(x => x.Price) / g.Count(),
                                     Variation = (((g.Sum(x => x.Price) / g.Count()) - nationalMean) / nationalMean) * 100
                                 };           

            return groupedResults;
        }
    }
}
