using Eternity.Common.DataTransfer;
using Eternity.Core;
using Eternity.Unity.Core;
using UnityEngine;

namespace Assets.Scripts.Eternity.Unity.Common.DeliveryService
{
    public class TestSendDataToServer : MonoBehaviour
    {
        private Vector3 _lastPosition; 

        public void Update()
        {
            if (transform.position != _lastPosition)
            {
                _lastPosition = transform.position;
                var position = new Position(_lastPosition.x, _lastPosition.y);

                EternityApp.Server.PostOffice.Send(position, RequestType.Move);
            }
        }
    }
}
