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
            byte[] bytes = new byte[4];

            bytes[0] = (byte)((value >> 24) & 0xFF);
            bytes[1] = (byte)((value >> 16) & 0xFF);
            bytes[2] = (byte)((value >> 8) & 0xFF);
            bytes[3] = (byte)((value) & 0xFF);

            return bytes;
        }






        /// <summary>
        /// Converts an array of uint (UInt32) to an array of S7 DWord (4 bytes) 
        /// </summary>
        public static byte[] ToByteArray(UInt32[] value)
        {
            ByteArray arr = new ByteArray();
            foreach (UInt32 val in value)
                arr.Add(ToByteArray(val));
            return arr.Array;
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
