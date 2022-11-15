using Microsoft.AspNetCore.Mvc;
using System.Net;
using Template.Validator.Api.Controllers.Bases;
using Template.Validator.Core.Validator;
using FluentValidation;

namespace Template.Validator.Api.Controllers;

    [Route("[controller]")]
    public class PingController : StandardController
    {
        private readonly ILogger<PingController> _logger;

        public PingController(ILogger<PingController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "Ping")]
        public ActionResult<DefaultResponse> Ping(PingRequest request)
        {
            _logger.LogInformation($"Acess Endpoint Ping with message: {request.Message}");
            return new DefaultResponse(HttpStatusCode.OK,true, new PingResponse("Pong"));
        }

    }

    public class PingRequest
    {
        public PingRequest(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }

    public class PingResponse
    {
        public PingResponse(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }


    public class PingRequestValidator : BaseModelValidator<PingRequest>
    {
        public PingRequestValidator()
        {
        RuleFor(pingRequest => pingRequest.Message)
            .NotEmpty()
                .WithMessage("Campo não pode ser nullo ou vazio.")
            .Must(x=> x.ToLower() == "ping")
                .WithMessage("Endpoint só aceita ping como valor de entrada.");
        }
    }


