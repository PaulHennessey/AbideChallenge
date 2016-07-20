using AbideChallenge.Data.Abstract;
using AbideChallenge.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;

namespace AbideChallenge.Data
{
    public class CSVReader : ICSVReader
    {
        private readonly IPostcodeHelper _postcodeHelper;

        public CSVReader()
        {}

        public CSVReader(IPostcodeHelper postcodeHelper)
        {
            _postcodeHelper = postcodeHelper;
        }

        /// <summary>
        /// Load practices into application cache.
        /// </summary>
        public void LoadPractices()
        {
            string practicesFile = ConfigurationManager.AppSettings["PracticesFile"];
            List<Practice> practices = new List<Practice>();

            using (StreamReader reader = new StreamReader(practicesFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split(',');

                    Practice practice = new Practice
                    {
                        Period = fields[0].Trim(),
                        PracticeCode = fields[1].Trim(),
                        PracticeName = fields[2].Trim(),
                        AddressLine1 = fields[3].Trim(),
                        AddressLine2 = fields[4].Trim(),
                        City = fields[5].Trim(),
                        County = fields[6].Trim(),
                        PostCode = fields[7].Trim(),
                        Region = _postcodeHelper.GetRegion(fields[7])
                    };
                    practices.Add(practice);
                }
            }

            HttpRuntime.Cache.Insert("Practices", practices);
        }


        /// <summary>
        /// Load prescriptions into application cache.
        /// </summary>
        public void LoadPrescriptions()
        {
            string prescriptionsFile = ConfigurationManager.AppSettings["PrescriptionsFile"];
            List<Prescription> prescriptions = new List<Prescription>();

            using (StreamReader reader = new StreamReader(prescriptionsFile))
            {
                // Skip the first line.
                string firstLine = reader.ReadLine();

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split(',');

                    Prescription prescription = new Prescription
                    {
                        SHA = fields[0].Trim(),
                        PCT = fields[1].Trim(),
                        Practice = fields[2].Trim(),
                        BNFCode = fields[3].Trim(),
                        BNFName = fields[4].Trim(),
                        Items = Convert.ToInt32(fields[5]),
                        NIC = Convert.ToDecimal(fields[6]),
                        ACTCost = Convert.ToDecimal(fields[7]),
                        Period = fields[8].Trim()
                    };
                    prescriptions.Add(prescription);
                }
            }

            HttpRuntime.Cache.Insert("Prescriptions", prescriptions);
        }     
    }
}
