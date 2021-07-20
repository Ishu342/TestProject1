using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace TestProject1
{
    [TestClass]
    public class RestSharpAPITest
    {
        //crearing restclient in public
        public IRestClient restClient;

        //adding annotation with the method description
        [TestMethod("New User")]
        public void CreateUserByPostRequest()
        {
            string jsonStrg = @"{
                        ""name"": ""morpheus"",
                        ""job"": ""leader""
                            }";
            //using apihelper class to access the methods here
            ApiHelper<CreateUser> RestApi = new ApiHelper<CreateUser>();
            //setting the source url
            var sourceUrl = RestApi.SetUrl("/api/users");
            //using post method
            var RestRequest = RestApi.CreatePostRequest(jsonStrg);
            //storing the response into the object
            var response = RestApi.GetResponse((RestClient)sourceUrl, RestRequest);
            CreateUser content = RestApi.GetContent<CreateUser>(response);
            //verifing the user name
            Assert.AreEqual(content.name, "morpheus");
            //verifing the user job
            Assert.AreEqual(content.job, "leader");           
        }

        [TestMethod("Users List")]
        public void GetUsers()
        {
            //using get method
            var restRequest = new RestRequest("/api/users?page=2", Method.GET);
            restRequest.AddHeader("Accept", "application/json");
            var restclient = new RestClient("https://reqres.in");
            //getting the response
            IRestResponse response = restclient.Execute(restRequest);     
            //verifing the response is shown
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
            //using put method to update the user data
            var RestRequest = restapi.CreatePutRequest(jsonStrg);
            var response = restapi.GetResponse((RestClient)sourceUrl, RestRequest);
            CreateUser content = restapi.GetContent<CreateUser>(response);
            //verifing updated user name
            Assert.AreEqual(content.name, "morpheus123");
            //verifing existed job data
            Assert.AreEqual(content.job, "leader");
        }
       
        [TestMethod("Delete User")]        
        public void DeleteRequest()
        {
            //using delete method
            IRestRequest RestRequest = new RestRequest(Method.DELETE);
            RestRequest.AddHeader("Accept", "application/json");
            ApiHelper<CreateUser> restapi = new ApiHelper<CreateUser>();
            var sourceUrl = restapi.SetUrl("/api/users/2");
            //using delete request from apihelper
            var delRequest = restapi.CreateDeleteRequest();
            var response = restapi.GetResponse((RestClient)sourceUrl, delRequest);
            //verifing the data is deleted
            Assert.AreEqual(true, response.IsSuccessful);
        }

    }
}
