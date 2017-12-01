using System;
using Eternity.Game;
using Eternity.Unity.Common.Components.Weaving;
using Eternity.Unity.Common.Reactive.Extensions;
using UnityEngine;

namespace Eternity.Unity.Core.Movement
{
    public class PlayerPosition : Weaver<Player>
    {        
        protected override void Weave(Player player)
        {
            player.X
                .OnMainThread()
                .OnNext(x => Move(x, player.Y.Current))
                .Do(DisposeOnDestroy);
            
            player.Y
                .OnMainThread()
                .OnNext(y => Move(player.X.Current, y))
                .Do(DisposeOnDestroy);

            Move(player.X.Current, player.Y.Current);
        }

        private void Move(int x, int y) =>
            transform.position = new Vector3(x, y);
    }
}