using System;

namespace S7.Net.Types
{
    /// <summary>
    /// Contains the conversion methods to convert Int from S7 plc to C#.
    /// </summary>
    public static class Int
    {
        /// <summary>
        /// Converts a S7 Int (2 bytes) to short (Int16)
        /// </summary>
        public static short FromByteArray(byte[] bytes, int startByte)
        {
            if (bytes.Length-startByte < 2)
            {
                throw new ArgumentException("Wrong number of bytes. Bytes array must contain 2 bytes.");
            }

            return BitConverter.IsLittleEndian
                ? (short)((bytes[startByte++] << 8) | bytes[startByte++])
                : (short)(bytes[startByte++] | (bytes[startByte++] << 8));
        }


        /// <summary>
        /// Converts a short (Int16) to a S7 Int byte array (2 bytes)
        /// </summary>
        public static byte[] ToByteArray(Int16 value)
        {
            return BitConverter.IsLittleEndian
                ? new byte[] { (byte)((value >> 8) & 0xFF), (byte)(value & 0xFF) }
                : new byte[] { (byte)(value & 0xFF), (byte)((value >> 8) & 0xFF) };
        }

        /// <summary>
        /// Converts an array of short (Int16) to a S7 Int byte array (2 bytes)
        /// </summary>
        public static byte[] ToByteArray(Int16[] value)
        {
            const int valueSize = sizeof(Int16);
            byte[] bytes = new byte[value.Length * valueSize];

            for (int i = 0; i < value.Length; i++)
            {
                byte[] valueBytes = ToByteArray(value[i]);
                Array.Copy(valueBytes, 0, bytes, i * valueSize, valueBytes.Length);
            }
            return bytes;
        }

        /// <summary>
        /// Converts an array of S7 Int to an array of short (Int16)
        /// </summary>
        public static Int16[] ToArray(byte[] bytes, int varCount, int startByte)
        {
            Int16[] values = new Int16[varCount];
            
            for (int cnt = 0; cnt < varCount; cnt++, startByte += 2)
                values[cnt] = FromByteArray(bytes, startByte);

            return values;
        }
        
        /// <summary>
        /// Converts a C# int value to a C# short value, to be used as word.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Int16 CWord(int value)
        {
            if (value > 32767)
            {
                value -= 32768;
                value = 32768 - value;
                value *= -1;
            }
            return (short)value;
        }

    }
}
