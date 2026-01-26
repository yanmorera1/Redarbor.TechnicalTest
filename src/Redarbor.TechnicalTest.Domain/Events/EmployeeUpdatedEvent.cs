namespace Redarbor.TechnicalTest.Domain.Events;

public record EmployeeUpdatedEvent(Employee Employee) : IDomainEvent;
