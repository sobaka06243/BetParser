using BetParser.Contracts.V1.Requests;
using BetParser.Contracts.V1.Responses;
using BetParser.Contracts.V1;
using BetParser.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Xml.Linq;

namespace BetParser.Server.Endpoints.V1.Odds;

public class ListOdds : EndpointBase
{
    [Produces("application/json")]
    [HttpGet(ApiRoutes.Odds.List, Name = $"{nameof(Odds)}.{nameof(ListOdds)}")]
    [SwaggerOperation(
        Description = "List odds",
        OperationId = $"{nameof(Odds)}.{nameof(ListOdds)}",
        Tags = new[] { $"{nameof(Odds)}" })
    ]
    public async Task<ListOddsResponse> HandleAsync(
    [FromServices] IDataOperator dataOperator,
    CancellationToken cancellationToken = default)
    {
        return await dataOperator.GetOdds();
    }
}
