namespace BetParser.Client;

public partial class BetParserClient
{
    internal System.Threading.Tasks.Task PrepareRequestAsync(
      System.Net.Http.HttpClient client,
      System.Net.Http.HttpRequestMessage request,
      string url,
      System.Threading.CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    internal Task PrepareRequestAsync(
        System.Net.Http.HttpClient client,
        System.Net.Http.HttpRequestMessage request,
        System.Text.StringBuilder urlBuilder,
        System.Threading.CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    internal Task ProcessResponseAsync(System.Net.Http.HttpClient client, System.Net.Http.HttpResponseMessage response, System.Threading.CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
