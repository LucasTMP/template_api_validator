using AutoMapper;
using Template.Validator.Api.DTOs;
using Template.Validator.Domain.Models;

namespace Template.Validator.Api.Mappers;

public class ApiProfile : Profile
{
    public ApiProfile()
    {
        CreateMap<ClientDTO, Client>().ReverseMap();
    }
}
