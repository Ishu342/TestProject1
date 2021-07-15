using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;

namespace TestProject1
{
    [TestClass]
    public class GetUsersAPIwithHTTPClient
    {
        private string url = "https://reqres.in";
        HttpClient httpClient = new HttpClient();
        [TestMethod]
        public void GetRequest()
        {
                      
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.RequestUri = new Uri(url + "/api/users?page=2");
            httpRequest.Method = HttpMethod.Get;
            httpRequest.Headers.Add("Accept", "application/json");
            Task<HttpResponseMessage> response = httpClient.SendAsync(httpRequest);
            HttpResponseMessage responsemsg = response.Result;
            Console.WriteLine("Response Message-->"+ responsemsg.ToString());
            HttpStatusCode statusCode = responsemsg.StatusCode;
            Console.WriteLine("Response Code-->" + statusCode);

        }
        [TestMethod]
        
            public void POSTRequest()
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage();            
            httpRequest.Method = HttpMethod.Post;
            httpRequest.Headers.Add("Accept", "application/json");
            string jsonStrg ="{" +
            "\"id\": \"7\","+
            "\"email\": \"john.lawson@reqres.in\","+
            "\first_name\": \"john\","+
            "\"last_name\": \"Lawson\""+            
            "}";

            HttpContent httpcontent = new StringContent(jsonStrg, Encoding.UTF8);
            Task<HttpResponseMessage> response = httpClient.PostAsync(url+"/api/users", httpcontent);
            HttpResponseMessage responsemsg = response.Result;
            Console.WriteLine("Response Message-->" + responsemsg.ToString());
            HttpStatusCode statusCode = responsemsg.StatusCode;
            Console.WriteLine("Response Code-->" + statusCode);

        }

        [TestMethod]

        public void PUTRequest()
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.Method = HttpMethod.Put;
            httpRequest.Headers.Add("Accept", "application/json");
            string jsonStrg = "{" +
            "\"id\": \"7\"," +
            "\"email\": \"johns.lawson@reqres.in\"," +
            "\first_name\": \"johns\"," +
            "\"last_name\": \"Lawson\"" +
            "}";

            HttpContent httpcontent = new StringContent(jsonStrg, Encoding.UTF8);
            Task<HttpResponseMessage> response = httpClient.PutAsync(url+"/api/users/2", httpcontent);
            HttpResponseMessage responsemsg = response.Result;
            Console.WriteLine("Response Message-->" + responsemsg.ToString());
            HttpStatusCode statusCode = responsemsg.StatusCode;
            Console.WriteLine("Response Code-->" + statusCode);
                        
        }

        [TestMethod]

        public void DELETERequest()
        {
            
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.Method = HttpMethod.Delete;
            httpRequest.Headers.Add("Accept", "application/json");
            Task<HttpResponseMessage> response = httpClient.DeleteAsync(url+"/api/users/2");
            HttpResponseMessage responsemsg = response.Result;
            Console.WriteLine("Response Message-->" + responsemsg.ToString());
            HttpStatusCode statusCode = responsemsg.StatusCode;
            Console.WriteLine("Response Code-->" + statusCode);

            httpClient.Dispose();
        }
    }
}
