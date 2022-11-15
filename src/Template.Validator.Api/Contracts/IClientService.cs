using Template.Validator.Domain.Models;

namespace Template.Validator.Api.Contracts;

public interface IClientService
{
    Task<Client> Add(Client client);
    Task<Client> Update(Client client);
    Task Delete(string id);
    Task<List<Client>> GetAll();
    Task<Client> Get(string id);

}
