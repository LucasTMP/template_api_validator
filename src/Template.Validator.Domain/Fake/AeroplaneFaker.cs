using Bogus;
using Template.Validator.Domain.Models;

namespace Template.Validator.Domain.Fake;

public class AeroplaneFaker : Faker<Aeroplane>
{
    private static string[] models = new[] { "Cessna 172", "Boeing 737", "Airbus A320", "Bombardier CRJ", "Boeing 727", "Airbus A330" };
    public AeroplaneFaker(string locale = "pt_BR") : base(locale)
    {
        RuleFor(o => o.Model, f => f.PickRandom(models));
        RuleFor(o => o.TurbineAmount, f => f.PickRandom(new[] { 2, 4, 6 }));
        RuleFor(o => o.PassengersAmount, f => f.PickRandom(new[] { 20, 40, 60 }));
        RuleFor(o => o.ChargeCapacity, f => f.PickRandom(new[] { 200d, 400d, 600d }));
    }
}
