using BetParser.Contracts.V1.Requests;
using BetParser.Contracts.V1.Responses;
using BetParser.Contracts.V1;
using BetParser.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BetParser.Server.Endpoints.V1.Odds;

public class CreateOdd : EndpointBase
{
    [Produces("application/json")]
    [HttpPost(ApiRoutes.Odds.Base, Name = $"{nameof(Odds)}.{nameof(CreateOdd)}")]
    [SwaggerOperation(
            Description = "Create new odd",
            OperationId = $"{nameof(Odds)}.{nameof(CreateOdd)}",
            Tags = new[] { $"{nameof(Odds)}" })
        ]
    public async Task HandleAsync(
        [FromBody] CreateOddRequest request,
        [FromServices] IDataOperator dataOperator,
        CancellationToken cancellationToken = default)
    {
        await dataOperator.CreateOdd(request.Odd);
    }
}
