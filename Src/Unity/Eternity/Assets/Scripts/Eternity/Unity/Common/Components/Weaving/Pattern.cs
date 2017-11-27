using UnityEngine;

namespace Eternity.Unity.Common.Components.Weaving
{
    public abstract class Pattern : MonoBehaviour
    {
        public abstract T Idea<T>() where T : class;
    }

    public abstract class Pattern<T> : Pattern
    {
        public override TIdea Idea<TIdea>()
        {
            return Idea() as TIdea;
        }

        protected abstract T Idea();
    }
}