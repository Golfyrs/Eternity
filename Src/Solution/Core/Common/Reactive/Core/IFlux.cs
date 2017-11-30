namespace Eternity.Reactive
{
    public interface IFlux : IFlow
    {
        void Pulse();
    }

    public interface IFlux<T> : IFlow<T>
    {
        void Pulse(T value);
    }
}