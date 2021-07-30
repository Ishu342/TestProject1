Feature: Game Balance Feature
	
@smoke
Scenario: As user I want to validate Game Balance
	Given I have user login post endpoint “{{hostName}}/v1/accounts/login/real”
	When I enter following details in request body
    | Username | Password | product   | marketType |
    | userName | password | productID | mmarket    |
    {
  "environment": {
    "clientTypeId": 5,
    "languageCode": "en"
  },
  "userName": "{{userName}}",
  "password": "{{password}}",
  "sessionProductId": {{productID}},
  "numLaunchTokens": 1,
  "marketType": "{{mmarket}}"
}
    And I enter request header
X-CorrelationId: {{$guid}}
X-Forwarded-For: {{$guid}}
X-Clienttypeid:5
Content-Type:application/json
    And I execute post async method

    Then I can see response body
{
    "account": {
        "core": {
            "currencyIsoCode": "USD",
            "isExternalAccount": false,
            "isExternalBalance": false,
            "registeredProductId": 00000,
            "userTypeId": 0,
            "userId": 0,
            "username": "username"
        },
        "balance": {
            "totalInAccountCurrency": 10000.0000,
            "balances": [
                {
                    "typeId": 0,
                    "amountInAccountCurrency": 10000.00
                },
                {
                    "typeId": 3,
                    "amountInAccountCurrency": 0.00
                },
                {
                    "typeId": 9,
                    "amountInAccountCurrency": 0.00
                }
            ],
            "pointBalances": [
                {
                    "typeId": 2,
                    "amount": 0.000000000000
                }
            ],
            "isBonusEnabled": true,
            "isExternalBonusEnabled": false
        }
    },
    "tokens": {
        "userTokenExpiryInSeconds": 600,
        "launchTokens": [
            "XGDhghfghfghghfghfghgfh"
        ],
        "refreshTokens": [
            "HUINDSRGDTHGHHFSSDG"
        ],
        "userToken": "JYDFGDFGFDGRTMPWZDFDFG"
    }
}
And Store “userToken” from above response
When I have refresh game play post endpoint “{{hostName}}/v1/games/module/{{moduleID}}/client/{{clientID}}/play”
And I enter moduleId, clientId and serverId to build request body
1.	{
2.	"packet": {
3.	"packetType": 7,
4.	"payload": "<Pkt version='6'><Id mid='{{MID}}' cid='{{CID}}' sid='{{PID}}' sessionid='' verb='AdvSlot' clientLang='en'/><Request verbex='Refresh'/></Pkt>",
5.	"useFilter": true,
6.	"isBase64Encoded": false
7.	}
8.	}
And I enter Bearer token, moduleId, produdtId in request header
Authorization:Bearer {{usertoken}}
X-Clienttypeid:38
X-correlationid:93D10259-30F8-4339-B456-3F30A43F65A2
X-Route-ProductId:{{productId}}
X-Route-ModuleId:{{moduleId}}
Content-Type:application/json
And Send async post gameplay endpoint
Then I must fetch financialbalance and Player balance from the below response body and it should be numeric
{
    "context": {
        "financials": {
            "betAmount": 0.0,
            "payoutAmount": 0.0
        },
        "balances": {
            "loyaltyBalance": 0,
            "totalInAccountCurrency": 10000.0
        }
    },
    "packet": {
        "payload": "<Pkt><Id sessionid=\"00000000-0000-0000-0000-000000000000\" verb=\"Refresh\" /><Response><Framework state=\"0\" /><Player balance=\"1000000\" autoCashedIn=\"0\" hasPlayedBefore=\"0\"><PlayInfos type=\"TopWins\" id=\"0\" serverTime=\"2021-07-23 13:24:34\" /></Player></Response></Pkt>",
        "packetType": 8,
        "isBase64Encoded": false
    }
}
