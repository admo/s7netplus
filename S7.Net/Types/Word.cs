using System;

namespace S7.Net.Types
{
    /// <summary>
    /// Contains the conversion methods to convert Words from S7 plc to C#.
    /// </summary>
    public static class Word
    {
        /// <summary>
        /// Converts a word (2 bytes) to ushort (UInt16)
        /// </summary>
        public static UInt16 FromByteArray(byte[] bytes, int startByte)
        {
            if (bytes.Length-startByte < 2)
            {
                throw new ArgumentException("Wrong number of bytes. Bytes array must contain 2 bytes.");
            }

            return BitConverter.IsLittleEndian
                ? (UInt16)((bytes[startByte++] << 8) | bytes[startByte++])
                : (UInt16)(bytes[startByte++] | (bytes[startByte++] << 8));
        }


        /// <summary>
        /// Converts 2 bytes to ushort (UInt16)
        /// </summary>
        public static UInt16 FromBytes(byte b1, byte b2)
        {
            return (UInt16)((b2 << 8) | b1);
        }


        /// <summary>
        /// Converts a ushort (UInt16) to word (2 bytes)
        /// </summary>
        public static byte[] ToByteArray(UInt16 value)
        {
            return BitConverter.IsLittleEndian
                ? new byte[] { (byte)((value >> 8) & 0xFF), (byte)(value & 0xFF) }
                : new byte[] { (byte)(value & 0xFF), (byte)((value >> 8) & 0xFF) };
        }

        /// <summary>
        /// Converts an array of ushort (UInt16) to an array of bytes
        /// </summary>
        public static byte[] ToByteArray(UInt16[] value)
        {
            const int valueSize = sizeof(UInt16);
            byte[] bytes = new byte[value.Length * valueSize];

            for (int i = 0; i < value.Length; i++)
            {
                byte[] valueBytes = ToByteArray(value[i]);
                Array.Copy(valueBytes, 0, bytes, i * valueSize, valueBytes.Length);
            }
            return bytes;
        }

        /// <summary>
        /// Converts an array of bytes to an array of ushort
        /// </summary>
        public static UInt16[] ToArray(byte[] bytes, int varCount, int startByte)
        {
            UInt16[] values = new UInt16[varCount];
            
            for (int cnt = 0; cnt < varCount; cnt++, startByte += 2)
                values[cnt] = FromByteArray(bytes, startByte);

            return values;
        }
    }
}
