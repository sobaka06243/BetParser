using BetParser.Contracts.V1;
using BetParser.Contracts.V1.Requests;
using BetParser.Contracts.V1.Responses;
using BetParser.Server.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Xml.Linq;

namespace BetParser.Server.Endpoints.V1.PreMatches;

public class CreatePreMatch : EndpointBase
{
    [Produces("application/json")]
    [HttpPost(ApiRoutes.PreMatches.Base, Name = $"{nameof(PreMatches)}.{nameof(CreatePreMatch)}")]
    [SwaggerOperation(
            Description = "Create new match",
            OperationId = $"{nameof(PreMatches)}.{nameof(CreatePreMatch)}",
            Tags = new[] { $"{nameof(PreMatches)}" })
        ]
    public async Task<CreateMatchResponse> HandleAsync(
        [FromBody] CreateMatchRequest request,
        [FromServices] IDataOperator dataOperator,
        CancellationToken cancellationToken = default)
    {
        return await dataOperator.CreateMatch(request.Match);  
    }
}
