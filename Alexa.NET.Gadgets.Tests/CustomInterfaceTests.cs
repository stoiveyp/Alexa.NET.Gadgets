using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Alexa.NET.Gadgets.CustomInterfaces;
using Alexa.NET.Response;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Alexa.NET.Gadgets.Tests
{
    public class CustomInterfaceTests
    {
        [Fact]
        public void NoUrlThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new EndpointApi((string)null, "token"));
        }

        [Fact]
        public void NoTokenThrowsInvalidOperationException()
        {
            Assert.Throws<InvalidOperationException>(() => new EndpointApi("https://api.amazonalexa.com", null));
        }

        [Fact]
        public async Task GetEndpointsHitsCorrectUrl()
        {
            var client = new HttpClient(new ActionHandler(req =>
            {
                Assert.Equal("/v1/endpoints", req.RequestUri.PathAndQuery);
            }))
            { BaseAddress = new Uri("https://api.amazonalexa.com", UriKind.Absolute) };
            var api = new EndpointApi(client);
            await api.GetEndpoints();
            client.Dispose();
        }

        [Fact]
        public async Task DeserializeEndpointsCorrectly()
        {
            var client = new HttpClient(new ActionHandler(req => Task.FromResult(
                new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(
                        Utility.ExampleFileContent("Endpoints.json"))

                })))
            { BaseAddress = new Uri("https://api.amazonalexa.com", UriKind.Absolute) };

            var api = new EndpointApi(client);
            var endpoints = await api.GetEndpoints();
            Assert.Equal(2,endpoints.Endpoints.Length);

            var first = endpoints.Endpoints.First();
            Assert.Equal("amzn1.ask.endpoint.ABC123", first.EndpointId);
            var capability = Assert.Single(first.Capabilities);
            Assert.Equal("AlexaInterface",capability.Type);
            Assert.Equal("Custom.CustomInterface1",capability.Interface);
            Assert.Equal("1.0",capability.Version);
        }

        [Fact]
        public void SendDirectiveSerializesCorrectly()
        {
            SendDirective.AddToDirectiveConverter();
            var directive = Utility.ExampleFileContent<IDirective>("SendDirective.json");
            var send = Assert.IsType<SendDirective>(directive);
            Assert.Equal("amzn1.ask.endpoint.ABC123",send.Endpoint.EndpointId);
            Assert.Equal("Custom.Robot",send.Header.Namespace);
            Assert.Equal("Spin",send.Header.Name);
            var payload = send.Payload as JObject;
            Assert.Equal("clockwise",payload.Value<string>("direction"));
            Assert.Equal(5,payload.Value<int>("times"));
        }

        [Fact]
        public void StartHandlerSerializesCorrectly()
        {
            StartEventHandler.AddToDirectiveConverter();
            var directive = Utility.ExampleFileContent<StartEventHandler>("StartEventHandler.json");
            var start = Assert.IsType<StartEventHandler>(directive);
        }

        [Fact]
        public void StopHandlerSerializesCorrectly()
        {
            Assert.True(false);
        }
    }
}
