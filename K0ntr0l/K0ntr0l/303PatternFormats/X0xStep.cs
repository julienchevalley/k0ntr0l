using System.Diagnostics;

namespace ThreeZeroThreePatternFormats
{
    public class X0xStep
    {
        public const byte HighestNoteValue = 0x3F; // E-5
        public const byte LowestNoteValue = 0x0B; // C-1
        public const int FreebeeStepSize = 9; // 9 characters

        public static string[] NoteNames => new [] { "c", "c#", "d", "d#", "e", "f", "f#", "g", "g#", "a", "a#", "b" };

        // the note value for a rest
        public const byte X0xRest = 0x00;
        // the note value for an end of pattern
        public const byte X0xEop = 0xFF;
        // the note value of C2 (the lowest C on the x0x keyboard - without any Up/Done)
        public const byte X0xC2 = 0x17;

        // an octave is 12 steps up/down
        public const byte Octave = 0x0C;


        public X0xStep() => NoteValue = X0xEop;

        public X0xStep(byte noteValue, bool slide, bool accent)
        {
            NoteValue = (byte)(noteValue & HighestNoteValue);
            HasSlide = slide;
            HasAccent = accent;
        }

        /// <summary>
        /// Constructs a step from its bare byte x0x internal representation.
        /// </summary>
        /// <param name="step"></param>
        public X0xStep(byte step)
        {
            if (step != X0xEop)
            {
                NoteValue = (byte)(step & HighestNoteValue); // clear the 2 highest bits just in case...

                HasAccent = (step & 0x40) == 0x40;
                HasSlide = (step & 0x80) == 0x80;
            }
            else
            {
                NoteValue = X0xEop;
            }
        }

        public X0xStep(K0ntr0lDataSet.StepRow step)
        {
            NoteValue = ! step.Gate ? X0xRest : step.Note;

            HasAccent = step.Accent;
            HasSlide = step.Slide;
        }

        public bool HasAccent { get; set; }

        public bool HasSlide { get; set; }

        public bool IsRest
        {
            get => NoteValue == X0xRest;
            set => NoteValue = X0xRest;
        }

        public bool IsEndOfPattern => NoteValue == X0xEop;

        public byte NoteValue { get; set; }

        /// <summary>
        /// Converts a step to its equivalent FreeBee notation
        /// </summary>
        public string ToFreeBee()
        {
            const string freeBeeTemplate = "{0} {1} {2} {3}"; // <note> <gate> <slide> <accent>
            
            string note;

            if (IsEndOfPattern)
            {
                note = string.Empty;// System.Environment.NewLine;
            }
            else
            {
                string gate;

                if (IsRest)
                {
                    // A fundamental issue is that the x0x box does not store gate/rest AND note information. its either a note, X-OR a rest.
                    // Making a note a rest loses the note data.
                    // So we'll output c2s instead...
                    note = "c-2";
                    gate = "0";
                }
                else
                {
                    gate = "1";
                    // convert x0x note value to a string.
                   
                    var octave = (NoteValue + 1) / Octave;
                    var noteNameIndex = (NoteValue + 1) % Octave;

                    Debug.Assert(NoteNames.Length == Octave);
                    Debug.Assert(noteNameIndex >= 0 && noteNameIndex < Octave);

                    note = NoteNames[noteNameIndex];
                    if (note.Length == 1)
                    {
                        note += "-";
                    }

                    note += octave;
                    Debug.Assert(note.Length == 3);
                }

                var slide = HasSlide ? "1" : "0";
                var accent = HasAccent ? "1" : "0";
                note = string.Format(freeBeeTemplate, note, gate, slide, accent);
            }

            return note;
        }

        public byte ToX0xByte()
        {
            var b = NoteValue;

            if (b == X0xEop) return b;

            if (HasAccent)
                b += 0x40;

            if (HasSlide)
                b += 0x80;

            return b;
        }

        public void Transpose(sbyte semitones)
        {
            if (IsEndOfPattern || IsRest)
                return;

            NoteValue = (byte)(NoteValue + semitones);
        }

    }
}
