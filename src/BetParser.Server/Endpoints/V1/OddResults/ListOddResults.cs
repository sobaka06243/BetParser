using BetParser.Contracts.V1;
using BetParser.Contracts.V1.Responses;
using BetParser.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BetParser.Server.Endpoints.V1.OddResults;

public class ListOddResults : EndpointBase
{
    [Produces("application/json")]
    [HttpGet(ApiRoutes.OddResults.List, Name = $"{nameof(OddResult)}.{nameof(ListOddResults)}")]
    [SwaggerOperation(
            Description = "List OddResults",
            OperationId = $"{nameof(OddResult)}.{nameof(ListOddResults)}",
            Tags = new[] { $"{nameof(OddResult)}" })
        ]
    public async Task<ListOddResultsResponse> HandleAsync(
        [FromServices] IDataOperator dataOperator,
        CancellationToken cancellationToken = default)
    {
        return await dataOperator.GetOddResults();
    }
}
