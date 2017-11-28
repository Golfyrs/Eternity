using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Eternity.Flows;
using Eternity.Unity.Common.Attributes;
using UnityEngine;

namespace Eternity.Unity.Common.Components
{
    public abstract class EternityComponent : MonoBehaviour
    {        
        // TODO: Move somewhere.
        private readonly MutableFlow _updates = new MutableFlow();
        protected IFlow Updates => _updates;
        
        private void Awake()
        {
            // TODO: Cache.
            var fields = ComponentFields(GetType());
            
            foreach (var field in fields)
                field.SetValue(this, GetComponent(field.FieldType));
            
            Initialize();
        }

        private void Update() => _updates.Push();
        
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