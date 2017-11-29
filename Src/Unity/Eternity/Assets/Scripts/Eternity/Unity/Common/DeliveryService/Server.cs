using System.Net;
using System.Threading.Tasks;
using Eternity.Common.DataTransfer;

namespace Assets.Scripts.Eternity.Unity.Common.DeliveryService
{
    public class Server
    {
        public PostOffice PostOffice { get; private set; }

        private static DeliveryServiceDepartment _deliveryServiceDepartment;
        private static DeliveryServiceConnection _deliveryServiceConnection;

        public async Task Start()
        {
            // Не класс, а полнейшая дичь. Его даже контейнером нельзя назвать.

            _deliveryServiceConnection = new DeliveryServiceConnection();
            var client = await _deliveryServiceConnection.Connect( IPAddress.Loopback, 5000 );

            
            _deliveryServiceDepartment = new DeliveryServiceDepartment(client);
            // Тоже полный треш.
            PostOffice = new PostOffice(_deliveryServiceDepartment);

            // Нужно запускать в отдельном thread. Внутри ProcessDepartment есть while(true) которые должен быть всегда готовым считывать данные.
            // Название ProcessDepartment очень не интуитивное, я бы никогда не догодался, что он делает.
            await _deliveryServiceConnection.ProcessDepartment(_deliveryServiceDepartment);
        }

    }
}
