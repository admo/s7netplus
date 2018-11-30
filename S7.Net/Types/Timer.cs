using System;

namespace S7.Net.Types
{
    /// <summary>
    /// Converts the Timer data type to C# data type
    /// </summary>
    public static class Timer
    {
        /// <summary>
        /// Converts the timer bytes to a double
        /// </summary>
        public static double FromByteArray(byte[] bytes, int startByte)
        {
            if (bytes.Length - startByte < 2)
            {
                throw new ArgumentException("Wrong number of bytes. Bytes array must contain 2 bytes.");
            }

            double wert = 0;

            wert = ((bytes[startByte]) & 0x0F) * 100.0;
            wert += ((bytes[startByte + 1] >> 4) & 0x0F) * 10.0;
            wert += ((bytes[startByte + 1]) & 0x0F) * 1.0;

            // this value is not used... may for a nother exponation
            //int unknown = (bytes[0] >> 6) & 0x03;

            switch ((bytes[startByte] >> 4) & 0x03)
            {
                case 0:
                    wert *= 0.01;
                    break;
                case 1:
                    wert *= 0.1;
                    break;
                case 2:
                    wert *= 1.0;
                    break;
                case 3:
                    wert *= 10.0;
                    break;
            }

            return wert;
        }

        /// <summary>
        /// Converts a ushort (UInt16) to an array of bytes formatted as time
        /// </summary>
        public static byte[] ToByteArray(UInt16 value)
        {
            return BitConverter.IsLittleEndian
                ? new byte[] { (byte)((value >> 8) & 0xFF), (byte)(value & 0xFF) }
                : new byte[] { (byte)(value & 0xFF), (byte)((value >> 8) & 0xFF) };
        }

        /// <summary>
        /// Converts an array of ushorts (Uint16) to an array of bytes formatted as time
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
        /// Converts an array of bytes formatted as time to an array of doubles
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static double[] ToArray(byte[] bytes, int varCount, int startByte)
        {
            double[] values = new double[varCount];
            
            for (int cnt = 0; cnt < varCount; cnt++, startByte += 2)
                values[cnt] = FromByteArray(bytes, startByte);

            return values;
        }
    }
}
