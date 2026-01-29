using MassTransit;

namespace Redarbor.TechnicalTest.Application.Employees.EventHandlers.Domain;

public class EmployeeUpdatedEventHandler
    (IPublishEndpoint publishEndpoint,
    ILogger<EmployeeUpdatedEventHandler> logger)
    : INotificationHandler<EmployeeCreatedEvent>
{
    public async Task Handle(EmployeeCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "EmployeeUpdatedEvent handled for EmployeeId: {EmployeeId}",
            notification.Employee.Id
        );

        var employeeCreatedIntegrationEvent = notification.Employee.ToDto();
        await publishEndpoint.Publish(employeeCreatedIntegrationEvent, cancellationToken);
    }
}
