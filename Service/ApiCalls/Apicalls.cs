namespace KaiCryptoTracker.AllApiCalls;

public class ApiCalls : IApiCalls
{
    private readonly IHttpClientFactory _httpclientFactory;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ApiCalls> _logger;

    public ApiCalls(IHttpClientFactory httpclientFactory, IConfiguration configuration, ILogger<ApiCalls> logger)
    {
        _httpclientFactory = httpclientFactory;
        _configuration = configuration;
        _logger = logger;
    }


    public async Task<string> Moralis(string url)
    {

        string jsondata = string.Empty;
        try
        {
            var client = _httpclientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Accept.ParseAdd("application/json");

            //get api key 
            string? apikey = _configuration.GetSection("Moralis")["key"];

            if (apikey != null) request.Headers.Add("X-API-Key", apikey);

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var streamcontent = response.Content.ReadAsStream();
                var reader = new StreamReader(streamcontent);
                 jsondata = await reader.ReadToEndAsync();
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }


        return jsondata;
    }
}