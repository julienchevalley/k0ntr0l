using System;
using System.Drawing;
using System.Windows.Forms;

namespace BangOnTekCommon
{
    public sealed partial class PianoRoll : Control
    {
        private PianoRollStep[] _sequence = new PianoRollStep[0];

        private const string Sharp = "Sharp";
        public enum KeyName { C, CSharp, D, DSharp, E, F, FSharp, G, GSharp, A, ASharp, B };
        public enum KeyColor { Black, White };

        private KeyName _lowestKey = KeyName.C;
        private int _keysCount = 25;
        private int _whiteKeyLength = 50;
        private int _blackKeyLength = 25;
        
        private float _laneHeight;
        private float _stepWidth;

        private static readonly Pen BlackPen = new Pen(Color.Black);
        private static readonly Pen LaneSeparatorPen = new Pen(Color.LightGray);
        private static readonly Pen BarDividerPen = new Pen(Color.DarkGray);
        private static readonly Pen QuarterDividerPen = new Pen(Color.White);
        private static readonly SolidBrush BlackBrush = new SolidBrush(Color.Black);
        private static readonly SolidBrush WhiteKeyNoteLaneBrush = new SolidBrush(Color.WhiteSmoke);
        private static readonly SolidBrush BlackKeyNoteLaneBrush = new SolidBrush(Color.LightGray);
        private static readonly SolidBrush WhiteBrush = new SolidBrush(Color.White);

        public static SolidBrush NoteBrush = new SolidBrush(Color.Navy);
        public static SolidBrush AccentedNoteBrush = new SolidBrush(Color.Blue);
        public static SolidBrush RestBrush = new SolidBrush(Color.Green);
        public static SolidBrush AccentedRestBrush = new SolidBrush(Color.SpringGreen);
        public static float SlidePenThickness = 2.0F;
        public static Pen SlidePen = new Pen(Color.Red, SlidePenThickness);
        
        private Font _rootKeyLabelFont;
        private static readonly StringFormat RooKeyLabelFormat = new StringFormat();

        public PianoRollStep[] Sequence
        {
            get => _sequence;
            set
            {
                _sequence = value ?? new PianoRollStep[0];

                Invalidate();
            }
        }

        public int BlackKeyLength
        {
            get => _blackKeyLength;

            set
            {
                if (value != _blackKeyLength)
                {
                    _blackKeyLength = value;
                    RecalculateLayout();
                }
            }
        }

        public int WhiteKeyLength
        {
            get => _whiteKeyLength;

            set
            {
                if (value != _whiteKeyLength)
                {
                    _whiteKeyLength = value;
                    RecalculateLayout();
                }
            }
        }
        
        public KeyName LowestKey 
        {
            get => _lowestKey;

            set
            {
                if (value != _lowestKey)
                {
                    _lowestKey = value;
                    
                    RecalculateLayout();
                }
            }
        }

        public Byte LowestKeyNoteValue
        {
            get;
            set;
        }

        public Byte RootKeyNoteValue
        {
            get;
            set;
        }

        public string RootKeyLabel
        {
            get;
            set;
        }


        public int KeysCount 
        {
            get => _keysCount;

            set
            {
                if (value != _keysCount)
                {
                    _keysCount = value;
                    RecalculateLayout();
                }
            }
        }

        public PianoRoll()
        {
            DoubleBuffered = true;

            InitializeComponent();

            RecalculateLayout();
            
            RooKeyLabelFormat.Alignment = StringAlignment.Center;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            // White background for the keys area
            pe.Graphics.FillRectangle(WhiteBrush, 0, 0.0F, _whiteKeyLength, ClientRectangle.Height);

            // light gray background for the lanes area
            pe.Graphics.FillRectangle(WhiteKeyNoteLaneBrush, _whiteKeyLength, 0.0F, ClientRectangle.Width - _whiteKeyLength, ClientRectangle.Height);
            
            // Draw the right edge of the keys
            pe.Graphics.DrawLine(BlackPen, _whiteKeyLength, 0.0F, _whiteKeyLength, ClientRectangle.Height);

            // Draw the keys and lanes
            KeyName currentKey = _lowestKey;
            for (float yPos = ClientRectangle.Height - _laneHeight; yPos >= 0.0F; yPos -= _laneHeight)
            {
                KeyName nextKey = IncrementKey(currentKey);

                if (IsBlackKey(currentKey))
                {
                    pe.Graphics.FillRectangle(BlackBrush, 0.0F, yPos, _blackKeyLength, _laneHeight);
                    pe.Graphics.DrawLine(BlackPen, 0.0F, yPos + _laneHeight / 2.0F, _whiteKeyLength, yPos + _laneHeight / 2.0F);
                    pe.Graphics.FillRectangle(BlackKeyNoteLaneBrush, WhiteKeyLength + 1.0F, yPos, ClientRectangle.Width - WhiteKeyLength, _laneHeight); 
                }
                else
                {
                    if (!IsBlackKey(nextKey))
                    {
                        pe.Graphics.DrawLine(BlackPen, 0.0F, yPos, _whiteKeyLength, yPos);
                        pe.Graphics.DrawLine(LaneSeparatorPen, _whiteKeyLength + 1.0F, yPos, ClientRectangle.Right, yPos); 
                    }
                }

                currentKey = nextKey;
            }

            // Draw the step divider
            int stepCount = 0;
            for (float xPos = _whiteKeyLength + _stepWidth; xPos <= ClientRectangle.Width; xPos += _stepWidth)
            {
                if (stepCount == 3)
                {
                    stepCount = 0;
                    pe.Graphics.DrawLine(BarDividerPen, xPos, 0.0F, xPos, ClientRectangle.Height);
                }
                else
                {
                    pe.Graphics.DrawLine(QuarterDividerPen, xPos, 0.0F, xPos, ClientRectangle.Height);
                    stepCount++;
                }
            }

            PaintRootKeyLabel(pe.Graphics);

            PaintSequence(pe.Graphics);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            RecalculateLayout();
        }

