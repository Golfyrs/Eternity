using System;
using System.Collections.Generic;
using Eternity.Core;
using Eternity.Unity.Common.Components.Weaving;
using UnityEngine;

namespace Eternity.Unity.Core.Movement
{
    public class PlayerPosition : Weaver<Player>
    {
        private IDisposable _x;
        private IDisposable _y;
        
        private void OnDestroy()
        {
            _x.Dispose();
            _y.Dispose();
        }
        
        private readonly List<Action> _actionsQueue = new List<Action>();

        public PlayerPosition()
        {
            Updates.OnNext(() =>
            {
                var queue = _actionsQueue;
                foreach (var item in queue)
                    item();
                
                _actionsQueue.Clear();
            });
        }
        
        protected override void Weave(Player idea)
        {
            _x = idea.X.OnNext(x => _actionsQueue.Add(() => Move(x, 0)));
            _y = idea.Y.OnNext(y => _actionsQueue.Add(() => Move(0, y)));

            transform.position = new Vector2(idea.X.Current, idea.Y.Current);
        }

        private void Move(int x, int y)
        {
            var delta = new Vector3(x, y);
            
            transform.position = transform.position
                .Lerp(transform.position + delta, 5 * Time.deltaTime);
        }
    }
}