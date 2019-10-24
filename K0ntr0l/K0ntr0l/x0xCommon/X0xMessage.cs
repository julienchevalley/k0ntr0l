using System;
using System.Linq;
using System.Diagnostics;
using ThreeZeroThreePatternFormats;

namespace X0xCommon
{
    /// <summary>
    /// Abstract base class encapsulating a basic x0xb0x control protocol message's data and functionality.
    /// </summary>
    internal abstract class X0xMessage
    {
        public const byte MsgWrapperSize = 0x04; // 4 bytes... Message Type (1 byte) + PayloadLength (2 bytes) + CRC (1byte)
        public const byte PingMsg = 0x01;
        public const byte RdPattMsg = 0x11;
        public const byte WrPattMsg = 0x10;
        public const byte PattMsg = 0x19;
        public const byte StatusMsg = 0x80;
        public const byte GetTempoMsg = 0x40;
        public const byte SetTempoMsg = 0x41;
        public const byte TempoMsg = 0x42;

        /// <summary>
        /// The unique byte identifier for the specific message type.
        /// </summary>
        public abstract byte MessageType { get; }

        /// <summary>
        /// The length of the actual data inside the message excluding overheads such as message type and CRC.
        /// </summary>
        public abstract byte PayloadLength { get; }

        /// <summary>
        /// The overall length of the message inclusive of everything.
        /// </summary>
        public byte MessageLength => (byte)(PayloadLength + MsgWrapperSize);

        /// <summary>
        /// Retrieves the message data.
        /// </summary>
        public abstract byte[] GetPayload();
        
        /// <summary>
        /// Retrieves the entire message as a byte array, ready for serial transmission.
        /// </summary>
        /// <returns></returns>
        public byte[] GetBytes()
        {
            var bytes = new byte[MessageLength];
            int byteIndex = 0;

            bytes[byteIndex] = MessageType;

            bytes[++byteIndex] = 0;
            bytes[++byteIndex] = PayloadLength;
           
            foreach(byte b in GetPayload())
            {
                bytes[++byteIndex] = b;
            }

            bytes[++byteIndex] = calc_CRC8(bytes, (ushort)(MessageLength - 1));

            return bytes;
        }

        /// <summary>
        /// Byte array used to perform cyclic redundancy check.
        /// Taken from:
        /// http://cell-relay.indiana.edu/mhonarc/cell-relay/1999-Jan/msg00074.html
        /// 8 bit CRC Generator, MSB shifted first
        /// Polynom: x^8 + x^2 + x^1 + 1
        ///</summary>
        private static readonly byte[] CRC8Table = new byte[] {
            0x00,0x07,0x0E,0x09,0x1C,0x1B,0x12,0x15,
            0x38,0x3F,0x36,0x31,0x24,0x23,0x2A,0x2D,
            0x70,0x77,0x7E,0x79,0x6C,0x6B,0x62,0x65,
            0x48,0x4F,0x46,0x41,0x54,0x53,0x5A,0x5D,
            0xE0,0xE7,0xEE,0xE9,0xFC,0xFB,0xF2,0xF5,
            0xD8,0xDF,0xD6,0xD1,0xC4,0xC3,0xCA,0xCD,
            0x90,0x97,0x9E,0x99,0x8C,0x8B,0x82,0x85,
            0xA8,0xAF,0xA6,0xA1,0xB4,0xB3,0xBA,0xBD,
            0xC7,0xC0,0xC9,0xCE,0xDB,0xDC,0xD5,0xD2,
            0xFF,0xF8,0xF1,0xF6,0xE3,0xE4,0xED,0xEA,
            0xB7,0xB0,0xB9,0xBE,0xAB,0xAC,0xA5,0xA2,
            0x8F,0x88,0x81,0x86,0x93,0x94,0x9D,0x9A,
            0x27,0x20,0x29,0x2E,0x3B,0x3C,0x35,0x32,
            0x1F,0x18,0x11,0x16,0x03,0x04,0x0D,0x0A,
            0x57,0x50,0x59,0x5E,0x4B,0x4C,0x45,0x42,
            0x6F,0x68,0x61,0x66,0x73,0x74,0x7D,0x7A,
            0x89,0x8E,0x87,0x80,0x95,0x92,0x9B,0x9C,
            0xB1,0xB6,0xBF,0xB8,0xAD,0xAA,0xA3,0xA4,
            0xF9,0xFE,0xF7,0xF0,0xE5,0xE2,0xEB,0xEC,
            0xC1,0xC6,0xCF,0xC8,0xDD,0xDA,0xD3,0xD4,
            0x69,0x6E,0x67,0x60,0x75,0x72,0x7B,0x7C,
            0x51,0x56,0x5F,0x58,0x4D,0x4A,0x43,0x44,
            0x19,0x1E,0x17,0x10,0x05,0x02,0x0B,0x0C,
            0x21,0x26,0x2F,0x28,0x3D,0x3A,0x33,0x34,
            0x4E,0x49,0x40,0x47,0x52,0x55,0x5C,0x5B,
            0x76,0x71,0x78,0x7F,0x6A,0x6D,0x64,0x63,
            0x3E,0x39,0x30,0x37,0x22,0x25,0x2C,0x2B,
            0x06,0x01,0x08,0x0F,0x1A,0x1D,0x14,0x13,
            0xAE,0xA9,0xA0,0xA7,0xB2,0xB5,0xBC,0xBB,
            0x96,0x91,0x98,0x9F,0x8A,0x8D,0x84,0x83,
            0xDE,0xD9,0xD0,0xD7,0xC2,0xC5,0xCC,0xCB,
            0xE6,0xE1,0xE8,0xEF,0xFA,0xFD,0xF4,0xF3
        };

