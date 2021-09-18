using System;
using UnityEngine;

namespace Gogos.Extensions
{
    public static class MathExtensions
    {
        /// <summary>
        /// Returns this float rounded up or down based on the specified midpoint.
        /// </summary>
        public static float RoundAt(this float f, float midpoint)
        {
            midpoint += Mathf.Floor(f);
            return f < midpoint ? Mathf.Floor(f) : Mathf.Ceil(f);
        }

        /// <summary>
        /// Returns the value of this instance multiplied by the time in seconds it took to complete the last frame.
        /// </summary>
        public static float WithDeltaTime(this float f)
        {
            return f * Time.deltaTime;
        }

        /// <summary>
        /// Returns the value of this instance multiplied by the interval in seconds at which physics are performed.
        /// </summary>
        public static float WithFixedDelaTime(this float f)
        {
            return f * Time.deltaTime;
        }

        /// <summary>
        /// Returns the value of this instance multiplied by the timeScale-independent time in seconds it took to complete the last frame.
        /// </summary>
        public static float WithUnscaledDeltaTime(this float f)
        {
            return f * Time.unscaledDeltaTime;
        }

        /// <summary>
        /// Returns the value of this instance taken to the specified exponential power.
        /// </summary>
        public static float ToPower(this float f, int exponent)
        {
            float total = 1;
            if (exponent < 0)
            {
                LoopAction(exponent * -1, (int i) => total *= i % 2 == 0 ? f * -1 : f);
                total = 1 / total;
            }
            else
            {
                LoopAction(exponent, (int i) => total *= f);
            }
            return total;
        }

        /// <summary>
        /// Loop a single action a specified number of times.
        /// </summary>
        public static void LoopAction(int iterations, Action<int> step)
        {
            for (int i = 0; i < iterations; i++)
            {
                step(i);
            }
        }

        /// <summary>
        /// Returns the value of this instance multiplied by the value of this instance.
        /// </summary>
        public static float Squared(this float f)
        {
            return f * f;
        }

        /// <summary>
        /// Returns the value of this instance taken to the 3rd power.
        /// </summary>
        public static float Cubed(this float f)
        {
            return f * f * f;
        }

        /// <summary>
        /// Returns the value of this instance multiplied by the value of this instance.
        /// </summary>
        public static int Squared(this int i)
        {
            return i * i;
        }

        /// <summary>
        /// Returns the value of this instance multiplied by the value of this instance.
        /// </summary>
        public static double Squared(this double d)
        {
            return d * d;
        }

        /// <summary>
        /// Returns the value of this instance multiplied by the value of this instance.
        /// </summary>
        public static long Squared(this long l)
        {
            return l * l;
        }

        /// <summary>
        /// Returns the value of this instance taken to Mathf.Floor with .5f added
        /// </summary>
        public static float FloorToIntSplit(this float f)
        {
            return Mathf.FloorToInt(f) + .5f;
        }

        /// <summary>
        /// Returns this float clamped within the Min and Max taking into account the objects scale.
        /// </summary>
        public static float ClampWithScale(this float f, Vector2 minMax, float halfScale)
        {
            for (int i = 0; i < halfScale; i++)
            {
                if (f - i <= minMax.x)
                {
                    f = minMax.x + halfScale;
                    break;
                }
                else if (f + i >= minMax.y)
                {
                    f = minMax.y - halfScale;
                    break;
                }
            }
            return f;
        }

        /// <summary>
        /// Converts this degrees into radians.
        /// </summary>
        public static float DegreesToRadians(this float degrees)
        {
            return degrees * (float)(Math.PI / 180.0);
        }

        /// <summary>
        /// Returns this euler angle clamped between 0 and 360 degrees.
        /// </summary>
        public static float ClampAngle(this float eulerAngle)
        {
            eulerAngle -= Mathf.CeilToInt(eulerAngle / 360f) * 360f;
            if (eulerAngle < 0)
            {
                eulerAngle += 360f;
            }
            return eulerAngle;
        }

        /// <summary>
        /// Returns a value proportional to this value in the range [oldMin, oldMax] when converted to the range [newMin, newMax]
        /// </summary>
        public static float ConvertValueToDifferentRange(this float f, float oldMin, float oldMax, float newMin, float newMax)
        {
            var oldRange = oldMax - oldMin;
            if (oldRange == 0)
            {
                return newMin;
            }
            else
            {
                var newRange = newMax - newMin;
                var newValue = ((f - oldMin) * newRange / oldRange) + newMin;
                return newValue;
            }
        }
    }
}
