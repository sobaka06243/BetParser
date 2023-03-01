using BetParser.Contracts.V1;
using BetParser.Contracts.V1.Requests;
using BetParser.Contracts.V1.Responses;
using BetParser.Server.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Xml.Linq;

namespace BetParser.Server.Endpoints.V1.OddResult;

public class CreateOddResult : EndpointBase
{
    [Produces("application/json")]
    [HttpPost(ApiRoutes.OddResults.Base, Name = $"{nameof(OddResult)}.{nameof(CreateOddResult)}")]
    [SwaggerOperation(
            Description = "Create new result odd",
            OperationId = $"{nameof(OddResult)}.{nameof(CreateOddResult)}",
            Tags = new[] { $"{nameof(OddResult)}" })
        ]
    public async Task HandleAsync(
        [FromBody] CreateOddResultRequest request,
        [FromServices] IDataOperator dataOperator,
        CancellationToken cancellationToken = default)
    {
         await dataOperator.CreateOddResult(request.Match, request.Result);  
    }
}
