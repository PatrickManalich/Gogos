using System.Text;

namespace Gogos
{
    public static class StringExtensions
    {
        public static string SplitOnCamelCase(this string s)
        {
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                if (i > 0 && char.IsUpper(s[i]))
                {
                    stringBuilder.Append(' ');
                }
                stringBuilder.Append(s[i]);
            }
            return stringBuilder.ToString();
        }
    }
}
