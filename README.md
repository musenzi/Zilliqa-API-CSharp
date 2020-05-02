Zilliqa SDK C# 

Mus_ZilliqaCSharp v1.0.0

This is a class library that communicates with the Zilliqa JsonRPC 2.0 API with C# using simple HttpWebClient.

The project is made in .Net core 3.1

DEV_URL = https://dev-api.zilliqa.com/

LIVE_URL= https://api.zilliqa.com/

Dependencies: NewtonJsoft (https://www.newtonsoft.com/)

- All API calls have been implemented. 
- At the moment Zilliqa object uses MusZil_APIClient directly I'll change that to IZilliqaAPIClient for DI 
- Create tranasction should be finetuned to be able to sign directly in class lib
- Started working on genrating addresses and encrypting decrypting
- Stared working on import export of private keys 

To use this project clone it directly to Visual Studio or use nuget package manage and search for "Mus_ZilliqaCSharp".

Direct download from nuget.org: https://www.nuget.org/packages/Mus_ZilliqaCSharp/

