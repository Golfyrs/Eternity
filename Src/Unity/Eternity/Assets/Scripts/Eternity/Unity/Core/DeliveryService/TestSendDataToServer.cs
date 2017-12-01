using System;
using System.Collections.Generic;
using Eternity.Dto;
using Eternity.Game;
using Eternity.Network;
using Eternity.Unity.Common.Components.Weaving;
using Eternity.Unity.Common.Reactive.Extensions;

namespace Eternity.Unity.Core.DeliveryService
{
    public class TestSendDataToServer : Weaver<Player>
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
            _x = idea.X.OnMainThread().OnNext(x => Move(idea.Name, x, 0));
            _y = idea.Y.OnMainThread().OnNext(y => Move(idea.Name, 0, y));
        }
        
        private void Move(string name, int x, int y)
        {
            EternityApp.Server.Send(RequestCode.Move, new MoveMessage
            {
                Name = name,
                X = x,
                Y = y
            });
        }
    }
}