        /// <summary>
        /// Calculate CRC.
        /// Adapted from: http://cell-relay.indiana.edu/mhonarc/cell-relay/1999-Jan/msg00074.html
        ///  8 bit CRC Generator, MSB shifted first
        ///  Polynom: x^8 + x^2 + x^1 + 1
        ///  
        ///  Calculates an 8-bit cyclic redundancy check sum for a packet.
        ///  This function takes care not to include the packet's check sum in calculating
        ///  the check sum.  Assumes the CRC is the last byte of the packet header.  Also
        ///  takes care to look in code space (instead of xdata space) when dealing with a
        ///  PFrag.
        /// </summary>
        /// <param name="buff">The buffer containing the data to perform the CRC on.</param>
        /// <param name="size">How much of the data to use for the CRC.</param>
        protected static byte calc_CRC8(byte[] buff, ushort size)
        {
            byte crc = 0;

            for (byte i = 0; i < size; i++)
            {
                crc = CRC8Table[crc ^ buff[i]];
            }

            return crc;
        }
    }

    /// <summary>
    /// X0xResponseMessage encapsulates functionality that is common to all x0xb0x response messages...
    /// </summary>
    internal abstract class X0xResponseMessage : X0xMessage
    {
        protected readonly byte[] MessageData;

        protected X0xResponseMessage(byte[] messageData)
        {
            // Perform sanity checks on message data.
            if (messageData == null)
                throw new ArgumentNullException(nameof(messageData));
            
            if (messageData.Length != MessageLength)
                throw new ArgumentException("messageData is not the required length");

            if (messageData[0] != MessageType)
                throw new ArgumentException("messageData contains incorrect message type");

            if (messageData[2] != PayloadLength)
                throw new ArgumentException("messageData contains invalid payload length");

            if (calc_CRC8(messageData,(ushort)(MessageLength - 1)) != messageData[MessageLength - 1])
                throw new ArgumentException("messageData has an incorrect checksum");

            MessageData = messageData;
        }

        public override byte PayloadLength { get; }

        public override byte MessageType { get; }

        public override byte[] GetPayload()
        {
            var payLoad = new byte[PayloadLength];

            for (int i = 0; i < PayloadLength; ++i)
            {
                payLoad[i] = MessageData[ i + 3];
            }
           
            return payLoad;            
        }
    }

    /// <summary>
    /// Ping message is the most basic message that can be sent to the b0x. Its only purpose is to elicit a reply from the b0x. Good for testing.
    /// </summary>
    internal sealed class PingMessage : X0xMessage
    {
        public override byte MessageType => PingMsg;

        public override byte PayloadLength => 0x0;

        public override byte[] GetPayload()
        {
            return new byte[PayloadLength]; // An empty array.
        }
    }

    /// <summary>
    /// GetTempo message requests the x0xb0x's current tempo.
    /// </summary>
    internal sealed class GetTempoMessage : X0xMessage
    {
        public override byte MessageType => GetTempoMsg;

        public override byte PayloadLength => 0x0;

        public override byte[] GetPayload()
        {
            return new byte[PayloadLength]; // An empty array.
        }
    }

    /// <summary>
    /// SetTempo message is sent to the box to set its tempo value.
    /// </summary>
    internal sealed class SetTempoMessage : X0xMessage
    {
        public const byte SetTempoMsgLen = 0x02;

        private ushort Tempo { get; }

        public SetTempoMessage(ushort tempo)
        {
            Tempo = tempo;
        }

        public override byte MessageType => SetTempoMsg;

        public override byte PayloadLength => SetTempoMsgLen;

        public override byte[] GetPayload()
        {
            return new[] { (byte)(Tempo >> 8), (byte)(Tempo & 0xFF) };
        }

    }


    /// <summary>
    /// ReadPatternMessage is sent to the b0x to request a specific pattern.
    /// </summary>
    internal sealed class ReadPatternMessage : X0xMessage
    {
        public const byte RdPattMsgLen = 0x02;

