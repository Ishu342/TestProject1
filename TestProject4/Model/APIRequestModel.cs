using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject4.Model
{
    public class APIRequestModel
    {
        public Packet packet { get; set; }

    }
    public class Packet    {
        public int packetType { get; set; }
        public string payload { get; set; }
        public bool useFilter { get; set; }
        public bool isBase64Encoded { get; set; }
    }
    
}
