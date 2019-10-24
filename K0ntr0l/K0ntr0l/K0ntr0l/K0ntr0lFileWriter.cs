using System.Collections.Generic;
using System.IO;
using ThreeZeroThreePatternFormats;

namespace K0ntr0l
{
    public class K0ntr0lFileWriter
    {
        public const byte XbpVersion = 100;

        public K0ntr0lFileWriter(ICollection<X0xPattern> patterns, string filePath)
        {
            using (BinaryWriter writer = new BinaryWriter(File.OpenWrite(filePath)))
            {
                // write the xoxbox pattern file dump...
                // it's all big endian, so again conversions apply...

                // first the version number
                writer.Write(XbpVersion);

                // next the number of patterns
                writer.Write((byte)0);
                writer.Write((byte)patterns.Count);

                // bank and location numbers are 1-based
                byte bankNum = 1;
                byte locNum = 1;
                // now write each pattern
                foreach (X0xPattern pattern in patterns)
                {
                    if (locNum > 7)
                    {
                        ++bankNum;
                        locNum = 1;
                    }

                    // Emulate the c0ntr0l bug!
                    if (bankNum == 0x0a)
                    {
                        writer.Write((byte)0x0d);
                    }

                    writer.Write(bankNum);
                    writer.Write(locNum);
                    foreach (var step in pattern.Steps)
                    {
                        var x0xStep = step.NoteValue;
                        if (x0xStep != X0xStep.X0xEop && x0xStep != X0xStep.X0xRest)
                        {
                            if (step.HasSlide)
                            {
                                x0xStep |= 0x80; // set the highest bit
                            }

                            if (step.HasAccent)
                            {
                                x0xStep |= 0x40; // set the 7th bit
                            }
                        }

                        writer.Write(x0xStep);
                    }

                    ++locNum;
                }
                // Done.
                writer.Close();
            }
        }
    }
}