        private byte Bank { get; }
        private byte Location { get; }

        /// <summary>
        /// Constructs a ReadPatternMessage
        /// </summary>
        /// <param name="bank">The bank number. Zero based. Must be greater than -1 and less than the firmware's pattern.h NUM_BANKS (currently 16).</param>
        /// <param name="location">The pattern location number. Zero-based. Must be greater than -1 and less than the firmware's pattern.h NUM_LOCS (currently 8)</param>
        public ReadPatternMessage(ushort bank, ushort location)
        {
            Bank = (byte)bank;
            Location = (byte)location;
        }

        public override byte MessageType => RdPattMsg;

        public override byte PayloadLength => RdPattMsgLen;

        public override byte[] GetPayload()
        {
            return new [] { Bank, Location };
        }
    }

    /// <summary>
    /// WritePatternMessage is sent to the b0x to write a pattern.
    /// </summary>
    internal sealed class WritePatternMessage : X0xMessage
    {
        public const byte WrPattMsgLen = X0xPattern.StepsPerPattern + 2;

        private byte Bank { get; }
        private byte Location { get; }
        private byte[] Pattern { get; }

        /// <summary>
        /// Constructs a WritePatternMessage
        /// </summary>
        /// <param name="bank">The bank number. Zero based. Must be greater than -1 and less than the firmware's pattern.h NUM_BANKS (currently 16).</param>
        /// <param name="location">The pattern location number . Zero-based. Must be greater than -1 and less than the firmware's pattern.h NUM_LOCS (currently 8)</param>
        /// /// <param name="pattern">The x0x byte array representation of the pattern. Patterns longer than 16 bytes will be truncated.</param>
        public WritePatternMessage(ushort bank, ushort location, byte[] pattern)
        {
            if (bank >= X0xCommunicationHandler.X0XBankCount * 2)
                throw new ArgumentOutOfRangeException("Invalid bank value: " + bank);

            if (location >= X0xCommunicationHandler.X0XLocationsPerBank)
                throw new ArgumentOutOfRangeException("Invalid location value: " + location);

            if (pattern == null)
                throw new ArgumentNullException(nameof(pattern));

            // The size of the pattern must be fixed to 16 bytes, so truncate/pad it as required.
            if (pattern.Length > X0xPattern.StepsPerPattern)
                pattern = pattern.Take(X0xPattern.StepsPerPattern).ToArray();

            while (pattern.Length < X0xPattern.StepsPerPattern)
                pattern = pattern.Concat(new [] {X0xStep.X0xEop} ).ToArray();
            
            
            Bank = (byte)bank;
            Location = (byte)location;
            
            Pattern = pattern;
        }

        public override byte MessageType => WrPattMsg;

        public override byte PayloadLength => WrPattMsgLen;

        public override byte[] GetPayload()
        {
            return (new [] { Bank, Location }).Concat(Pattern).ToArray();
        }
    }

    /// <summary>
    /// Encapsulates a x0xb0x status reply message
    /// </summary>
    internal sealed class StatusMessage : X0xResponseMessage
    {
        public enum X0xStatus { NotOk = 0, Ok = 1 };

        public const byte StatusMsgLen = 1 + MsgWrapperSize;

        public StatusMessage(byte[] messageData) : base(messageData) { }

        public override byte MessageType => StatusMsg;

        public override byte PayloadLength => 1;

        public X0xStatus Status => GetPayload()[0] == 1 ? X0xStatus.Ok : X0xStatus.NotOk;
    }

    internal sealed class TempoMessage : X0xResponseMessage
    {
        public const byte TempoMessageLen = 2 + MsgWrapperSize;

        public TempoMessage(byte[] messageData) : base(messageData) { }

        public override byte MessageType => TempoMsg;

        public override byte PayloadLength => 2;

        public ushort Tempo
        {
            get
            {
                var bytes = GetPayload();
                Debug.Assert(bytes.Length == 2);
                ushort tempo = bytes[0];
                tempo <<= 8;
                tempo += bytes[1];

                return tempo;
            }
        }
    }


    /// <summary>
    /// PatternMessage is received from the x0xb0x in response to a ReadPatternMessage and contains the actual pattern data.
    /// </summary>
    internal sealed class PatternMessage : X0xResponseMessage
    {
        public const byte PattMsgLen = X0xPattern.StepsPerPattern + MsgWrapperSize;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="messageData">The entire message data (not just the payload).</param>
        public PatternMessage(byte[] messageData) : base(messageData) {}

        public override byte MessageType => PattMsg;

        public override byte PayloadLength => X0xPattern.StepsPerPattern;

        public X0xPattern Pattern => new X0xPattern(GetPayload());
    }

}
