using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace AbideChallenge.Domain
{
    public class Question3Row
    {
        public string PostCode { get; set; }

        [DataType(DataType.Currency)]
        public decimal Spend { get; set; }
    }

    public class Question4Row
    {
        public Region Region { get; set; }

        // This gets hold of the enum display attributes. Probably should be in an extension method somewhere...
        public string RegionDisplay
        {
            get
            {
                MemberInfo memberInfo = Region.GetType().GetMember(Region.ToString()).First();

                if (memberInfo.CustomAttributes.Count() == 0)
                {
                    return memberInfo.Name;
                }
                else
                {
                    return memberInfo.GetCustomAttribute<DisplayAttribute>().GetName();
                }
            }
        }

        [DataType(DataType.Currency)]
        public decimal AveragePrice { get; set; }

        public decimal Variation { get; set; }
    }


    public class Question5Row
    {
        public string PracticeName { get; set; }
        public string PracticeTown { get; set; }
        public Region Region { get; set; }

        // This gets hold of the enum display attributes. Probably should be in an extension method somewhere...
        public string RegionDisplay
        {
            get
            {
                MemberInfo memberInfo = Region.GetType().GetMember(Region.ToString()).First();

                if (memberInfo.CustomAttributes.Count() == 0)
                {
                    return memberInfo.Name;
                }
                else
                {
                    return memberInfo.GetCustomAttribute<DisplayAttribute>().GetName();
                }
            }
        }

        [DataType(DataType.Currency)]
        public decimal Spend { get; set; }
    }
}
