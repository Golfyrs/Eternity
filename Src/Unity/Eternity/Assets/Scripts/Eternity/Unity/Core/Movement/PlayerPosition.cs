using System;
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
        
        protected override void Weave(Player idea)
        {
            _x = idea.X.OnNext(x => Move(x, 0));
            _y = idea.Y.OnNext(y => Move(0, y));

            transform.position = new Vector2(idea.X.Value(), idea.Y.Value());
        }

        private void Move(int x, int y)
        {
            var delta = new Vector3(x, y);
            
            transform.position = transform.position
                .Lerp(transform.position + delta, 5 * Time.deltaTime);
        }
    }
}