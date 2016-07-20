using AbideChallenge.Domain;
using System.Collections.Generic;

namespace AbideChallenge.Services.Abstract
{
    public interface IPrescriptionService
    {
        void LoadPrescriptions();
        IEnumerable<Prescription> GetPrescriptionsByBNFName(string bnfName);
        IEnumerable<Question3Row> Question3();
        IEnumerable<Question4Row> Question4();
    }
}
