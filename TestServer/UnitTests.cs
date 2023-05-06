using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace TestServer
{
    public class UnitTest1
    {
        private HttpClient client;
        private IHost host;

        public UnitTest1()
        {
            host = CreateHostBuilder().Build();
            host.Start();
            client = host.GetTestClient();
        }

        public static IHostBuilder CreateHostBuilder() =>
                Host.CreateDefaultBuilder()
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                        webBuilder.UseTestServer();
                    });

        [Fact]
        public async Task TestGetAsync()
        {
            var weatherForecasts = await GetResponseObjectAsync<IEnumerable<WeatherForecast>>("/WeatherForecast");
            Assert.NotNull(weatherForecasts);
        }

        [Fact]
        public async Task TestWebSocket()
        {
            var server = host.GetTestServer();
            var wsClient = server.CreateWebSocketClient();
            var wsUri = new UriBuilder(server.BaseAddress)
            {
                Scheme = "ws",
                Path= "websocket"
            }.Uri;

            var socket = await wsClient.ConnectAsync(wsUri, CancellationToken.None);
            Assert.Equal(WebSocketState.Open, socket.State);
        }

        private async Task<T> GetResponseObjectAsync<T>(string url, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            var strResponse = await GetResponseAsStringAsync(url, expectedStatusCode);
            return JsonSerializer.Deserialize<T>(strResponse, new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
        }

        private async Task<string> GetResponseAsStringAsync(string url, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            using var response = await GetResponseAsync(url, expectedStatusCode);
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<HttpResponseMessage> GetResponseAsync(string url, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await client.SendAsync(requestMessage);
            
            Assert.Equal(expectedStatusCode, response.StatusCode);
            return response;
        }
    }
}
