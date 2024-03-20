using UnityEngine;

namespace Extensions
{
    public static class NullCheck
    {
        public static bool IsNull(this Object obj)
        {
            return System.Object.ReferenceEquals(obj, null);
        }
        
        public static bool IsNull(this GameObject obj)
        {
            return System.Object.ReferenceEquals(obj, null);
        }
        
        public static bool IsNull(this Component obj)
        {
            return System.Object.ReferenceEquals(obj, null);
        }
        
        public static bool IsNull(this System.Object obj)
        {
            return System.Object.ReferenceEquals(obj, null);
        }

        public static bool IsNotNull(this System.Object obj)
        {
            return !System.Object.ReferenceEquals(obj, null);
        }
    }
}