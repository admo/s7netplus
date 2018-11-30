using System;

namespace S7.Net.Types
{
    /// <summary>
    /// Contains the conversion methods to convert DInt from S7 plc to C# int (Int32).
    /// </summary>
    public static class DInt
    {
        /// <summary>
        /// Converts a S7 DInt (4 bytes) to int (Int32)
        /// </summary>
        public static Int32 FromByteArray(byte[] bytes, int startByte)
        {
            if (bytes.Length - startByte < 4)
            {
                throw new ArgumentException("Wrong number of bytes. Bytes array must contain 4 bytes.");
            }

            return BitConverter.IsLittleEndian
                ? (bytes[startByte++] << 24) | (bytes[startByte++] << 16) | (bytes[startByte++] << 8) | bytes[startByte++]
                : bytes[startByte++] | (bytes[startByte++] << 8) | (bytes[startByte++] << 16) | (bytes[startByte++] << 24);
        }


        /// <summary>
        /// Converts a int (Int32) to S7 DInt (4 bytes)
        /// </summary>
        public static byte[] ToByteArray(Int32 value)
        {
            return BitConverter.IsLittleEndian
                ? new byte[] { (byte)((value >> 24) & 0xFF), (byte)((value >> 16) & 0xFF), (byte)((value >> 8) & 0xFF), (byte)(value & 0xFF) }
                : new byte[] { (byte)(value & 0xFF), (byte)((value >> 8) & 0xFF), (byte)((value >> 16) & 0xFF), (byte)((value >> 24) & 0xFF) };
        }

        /// <summary>
        /// Converts an array of int (Int32) to an array of bytes
        /// </summary>
        public static byte[] ToByteArray(Int32[] value)
        {
            const int valueSize = sizeof(Int32);
            byte[] bytes = new byte[value.Length * valueSize];

            for (int i = 0; i < value.Length; i++)
            {
                byte[] valueBytes = ToByteArray(value[i]);
                Array.Copy(valueBytes, 0, bytes, i * valueSize, valueBytes.Length);
            }
            return bytes;
        }

        /// <summary>
        /// Converts an array of S7 DInt to an array of int (Int32)
        /// </summary>
        public static Int32[] ToArray(byte[] bytes, int varCount, int startByte)
        {
            Int32[] values = new Int32[varCount];
            
            for (int cnt = 0; cnt < varCount; cnt++, startByte += 4)
                values[cnt] = FromByteArray(bytes, startByte);

            return values;
        }
        

    }
}
