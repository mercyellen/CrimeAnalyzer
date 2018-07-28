using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CrimeAnalyzer
{
    public class CrimeStats
    {
        public CrimeStats()
        {
            
        }

        public double Year { 
            get; 
            set; 
        }
        public double Population { 
            get;
            set;
        }
        public double Murders { 
            get; 
            set; 
        }
        public double Rapes { 
            get;
            set;
        }
        public double Robberies { 
            get;
            set;
        }
        public double AggravatedAssaults {
            get;
            set;
        }
        public double PropertyCrimes {
            get;
            set;
        }
        public double Burglaries {
            get;
            set;
        }
        public double ViolentCrimes { 
            get;
            set;
        }
        public double Thefts { 
            get;
            set; 
        }
        public double MotorVehicleThefts { 
            get;
            set;
        } 


        private static List<CrimeStats> crimeStatsList = new List<CrimeStats>();

        public void Read(string file)
        {
            Console.WriteLine("Reading data from file : {file}");
            try
            {
                string[] lines = File.ReadAllLines(file);
                for (int index = 0; index < lines.Length; index++)
                {
                    string line = lines[index];
                    string[] values = line.Split(',');

                    CrimeStats crimeStat = new CrimeStats();
                    crimeStat.Year = Int32.Parse(values[0]);
                    crimeStat.Population = Int32.Parse(values[1]);
                    crimeStat.Murders = Int32.Parse(values[2]);
                    crimeStat.Rapes = Int32.Parse(values[3]);
                    crimeStat.Robberies = Int32.Parse(values[4]);
                    crimeStat.ViolentCrimes = Int32.Parse(values[5]);
                    crimeStat.Thefts = Int32.Parse(values[6]);
                    crimeStat.MotorVehicleThefts = Int32.Parse(values[7]);

                    crimeStatsList.Add(crimeStat);
                }
                Console.WriteLine("Reading completed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to read CSV file. Please try again.");
                throw ex;
            }
        }

        public void linqWriting(string filePath)
        {
            try
            {
                if (crimeStatsList != null && crimeStatsList.Any())
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("Crime Analyzer Report\n");

                    double minYear = crimeStatsList.Min(n => n.Year);
                    double maxYear = crimeStatsList.Max(n => n.Year);

                    double yearRange = maxYear - minYear + 1;
                    stringBuilder.Append("Period: {minYear}-{maxYear} ({yearRange} years)\n");


                    var numberOfYears = from crimeStat in crimeStatsList
                                        select crimeStat.Year;

                    stringBuilder.Append("Years murders per year: {mYearsStr}\n");

                    var yearsWhereMurdersWereLessThan15000 = from crimeStat in crimeStatsList
                                                             where crimeStat.Murders < 15000
                                                             select crimeStat.Year;
                    stringBuilder.Append("Years murders per year < 15000: {yearsWhereMurdersWereLessThan15000}\n");

                    var aWholeLotOfRobberies = from crimeStat in crimeStatsList
                                               where crimeStat.Robberies > 500000
                                               select crimeStat;


                    string robberiesYears = null;
                    for (int i = 0; i < aWholeLotOfRobberies.Count(); i++)
                    {
                        CrimeStats crimeStats = aWholeLotOfRobberies.ElementAt(i);
                        robberiesYears += "{crimeStats.Year} = {crimeStats.Robberies}";
                        if (i < aWholeLotOfRobberies.Count() - 1)
                        {
                            robberiesYears += ", ";
                        }
                    }
                    stringBuilder.Append("Robberies per year > 500000: {robberiesYears}\n");

                    var violentCrime = from crimeStats in crimeStatsList
                                       where crimeStats.Year == 2010
                                       select crimeStats;

                    CrimeStats violentCrimeData = violentCrime.First();
                    double violentCrimePerCapita = (double)violentCrimeData.ViolentCrimes / (double)violentCrimeData.Population;
                    stringBuilder.Append("Violent crime per capita rate (2010): {violentCrimePerCapita}\n");

                    double avgMurders = crimeStatsList.Sum(n => n.Murders) / crimeStatsList.Count;
                    stringBuilder.Append("Average murder per year (all years): {avgMurders}\n");

                    double murders1 = crimeStatsList
                    .Where(n => n.Year >= 1994 && n.Year <= 1997)
                    .Sum(y => y.Murders);
                    double avgMurders1 = murders1 / crimeStatsList.Count;
                    stringBuilder.Append("Average murder per year (1994-1997): {avgMurders1}\n");

                    double murders2 = crimeStatsList
                    .Where(n => n.Year >= 2010 && n.Year <= 2014)
                    .Sum(y => y.Murders);
                    
                    double avgMurders2 = murders2 / crimeStatsList.Count;
                    stringBuilder.Append("Average murder per year (2010-2014): {avgMurders2}\n");

                    double minTheft = crimeStatsList
                    .Where(n => n.Year >= 1999 && n.Year <= 2004)
                    .Min(n => n.Thefts);
                    stringBuilder.Append("Minimum thefts per year (1999-2004): {minTheft}\n");

                    double maxTheft = crimeStatsList
                    .Where(n => n.Year >= 1999 && n.Year <= 2004)
                    .Max(n => n.Thefts);
                    stringBuilder.Append("Maximum thefts per year (1999-2004): {maxTheft}\n");

                    double maxVehicleTheft = crimeStatsList.OrderByDescending(n => n.MotorVehicleThefts).First().Year;
                    stringBuilder.Append("Year of highest number of motor vehicle thefts: {maxVehicleTheft}\n");

                    using (var stream = new StreamWriter(filePath))
                    {
                        stream.Write(stringBuilder);
                    }
                    Console.WriteLine("Written report successfully.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in writing report file.");
                throw e;
            }
        }
    }
}
