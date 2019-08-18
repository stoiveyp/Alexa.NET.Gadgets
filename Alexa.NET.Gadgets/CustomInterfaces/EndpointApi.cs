using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Alexa.NET.Request;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Alexa.NET.Gadgets.CustomInterfaces
{
    public class EndpointApi
    {
        private static readonly JsonSerializer Serializer = JsonSerializer.CreateDefault();
        public HttpClient Client { get; }
        public EndpointApi(SkillRequest request):this(request.Context.System.ApiEndpoint,request.Context.System.ApiAccessToken)
        {

        }

        public EndpointApi(string baseAddress, string token):this(new Uri(baseAddress,UriKind.Absolute),token)
        {

        }

        public EndpointApi(Uri baseAddress, string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new InvalidOperationException("Unable to create Api client without a bearer token");
            }

            Client = new HttpClient
            {
                BaseAddress = baseAddress,
                DefaultRequestHeaders = {Authorization = new AuthenticationHeaderValue("Bearer", token)}
            };
        }

        public EndpointApi(HttpClient client)
        {
            Client = client;
        }



        public async Task<EndpointResponse> GetEndpoints()
        {
            using (var stream = await Client.GetStreamAsync("/v1/endpoints"))
            {
                using (var json = new JsonTextReader(new StreamReader(stream)))
                {
                    return Serializer.Deserialize<EndpointResponse>(json);
                }
            }
        }
    }
}
