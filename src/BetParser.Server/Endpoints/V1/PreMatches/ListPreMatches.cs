using BetParser.Contracts.V1.Requests;
using BetParser.Contracts.V1;
using BetParser.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Xml.Linq;
using BetParser.Contracts.V1.Responses;

namespace BetParser.Server.Endpoints.V1.PreMatches;

public class ListPreMatches : EndpointBase
{
    [Produces("application/json")]
    [HttpGet(ApiRoutes.PreMatches.List, Name = $"{nameof(PreMatches)}.{nameof(ListPreMatches)}")]
    [SwaggerOperation(
            Description = "List matches",
            OperationId = $"{nameof(PreMatches)}.{nameof(ListPreMatches)}",
            Tags = new[] { $"{nameof(PreMatches)}" })
        ]
    public async Task<ListMatchesResponse> HandleAsync(
        [FromServices] IDataOperator dataOperator,
        CancellationToken cancellationToken = default)
    {
        return await dataOperator.GetMatches();
    }
}
