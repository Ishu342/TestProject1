using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject1
{
    [TestClass]
    public class HTTPClientAPITest
    {
        //base url
        private string url = "https://reqres.in";
        //creating http client
        HttpClient httpClient = new HttpClient();

        //adding annotation with the method description
        [TestMethod("Users List")]
        public void GetRequest()
        {                      
            //creating httprequest
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.RequestUri = new Uri(url + "/api/users?page=2");
            //using get method
            httpRequest.Method = HttpMethod.Get;
            //adding header as json
            httpRequest.Headers.Add("Accept", "application/json");
            //sending therequest
            Task<HttpResponseMessage> response = httpClient.SendAsync(httpRequest);
            //getting result by httpresponse
            HttpResponseMessage responsemsg = response.Result;            
            HttpStatusCode statusCode = responsemsg.StatusCode;            
            //verifing statuscode
            Assert.AreEqual("OK",statusCode.ToString());
            //verifing that the data is present
            Assert.IsNotNull(true);
        }

        [TestMethod("New User")]        
            public void POSTRequest()
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            //using post method
            httpRequest.Method = HttpMethod.Post;
            httpRequest.Headers.Add("Accept", "application/json");
            //creating json data
            string jsonStrg ="{" +
            "\"id\": \"7\","+
            "\"email\": \"john.lawson@reqres.in\","+
            "\first_name\": \"john\","+
            "\"last_name\": \"Lawson\""+            
            "}";

            HttpContent httpcontent = new StringContent(jsonStrg, Encoding.UTF8);
            Task<HttpResponseMessage> response = httpClient.PostAsync(url+"/api/users", httpcontent);
            HttpResponseMessage responsemsg = response.Result;            
            HttpStatusCode statusCode = responsemsg.StatusCode;    
            //verifing the status code
            Assert.AreEqual("Created",statusCode.ToString());

        }

        [TestMethod("Update User")]

        public void PUTRequest()
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            //using put method to upfate the data
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
            HttpStatusCode statusCode = responsemsg.StatusCode;            
            String msg = "OK";
            //verifing the statuscode
            Assert.AreEqual(msg,statusCode.ToString());

        }

        [TestMethod("Delete User")]

        public void DELETERequest()
        {            
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            //using delete method
            httpRequest.Method = HttpMethod.Delete;
            httpRequest.Headers.Add("Accept", "application/json");
            //task has to be created when async is in use
            Task<HttpResponseMessage> response = httpClient.DeleteAsync(url+"/api/users/2");
            HttpResponseMessage responsemsg = response.Result;            
            HttpStatusCode statusCode = responsemsg.StatusCode;
            //verifing statuscode
            Assert.AreEqual("NoContent", statusCode.ToString());
            //closing the opened httpclient
            httpClient.Dispose();
        }
    }
}
