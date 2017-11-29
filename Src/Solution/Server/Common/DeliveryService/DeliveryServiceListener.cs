using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Eternity.Common.DataTransfer;

namespace Eternity.Server.Common.DeliveryService
{
    public class DeliveryServiceListener
    {
        public static List<TcpClient> Clients = new List<TcpClient>();

        private readonly HashSet<Task> _activeDepartments = new HashSet<Task>();
        private bool _running = true;

        public async Task Listener(IPAddress ipAddress, int port)
        {
            var listener = TcpListener.Create(port);
            listener.Start();

            while (_running)
            {
                var department = await listener.AcceptTcpClientAsync();
                // Вот это совсем не по правилам. Вроде thread + async / await не советуют использовать вместе.
                var thread = new Thread(() => ProcessDepartment(department));
                thread.Start();
            }

            await Task.WhenAll(_activeDepartments.ToArray());
        }

        public void StopListener()
        {
            _running = false;
        }

        private async Task ProcessDepartment(TcpClient client)
        {
            Console.WriteLine("Client connected, IP:"); // Нужно разобраться как идентифицировать клиента. 
            Clients.Add(client);

            using (var department = new DeliveryServiceDepartment(client))
            {
                while (_running)
                {
                    Task<Message> task = null;
                    try
                    {
                        task = department.Get();

                        _activeDepartments.Add(task);

                        await task;

                        PostOffice.AcceptParcel(task.Result);
                        // После получения новых координат разослать данные всем подключенным клиентам.
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    finally
                    {
                        if (task != null)
                            _activeDepartments.Remove(task);
                    }
                }
            }
        }
    }
}