using System;
using System.IO.Ports;
using System.Diagnostics;
using ThreeZeroThreePatternFormats;

namespace X0xCommon
{
    public delegate void X0xCommunicationFeedbackHandler(decimal percentComplete);

    public class X0xTempoChangedEventArgs : EventArgs
    {
        public ushort Tempo { get; }

        public X0xTempoChangedEventArgs(ushort tempo)
        {
            Tempo = tempo;
        }
    }
    /// <summary>
    /// This class is responsible for all communications with the x0xb0x.
    /// </summary>
    public class X0xCommunicationHandler : IDisposable
    {
        public delegate void TempoChangedEventHandler(object sender, X0xTempoChangedEventArgs arg);

        public event TempoChangedEventHandler TempoChanged;

        public static readonly byte X0XBankCount = 16;
        public static readonly byte X0XLocationsPerBank = 8;

        private static readonly byte X0XPatternSize = 16;
        private static readonly ushort X0XBankSize = (ushort) (X0XLocationsPerBank * X0XPatternSize);

        public static readonly ushort EepromSize = 4096; // 4 KBytes

        public const int X0XDefaultConnectionBaudRate = 19200;
        public static readonly int X0XDefaultIoTimeoutMs = 5000;
        public static readonly int X0XDefaultWaitForBytes = 2000;
        public static readonly int X0XDefaultWaitForBytesSleepTime = 1;

        public string Port { get; set; }
        public int BaudRate { get; set; }
        /// <summary>
        /// The time to wait for the expected number of bytes to be received in a Reply message
        /// before a time out occurs. In Milliseconds
        /// </summary>
        public int WaitForBytesTimeOut { get; set; }

        // The time to sleep before each attempt to read a byte in a Reply message. In Milliseconds
        public int WaitForBytesSleepTime { get; set; }

        /// <summary>
        /// The time to wait before a serial port I/O operation times out
        /// </summary>
        public int SerialPortReadWriteTimeOut { get; set; }

        private SerialPort _serialPort;

        private volatile bool _isExpectingReply;
        private bool IsExpectingReply 
        {
            get => _isExpectingReply;
            set => _isExpectingReply = value;
        }

        public bool IsConnected => _serialPort != null && _serialPort.IsOpen;

        public X0xCommunicationHandler(string port, int baudRate = X0XDefaultConnectionBaudRate)
        {
            Port = port;
            BaudRate = baudRate;
            IsExpectingReply = false;
            WaitForBytesTimeOut = X0XDefaultWaitForBytes;
            WaitForBytesSleepTime = X0XDefaultWaitForBytesSleepTime;
            SerialPortReadWriteTimeOut = X0XDefaultIoTimeoutMs;
        }

        public void Ping()
        {
            if (!IsConnected)
            {
                Connect();
            }

            try
            {
                IsExpectingReply = true;

                SendX0xMessage(new PingMessage());

                var reply = ReceiveX0xMessage();

                if (!(reply is StatusMessage))
                {
                    ThrowUnexpectedReplyException("Ping", reply);
                }

            }
            finally
            {
                IsExpectingReply = false;
            }
        }
        
        public X0xPattern RetrievePattern(ushort bank, ushort location)
        {
            if (!IsConnected)
            {
                Connect();
            }

            try
            {
                IsExpectingReply = true;
                SendX0xMessage(new ReadPatternMessage(bank, location));

                X0xMessage reply = ReceiveX0xMessage();

                if (!(reply is PatternMessage))
                {
                    ThrowUnexpectedReplyException("RetrievePattern", reply);
                }

                var pattern = (reply as PatternMessage)?.Pattern;
                Debug.Assert(pattern != null, nameof(pattern) + " != null");
                pattern.Name = "x0x - Bank " + (bank + 1).ToString("00") + " Loc " + (location + 1).ToString("00");
                return pattern;
            }
            finally
            {
                IsExpectingReply = false;
            }
        }

