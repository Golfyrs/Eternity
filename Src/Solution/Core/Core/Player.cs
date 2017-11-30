using Eternity.Reactive;

namespace Eternity.Core
{
    public class Player
    {
        private readonly IFlux<int> _xFlow = new PureFlux<int>();
        private readonly IFlux<int> _yFlow = new PureFlux<int>();

        public Player(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public IFlow<int> X => _xFlow;
        public IFlow<int> Y => _yFlow;

        public void Move(int x, int y)
        {
            _xFlow.Pulse(x);
            _yFlow.Pulse(y);
        }
    }
}