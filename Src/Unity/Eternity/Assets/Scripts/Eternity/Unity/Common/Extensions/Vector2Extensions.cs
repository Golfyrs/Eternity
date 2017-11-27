// ReSharper disable once CheckNamespace

namespace UnityEngine
{
    /// <summary>
    ///     Represents extensions methods for <see cref="Vector2"/>.
    /// </summary>
    public static class Vector2Extensions
    {
        /// <summary>
        ///     Changes <code>Vector2.x</code>.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="x">X coordinate of vector.</param>
        /// <returns><see cref="Vector2"/> object with changed position.</returns>
        /// <remarks>This method works in immutable way, so old value is left unchanged.</remarks>
        public static Vector2 WithX(this Vector2 self, float x) => new Vector2(x, self.y);
        
        /// <summary>
        ///     Changes <code>Vector2.x</code> value using specified <see cref="Vector2"/> position.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="other"><see cref="Vector2"/> object, which <b>x</b> coordinate will be used as value for new vector.</param>
        /// <returns><see cref="Vector2"/> object with changed position.</returns>
        /// <remarks>This method works in immutable way, so old value is left unchanged.</remarks>
        public static Vector2 WithX(this Vector2 self, Vector2 other) => new Vector2(other.x, self.y);
        
        /// <summary>
        ///     Changes <code>Vector2.y</code> value returning new object.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="y">Y coordinate of vector.</param>
        /// <returns><see cref="Vector2"/> object with changed position.</returns>
        /// <remarks>This method works in immutable way, so old value is left unchanged.</remarks>
        public static Vector2 WithY(this Vector2 self, float y) => new Vector2(self.x, y);
        
        /// <summary>
        ///     Changes <code>Vector2.y</code> value using specified <see cref="Vector2"/> position.
        /// </summary>
        /// <param name="self"><code>this</code> object.</param>
        /// <param name="other"><see cref="Vector2"/> object, which <b>y</b> coordinate will be used as value for new vector.</param>
        /// <returns><see cref="Vector2"/> object with changed position.</returns>
        /// <remarks>This method works in immutable way, so old value is left unchanged.</remarks>
        public static Vector2 WithY(this Vector2 self, Vector2 other) => new Vector2(self.x, other.y);

        public static Vector2 Lerp(this Vector2 self, Vector2 other, float factor) =>
            Vector2.Lerp(self, other, factor);
    }
}