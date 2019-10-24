using System;
using System.Net;
using System.Diagnostics;
using System.Globalization;

namespace K0ntr0l
{
    class K0ntr0lVersionChecker
    {
        public static readonly string K0ntr0lVersionFilePath = "https://kontrol.bangontek.com/latestbuildnumber.txt";

        private readonly NumberFormatInfo _numFormatInfo;
        
        public decimal LatestBuildNumber
        {
            get;
        }

        public decimal CurrentBuildNumber => decimal.Parse(Properties.Resources.BuildNumber, _numFormatInfo);

        public bool UpdateRequired => LatestBuildNumber > CurrentBuildNumber;

        public K0ntr0lVersionChecker()
        {
            _numFormatInfo = new NumberFormatInfo {CurrencyDecimalSeparator = "."};

            using (var client = new WebClient())
            {
                try
                {
                    var s = client.DownloadString(K0ntr0lVersionFilePath);
                    
                    LatestBuildNumber = decimal.Parse(s, _numFormatInfo);
                }
                catch(Exception ex)
                {
                    Debug.WriteLine("K0ntr0lVersionChecker - Error reading {0} : {1}",
                        K0ntr0lVersionFilePath, ex.Message);
                    throw;
                }
            }
        }
    }
}
