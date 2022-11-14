namespace Template.Validator.Domain.Models;

public class Aeroplane : Entity
{
    public string Identification { get; set; }
    public string Model { get; set; }
    public int TurbineAmount { get; set; }
    public int PassengersAmount { get; set; }
    public double ChargeCapacity { get; set; }

}
