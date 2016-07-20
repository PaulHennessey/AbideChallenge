using System.ComponentModel.DataAnnotations;

namespace AbideChallenge.Domain
{
    public enum Region
    {
        [Display(Name = "North Scotland")]
        NorthScotland,

        [Display(Name = "Southern Scotland")]
        SouthernScotland,

        [Display(Name = "North East")]
        NorthEast,

        [Display(Name = "North West")]
        NorthWest,

        [Display(Name = "Northern Ireland")]
        NorthernIreland,

        [Display(Name = "Wales Borders")]
        WalesBorders,

        [Display(Name = "West Midlands")]
        WestMidlands,

        [Display(Name = "East Midlands")]
        EastMidlands,

        [Display(Name = "East Anglia")]
        EastAnglia,

        [Display(Name = "Central Southern")]
        CentralSouthern,

        [Display(Name = "South West")]
        SouthWest,

        [Display(Name = "South East")]
        SouthEast,

        [Display(Name = "Home Counties")]
        HomeCounties,

        [Display(Name = "Outer London")]
        OuterLondon,

        [Display(Name = "Central London")]
        CentralLondon,

        Unknown
    }
}
