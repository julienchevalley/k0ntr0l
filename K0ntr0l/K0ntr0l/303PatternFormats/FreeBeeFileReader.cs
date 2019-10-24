using System;
using System.IO;
using System.Diagnostics;

namespace ThreeZeroThreePatternFormats
{
    public class FreeBeeFileReader : Base303PatternFileReader
    {
        private readonly string _tempoMarker = "Tempo: ";

         public FreeBeeFileReader(string filePath) : base(filePath)
        {
            using (var reader = new StreamReader(FilePath))
            {
                K0ntr0lDataSet.PatternRow currentPattern = null;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line.StartsWith(";"))
                    {
                        if (line.Contains("ABL3"))
                        {
                            Debug.WriteLine("Ignoring ABL3 pattern format");
                            break;
                        }

                        int tempoIndex = line.IndexOf(_tempoMarker, StringComparison.Ordinal);
                        if (tempoIndex != -1)
                        {
                            if (currentPattern == null)
                                currentPattern = CreatePattern();

                            int tempoDotIndex = line.IndexOf('.', tempoIndex);
                            string tempoValue = line.Substring(tempoIndex + _tempoMarker.Length, tempoDotIndex - (tempoIndex + _tempoMarker.Length));
                            if (ushort.TryParse(tempoValue, out ushort tempo))
                                currentPattern.Tempo = tempo;

                        }
                    }

                    if (line.Length >= X0xStep.FreebeeStepSize && line[0] != ';')
                    {
                        if (currentPattern == null)
                        {
                            currentPattern = CreatePattern();
                        }

                        AddStepToPattern(currentPattern, line);

                        if (currentPattern.Length == X0xPattern.StepsPerPattern)
                        {
                            currentPattern = null;
                        }
                    }
                }
            }

            _data.AcceptChanges();
        }

        private K0ntr0lDataSet.PatternRow CreatePattern()
        {
            var patternDescription = Path.GetFileNameWithoutExtension(FilePath);
            if ( _data.Pattern.Count > 0)
                patternDescription += $" (part {_data.Pattern.Count + 1})";

            return _data.Pattern.AddPatternRow(Guid.NewGuid(), patternDescription, 0, FilePath, 0, null,string.Empty);
        }

        private void AddStepToPattern(K0ntr0lDataSet.PatternRow pattern, string freeBeeStep)
        {
            const string incorrectFormat = "FREEBEE step is incorrectly formatted: '{0}'. {1}.";
                       
            string step = freeBeeStep.ToLower().Replace(Environment.NewLine, string.Empty).Trim();

            if (step == string.Empty)
            {
                return;
            }
                        
            string[] noteComponents = step.Split(new [] {' '}, 4);

            // Sanity checks...
            if (noteComponents.Length != 4)
                throw new ArgumentException(string.Format(incorrectFormat, step, "Step is too short, something is missing"));

            if (noteComponents[0].Length != 3)
                throw new ArgumentException(string.Format(incorrectFormat, step, "Unrecognised note value"));

            // convert the note value
            var noteName = noteComponents[0].Substring(0, 2).Replace("-", string.Empty);
            var noteIndex = Array.IndexOf(X0xStep.NoteNames, noteName); 
                        
            if (noteIndex >= X0xStep.Octave || noteIndex < 0)
            {
                throw new ArgumentException(string.Format(incorrectFormat, step, "Unrecognised note name"));
            }

            if (! int.TryParse(noteComponents[0].Substring(2,1),out var octave))
                throw new ArgumentException(string.Format(incorrectFormat, step, "Unrecognised octave value"));

            if (octave < 1)
                octave = 1;

            if (octave > 5)
                octave = 5;

                        
            int noteValue = noteIndex - 1 + octave * X0xStep.Octave;
            // ABL is tuned one octave lower...
            noteValue -= X0xStep.Octave;

            if (noteValue < X0xStep.LowestNoteValue)
                noteValue += X0xStep.Octave;
            else if (noteValue > X0xStep.HighestNoteValue)
                noteValue -= X0xStep.Octave;

            _data.Step.AddStepRow(pattern, pattern.Length, (byte)noteValue, noteComponents[1] == "1", noteComponents[3] == "1", noteComponents[2] == "1");

            pattern.Length++;
        }
    }
}
