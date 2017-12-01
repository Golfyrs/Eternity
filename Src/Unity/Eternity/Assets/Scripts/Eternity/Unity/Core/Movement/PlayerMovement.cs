using System;
using Eternity.Game;
using Eternity.Reactive.Extensions;
using Eternity.Unity.Common;
using Eternity.Unity.Common.Components.Weaving;
using UnityEngine;

namespace Eternity.Unity.Core.Movement
{
    public class PlayerMovement : Weaver<Player>
    {
        protected override void Weave(Player player) =>
            Dispatcher.Updates
                .OnNext(() => player.Move(
                    (int) Input.GetAxisRaw("Horizontal") + player.X.Current,
                    (int) Input.GetAxisRaw("Vertical") + player.Y.Current))
                .Do(DisposeOnDestroy);
    }
}