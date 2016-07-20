using AbideChallenge.Domain;
using System.Collections.Generic;

namespace AbideChallenge.Repositories.Abstract
{
    public interface IPrescriptionRepository
    {
        void LoadPrescriptions();
        IEnumerable<Prescription> GetPrescriptions();
    }
}