        private void PaintSequence(Graphics g)
        {
            for (int i = 0; i < _sequence.Length; ++i)
            {
                PaintStep(g, i, _sequence[i].Note, _sequence[i].HasAccent, _sequence[i].HasSlide, _sequence[i].IsRest);
            }
        }

        private void PaintRootKeyLabel(Graphics g)
        {
            if (! string.IsNullOrEmpty(RootKeyLabel) && RootKeyNoteValue != 0
                && _laneHeight > 5.0f)
            {
                // Draw the note label, centered on the key.
                float yPos = GetNoteKeyYPos(RootKeyNoteValue);
                RectangleF labelRect;
                Brush textBrush;
                if (IsNoteBlackKey(RootKeyNoteValue))
                {
                    labelRect = new RectangleF(0.0F, yPos, _blackKeyLength, _laneHeight);
                    textBrush = WhiteBrush;
                }
                else
                {
                    labelRect = new RectangleF(_blackKeyLength - 10.0F, yPos - 1.0F, _whiteKeyLength + 10.0F - _blackKeyLength, _laneHeight);
                    textBrush = BlackBrush;
                }

                g.DrawString(RootKeyLabel, _rootKeyLabelFont, textBrush, labelRect, RooKeyLabelFormat);
            }
        }

        private void PaintStep(Graphics g, int stepNumber, byte noteValue, bool hasAccent, bool hasSlide, bool isRest)
        {
            var xPos = GetStepXPos(stepNumber);

            var yPos = GetNoteKeyYPos(noteValue);

            Brush stepBrush;
            if (isRest)
            {
                stepBrush = hasAccent ? AccentedRestBrush : RestBrush;
            }
            else
            {
                stepBrush = hasAccent ? AccentedNoteBrush : NoteBrush;
            }

            g.FillRectangle(stepBrush, xPos, yPos + 1, _stepWidth - 2, _laneHeight - 2);

            if (hasSlide)
            {
                g.DrawLine(SlidePen, xPos, yPos + _laneHeight - SlidePen.Width, xPos + _stepWidth, yPos + _laneHeight - SlidePen.Width);
            }
        }

        /// <summary>
        /// Given a key number (zero-based, starts from the lowest key), returns the corresponding client area y-axis position.
        /// </summary>
        private float GetKeyYPos(int keyNumber)
        {
            return (ClientRectangle.Height - (1.0F + keyNumber) * _laneHeight);
        }

        /// <summary>
        /// Given a step number, returns the corresponding client area x-axis position.
        /// </summary>
        /// <param name="stepNumber"></param>
        /// <returns></returns>
        private float GetStepXPos(int stepNumber)
        {
            return _whiteKeyLength + 1.0F + stepNumber * _stepWidth;
        }


        /// <summary>
        /// Given a note value, returns the corresponding client are y-axis position.
        /// </summary>
        private float GetNoteKeyYPos(byte noteValue)
        {
            return GetKeyYPos(noteValue - LowestKeyNoteValue);
        }

        private void RecalculateLayout()
        {
            // Calculate number of white and black keys based on starting (lowest) key and total number of keys.
            _laneHeight = ClientRectangle.Height / (float)KeysCount;
  
            _stepWidth = (ClientRectangle.Width - _whiteKeyLength) / 16.0F;
            if (_laneHeight > 5.0f)
            {
                _rootKeyLabelFont = new Font(FontFamily.GenericSansSerif, _laneHeight - 2.0F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

            Invalidate();
        }

        private KeyName IncrementKey(KeyName key)
        {
            ++key;
            if (key > KeyName.B)
            {
                key = KeyName.C;
            }

            return key;
        }

        private KeyName DecrementKey(KeyName key)
        {
            --key;
            if (key < KeyName.C)
            {
                key = KeyName.B;
            }

            return key;
        }

        private bool IsBlackKey(KeyName key)
        {
            return key.ToString().EndsWith(Sharp);
        }

        private bool IsNoteBlackKey(byte noteValue)
        {
            int offset = noteValue - LowestKeyNoteValue;

            KeyName key = LowestKey;

            for (int i = 0; i < offset; ++i)
                key = IncrementKey(key);
  
            return IsBlackKey(key);
        }

    }
}
