using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Template.Validator.Api.Contracts;
using Template.Validator.Api.Controllers.Bases;
using Template.Validator.Api.DTOs;
using Template.Validator.Domain.Models;

namespace Template.Validator.Api.Controllers
{
    [Route("[controller]")]
    public class ClientController : StandardController
    {
        //private readonly IClientService _service;
        private readonly ILogger<ClientController> _logger;
        private readonly IMapper _mapper;

        public ClientController(ILogger<ClientController> logger,
                                //IClientService service,
                                IMapper mapper)
        {
            _logger = logger;
            //_service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClientDTO dto)
        {
            var client = _mapper.Map<Client>(dto);
            //_service.Add(client);
            return Created("", dto);
        }
    }
}