        public void WritePattern(ushort bank, ushort location, X0xPattern pattern)
        {
            if (!IsConnected)
            {
                Connect();
            }

            try
            {
                IsExpectingReply = true;
                SendX0xMessage(new WritePatternMessage(bank, location, pattern.ToByteArray()));

                X0xMessage reply = ReceiveX0xMessage();

                if (!(reply is StatusMessage))
                {
                    ThrowUnexpectedReplyException("WritePattern", reply);
                }

                if (((StatusMessage) reply).Status != StatusMessage.X0xStatus.Ok)
                {
                    throw new Exception("WritePattern: x0xb0x returned Status = Not OK");
                }
            }
            finally
            {
                IsExpectingReply = false;
            }
        }

        public byte[] ReadEepromContents(X0xCommunicationFeedbackHandler handler = null)
        {
            if (!IsConnected)
            {
                Connect();
            }

            try
            {
                IsExpectingReply = true;
                var dump = new byte[EepromSize];
                // HACK
                // Since the firmware does not officially support doing this we exploit the lack of bounds checking in its implementation
                // of the read pattern message handler to read past the pattern area into the track area. 
                // (the first 2048 bytes of the 4096 bytes eeprom are for patterns, the last 2048 are for tracks)
                var percentComplete = decimal.Zero;
                var percentIncrement = (decimal)(X0xPattern.StepsPerPattern * 100) / EepromSize;

                for (ushort bank = 0; bank < X0XBankCount * 2; ++bank)
                {
                    for (ushort location = 0; location < X0XLocationsPerBank; ++location)
                    {
                        handler?.Invoke(percentComplete);

                        SendX0xMessage(new ReadPatternMessage(bank, location));

                        X0xMessage reply = ReceiveX0xMessage();

                        if (!(reply is PatternMessage))
                        {
                            ThrowUnexpectedReplyException("ReadEepromContents", reply);
                        }

                        Array.Copy(reply.GetPayload(), 0, dump, bank * X0XBankSize + location * X0XPatternSize, X0XPatternSize);

                        if (handler == null) continue;

                        percentComplete += percentIncrement;
                        handler(percentComplete);
                    }
                }

                return dump;
            }
            finally
            {
                IsExpectingReply = false;
            }
        }

        public void WriteEepromContents(byte[] bytes, X0xCommunicationFeedbackHandler handler = null)
        {
            if (!IsConnected)
            {
                Connect();
            }

            try
            {
                IsExpectingReply = true;
                if (bytes.Length != EepromSize)
                {
                    throw new ArgumentException("WriteEepromContents: bytes array is unexpected length: " + bytes.Length);
                }

                var percentComplete = decimal.Zero;
                var percentIncrement = (decimal)(X0xPattern.StepsPerPattern * 100) / EepromSize;

                for (ushort bank = 0; bank < X0XBankCount * 2; ++bank)
                {
                    for (ushort location = 0; location < X0XLocationsPerBank; ++location)
                    {
                        handler?.Invoke(percentComplete);

                        var block = new byte[X0XPatternSize];
                        Array.Copy(bytes, bank * X0XBankSize + location * X0XPatternSize, block, 0, X0XPatternSize);

                        SendX0xMessage(new WritePatternMessage(bank, location, block));

                        var reply = ReceiveX0xMessage();

                        if (!(reply is StatusMessage))
                        {
                            ThrowUnexpectedReplyException("WriteEepromContents", reply);
                        }

                        if (((StatusMessage)reply).Status != StatusMessage.X0xStatus.Ok)
                        {
                            throw new Exception("WriteEepromContents: x0xb0x returned Status = Not OK");
                        }

                        if (handler == null) continue;

                        percentComplete += percentIncrement;
                        handler(percentComplete);
                    }
                }
            }
            finally
            {
                IsExpectingReply = false;
            }
        }

