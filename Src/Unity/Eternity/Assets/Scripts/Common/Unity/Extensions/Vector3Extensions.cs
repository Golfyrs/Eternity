// ReSharper disable once CheckNamespace

namespace UnityEngine
{
    public static class Vector3Extensions
    {
        public static Vector3 WithX(this Vector3 self, float x) => new Vector3(x, self.y, self.z);
        public static Vector3 WithX(this Vector3 self, Vector3 other) => new Vector3(other.x, self.y, self.z);
        
        public static Vector3 WithY(this Vector3 self, float y) => new Vector3(self.x, y, self.z);
        public static Vector3 WithY(this Vector3 self, Vector3 other) => new Vector3(self.x, other.y, self.z);
        
        public static Vector3 WithZ(this Vector3 self, float z) => new Vector3(self.x, self.y, z);
        public static Vector3 WithZ(this Vector3 self, Vector3 other) => new Vector3(self.x, self.y, other.z);
        
        public static Vector3 WithXY(this Vector3 self, float x, float y) => new Vector3(x, y, self.z);
        public static Vector3 WithXY(this Vector3 self, Vector3 other) => new Vector3(other.x, other.y, self.z);
        
        public static Vector3 WithXZ(this Vector3 self, float x, float z) => new Vector3(x, self.y, z);
        public static Vector3 WithXZ(this Vector3 self, Vector3 other) => new Vector3(other.x, self.y, other.z);
        
        public static Vector3 WithYZ(this Vector3 self, float y, float z) => new Vector3(self.x, y, z);
        public static Vector3 WithYZ(this Vector3 self, Vector3 other) => new Vector3(self.x, other.y, other.z);

        public static Vector3 Lerp(this Vector3 self, Vector3 other, float factor) =>
            Vector3.Lerp(self, other, factor);
    }
}