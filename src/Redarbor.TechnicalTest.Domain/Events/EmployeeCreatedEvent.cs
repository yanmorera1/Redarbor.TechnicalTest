namespace Redarbor.TechnicalTest.Domain.Events;

public record EmployeeCreatedEvent(Employee Employee) : IDomainEvent;
