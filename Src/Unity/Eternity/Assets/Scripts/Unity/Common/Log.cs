using System;
using System.Diagnostics;
using System.Reflection;
using Debug = UnityEngine.Debug;

namespace Unity.Common
{
    /// <summary>
    ///     Represents static type object that helps to log common situations to Unity console.
    /// </summary>
    public static class Log
    {
        public static void Message(string message)
        {
            CallingMethod()
                .As(x => $"<color=#00B524>[ {x.DeclaringType.Name}.{x.Name} ]</color>\n<color=white>{message}</color>")
                .Do(Debug.Log);
        }

        public static void Warning(string message)
        {
            CallingMethod()
                .As(x => $"<color=#FFCE00>[ {x.DeclaringType.Name}.{x.Name} ]</color>\n<color=white>{message}</color>")
                .Do(Debug.LogWarning);
        }
        
        public static void Error(string message)
        {
            CallingMethod()
                .As(x => $"<color=#DF0000>[ {x.DeclaringType.Name}.{x.Name} ]</color>\n<color=white>{message}</color>")
                .Do(Debug.LogError);
        }
        
        #region Helpers
        
        private static MethodBase CallingMethod()
        {
            return new StackFrame(2).GetMethod();
        }
        
        #endregion
    }
}