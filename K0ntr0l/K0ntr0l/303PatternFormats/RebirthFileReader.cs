using System;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Linq;


namespace ThreeZeroThreePatternFormats
{
    public class RebirthFileReader : Base303PatternFileReader
    {
        // Number of 303s in a rebirth song file
        public const uint NumberOf303S = 2;

        // Number of patterns in each rebirth 303
        public const uint PatternsPer303 = 32;

        private readonly BinaryReader _reader;

        public RebirthFileReader(string filePath) : base(filePath)
        {
            using (_reader = new BinaryReader(File.Open(filePath, FileMode.Open,FileAccess.Read), Encoding.ASCII))
            {
                // first should be "CAT "
                var tag = ReadChunkMarker();
                VerifyTagMatches(tag, "CAT ");

                // then comes the length of the RB40 chunk
                /*uint rb40Length =*/
                ReadChunkLength();

                // followed by the RB40 chunk marker
                tag = ReadChunkMarker();
                VerifyTagMatches(tag, "RB40");

                UInt64 tempo = 120;
                // order of items in RB40 catalog level is not fixed, 
                // although ReBirth 2.0.1 writes in a specific order... HEAD, GLOB, USRI, CAT DEVL, CAT TRKL

                string rbsWindowTitle = string.Empty;
                string rbsSongInfo = string.Empty;

                tag = ReadChunkMarker();
                while (tag != "CAT ")
                {
                    switch (tag)
                    {
                        case "GLOB":
                        {
                            var globChunkLength = ReadChunkLength();
                            Debug.Assert(globChunkLength == 512);
                            // skip the first 2 bytes to get to the tempo
                            ReadChunk(2);
                            // Tempo ULONG BPM*1000
                            byte[] tempoBytes = ReadChunk(4);
                            if (BitConverter.IsLittleEndian)
                                Array.Reverse(tempoBytes);
                            tempo = BitConverter.ToUInt32(tempoBytes,0);
                            // read to the end of the chunck
                            ReadChunk(globChunkLength - 6);
                            break;
                        }
                        case "USRI":
                        {
                            var usriChunkLength = ReadChunkLength();
                            //Debug.Assert(usriChunkLength == 512);
                            rbsWindowTitle = Encoding.Default.GetString(ReadChunk(41)).Replace("\0","");
                            rbsSongInfo = Encoding.Default.GetString(ReadChunk(201)).Replace("\r", "\r\n");
                            ReadChunk(usriChunkLength - (41 + 201));
                            break;
                        }
                        default:
                        {
                            Debug.WriteLine("Skipping '" + tag + "' section...");
                            // read the size of this cat chunk
                            var length = ReadChunkLength();
                            ReadChunk(length);
                            break;
                        }
                    }

                    tag = ReadChunkMarker();
                }

                // read the size of this cat chunk
                uint catLength = ReadChunkLength();
                // read the type, this should be the device list
                tag = ReadChunkMarker();
                VerifyTagMatches(tag, "DEVL");
                VerifyChunkLength(catLength, 14850);
                Debug.WriteLine("Found 'DEVL' (device list) section.");
                
                // next is a series of tags which we ignore.
                // their order is fixed.
                string[] tags = new string[] { "MIXR", "DELY", "PCF ", "DIST", "COMP" };
                foreach (string expectedTag in tags)
                {
                    tag = ReadChunkMarker();
                    VerifyTagMatches(tag, expectedTag);
                    ReadChunk(ReadChunkLength());
                }

                // at this point, we should be ready to get what we're after, 
                // the 303 chunks!

                // for each 303...
                for (uint i = 0; i < NumberOf303S; ++i)
                {
                    Debug.WriteLine("Reading 303 data, " + (i + 1) + " of " + NumberOf303S);
                    tag = ReadChunkMarker();
                    if (tag != "303 ")
                        break;

                    VerifyTagMatches(tag, "303 ");
                    var t0tChunkLength = ReadChunkLength();
                    VerifyChunkLength(t0tChunkLength, 1097);
                    // skip 9 bytes of info we can't use
                    ReadChunk(9);

                    // from here on we read the patterns' data
                    for (int pat = 0; pat < PatternsPer303; ++pat)
                    {
                        Debug.WriteLine("Reading pattern " + (pat + 1) + " of " + PatternsPer303);
                        var pattern = _data.Pattern.NewPatternRow();
                        pattern.PatternId = Guid.NewGuid();
                        pattern.Name = Path.GetFileNameWithoutExtension(filePath) + " - 303 #" + (i + 1) + " - Pat #" + (pat + 1).ToString("00");
                        pattern.Source = filePath;
                        pattern.Tempo = (ushort)(tempo / 1000UL);
                        pattern.Description = rbsWindowTitle + "-" + rbsSongInfo;
                                               
                        // first byte is shuffle on/off data, which we ignore [yet].
                        _reader.ReadByte();
                        // second byte is pattern length
                        pattern.Length = _reader.ReadByte();
                        if (pattern.Length > X0xPattern.StepsPerPattern)
                        {
                            pattern.Length = X0xPattern.StepsPerPattern;
                        }

                        _data.Pattern.AddPatternRow(pattern);

                        // then we have 16 steps
                        for (byte step = 0; step < pattern.Length; ++step)
                        {
                            Debug.WriteLine("Step " + (step + 1) + " of " + X0xPattern.StepsPerPattern);

                            // each step is 2 bytes
                            // the first byte is the note/tone information (from C1 to C2)
                            // the second one is a bitmask for slide/accent/rest/oct up/oct down.. etc
                            byte note = _reader.ReadByte();
                            Debug.WriteLine("  Note = " + note);
                            byte flags = _reader.ReadByte();
                            Debug.WriteLine(". Flags = " + flags);

                            // extract the flags...
                            // bit 0 = No slide/Slide (0x01)
                            var slide = (flags & 0x01) == 0x01;
                            // bit 1 = No accent/Accent (0x02)
                            var accent = (flags & 0x02) == 0x02;
                            // bit 2 = Normal/Transpose up (0x04)
                            var transposeUp = (flags & 0x04) == 0x04;
                            // bit 3 = Normal/Transpose down (0x08)
                            var transposeDown = (flags & 0x08) == 0x08;
                            // bit 4 = Pause/Note (0x10)
                            var noteOn = (flags & 0x10) != 0x10;

                            note = X0xStep.X0xC2;
                            if (transposeUp)
                            {
                                note += X0xStep.Octave;
                            }
                            if (transposeDown)
                            {
                                note -= X0xStep.Octave;
                            }

                            _data.Step.AddStepRow(pattern, step, note, noteOn, accent, slide);            
                        }
                    }
                    _reader.ReadByte();//???
                }

                // TODO: Read tempo from GLOB chunk

                _reader.Close();

                _data.AcceptChanges();
                // verify that we've read enough patterns
                /*if (_data.Pattern.Count != PATTERNS_PER_303 * NUMBER_OF_303S)
                {
                    throw new Exception("Failed to read enough patterns");
                }*/
            }
        }

