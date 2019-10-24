using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using BangOnTekCommon;

namespace ThreeZeroThreePatternFormats
{
    public partial class K0ntr0lDataSet
    {
        private readonly Dictionary<string, Guid> _patternDictionary = new Dictionary<string, Guid>();

        public void RebuildX0xPatternIndex()
        {
            _patternDictionary.Clear();

            foreach (var pattern in Pattern)
            {
                _patternDictionary.Add(pattern.X0xPatternBytes.ToByteString(), pattern.PatternId);
            }
        }

        public void AddPatternsIfRequired(K0ntr0lDataSet data)
        {
            foreach (var pattern in data.Pattern)
            {
                string bytes = pattern.X0xPatternBytes.ToByteString();
                if (!_patternDictionary.ContainsKey(bytes))
                {
                    Pattern.ImportRow(pattern);
                    _patternDictionary.Add(bytes, pattern.PatternId);
                    foreach (var step in pattern.GetStepRows())
                    {
                        Step.ImportRow(step);
                    }
                }
                else
                {
                    if (pattern.Source != "x0xb0x")
                    {
                        var target = Pattern.FindByPatternId(_patternDictionary[bytes]);
                        if (target.Name == "Empty")
                            continue;
                        if (target.Source == "x0xb0x" || 
                            (target.Description == string.Empty && pattern.Description != string.Empty ))
                        {
                            Debug.Assert(target.Name != "Empty");
                            target.BeginEdit();
                            target.Source = pattern.Source;
                            target.Name = pattern.Name;
                            target.Description = pattern.Description;
                            target.EndEdit();
                            target.AcceptChanges();
                        }
                    }
                }
            }
        }

        public PatternRow AddPatternIfRequired(X0xPattern x0xPattern, ushort tempo)
        {
            var bytes = x0xPattern.ToByteArray();
            var bytesstring = bytes.ToByteString();

            if (_patternDictionary.TryGetValue(bytesstring, out Guid patternGuid))
            {
                return Pattern.FindByPatternId(patternGuid);
            }

            // Add the pattern
            var pattern = Pattern.AddPatternRow(Guid.NewGuid(), x0xPattern.Name, x0xPattern.Length, "x0xb0x", tempo, bytes, string.Empty);
            _patternDictionary.Add(bytesstring, pattern.PatternId);

            // Add the steps
            byte lastNoteValue = X0xStep.X0xC2;
            byte stepNumber = 0;
            foreach (var step in x0xPattern.Steps)
            {
                if (step.IsEndOfPattern)
                    break;

                byte noteValue;
                if (step.IsRest)
                {
                    noteValue = lastNoteValue;
                }
                else
                {
                    noteValue = step.NoteValue;
                    lastNoteValue = noteValue;
                }

                Step.AddStepRow(pattern, stepNumber++, noteValue, !step.IsRest, step.HasAccent, step.HasSlide);
            }

            // Set the pattern length
            pattern.Length = stepNumber;

            return pattern;
        }

        partial class PatternDataTable
        {

        }

        partial class PatternRow
        {
            public void OctaveUp()
            {
                foreach (var step in GetStepRows())
                    step.Note += X0xStep.Octave;
            }

            public void OctaveDown()
            {
                foreach (var step in GetStepRows())
                    step.Note -= X0xStep.Octave;
            }

            public string ToFreeBeeString()
            {
                StringBuilder sb = new StringBuilder();

                foreach (var step in GetStepRows())
                {
                    sb.AppendFormat("{0}{1}", step.ToFreeBeeString(), Environment.NewLine);//.NoteValue.ToString("X2"));
                }

                return sb.ToString();
            }

            public PianoRollStep[] ToPianoRollSequence()
            {
                var sequence = new List<PianoRollStep>();

                foreach (var step in GetStepRows())
                {
                    sequence.Add(new PianoRollStep() { Note = step.Note, IsRest = !step.Gate, HasSlide = step.Slide, HasAccent = step.Accent });
                }

                return sequence.ToArray();
            }
        }

        partial class StepRow
        {
            public string ToFreeBeeString()
            {
                const string freeBeeTemplate = "{0} {1} {2} {3}"; // <note> <gate> <slide> <accent>

                int octave = (Note + 1) / X0xStep.Octave;
                int noteNameIndex = (Note + 1) % X0xStep.Octave;

                Debug.Assert(X0xStep.NoteNames.Length == X0xStep.Octave);
                Debug.Assert(noteNameIndex >= 0 && noteNameIndex < X0xStep.Octave);

                string note = X0xStep.NoteNames[noteNameIndex];
                if (note.Length == 1)
                {
                    note += "-";
                }

                note += octave;
                Debug.Assert(note.Length == 3);

                return string.Format(freeBeeTemplate, note, Gate ? "1" : "0", Slide ? "1" : "0", Accent ? "1" : "0");
            }
        }

    }
}
