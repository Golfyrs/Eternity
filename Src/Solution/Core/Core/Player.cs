using Eternity.Flows;

namespace Eternity.Core
{
    public class Player
    {
        private readonly MutableFlow<int> _xFlow = new MutableFlow<int>();
        private readonly MutableFlow<int> _yFlow = new MutableFlow<int>();

        public Player(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public IFlow<int> X => _xFlow;
        public IFlow<int> Y => _yFlow;

        public void Move(int x, int y)
        {
            _xFlow.Push(x);
            _yFlow.Push(y);
        }
    }
}