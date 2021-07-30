using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject4.Model
{
   public class APIResponseModel
{

        public Context context { get; set; }
        public Packet packet { get; set; }
        public object Datas { get; internal set; }
    }
    public class Financials
    {
        public double betAmount { get; set; }
        public double payoutAmount { get; set; }
    }

    public class Balances
    {
        public int loyaltyBalance { get; set; }
        public double totalInAccountCurrency { get; set; }
    }

    public class Context
    {
        public Financials financials { get; set; }
        public Balances balances { get; set; }
    }

    public class Packets
    {
        public string payload { get; set; }
        public int packetType { get; set; }
        public bool isBase64Encoded { get; set; }
    }
}
