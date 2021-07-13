using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using System.IO;
using Flurl;
using RestSharp.Authenticators;

namespace TestProject1
{
    class ApiHelper<T>
    {

        public IRestClient restClient;
        public IRestRequest RestRequest;
        public string baseurl = "https://reqres.in";

        public IRestClient SetUrl(string sourceUrl)
        {
            var url = Url.Combine(baseurl, sourceUrl);
            var restClient = new RestClient(url);
            restClient.Authenticator = new HttpBasicAuthenticator("admin", "welcome");
            return restClient;

        }

        public IRestRequest CreatePostRequest(string jsonStrg)
        {
            IRestRequest RestRequest = new RestRequest(Method.POST);
            RestRequest.AddHeader("Accept", "application/json");
            RestRequest.AddParameter("application/json", jsonStrg, ParameterType.RequestBody);
            return RestRequest;

        }


        public IRestRequest CreatePutRequest(string jsonstr)
        {
            IRestRequest RestRequest = new RestRequest(Method.PUT);
            RestRequest.AddHeader("Accept", "application/json");
            RestRequest.AddParameter("application/json", jsonstr, ParameterType.RequestBody);
            return RestRequest;

        }
        public IRestRequest CreateGetRequest()
        {
            IRestRequest RestRequest = new RestRequest(Method.GET);
            RestRequest.AddHeader("Accept", "application/json");            
            return RestRequest;

        }
        public IRestRequest CreateDeleteRequest()
        {
            IRestRequest RestRequest = new RestRequest(Method.DELETE);
            RestRequest.AddHeader("Accept", "application/json");
            return RestRequest;

        }

        public IRestResponse GetResponse(RestClient restClient, IRestRequest RestRequest)
        {
            return restClient.Execute(RestRequest);

        }

        public DataTableObject GetContent<DataTableObject>(IRestResponse restResponse)
        {
            var content = restResponse.Content;
            DataTableObject deserializeobject = Newtonsoft.Json.JsonConvert.DeserializeObject< DataTableObject>(content);
            return deserializeobject;

        }

        
    }
}
