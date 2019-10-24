using System;
using System.Collections.Generic;
using System.Linq;
using BangOnTekCommon;

namespace ThreeZeroThreePatternFormats
{
    public class X0xPattern
    {
        // pattern is always made up of 16 steps		
        public const byte StepsPerPattern = 16;

        public string Name { get; set; }

        /// <summary>
        /// Default constructor. Creates a blank pattern
        /// </summary>
        public X0xPattern()
        {
            // initialise all steps to EOP.
            for (int i = 0; i < StepsPerPattern; ++i)
                Steps[i] = new X0xStep();
        }

        /// <summary>
        /// Constructs a pattern from a byte array.
        /// </summary>
        /// <param name="bytes">An array of bytes. Length MUST be the same as STEPS_PER_PATTERN.</param>
        public X0xPattern(byte[] bytes)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            if (bytes.Length != StepsPerPattern)
            {
                throw new ArgumentException(nameof(bytes) +" is incorrect length");
            }

            for (int i = 0; i < StepsPerPattern; ++i)
            {
                Steps[i] = new X0xStep(bytes[i]);
            }

        }

        public X0xPattern(IEnumerable<X0xStep> steps) : this()
        {
            var x0xSteps = steps as X0xStep[] ?? steps.ToArray();

            if (x0xSteps.Length > StepsPerPattern)
            {
                throw new ArgumentException("Pattern: trying to construct a pattern with too many steps");
            }

            var i = 0;
            foreach (var step in x0xSteps)
                Steps[i++] = step;
        }

        public X0xPattern(K0ntr0lDataSet.PatternRow pattern) : this()
        {
            var stepIndex = 0;
            foreach (var step in pattern.GetStepRows())
            {
                Steps[stepIndex++] = new X0xStep(step);
            }
        }

        public X0xStep this[uint pos]
        {
            get
            {
                ValidateStepIndex(pos);
                return Steps[pos];
            }

            set
            {
                ValidateStepIndex(pos);
                Steps[pos] = value;
            }
        }



        public byte[] ToByteArray()
        {
            var bytes = new byte[Steps.Length];
            for (var i = 0; i < bytes.Length; ++i)
                bytes[i] = Steps[i].ToX0xByte();

            return bytes;
        }

        public PianoRollStep[] ToPianoRollSequence()
        {
            var sequence = new List<PianoRollStep>();

            var lastKnownNoteValue = X0xStep.X0xC2;
            foreach (var step in Steps)
            {
                if (step.IsEndOfPattern)
                    break;

                if (step.IsRest)
                {
                    sequence.Add(new PianoRollStep() { Note = lastKnownNoteValue, IsRest = true, HasSlide = step.HasSlide, HasAccent = step.HasAccent });
                }
                else
                {
                    sequence.Add(new PianoRollStep() { Note = step.NoteValue, IsRest = false, HasSlide = step.HasSlide, HasAccent = step.HasAccent });
                    lastKnownNoteValue = step.NoteValue;
                }
            }

            return sequence.ToArray();
        }


        public X0xStep[] Steps { get; } = new X0xStep[StepsPerPattern];

        public byte Length { get; set; } = StepsPerPattern;

        private static void ValidateStepIndex(uint pos)
        {
            if (pos >= StepsPerPattern)
            {
                throw new IndexOutOfRangeException("Invalid step index");
            }

            if (pos <= 0) throw new ArgumentOutOfRangeException(nameof(pos));
        }

        public void Transpose(sbyte semitones)
        {
            foreach (var step in Steps)
            {
                step.Transpose(semitones);
            }
        }
    }
}
