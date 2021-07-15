using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using RestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject1
{
    [TestClass]
    public class RestAPITest
    {
        public IRestClient restClient;
        [TestMethod]
        public void CreateUserByPostRequest()
        {
            string jsonStrg = @"{
                        ""name"": ""morpheus"",
                        ""job"": ""leader""
                            }";

            ApiHelper<CreateUser> restapi = new ApiHelper<CreateUser>();
            var sourceUrl = restapi.SetUrl("/api/users");
            var RestRequest = restapi.CreatePostRequest(jsonStrg);
            var response = restapi.GetResponse((RestClient)sourceUrl, RestRequest);
            CreateUser content = restapi.GetContent<CreateUser>(response);
            Console.WriteLine("response content-->" + response.Content);
            Assert.AreEqual(content.name, "morpheus");

            Assert.AreEqual(content.job, "leader");
           // Assert.AreEqual(response.StatusCode, Created);
            //Assert.AreEqual(response.IsSuccessful, "true");
        }

        [TestMethod]
        public ListOfUsersDTO GetUsers<ListOfUsersDTO>()
        {
            var restRequest = new RestRequest("/api/users?page=2", Method.GET);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
             
            IRestResponse response = restClient.Execute(restRequest);
            var content = response.Content;

            var users = JsonConvert.DeserializeObject<ListOfUsersDTO>(content);
            
            return users;
        }


        [TestMethod]
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
        public IRestRequest CreateGetRequest()
        {
            IRestRequest RestRequest = new RestRequest(Method.GET);
            RestRequest.AddHeader("Accept", "application/json");
            return RestRequest;

        }
        [TestMethod]
        public void DeleteRequest()
        {
            IRestRequest RestRequest = new RestRequest(Method.DELETE);
            RestRequest.AddHeader("Accept", "application/json");

            ApiHelper<CreateUser> restapi = new ApiHelper<CreateUser>();
            var sourceUrl = restapi.SetUrl("/api/users/2");
            var delRequest = restapi.CreateDeleteRequest();
            var response = restapi.GetResponse((RestClient)sourceUrl, delRequest);

            Console.WriteLine("Response code" + response.IsSuccessful);

        }

    }
}
