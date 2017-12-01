using System;
using Eternity.Unity.Common.Components;
using UnityEngine;

namespace Eternity.Unity.Common.Attributes
{
    /// <summary>
    ///     Represents attribute that is used to automatically inject components.
    /// 
    ///     Components are injected through <see cref="MonoBehaviour.GetComponent{T}"/> 
    ///     and only for children of <see cref="EternityComponent"/> class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ComponentAttribute : Attribute { }
}