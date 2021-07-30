using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using Glimpse.AspNet.Model;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using TestProject4.Helper;
using TestProject4.Model;
using Environment = TestProject4.Model.Environment;

namespace TestProject4.Test
{
    class LoginTestClass
    {
        LoginRequestModel loginModel;
        public static string token;
        private Random random = new Random();
        static string hostName = ConfigurationManager.AppSettings["hostName"];
        string userName = ConfigurationManager.AppSettings["userName"];
        string password = ConfigurationManager.AppSettings["password"];
        string productId = "";
        string mmarket = "";
        string guid = ConfigurationManager.AppSettings["guid"];

        
        public LoginTestClass(LoginRequestModel loginModel)
        {
            this.LoginModel = loginModel;
        }

        public object ConfigData { get; private set; }
        public string userToken { get; private set; }
        public LoginRequestModel LoginModel { get => loginModel; set => loginModel = value; }

        [Test]
        public void LoginTestAPI()
        {
            string posturl = hostName + "/v1/accounts/login/real";
            try
            {
                Dictionary<string, string> headers = new Dictionary<string, string>()
                {
                {"X-CorrelationId","{{$guid}}" +guid },
                {"X-Forwarded-For","{{$guid}}" +guid},
                {"X-Clienttypeid", "5" },
                {"Content-Type","application/json" }
                };
                PostHelper<LoginResponseModel> restClientHelper1 = new PostHelper<LoginResponseModel>();
                RestSharp.IRestResponse<Tokens> restResponse = (IRestResponse<Tokens>)(IRestResponse)restClientHelper1.PostRequest
                    (posturl, headers, Method.POST, DataFormat.Json);
                
                string userToken = restResponse.Data.userToken;
                int moduleID = random.Next(100);
                int productID = random.Next(100);
                int clientID = random.Next(100);
                int serverID = random.Next(100);
                string refreshpostEndPoint = hostName + "/v1/games/module/{{$moduleID}}/client/{{$clientID}}/play" + moduleID + clientID;
                Dictionary<string, string> headers1 = new Dictionary<string, string>()
                {
                    { "Authorization:", "Bearer" + userToken },
                    { "X-CorrelationId", "93D10259-30F8-4339-B456-3F30A43F65A2" },
                    { "X-Route-ProductId", "" + productID },
                    { "X-Route-ModuleId", "" + moduleID },
                    { "Content-Type", "application/json" }
                };
                
                IRestResponse<Balance> restResponse1 = (IRestResponse<Balance>)(IRestResponse)restClientHelper1.PostRequest
                    (posturl, headers, Method.POST, DataFormat.Json);
                IRestResponse<Financials> restResponse2 = (IRestResponse<Financials>)(IRestResponse)restClientHelper1.PostRequest
                    (posturl, headers, Method.POST, DataFormat.Json);

                Assert.AreEqual(200, (int)restResponse.StatusCode);

                bool totalInAccount = restClientHelper1.IsNumericalvalue(restResponse1.Data.totalInAccountCurrency);
                Assert.IsTrue(totalInAccount, "User Account Balance is not Numeric");
                bool payoutAmount = restClientHelper1.IsNumericalvalue(restResponse2.Data.payoutAmount);
                Assert.IsTrue(payoutAmount, "User Pay Out IsNumericalvalue is not NUmeric");
                bool betAmount = restClientHelper1.IsNumericalvalue(restResponse2.Data.betAmount);
                Assert.IsTrue(betAmount, "User Bet Amount is not Numeric");
            }
            catch {
            }
        }

        private LoginRequestModel GetJsonObjects()
            {
                LoginRequestModel requestModel = new LoginRequestModel();
                requestModel.userName = userName;
                requestModel.password = password;
                requestModel.sessionProductId = productId;
                requestModel.numLaunchTokens = random.Next(1000);
                requestModel.marketType = mmarket;

                Environment environment = new Environment();
                environment.clientTypeId = random.Next(1000);
                environment.languageCode = "en";
                Tokens tokens = new Tokens();
                tokens.userToken = userToken;
                return requestModel;
            }
        private Packet GetPacketObject()
        {
            Packet packet = new Packet();
            packet.packetType = random.Next(1000);
            packet.payload = "";
            packet.useFilter = true;
            packet.isBase64Encoded = false;
            Balances balances = new Balances();
            int loyaltyBalance = 0;
            balances.loyaltyBalance = loyaltyBalance;
            double totalInAccountCurrency = 0;
            balances.totalInAccountCurrency = totalInAccountCurrency;
            Financials financials = new Financials();
            double payoutAmount = 0;
            financials.payoutAmount = payoutAmount;
            return packet;
        }

    }
}
