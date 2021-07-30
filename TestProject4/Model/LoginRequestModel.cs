using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject4.Model
{
    public class LoginRequestModel
    {
        public Environment environment { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string sessionProductId { get; set; }
        public int numLaunchTokens { get; set; }
        public string marketType { get; set; }
    }

    public class Environment
    {
        public int clientTypeId { get; set; }
        public string languageCode { get; set; }
    }
}
