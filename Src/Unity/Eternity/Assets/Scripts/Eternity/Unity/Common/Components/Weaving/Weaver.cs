namespace Eternity.Unity.Common.Components.Weaving
{
    public abstract class Weaver<T> : EternityComponent where T : class 
    {
        protected override void Initialize()
        {
            base.Initialize();
            
            var pattern = GetComponentInParent<Pattern>();
            var idea = pattern.Idea<T>();

            Weave(idea);
        }

        protected abstract void Weave(T idea);
    }
}