using System;
using System.Collections.Generic;
using Eternity.Core;
using Eternity.Core.Dto;
using Eternity.Network;
using Eternity.Unity.Common.Components.Weaving;
using Eternity.Unity.Core;
using UnityEngine;

namespace Eternity.Unity.Common.DeliveryService
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
        
        private readonly List<Action> _actionsQueue = new List<Action>();

        public TestSendDataToServer()
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
            _x = idea.X.OnNext(x => _actionsQueue.Add(() => Move(idea.Name, x, 0)));
            _y = idea.Y.OnNext(y => _actionsQueue.Add(() => Move(idea.Name, 0, y)));
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
