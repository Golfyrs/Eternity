using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Eternity.Reactive;
using Eternity.Unity.Common.Attributes;
using UnityEngine;

namespace Eternity.Unity.Common.Components
{
    /// <summary>
    ///     Represents base class for all the components in game.
    /// 
    ///     Contains some generic methods for overloading along with injections logic.
    /// </summary>
    public abstract class EternityComponent : MonoBehaviour
    {
        private void Awake()
        {
            // Injection.
            // TODO: Cache.
            var fields = ComponentFields(GetType());
            
            foreach (var field in fields)
                field.SetValue(this, GetComponent(field.FieldType));
            
            Initialize();
        }
        
        /// <summary>
        ///     Called at the post-Awake time.
        /// </summary>
        protected virtual void Initialize() { }
        
        #region Helpers

        private static IEnumerable<FieldInfo> ComponentFields(Type type)
        {
            var fields = type
                .GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.GetCustomAttribute<ComponentAttribute>() != null);

            return type != typeof(EternityComponent)
                ? fields.Concat(ComponentFields(type.BaseType))
                : fields;
        }
        
        #endregion
    }
}