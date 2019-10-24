using System.Collections.Generic;
using System.Text;

namespace BangOnTekCommon
{
    public static class IEnumerableExtensions
    {
        public static string ToByteString(this IEnumerable<byte> bytes)
        {
            var builder = new StringBuilder();

            foreach (byte b in bytes)
                builder.Append(b.ToString("X2"));

            return builder.ToString();
        }
    }
}
