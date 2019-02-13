using System;
using System.IO;
using System.Threading.Tasks;
using messages.events;
using MassTransit;
using notifications.service.helpers;
using notifications.service.models;

namespace notifications.service.consumers
{
    public class DatabaseUpdatedConsumer : IConsumer<SchemaChanged>
    {
        private const string AppId = "RedgateNotifications.App";

        public DatabaseUpdatedConsumer()
        {
            models.ShortCutCreator.TryCreateShortcut("RedgateNotifications.App", "RedgateNotifications");
        }

        [MTAThread]
        public Task Consume(ConsumeContext<SchemaChanged> context)
        {
            var dbDetails = context.Message;
            var connectionDetails = dbDetails.Database.ConnectionDetails;
            var notification = new RedgateNotification
            {
                Title = "Schema change",
                Message = $"{connectionDetails.Database} now has {dbDetails.Database.Tables.Count} tables",
                ImagePath = Path.GetFullPath("img/search.png")
            };

            CommonHelper.SaveNotification(notification);

            CommonHelper.ShowToast(AppId, notification);

            return Task.CompletedTask;
        }
    }
}