        public ushort GetTempo()
        {
            if (!IsConnected)
            {
                Connect();
            }

            try
            {
                IsExpectingReply = true;
                SendX0xMessage(new GetTempoMessage());
                X0xMessage reply = ReceiveX0xMessage();
                if (!(reply is TempoMessage))
                {
                    ThrowUnexpectedReplyException("GetTempo", reply);
                }

                return ((TempoMessage) reply).Tempo;
            }
            finally
            {
                IsExpectingReply = false;
            }
        }

        public void SetTempo(ushort tempo)
        {
            if (!IsConnected)
            {
                Connect();
            }

            SendX0xMessage(new SetTempoMessage(tempo));
        }

        private void ThrowUnexpectedReplyException(string sourceMethod, X0xMessage reply)
        {
            throw new Exception($"{sourceMethod}: received unexpected reply: {reply.MessageType.ToString("X2")}");
        }


        private void Connect()
        {
            if (!IsConnected)
            {
                Disconnect(); // this is to ensure the serial port object is always properly disposed off

                _serialPort = new SerialPort(Port, BaudRate);
                _serialPort.DataReceived += _serialPort_DataReceived;
                _serialPort.ReadTimeout = _serialPort.WriteTimeout = X0XDefaultIoTimeoutMs;
                _serialPort.Open();
            }
        }

        void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (IsExpectingReply)
                return;
            try
            {
                X0xMessage message = ReceiveX0xMessage();
                if (message is TempoMessage tempoMessage)
                {
                    TempoChanged?.Invoke(this, new X0xTempoChangedEventArgs(tempoMessage.Tempo));
                }
            }
            catch (Exception ex)
            {
                // do nothing.
                Debug.WriteLine("DataReceived - Exception: " + ex.Message);
            }
        }

        private void Disconnect()
        {
            if (_serialPort != null)
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }

                _serialPort.DataReceived -= _serialPort_DataReceived;
                _serialPort.Dispose();

                _serialPort = null;
            }
        }

        private void SendX0xMessage(X0xMessage message)
        {
            _serialPort.Write(message.GetBytes(), 0, message.MessageLength);
        }

        private X0xMessage ReceiveX0xMessage()
        {
            System.Threading.Thread.Sleep(WaitForBytesSleepTime);

            byte messageType = (byte)_serialPort.ReadByte();

            switch (messageType)
            {
                case X0xMessage.PattMsg:
                    {
                        var bytes = new byte[PatternMessage.PattMsgLen];
                        bytes[0] = messageType;
                        WaitForBytes(PatternMessage.PattMsgLen - 1);
                        _serialPort.Read(bytes, 1, PatternMessage.PattMsgLen - 1);
                        return new PatternMessage(bytes);
                    }
                case X0xMessage.StatusMsg:
                    {
                        var bytes = new byte[StatusMessage.StatusMsgLen];
                        bytes[0] = messageType;
                        WaitForBytes(StatusMessage.StatusMsgLen - 1);
                        _serialPort.Read(bytes, 1, StatusMessage.StatusMsgLen - 1);
                        return new StatusMessage(bytes);
                    }
                case X0xMessage.TempoMsg:
                    {
                        var bytes = new byte[TempoMessage.TempoMessageLen];
                        bytes[0] = messageType;
                        WaitForBytes(TempoMessage.TempoMessageLen - 1);
                        _serialPort.Read(bytes, 1, TempoMessage.TempoMessageLen - 1);
                        return new TempoMessage(bytes);
                    }
                default:
                    {
                        _serialPort.DiscardInBuffer();
                        throw new Exception("Unknown message type: " + messageType.ToString("X2"));
                    }
            }
        }

        private void WaitForBytes(ushort byteCount)
        {
            var startTime = DateTime.UtcNow;

            while (_serialPort.BytesToRead < byteCount)
            {
                System.Threading.Thread.Sleep(WaitForBytesSleepTime);

                TimeSpan beenWaitingFor = DateTime.UtcNow - startTime;
                // We can't wait forever...
                if (beenWaitingFor.TotalMilliseconds > WaitForBytesTimeOut)
                    break;
            }
        }

        public void Dispose()
        {
            Disconnect();
        }
    }
}
