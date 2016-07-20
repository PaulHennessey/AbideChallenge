using System.ComponentModel.DataAnnotations;

namespace AbideChallenge.Models
{
    public class Question2ViewModel
    {
        [DataType(DataType.Currency)]
        public decimal AverageCost { get; set; }
    }
}