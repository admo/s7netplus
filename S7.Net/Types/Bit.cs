using System;
using System.Collections;
using System.Linq;

namespace S7.Net.Types
{
    /// <summary>
    /// Contains the conversion methods to convert Bit from S7 plc to C#.
    /// </summary>
    public static class Bit
    {
        /// <summary>
        /// Converts a Bit to bool
        /// </summary>
        public static bool FromByte(byte v, byte bitAdr)
        {
            return (v & (1 << bitAdr)) != 0;
        }

        /// <summary>
        /// Converts an array of bytes to a BitArray
        /// </summary>
        public static BitArray ToBitArray(byte[] bytes, int varCount, int startByte)
        {
            int byteCount = (varCount / 8) + 1;
            if (bytes.Length - startByte < byteCount)
            {
                throw new ArgumentException("Wrong number of bytes. Bytes array must contain " + byteCount + " bytes.");
            }

            BitArray bitArr = new BitArray(bytes.Skip(startByte).Take(byteCount).ToArray());
            return bitArr;
        }
    }
}
