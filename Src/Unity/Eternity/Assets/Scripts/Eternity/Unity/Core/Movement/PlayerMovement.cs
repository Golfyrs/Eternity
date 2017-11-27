using System;
using Eternity.Core;
using Eternity.Unity.Common.Components.Weaving;
using UnityEngine;

namespace Eternity.Unity.Core.Movement
{
    public class PlayerMovement : Weaver<Player>
    {
        private IDisposable _update;
        private void OnDestroy() => _update.Dispose();
        
        protected override void Weave(Player idea) =>
            _update = Updates.OnNext(() => idea.Move(
                (int) Input.GetAxisRaw("Horizontal"),
                (int) Input.GetAxisRaw("Vertical")));
    }
}