using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Template.Validator.Api.Controllers.Bases;
using Template.Validator.Core.Validator;

namespace Template.Validator.Api.Controllers;


[Route("api/v1/test-controller")]
[Produces("application/json")]
[ApiVersion("1"), ApiExplorerSettings(GroupName = "Tests"), SwaggerTag(description: "Endpoints focused on integrity checks and application procedures.")]
public class TestsController : StandardController
{
    private readonly ILogger<TestsController> _logger;

    public TestsController(ILogger<TestsController> logger)
    {
        _logger = logger;
    }

    /// <summary>Endpoint to verify that the application continues to respond.</summary>
    /// <remarks>Exemple Request: 
    ///   
    ///     Post /ping  [DTO PingRequest]
    ///     {
    ///        "Message": "Pong"
    ///     }</remarks>
    /// <response code="200">Return a Pong to verify that the application continues to respond.</response>
    /// <response code="400">There was some validation error in the input data and/or information processing.</response>
    /// <response code="500">An internal error occurred in the application while processing the information.</response>
    [ProducesResponseType(typeof(DefaultResponse<PingResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(DefaultResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(DefaultResponse), StatusCodes.Status500InternalServerError)]
    [HttpPost("ping")]
    public ActionResult<DefaultResponse> Ping(PingRequest request)
    {
        _logger.LogInformation($"Acess Endpoint Ping with message: {request.Message}");
        return new DefaultResponse(HttpStatusCode.OK, true, new PingResponse("Pong"));
    }

    /// <summary>Endpoint to generate an exception and check the application's internal handling..</summary>
    /// <response code="500">An internal error occurred in the application while processing the information.</response>
    [ProducesResponseType(typeof(DefaultResponse), StatusCodes.Status500InternalServerError)]
    [HttpGet("exception")]
    public OkResult Exception()
    {
        throw new Exception("Exception Endpoint.");
    }
}

/// <summary>DTO Request Ping message.</summary>
public class PingRequest
{
    public PingRequest(string message)
    {
        Message = message;
    }

    /// <summary>Ping message.</summary>
    /// <example>Ping</example>
    public string Message { get; set; }
}

/// <summary>DTO Response Ping message.</summary>
public class PingResponse
{
    public PingResponse(string message)
    {
        Message = message;
    }

    /// <summary>Ping message.</summary>
    /// <example>Pong</example>
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


