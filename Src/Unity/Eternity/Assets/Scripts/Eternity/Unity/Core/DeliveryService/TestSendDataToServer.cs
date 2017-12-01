using System;
using Eternity.Dto;
using Eternity.Game;
using Eternity.Network;
using Eternity.Unity.Common.Components.Weaving;
using Eternity.Unity.Common.Reactive.Extensions;

namespace Eternity.Unity.Core.DeliveryService
{
    public class TestSendDataToServer : Weaver<Player>
    {        
        
        protected override void Weave(Player player)
        {
            player.X
                .OnMainThread()
                .OnNext(x => Move(player.Name, x, player.Y.Current))
                .Do(DisposeOnDestroy);
            
            player.Y
                .OnMainThread()
                .OnNext(y => Move(player.Name, player.X.Current, y))
                .Do(DisposeOnDestroy);
        }

        private static int _lastX;
        private static int _lastY;

        private static void Move(string name, int x, int y)
        {
            if (_lastX == x && _lastY == y)
                return;
            
            EternityApp.Server.Send(RequestCode.Move, new MoveMessage
            {
                Name = name,
                X = x,
                Y = y
            });

            _lastX = x;
            _lastY = y;
        }
            
    }
}
