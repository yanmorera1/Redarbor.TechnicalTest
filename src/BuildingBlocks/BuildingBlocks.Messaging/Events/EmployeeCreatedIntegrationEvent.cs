namespace BuildingBlocks.Messaging.Events;

public record EmployeeCreatedIntegrationEvent : IntegrationEvent
{
    public string Email { get; set; }
    public string Password { get; set; }
    public int RoleId { get; set; }
}
