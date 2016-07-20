using AbideChallenge.Domain;

namespace AbideChallenge.Data.Abstract
{
    public interface IPostcodeHelper
    {
        Region GetRegion(string postcode);
    }
}
