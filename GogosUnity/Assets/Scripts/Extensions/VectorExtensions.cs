using UnityEngine;

namespace Gogos.Extensions
{
    public static class VectorExtensions
    {
        /// <summary>
        /// Returns this Vector2 with a specified X value.
        /// </summary>
        public static Vector2 WithX(this Vector2 vec, float x)
        {
            vec.x = x;
            return vec;
        }

        /// <summary>
        /// Returns this Vector2 with a specified Y value.
        /// </summary>
        public static Vector2 WithY(this Vector2 vec, float y)
        {
            vec.y = y;
            return vec;
        }

        /// <summary>
        /// Flips the X and Y of this Vector2.
        /// </summary>
        public static Vector2 Flip(this Vector2 vec)
        {
            return new Vector2(vec.y, vec.x);
        }

        /// <summary>
        /// Returns this Vector3 with it's X and Z as a new Vector2.
        /// </summary>
        public static Vector2 ConvertToVector2(this Vector3 vec)
        {
            return new Vector2(vec.x, vec.z);
        }

        /// <summary>
        /// Are all of this Vector's values greater than or equal to the specified Vector.
        /// </summary>
        public static bool GreaterThanOrEqual(this Vector2 vec1, Vector2 vec2)
        {
            if (vec1.x >= vec2.x && vec1.y >= vec2.y)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns this Vector3 with a specified X value.
        /// </summary>
        public static Vector3 WithX(this Vector3 vec, float x)
        {
            vec.x = x;
            return vec;
        }

        /// <summary>
        /// Returns this Vector3 with a specified Y value.
        /// </summary>
        public static Vector3 WithY(this Vector3 vec, float y)
        {
            vec.y = y;
            return vec;
        }

        /// <summary>
        /// Returns this Vector3 with a specified Z value.
        /// </summary>
        public static Vector3 WithZ(this Vector3 vec, float z)
        {
            vec.z = z;
            return vec;
        }

        /// <summary>
        /// Returns this Vector3 with a specified X and Y value.
        /// </summary>
        public static Vector3 WithXY(this Vector3 vec, float x, float y)
        {
            vec.x = x;
            vec.y = y;
            return vec;
        }

        /// <summary>
        /// Returns this Vector3 with a specified X and Z value.
        /// </summary>
        public static Vector3 WithXZ(this Vector3 vec, float x, float z)
        {
            vec.x = x;
            vec.z = z;
            return vec;
        }

        /// <summary>
        /// Returns this Vector3 with a specified Y and Z value.
        /// </summary>
        public static Vector3 WithYZ(this Vector3 vec, float y, float z)
        {
            vec.y = y;
            vec.z = z;
            return vec;
        }

        /// <summary>
        /// Flips the X, Y, and Z of this Vector3.
        /// </summary>
        public static Vector3 Flip(this Vector3 vec)
        {
            return new Vector3(vec.z, vec.y, vec.x);
        }

        /// <summary>
        /// Returns this Vector2 with it's X and Y as a new Vector3's X and Z with a specified Y.
        /// </summary>
        public static Vector3 ConvertToVector3(this Vector2 vec, float y = 0)
        {
            return new Vector3(vec.x, y, vec.y);
        }

        /// <summary>
        /// Are all of this Vector's values greater than or equal to the specified Vector.
        /// </summary>
        public static bool GreaterThanOrEqual(this Vector3 vec1, Vector3 vec2)
        {
            if (vec1.x >= vec2.x && vec1.y >= vec2.y && vec1.z >= vec2.z)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns this Vector3 rounded to the nearest 1/2.
        /// </summary>
        public static Vector3 RoundToNearestHalf(this Vector3 vec)
        {
            vec.x = Mathf.RoundToInt(vec.x * 2) / 2f;
            vec.z = Mathf.RoundToInt(vec.z * 2) / 2f;
            return vec;
        }

        /// <summary>
        /// Returns this Vector3 locked to the nearest 1/2.
        /// *Note: This produces cleaner results than RoundToNearestHalf
        /// </summary>
        public static Vector3 LockToNearestHalf(this Vector3 vec)
        {
            vec.x = vec.x.FloorToIntSplit();
            vec.z = vec.z.FloorToIntSplit();
            return vec;
        }

        /// <summary>
        /// Returns this Vector3 rounded to the nearest integer.
        /// </summary>
        public static Vector3 RoundToInt(this Vector3 vec)
        {
            vec.x = Mathf.RoundToInt(vec.x);
            vec.y = Mathf.RoundToInt(vec.y);
            vec.z = Mathf.RoundToInt(vec.z);
            return vec;
        }

        /// <summary>
        /// Returns this Vector3 with it's X and Z rounded to the nearest integer.
        /// </summary>
        public static Vector3 RoundToIntXZ(this Vector3 vec)
        {
            vec.x = Mathf.RoundToInt(vec.x);
            vec.z = Mathf.RoundToInt(vec.z);
            return vec;
        }

        /// <summary>
        /// Returns this Vector3 clamped within a bounds taking into account the object size.
        /// </summary>
        public static Vector3 ClampWithinBounds(this Vector3 vec, Bounds bounds, Vector3 halfScale)
        {
            vec.x = vec.x.ClampWithScale(new Vector2(bounds.min.x, bounds.max.x), halfScale.x);
            vec.z = vec.z.ClampWithScale(new Vector2(bounds.min.z, bounds.max.z), halfScale.z);
            return vec;
        }

        /// <summary>
        /// Returns this Vector4 with a specified X value.
        /// </summary>
        public static Vector4 WithX(this Vector4 vec, float x)
        {
            vec.x = x;
            return vec;
        }

        /// <summary>
        /// Returns this Vector4 with a specified Y value.
        /// </summary>
        public static Vector4 WithY(this Vector4 vec, float y)
        {
            vec.y = y;
            return vec;
        }

        /// <summary>
        /// Returns this Vector4 with a specified Z value.
        /// </summary>
        public static Vector4 WithZ(this Vector4 vec, float z)
        {
            vec.z = z;
            return vec;
        }

        /// <summary>
        /// Returns this Vector4 with a specified Z value.
        /// </summary>
        public static Vector4 WithW(this Vector4 vec, float w)
        {
            vec.w = w;
            return vec;
        }
    }
}