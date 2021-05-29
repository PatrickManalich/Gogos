using UnityEngine;

namespace Gogos.Extensions
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Returns this Color with a specified R value.
        /// </summary>
        public static Color WithR(this Color c, float r)
        {
            c.r = r;
            return c;
        }

        /// <summary>
        /// Returns this Color with a specified G value.
        /// </summary>
        public static Color WithG(this Color c, float g)
        {
            c.g = g;
            return c;
        }

        /// <summary>
        /// Returns this Color with a specified B value.
        /// </summary>
        public static Color WithB(this Color c, float b)
        {
            c.b = b;
            return c;
        }

        /// <summary>
        /// Returns this Color with a specified A value.
        /// </summary>
        public static Color WithA(this Color c, float a)
        {
            c.a = a;
            return c;
        }
    }
}