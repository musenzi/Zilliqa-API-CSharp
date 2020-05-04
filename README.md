Zilliqa SDK C# .NETCore 3.1

Mus_ZilliqaCSharp v1.1.0

This is a class library that communicates with the Zilliqa JsonRPC 2.0 API with C# using simple HttpWebClient.

The project is made in .Net core 3.1

- All API calls have been implemented
- Added wallet to sign transactions and store private & public keys
- Export key to more secure json format. (Needs more testing!)
- Create transaction with signed transaction from wallet works (Only basic transactions tested)

TODO 
- Test createtransaction with contracts 
- General improvements and debugging

Dependecies: 
- BouncyCastle.NetCore (>= 1.8.6)
- Google.Protobuf (>= 3.11.4)
- Google.Protobuf.Tools (>= 3.11.4)
- Newtonsoft.Json (>= 12.0.3)
- Scrypt.NET (>= 1.3.0)

DEV_URL = https://dev-api.zilliqa.com/

LIVE_URL= https://api.zilliqa.com/

To use this project clone it directly to Visual Studio or use nuget package manage and search for "Mus_ZilliqaCSharp".

Direct download from nuget.org: https://www.nuget.org/packages/Mus_ZilliqaCSharp/

