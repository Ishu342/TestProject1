using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace TestProject1
{
    [TestClass]
    public class RestSharpAPITest
    {
        public IRestClient restClient;
        [TestMethod("New User")]
        public void CreateUserByPostRequest()
        {
            string jsonStrg = @"{
                        ""name"": ""morpheus"",
                        ""job"": ""leader""
                            }";

            ApiHelper<CreateUser> RestApi = new ApiHelper<CreateUser>();
            var sourceUrl = RestApi.SetUrl("/api/users");
            var RestRequest = RestApi.CreatePostRequest(jsonStrg);
            var response = RestApi.GetResponse((RestClient)sourceUrl, RestRequest);
            CreateUser content = RestApi.GetContent<CreateUser>(response);
            Console.WriteLine("response content-->" + response.Content);
            Assert.AreEqual(content.name, "morpheus");
            Assert.AreEqual(content.job, "leader");           
        }

        [TestMethod("Users List")]
        public void GetUsers()
        {
            var restRequest = new RestRequest("/api/users?page=2", Method.GET);
            restRequest.AddHeader("Accept", "application/json");
            //restRequest.RequestFormat = DataFormat.Json;
            var restclient = new RestClient("https://reqres.in");
            IRestResponse response = restclient.Execute(restRequest);         
            Assert.AreEqual(true, response.IsSuccessful);
        }
        
        [TestMethod("Update User")]
        public void CreatePutRequest()
        {
            string jsonStrg = @"{
                        ""name"": ""morpheus123"",
                        ""job"": ""leader""
                            }";
            ApiHelper<CreateUser> restapi = new ApiHelper<CreateUser>();
            var sourceUrl = restapi.SetUrl("/api/users/2");
            var RestRequest = restapi.CreatePutRequest(jsonStrg);
            var response = restapi.GetResponse((RestClient)sourceUrl, RestRequest);
            CreateUser content = restapi.GetContent<CreateUser>(response);
            Console.WriteLine("response content-->" + response.Content);
            Assert.AreEqual(content.name, "morpheus123");
            Assert.AreEqual(content.job, "leader");
        }
       
        [TestMethod("Delete User")]        
        public void DeleteRequest()
        {
            IRestRequest RestRequest = new RestRequest(Method.DELETE);
            RestRequest.AddHeader("Accept", "application/json");
            ApiHelper<CreateUser> restapi = new ApiHelper<CreateUser>();
            var sourceUrl = restapi.SetUrl("/api/users/2");
            var delRequest = restapi.CreateDeleteRequest();
            var response = restapi.GetResponse((RestClient)sourceUrl, delRequest);
            Assert.AreEqual(true, response.IsSuccessful);
        }

    }
}
