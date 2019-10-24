using BangOnTekCommon;

namespace ThreeZeroThreePatternFormats
{
    public class Base303PatternFileReader
    {
        private bool _isX0xConversionDone;
        protected readonly K0ntr0lDataSet _data = new K0ntr0lDataSet();
        
        public string FilePath { protected set; get; }

        public Base303PatternFileReader(string filePath)
        {
            FilePath = filePath;
        }

        public K0ntr0lDataSet Data
        {
            get
            {
                if (!_isX0xConversionDone)
                {
                    PerformX0xConversion();
                }

                return _data;
            }
        }

        private void PerformX0xConversion()
        {
            foreach (var pattern in _data.Pattern)
            {
                pattern.X0xPatternBytes = new X0xPattern(pattern).ToByteArray();

                System.Diagnostics.Debug.WriteLine("Converted pattern: " + pattern.Name + "(" + pattern.PatternId.ToString() + ")" + " bytes = " + pattern.X0xPatternBytes.ToByteString());
            }
            
            _data.AcceptChanges();

            _isX0xConversionDone = true;
        }
    }
}
