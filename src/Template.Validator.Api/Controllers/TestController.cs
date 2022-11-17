using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Template.Validator.Api.Controllers.Bases;
using Template.Validator.Core.Validator;

namespace Template.Validator.Api.Controllers;

[Route("api/v1/test-controller")]
public class TestController : StandardController
{
    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
    }

    [HttpPost("ping")]
    public ActionResult<DefaultResponse> Ping(PingRequest request)
    {
        _logger.LogInformation($"Acess Endpoint Ping with message: {request.Message}");
        return new DefaultResponse(HttpStatusCode.OK, true, new PingResponse("Pong"));
    }

    [HttpGet("exception")]
    public OkResult Exception()
    {
        throw new Exception("Exception Endpoint.");
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
            .Must(x => x.ToLower() == "ping")
                .WithMessage("Endpoint só aceita ping como valor de entrada.");
    }
}


