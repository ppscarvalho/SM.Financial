using MediatR;
using Microsoft.AspNetCore.Mvc;
using SM.Resource.Communication.Mediator;
using SM.Resource.Messagens.CommonMessage.Notifications;

namespace SM.Financial.Apresentation.Api.Controllers.BaseController
{
    public abstract class ControllerConfiguration : ControllerBase
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediatorHandler;

        protected ControllerConfiguration(
            INotificationHandler<DomainNotification> notifications,
            IMediatorHandler mediatorHandler)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediatorHandler = mediatorHandler;
        }

        protected bool ValidOperation()
        {
            return !_notifications.ExistNotification();
        }

        protected IEnumerable<string> GetMessageError()
        {
            return _notifications.GetNotifications().Select(c => c.Value).ToList();
        }

        protected void NotificarErro(string id, string message)
        {
            _mediatorHandler.PublishNotification(new DomainNotification(id, message));
        }
    }
}
