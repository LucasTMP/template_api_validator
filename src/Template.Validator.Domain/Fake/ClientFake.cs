using Bogus;
using Template.Validator.Domain.Models;

namespace Template.Validator.Domain.Fake;

public class ClientFaker : Faker<Client>
{
    public ClientFaker(string locale = "pt_BR") : base(locale)
    {
        RuleFor(o => o.Name, f => f.Name.FullName());
        RuleFor(o => o.Street, f => f.Address.StreetName());
        RuleFor(o => o.Number, f => f.Random.Int(1, 2000));
        RuleFor(o => o.District, f => f.Address.StreetName());
        RuleFor(o => o.City, f => f.Address.City());
        RuleFor(o => o.State, f => f.Address.State());
        RuleFor(o => o.ZipCode, f => f.Address.ZipCode());
        RuleFor(o => o.Phone, f => f.Phone.PhoneNumber());
    }
}
