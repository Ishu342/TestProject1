using System;
using System.Collections.Generic;
using System.Net.Http;
using Flurl;
using RestSharp;
using RestSharp.Authenticators;
using TestProject4.Model;

namespace TestProject4.Helper
{
    public class PostHelper<T>
    {

        public IRestClient restClient;
        public IRestRequest RestRequest;
        public string PostUrl = "{{hostName}}/v1/accounts/login/real";
        public IRestClient SetUrl(string sourceUrl)
        {
            var url = Url.Combine(PostUrl, sourceUrl);
            var restClient = new RestClient(url);
            restClient.Authenticator = new HttpBasicAuthenticator("admin", "welcome");
            return restClient;
        }
        
        

        public IRestResponse PostRequest(string url, Dictionary<string, string> headers, object body,
            DataFormat dataFormat)
        {
            restClient = new RestClient();
            IRestRequest restRequest = CreatePostRequest<T>(url, headers, Method.POST, body, dataFormat);
            IRestResponse restResponse = restClient.Execute(restRequest);
            return restResponse;
        }

        
        public IRestRequest CreatePostRequest<T>(String url, Dictionary<string, string> headers, Method method,
            object body, DataFormat dataFormat)
        {
            IRestRequest RestRequest = new RestRequest(Method.POST);
            RestRequest.AddHeader("Accept", "application/json");
            RestRequest.AddParameter("application/json", 
                ParameterType.RequestBody);

            if (headers != null)
            {
                foreach (string key in headers.Keys)
                {
                    RestRequest.AddHeader(key, headers[key]);
                }
            }
            if (body != null)
            {
                RestRequest.RequestFormat = DataFormat.Json;
                //IRestRequest.RequestFormat = dataFormat;
                RestRequest.AddBody(body);
            }
            return RestRequest;
        }

        
        public bool IsNumericalvalue(double n)
        {
            bool isNum = false;
            if (n >= 0)
            {
                isNum = true;
            }
            return isNum;
        }

        internal bool IsNumericalvalue(object loyaltyBalance)
        {
            throw new NotImplementedException();
        }

        internal LoginResponseModel CreatePostRequest(object url, object headers, object p, object json)
        {
            throw new NotImplementedException();
        }

        internal IRestResponse CreatePostRequest(string posturl, Dictionary<string, string> headers, LoginRequestModel loginModel, object json)
        {
            throw new NotImplementedException();
        }
    }
}
