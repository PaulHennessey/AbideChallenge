using AbideChallenge.Domain;
using AbideChallenge.Questions.Abstract;
using AbideChallenge.Repositories.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace AbideChallenge.Questions.Concrete
{
    public class QuestionService : IQuestionService
    {
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly IPracticeRepository _practiceRepository;

        public QuestionService()
        { }


        public QuestionService(IPrescriptionRepository prescriptionRepository, IPracticeRepository practiceRepository)
        {
            _prescriptionRepository = prescriptionRepository;
            _practiceRepository = practiceRepository;
        }

        /// <summary>
        /// How many practices are in London?
        /// 
        /// This is straightforward, just count the practices in both central and outer London.
        /// </summary>
        /// <returns></returns>
        public int Question1()
        {
            int practiceCountCentralLondon = _practiceRepository.GetPractices().Where(p => p.Region == Region.CentralLondon).Count();
            int practiceCountOuterLondon = _practiceRepository.GetPractices().Where(p => p.Region == Region.OuterLondon).Count();

            return practiceCountCentralLondon + practiceCountOuterLondon;
        }


        /// <summary>
        /// What was the average actual cost of all peppermint oil prescriptions?
        /// 
        /// Possible issue here with naming - is it possible for a prescription name to be misspelt? 
        /// Can't find any examples but I suppose it's possible. Use of fuzzy search?
        /// </summary>
        /// <returns></returns>
        public decimal Question2()
        {
            IEnumerable<Prescription> prescriptions = _prescriptionRepository.GetPrescriptions().Where(p => p.BNFName.Contains("Peppermint Oil"));

            return prescriptions.Sum(p => p.ACTCost) / prescriptions.Count();
        }


        /// <summary>
        /// Which 5 post codes have the highest actual spend, and how much did each spend in total? 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Question3Row> Question3()
        {
            IEnumerable<Practice> allPractices = _practiceRepository.GetPractices();
            IEnumerable<Prescription> allPrescriptions = _prescriptionRepository.GetPrescriptions();

            // Join the prescriptions to the practices in order to get hold of the postcodes.
            var joinedResults = from practice in allPractices
                                join prescription in allPrescriptions on practice.PracticeCode equals prescription.Practice
                                select new 
                                {
                                    PostCode = practice.PostCode,
                                    ACTCost = prescription.ACTCost
                                };

            // Now group them by postcode and sum the cost.
            var groupedResults = from result in joinedResults
                                 group result by result.PostCode into g
                                 select new Question3Row
                                 {
                                     PostCode = g.Key,
                                     Spend = g.Sum(x => x.ACTCost)
                                 };

            // Order them and grab the top 5.
            var orderedResults = groupedResults.OrderByDescending(r => r.Spend).Take(5);

            return orderedResults;
        }



        /// <summary>
        /// For each region of England (North East, South West, London, etc.): 
        ///     a.What was the average price per prescription of Flucloxacillin(excluding CoFluampicil)? 
        ///     b.How much did this vary from the national mean?
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Question4Row> Question4()
        {
            IEnumerable<Practice> allPractices = _practiceRepository.GetPractices();
            IEnumerable<Prescription> allPrescriptions = _prescriptionRepository.GetPrescriptions();

            // There are several products that contain Flucloxacillin. For example:
            //    Flucloxacillin Sodium
            //    Flucloxacillin Magnesium
            //    Co-Fluampicil(Flucloxacillin / Ampicillin)
            // We want them all apart thosse that contain Co-Fluampicil.
            var selectedPrescriptions = allPrescriptions.Where(p => p.BNFName.Contains("Flucloxacillin") && !p.BNFName.Contains("Co-Fluampicil"));


            // Join the prescriptions with the practices to get the regions. 
            var simpleResults = from prescription in selectedPrescriptions
                                join practice in allPractices on prescription.Practice equals practice.PracticeCode
                                select new
                                {
                                    Region = practice.Region,
                                    Price = prescription.ACTCost
                                };


            // Get the national mean cost.
            // Note that there are lots of prescriptions with invalid practice codes - for exammple, there is a prescription from practice Y01784,
            // which doesn't exist. We calcualate the mean here, after the join, so the prescriptions with invalid codes are ignored.
            decimal nationalMean = simpleResults.Sum(p => p.Price) / simpleResults.Count();


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

        /// <summary>
        /// Which five practices had the highest spend?
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Question5Row> Question5()
        {
            IEnumerable<Practice> allPractices = _practiceRepository.GetPractices();
            IEnumerable<Prescription> allPrescriptions = _prescriptionRepository.GetPrescriptions();

            // Join the prescriptions with the practices. 
            var simpleResults = from prescription in allPrescriptions
                                join practice in allPractices on prescription.Practice equals practice.PracticeCode
                                select new
                                {
                                    Code = practice.PracticeCode,
                                    Name = practice.PracticeName,
                                    Town = practice.City,
                                    Region = practice.Region,
                                    Cost = prescription.ACTCost
                                };


            // Now group them by practice code and work out the spend
            var groupedResults = from result in simpleResults
                                 group result by result.Code into g
                                 select new Question5Row
                                 {
                                     PracticeName = g.First().Name,
                                     PracticeTown = g.First().Town,
                                     Region = g.First().Region,
                                     Spend = g.Sum(x => x.Cost)
                                 };

            // Order them and grab the top 5.
            var orderedResults = groupedResults.OrderByDescending(r => r.Spend).Take(5);

            return orderedResults;
        }

    }
}
