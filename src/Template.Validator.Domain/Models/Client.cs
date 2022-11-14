namespace Template.Validator.Domain.Models;

public class Client : Entity
{
    public string Name { get; set; }
    public string Street { get; set; }
    public int Number { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Phone { get; set; }
}
