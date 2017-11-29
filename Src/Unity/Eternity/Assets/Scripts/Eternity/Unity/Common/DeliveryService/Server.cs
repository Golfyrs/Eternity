using System.Net;
using System.Threading.Tasks;
using Eternity.Common.DataTransfer;
using Eternity.Unity.Common;

namespace Assets.Scripts.Eternity.Unity.Common.DeliveryService
{
    public class Server
    {
        public PostOffice PostOffice { get; private set; }

        private static DeliveryServiceConnection _deliveryServiceConnection;

        public async Task Start()
        {
            _deliveryServiceConnection = new DeliveryServiceConnection();
            var tuple = await _deliveryServiceConnection.Connect( IPAddress.Loopback, 5000 );
            
            if (tuple.Item2)
                Log.Message("The client has connected to the server");
            else
                Log.Error("The client could not connect to the server! ;( ");
            
            // Тоже полный треш.
            PostOffice = new PostOffice(new DeliveryServiceDepartment(tuple.Item1));
            PostOffice.PackageArrived = s => Log.Message($"You received data: {s}");

            // Нужно запускать в отдельном thread. Внутри ProcessDepartment есть while(true) которые должен быть всегда готовым считывать данные.
            // Название ProcessDepartment очень не интуитивное, я бы никогда не догодался, что он делает.
            _deliveryServiceConnection.StartListening();
        }
    }
}
