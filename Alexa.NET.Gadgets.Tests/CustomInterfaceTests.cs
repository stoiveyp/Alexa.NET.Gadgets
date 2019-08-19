using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Alexa.NET.Gadgets.CustomInterfaces;
using Alexa.NET.Request;
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

            const string gameOverText = "Game over! Would you like to hear your stats?";
            var token = Guid.Parse("1234abcd-40bb-11e9-9527-6b98b093d166");
            var expected = new StartEventHandler(
                token,
                new Expiration(8000, new {gameOverSpeech=gameOverText}),
                FilterMatchAction.SendAndTerminate,
                new CombinedFilterExpression(
                    CombinationOperator.And,
                    new ComparisonFilterExpression(ComparisonOperator.Equals, "header.namespace", "Custom.Robot"),
                    new ComparisonFilterExpression(ComparisonOperator.GreaterThan, "payload.angle", 10)
                        )
            );

            //remove token due to DeepEquals Guid mismatch
            Assert.True(Utility.CompareJson(expected, "StartEventHandler.json","token"));

            var directive = Utility.ExampleFileContent<IDirective>("StartEventHandler.json");
            var start = Assert.IsType<StartEventHandler>(directive);

            Assert.NotNull(start.Expiration);
            Assert.Equal(token, start.Token);
            Assert.Equal(8000,start.Expiration.Milliseconds);

            var payload = Assert.IsType<JObject>(start.Expiration.Payload);
            Assert.Equal(gameOverText, payload.Value<string>("gameOverSpeech"));

            Assert.Equal(FilterMatchAction.SendAndTerminate,start.EventFilter.FilterMatchAction);

            var combined = Assert.IsType<CombinedFilterExpression>(start.EventFilter.FilterExpression);
            Assert.Equal(2,combined.Filters.Length);
            Assert.True(combined.Filters.All(f => f is ComparisonFilterExpression));
            var last = combined.Filters.Cast<ComparisonFilterExpression>().Last();
            Assert.Equal((Int64)10,last.Value);
            Assert.Equal("payload.angle",last.Variable);
        }

        [Fact]
        public void StopHandlerSerializesCorrectly()
        {
            StopEventHandler.AddToDirectiveConverter();
            var directive = Utility.ExampleFileContent<IDirective>("StopEventHandler.json");
            var stop = Assert.IsType<StopEventHandler>(directive);
            Assert.Equal(Guid.Parse("1234abcd-40bb-11e9-9527-6b98b093d166"), stop.Token);
        }

        [Fact]
        public void EventReceivedRequest()
        {
            new CustomInterfaceHandler().AddToRequestConverter();
            var skillRequest = Utility.ExampleFileContent<SkillRequest>("EventsReceived.json");
            var request = Assert.IsType<EventsReceivedRequest>(skillRequest.Request);

            Assert.Equal("1234abcd-40bb-11e9-9527-6b98b093d166",request.Token.ToString());
            var foundEvent = Assert.Single(request.Events);

            Assert.Equal("Custom.Robot",foundEvent.Header.Namespace);
            Assert.Equal("EyeBlink",foundEvent.Header.Name);
            Assert.Equal("amzn1.ask.endpoint.ABCD",foundEvent.Endpoint.EndpointId);

            var payload = foundEvent.Payload as JObject;
            Assert.Equal("ok",payload.Value<string>("ack"));

        }
    }
}
