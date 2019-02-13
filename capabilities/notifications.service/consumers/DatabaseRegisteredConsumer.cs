using System;
using System.IO;
using System.Threading.Tasks;
using messages;
using MassTransit;
using notifications.service.helpers;
using notifications.service.models;

namespace notifications.service.consumers
{
    public class DatabaseRegisteredConsumer : IConsumer<DatabaseRegistered>
    {
        private const string AppId = "RedgateNotifications.App";

        public DatabaseRegisteredConsumer()
        {
            models.ShortCutCreator.TryCreateShortcut("RedgateNotifications.App", "RedgateNotifications");       
        }

        [MTAThread]
        public Task Consume(ConsumeContext<DatabaseRegistered> context)
        {
            var dbDetails = context.Message;

            var notification = new RedgateNotification
            {
                Title = "Database Registry",
                Message = $"{dbDetails.Database.Server}-{dbDetails.Database.Database} has been registered",
                ImagePath = Path.GetFullPath("img/search.png")
            };

            CommonHelper.SaveNotification(notification);

            CommonHelper.ShowToast(AppId, notification);

            return Task.CompletedTask;
        }
       
    }
}