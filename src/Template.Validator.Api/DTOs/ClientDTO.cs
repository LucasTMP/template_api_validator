namespace Template.Validator.Api.DTOs;

public record ClientDTO
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string Street { get; set; }
    public int Number { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Phone { get; set; }
}
