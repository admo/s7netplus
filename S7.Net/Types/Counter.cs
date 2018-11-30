using System;

namespace S7.Net.Types
{
    /// <summary>
    /// Contains the conversion methods to convert Counter from S7 plc to C# ushort (UInt16).
    /// </summary>
    public static class Counter
    {
        /// <summary>
        /// Converts a Counter (2 bytes) to ushort (UInt16)
        /// </summary>
        public static UInt16 FromByteArray(byte[] bytes, int startByte)
        {
            if (bytes.Length - startByte < 2)
            {
                throw new ArgumentException("Wrong number of bytes. Bytes array must contain 2 bytes.");
            }
            
            return BitConverter.IsLittleEndian
                ? (UInt16)((bytes[startByte++] << 8) | bytes[startByte++])
                : (UInt16)(bytes[startByte++] | (bytes[startByte++] << 8));
        }


        /// <summary>
        /// Converts a ushort (UInt16) to word (2 bytes)
        /// </summary>
        public static byte[] ToByteArray(UInt16 value)
        {
            byte[] bytes = new byte[2];

            bytes[0] = (byte)((value << 8) & 0xFF);
            bytes[1] = (byte)((value) & 0xFF);
            
            return bytes;
        }

        /// <summary>
        /// Converts an array of ushort (UInt16) to an array of bytes
        /// </summary>
        public static byte[] ToByteArray(UInt16[] value)
        {
            ByteArray arr = new ByteArray();
            foreach (UInt16 val in value)
                arr.Add(ToByteArray(val));
            return arr.Array;
        }

        /// <summary>
        /// Converts an array of bytes to an array of ushort
        /// </summary>
        public static UInt16[] ToArray(byte[] bytes, int varCount, int startByte)
        {
            UInt16[] values = new UInt16[varCount];
            
            for (int cnt = 0; cnt < varCount; cnt++, startByte += 2)
                values[cnt] = FromByteArray(bytes, varCount);

            return values;
        }
    }
}