        private static void VerifyTagMatches(string tag, string tagToMatch)
        {
            if (tag != tagToMatch)
            {
                throw new Exception("Expected: '" + tagToMatch + "'. Found '" + tag + "' instead.");
            }
        }

        private static void VerifyChunkLength(uint length, uint requiredLength)
        {
            if (length != requiredLength)
            {
                throw new Exception("Invalid chunk length. Expected: " + requiredLength + ". Found: " + length + ".");
            }
        }

        private string ReadChunkMarker()
        {
            Debug.WriteLine("Reading chunk marker...");
            char[] chars = _reader.ReadChars(4);
            string chunk = new string(chars);
            Debug.WriteLine("Chunk marker = '" + chunk + "'.(");
            foreach (char c in chars)
            {
                Debug.WriteLine(Convert.ToInt16(c) + ",");
            }
            return chunk;
        }

        private uint ReadChunkLength()
        {
            Debug.WriteLine("Reading chunk length...");
            // RSB files are big endian, but BinaryRead works in little endian mode,
            // hence the conversion.
            uint l = Convert.ToUInt32(System.Net.IPAddress.HostToNetworkOrder(_reader.ReadInt32()));
            Debug.WriteLine("Chunk length = " + l + " bytes.");
            return l;
        }

        private byte[] ReadChunk(uint length)
        {
            Debug.WriteLine("Reading " + length + " bytes chunk...");
            byte[] chunk = _reader.ReadBytes(Convert.ToInt32(length));
            if (chunk.Length != Convert.ToInt32(length))
            {
                throw new EndOfStreamException("End of stream found trying to read " + length + " bytes.");
            }

            return chunk;
        }

    }
}
