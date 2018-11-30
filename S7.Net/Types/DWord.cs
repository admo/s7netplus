using System;

namespace S7.Net.Types
{
    /// <summary>
    /// Contains the conversion methods to convert DWord from S7 plc to C#.
    /// </summary>
    public static class DWord
    {
        /// <summary>
        /// Converts a S7 DWord (4 bytes) to uint (UInt32)
        /// </summary>
        public static UInt32 FromByteArray(byte[] bytes, int startByte)
        {
            if (bytes.Length - startByte < 2)
            {
                throw new ArgumentException("Wrong number of bytes. Bytes array must contain 4 bytes.");
            }

            return BitConverter.IsLittleEndian
                ? (UInt32)(bytes[startByte++] << 24 | bytes[startByte++] << 16 | bytes[startByte++] << 8 | bytes[startByte++])
                : (UInt32)(bytes[startByte++] | bytes[startByte++] << 8 | bytes[startByte++] << 16 | bytes[startByte++] << 24);
        }


        /// <summary>
        /// Converts 4 bytes to DWord (UInt32)
        /// </summary>
        public static UInt32 FromBytes(byte b1, byte b2, byte b3, byte b4)
        {
            return (UInt32)((b4 << 24) | (b3 << 16) | (b2 << 8) | b1);
        }


        /// <summary>
        /// Converts a uint (UInt32) to S7 DWord (4 bytes) 
        /// </summary>
        public static byte[] ToByteArray(UInt32 value)
        {
            return BitConverter.IsLittleEndian
                ? new byte[] { (byte)((value >> 24) & 0xFF), (byte)((value >> 16) & 0xFF), (byte)((value >> 8) & 0xFF), (byte)(value & 0xFF) }
                : new byte[] { (byte)(value & 0xFF), (byte)((value >> 8) & 0xFF), (byte)((value >> 16) & 0xFF), (byte)((value >> 24) & 0xFF) };
        }






        /// <summary>
        /// Converts an array of uint (UInt32) to an array of S7 DWord (4 bytes) 
        /// </summary>
        public static byte[] ToByteArray(UInt32[] value)
        {
            const int valueSize = sizeof(UInt32);
            byte[] bytes = new byte[value.Length * valueSize];

            for (int i = 0; i < value.Length; i++)
            {
                byte[] valueBytes = ToByteArray(value[i]);
                Array.Copy(valueBytes, 0, bytes, i * valueSize, valueBytes.Length);
            }
            return bytes;
        }

        /// <summary>
        /// Converts an array of S7 DWord to an array of uint (UInt32)
        /// </summary>
        public static UInt32[] ToArray(byte[] bytes, int varCount, int startByte)
        {
            UInt32[] values = new UInt32[varCount];
            
            for (int cnt = 0; cnt < varCount; cnt++, startByte += 4)
                values[cnt] = FromByteArray(bytes, startByte);

            return values;
        }
    }
}
