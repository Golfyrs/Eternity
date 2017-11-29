using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Eternity.Common.DataTransfer;

namespace Assets.Scripts.Eternity.Unity.Common.DeliveryService
{
    public class DeliveryServiceConnection
    {
        private TcpClient _client;

        public async Task<TcpClient> Connect( IPAddress address, int port )
        {
            _client = new TcpClient();
            await _client.ConnectAsync(address, port);

            return _client;
        }

        public async Task ProcessDepartment(DeliveryServiceDepartment department)
        {
            // Вот эта вот хуйня изобретена укропами, взята прямо с поля боя. 
            // Нет, ну вы можете представить насколько надо иметь больное воображение, чтобы такую ебалу создать.
            // Она должна находится в режиме принятия данных и желательно не тушить приложение.
            //                while (_running)
            {
                try
                {
                    var task = await department.Get();
                    PostOffice.AcceptParcel(task);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
