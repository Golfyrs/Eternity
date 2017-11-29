using Eternity.Core.Dto;
using Eternity.Network;
using Eternity.Unity.Core;
using UnityEngine;

namespace Eternity.Unity.Common.DeliveryService
{
    public class TestSendDataToServer : MonoBehaviour
    {
        private Vector3 _lastPosition; 

        public void Update()
        {
            if (transform.position == _lastPosition)
                return;
            
            _lastPosition = transform.position;
            var position = new MoveMessage
            {
                X = _lastPosition.x,
                Y = _lastPosition.y,
                Name = "Test"
            };

            EternityApp.Server.Send(RequestCode.Move, position);
        }
    }
}
