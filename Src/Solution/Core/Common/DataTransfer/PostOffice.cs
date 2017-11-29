using System;
using Eternity.Core;

namespace Eternity.Common.DataTransfer
{
    // САМЫЙ СПОРНЫЙ КЛАСС!!! В нем присутствуют методы для отправки посылки, и static методы для принятия.

    // Можно заменить на MessengerHandler c методом Handle или 
    // UnloadingController с методом Unloading или DeliveringMessage с методом deliver.

    // Для первой итерации оставлю как статический класс, дальше стоит добавить интерфес + прокидывать через dependency injection.
    public class PostOffice
    {
        // Временное событие для тестирования.
        public static Action<String> PackageArrived;

        private readonly DeliveryServiceDepartment _department;

        public PostOffice(DeliveryServiceDepartment client)
        {
            _department = client;
        }

        public void Send<T>(T data, RequestType method)
        {
            if (_department == null)
                throw new Exception("Department is null, you need to connect to the server!");

            var pack = PackingService.Packing(data, method);
            var _ = _department.Send(pack);
        }

        public static void Accept(Message message)
        {
            switch (message.Method)
            {
                case RequestType.Move:
                    var data = message.Body as Position;
                    
                    // Вызов какого-то Core класса, для обновления позиции юзера, тут 100% не хватает id или имени игрока.
                    Console.WriteLine($"X: {data.X}, Y: {data.Y}");
                    PackageArrived?.Invoke($"X: {data.X}, Y: {data.Y}");
                    break;
            }
        }
    }
}
