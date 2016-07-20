using AbideChallenge.Data.Abstract;
using AbideChallenge.Domain;
using System;
using System.Linq;

namespace AbideChallenge.Data.Concrete
{
    public class PostcodeHelper : IPostcodeHelper
    {
        /// <summary>
        /// From wikipedia:
        ///     "postcodes are alphanumeric, and are variable in length: ranging from six to eight characters 
        ///     (including a space) long. Each post code is divided into two parts separated by a single space: 
        ///     the outward code and the inward code respectively. The outward code includes the postcode area 
        ///     and the postcode district, respectively. The inward code includes the postcode sector and the 
        ///     postcode unit respectively. Examples of postcodes include "SW1W 0NY", "PO16 7GZ", "GU16 7HF", 
        ///     or "L1 8JQ"."
        ///     
        /// Again from wikipedia:
        ///     "the regions is the highest tier of sub-national government" in the UK. They are:
        ///         North Scotland
        ///         Southern Scotland
        ///         North East
        ///         North West
        ///         Northern Ireland
        ///         Wales Borders
        ///         West Midlands
        ///         East Midlands
        ///         East Anglia
        ///         Central Southern
        ///         South West
        ///         South East
        ///         Home Counties
        ///         Outer London
        ///         Central London
        ///
        /// I need to extract the postcode area and map it to a region. Ideally this would be done with a Royal Mail 
        /// web service based on PAF (the Postcode Address File) but I couldn't find a free one.
        /// </summary>
        /// <param name="postcode"></param>
        /// <returns></returns>
        public Region GetRegion(string postcode)
        {
            // Clean up string
            postcode = postcode.Trim();

            // Extract postcode district
            string district = postcode.Split(' ')[0];

            if(string.IsNullOrEmpty(postcode) ||               // Postcode is empty
               !postcode.Any(x => Char.IsWhiteSpace(x)) ||     // Postcode has no space
               Char.IsDigit(district, 0))                      // First character is a number                
            {
                return Region.Unknown;
            }
            else if (Char.IsDigit(district, 1))     // If the second character is a digit, e.g. W1.
            {
                switch (district.Substring(0, 1))
                {
                    case "G":
                        return Region.SouthernScotland;
                    case "S":
                        return Region.NorthEast;
                    case "M":
                    case "L":
                        return Region.NorthWest;
                    case "B":
                        return Region.WestMidlands;
                    case "N":
                    case "E":
                    case "W":
                        return Region.CentralLondon;
                    default:
                        return Region.Unknown;
                }
            }
            else                                    // Both characters are letters...
            {
                switch (district.Substring(0, 2))
                {
                    case "HS":
                    case "KW":
                    case "IV":
                    case "AB":
                    case "PH":
                    case "DD":
                    case "FK":
                    case "PA":
                    case "ZE":
                        return Region.NorthScotland;
                    case "KA":
                    case "KY":
                    case "EH":
                    case "ML":
                    case "DG":
                        return Region.SouthernScotland;
                    case "TD":
                    case "NE":
                    case "SR":
                    case "DH":
                    case "CA":
                    case "DL":
                    case "TS":
                    case "YO":
                    case "HG":
                    case "BD":
                    case "LS":
                    case "HX":
                    case "WF":
                    case "HD":
                    case "DN":
                    case "HU":
                        return Region.NorthEast;
                    case "LA":
                    case "IM":
                    case "FY":
                    case "PR":
                    case "BB":
                    case "OL":
                    case "BL":
                    case "WN":
                    case "SK":
                    case "WA":
                    case "CW":
                    case "CH":
                        return Region.NorthWest;
                    case "BT":
                        return Region.NorthernIreland;
                    case "LL":
                    case "SY":
                    case "LD":
                    case "SA":
                    case "HR":
                    case "NP":
                    case "CF":
                        return Region.WalesBorders;
                    case "ST":
                    case "TF":
                    case "WS":
                    case "WV":
                    case "DY":
                    case "CV":
                    case "WR":
                        return Region.WestMidlands;
                    case "LN":
                    case "NG":
                    case "DE":
                    case "LE":
                    case "NN":
                        return Region.EastMidlands;
                    case "PE":
                    case "NR":
                    case "IP":
                    case "CB":
                    case "CO":
                    case "CM":
                    case "SG":
                    case "SS":
                        return Region.EastAnglia;
                    case "GL":
                    case "OX":
                    case "MK":
                    case "SN":
                    case "RG":
                    case "GU":
                    case "SO":
                    case "SP":
                    case "BH":
                    case "PO":
                        return Region.CentralSouthern;
                    case "BS":
                    case "BA":
                    case "DT":
                    case "TA":
                    case "EX":
                    case "TQ":
                    case "PL":
                    case "TR":
                        return Region.SouthWest;
                    case "ME":
                    case "CT":
                    case "TN":
                    case "BN":
                    case "RH":
                        return Region.SouthEast;
                    case "LU":
                    case "HP":
                    case "SL":
                    case "AL":
                    case "DA":
                    case "EN":
                    case "RM":
                    case "UB":
                        return Region.HomeCounties;
                    case "WD":
                    case "HA":
                    case "TW":
                    case "SM":
                    case "CR":
                    case "BR":
                    case "IG":
                    case "KT":
                        return Region.OuterLondon;
                    case "SE":
                    case "SW":
                    case "NW":
                    case "WC":
                    case "EC":
                        return Region.CentralLondon;
                    default:
                        return Region.Unknown;
                }
            }
        }

    }
}
