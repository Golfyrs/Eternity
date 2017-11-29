using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Eternity.Common.DataTransfer;
using Eternity.Unity.Common;

namespace Assets.Scripts.Eternity.Unity.Common.DeliveryService
{
    public class DeliveryServiceConnection
    {
        private const int ConnectTimeoutMilliseconds = 1000;

        private TcpClient _client;

        public async Task<TcpClient> Connect( IPAddress address, int port )
        {
            var tcpClient = new TcpClient();

            var connectionTask = tcpClient
                .ConnectAsync(address, port)
                .ContinueWith(
                    task => task.IsFaulted ? null : tcpClient,
                    TaskContinuationOptions.ExecuteSynchronously);

            var timeoutTask = Task.Delay(ConnectTimeoutMilliseconds)
                .ContinueWith<TcpClient>(task => null, TaskContinuationOptions.ExecuteSynchronously);

            var resultTask = Task.WhenAny(connectionTask, timeoutTask)
                .Unwrap();
            resultTask.Wait();
            
            var resultTcpClient = await resultTask;

            if (resultTcpClient != null)
                Log.Message("The client has connected to the server");
            else
                Log.Error("The client could not connect to the server! ;( ");

            return resultTcpClient;
        }

        public async Task ProcessDepartment(DeliveryServiceDepartment department)
        {
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
