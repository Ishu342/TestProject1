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
        [TestMethod]
        public void CreateUser()
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
            
            Assert.AreEqual(content.name, "morpheus");

            Assert.AreEqual(content.job, "leader");
            Assert.AreEqual(response.StatusCode, "200");
            Assert.AreEqual(response.IsSuccessful, "True");
        }

    }
}
