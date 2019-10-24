using System;

namespace ThreeZeroThreePatternFormats
{
    public class PhoscyonFileReader : Base303PatternFileReader
    {
        public PhoscyonFileReader(string filePath) : base(filePath)
        {
            var patternSet = new PhoscyonDataSet();
            var savedNameSpace = patternSet.Namespace;
            patternSet.Namespace = string.Empty;
                       
            patternSet.ReadXml(FilePath);
            patternSet.AcceptChanges();
            patternSet.Namespace = savedNameSpace;

            foreach (var pattern in patternSet.Pattern)
            {
                var kPattern = _data.Pattern.AddPatternRow(Guid.NewGuid(), pattern.name, byte.Parse(pattern.length), FilePath, ushort.Parse(pattern.tempo), null, string.Empty);

                byte stepNumber = 0;
                foreach (var step in pattern.GetStepRows())
                {
                     byte noteValue = (byte)(X0xStep.X0xC2 + byte.Parse(step.note));

                    if (step.octaveUp == "1")
                    {
                        noteValue += X0xStep.Octave;
                    }

                    if (step.octaveDown == "1")
                    {
                        noteValue -= X0xStep.Octave;
                    }
                    _data.Step.AddStepRow(kPattern, stepNumber++, noteValue, step.gate == "1", step.accent == "1", step.slide == "1");
                }
            }

            _data.AcceptChanges();
        }
    }
}
