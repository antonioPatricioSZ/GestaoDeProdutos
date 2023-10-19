using Microsoft.Extensions.Diagnostics.HealthChecks;
using RestSharp;

namespace Api.Services.Health;

public class ApiHealthCheck : IHealthCheck {

    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, CancellationToken cancellationToken = default
    ){
        
        var url = "https://chuck-norris-jokes.p.rapidapi.com/de/jokes/random.br.teste/patricio";
        var client = new RestClient();
        var request = new RestRequest(url, Method.Get);
        request.AddHeader(
            "X-RapidAPI-Key", 
            "e780fc7a88msh9cedcd28bbef41cp1a1198jsn874f2edab09e"
        );
        request.AddHeader("X-RapidAPI-Host", "chuck-norris-jokes.p.rapidapi.com");

        var response = client.Execute(request);

        if(response.IsSuccessful) {
            return Task.FromResult(HealthCheckResult.Healthy());
        } else {
            return Task.FromResult(HealthCheckResult.Unhealthy());
        }

    }

}
