using System.Collections.Generic;
using Eternity.Flows;

namespace Eternity.Core
{
    public class World
    {
        private readonly IDictionary<string, Player> _playerByName = new Dictionary<string, Player>();
        private readonly MutableFlow<IEnumerable<Player>> _players = new MutableFlow<IEnumerable<Player>>();
        
        public Player Player(string name)
        {
            return _playerByName[name];
        }
        
        public IFlow<IEnumerable<Player>> Players() => _players;

        public void Spawn(string name, int x, int y)
        {
            var player = new Player(name);
            player.Move(x, y);

            _playerByName[name] = player;
            _players.Push(_playerByName.Values); // TODO: Holy crap.
        }
    }
}