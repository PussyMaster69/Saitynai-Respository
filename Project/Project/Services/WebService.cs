using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Project.Models;
using RestSharp;

namespace Project.Services
{
    public class WebService : IWebService
    {
        public async Task<AuthenticationResult> ValidateGoogleToken(string token)
        {
            var restClient = new RestClient("https://www.googleapis.com/oauth2/v3/tokeninfo");
            var request = new RestRequest(Method.GET);
            request.AddQueryParameter("id_token", token);
            TaskCompletionSource<IRestResponse> taskCompletion = new TaskCompletionSource<IRestResponse>();

            RestRequestAsyncHandle handle = restClient.ExecuteAsync(
                request, r => taskCompletion.SetResult(r));
            RestResponse response = (RestResponse)(await taskCompletion.Task);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<AuthenticationResult>(response.Content);
            }
            return null;
        }
    }
}
