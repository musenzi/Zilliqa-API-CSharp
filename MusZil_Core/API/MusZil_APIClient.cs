using MusZil_Core.Accounts;
using MusZil_Core.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MusZil_Core.API
{
    public class MusZil_APIClient
    {
        //put this in config file of app
        const string JSONRPC = "2.0";
        const string DEV_URL = "https://dev-api.zilliqa.com/";

        private readonly object requestLock = new object();

        public string Url { get; private set; }
        public MusZil_APIClient(string url = DEV_URL)
        {
            Url = url;
        }
        #region Accounts

        public async Task<MusResult> GetBalance(string address)
        {
            var req = new MusRequest("GetBalance", address);
            var result = await CallMethod(req);
            return ResponseHandler.GetBalanceFromResult(ref result);
        }
        public async Task<MusResult> GetBalance(Address address)
        {
            return await GetBalance(address.Raw);
        }
        public async Task<MusResult> GetBalance(Account acc)
        {
            return await GetBalance(acc.Address.Raw);
        }

        #endregion

		#region BlockChain
		public async Task<MusResult> GetNetworkId()
		{
			var req = RequestFactory.New("GetNetworkId", "");
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}

		public async Task<MusResult> GetBlockchainInfo()
		{
			var req = RequestFactory.New("GetBlockchainInfo", "");
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}

		public async Task<MusResult> GetShardingStructure()
		{
			var req = RequestFactory.New("GetShardingStructure", "");
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}


		public async Task<MusResult> GetDSBlockListing(int pageNumber = 0)
		{
			var req = RequestFactory.New("DSBlockListing", pageNumber.ToString());
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}


		public async Task<MusResult> GetTxBlockListing(int pageNumber)
		{
			var req = RequestFactory.New("TxBlockListing", pageNumber.ToString());
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}


		public async Task<MusResult> GetNumDSBlocks()
		{
			var req = RequestFactory.New("GetNumDSBlocks", "");
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}

		public async Task<MusResult> GetDSBlockRate()
		{
			var req = RequestFactory.New("GetDSBlockRate", "");
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}

		public async Task<MusResult> GetDSBlockListing()
		{
			var req = RequestFactory.New("DSBlockListing","");
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}

		public async Task<MusResult> GetDsBlock(string blockNumber)
		{
			var req = RequestFactory.New("GetDsBlock", blockNumber);
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}

		public async Task<MusResult> GetTxBlock(string blockNumber)
		{
			var req = RequestFactory.New("GetTxBlock", blockNumber);
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}

		public async Task<MusResult> GetNumTxBlocks()
		{
			var req = RequestFactory.New("GetNumTxBlocks", "");
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}

		public async Task<MusResult> GetTxBlockRate()
		{
			var req = RequestFactory.New("GetTxBlockRate", "");
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}


		public async Task<MusResult> GetLatestDsBlock()
		{
			var req = RequestFactory.New("GetLatestDsBlock", "");
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}

		public async Task<MusResult> GetNumTransactions()
		{
			var req = RequestFactory.New("GetNumTransactions", "");
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}

		public async Task<MusResult> GetTransactionRate()
		{
			var req = RequestFactory.New("GetTransactionRate", "");
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}

		public async Task<MusResult> GetCurrentMiniEpoch()
		{
			var req = RequestFactory.New("GetCurrentMiniEpoch", "");
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}

		public async  Task<MusResult> GetCurrentDSEpoch()
		{
			var req = RequestFactory.New("GetCurrentDSEpoch", "");
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}

		public async Task<MusResult> GetPrevDifficulty()
		{
			var req = RequestFactory.New("GetPrevDifficulty", "");
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}

		public async Task<MusResult> GetPrevDSDifficulty()
		{
			var req = RequestFactory.New("GetPrevDSDifficulty", "");
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}

		public async Task<MusResult> GetLatestTxBlock()
		{
			var req = RequestFactory.New("GetLatestTxBlock", "");
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}

		#endregion

		#region Contracts

		/// <summary>
		/// Gets contractCode, overloaded with Address,Contract
		/// </summary>
		/// <param name="address"></param>
		/// <returns></returns>
		public async Task<MusResult> GetContractCode(string address)
		{
			var req = new MusRequest("GetSmartContractCode", address.TrimStart('0').TrimStart('x'));
			var result = await CallMethod(req);
			return ResponseHandler.GetContractCode(ref result);
		}
		public async Task<MusResult> GetContractCode(Address address)
		{
			return await GetContractCode(address.Raw);
		}
		public async Task<MusResult> GetContractCode(Contract c)
		{
			return await GetContractCode(c.Address.Raw);
		}
		/// <summary>
		/// Gets Contract Balance, overloaded with: Address,Contract
		/// </summary>
		/// <param name="address"></param>
		/// <returns></returns>
		public async Task<MusResult> GetContractBalance(string address)
		{
			var req = new MusRequest("GetSmartContractState", address.TrimStart('0').TrimStart('x'));
			var result = await CallMethod(req);
			return ResponseHandler.GetContractBalance(ref result);
		}
		public async Task<MusResult> GetContractBalance(Address address)
		{
			return await GetContractBalance(address.Raw);
		}
		public async Task<MusResult> GetContractBalance(Contract con)
		{
			return await GetContractBalance(con.Address.Raw);
		}

		/// <summary>
		/// Gets all contracts for one account
		/// </summary>
		/// <param name="account"></param>
		/// <returns></returns>
		public async Task<MusResult> GetContracts(Account account)
		{
			return await GetSmartContracts(account.Address.Base16.Replace("0x",""));
		}
		public async Task<MusResult> GetSmartContractCode(String address)
		{
			var req = RequestFactory.New("GetSmartContractCode", address);
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}

		public async Task<MusResult> GetSmartContracts(string address)
		{
			var req = RequestFactory.New("GetSmartContracts", address);
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}

		public async Task<MusResult> GetContractAddressFromTransactionID(string address)
		{
			var req = RequestFactory.New("GetContractAddressFromTransactionID", address);
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}


		public async Task<MusResult> GetSmartContractState(String address)
		{
			var req = RequestFactory.New("GetSmartContractState", address);
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);

		}

		public async Task<MusResult> GetSmartContractInit(string address)
		{
			var req = RequestFactory.New("GetSmartContractInit", address);
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}
		#endregion

		#region Transactions
		public async Task<MusResult> CreateTransaction(string payload)
		{
			var req = RequestFactory.New("CreateTransaction", payload);
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}

		public async Task<MusResult> GetMinimumGasPrice()
		{
			var req = RequestFactory.New("GetMinimumGasPrice", "");
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}


		public async Task<MusResult> GetTransaction(string hash)
		{
			var req = RequestFactory.New("GetTransaction", hash);
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}

		public async Task<MusResult> GetRecentTransactions()
		{
			var req = RequestFactory.New("GetRecentTransactions", "");
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}

		public async Task<MusResult> GetTransactionsForTxBlock(string blockNum)
		{
			var req = RequestFactory.New("GetTransactionsForTxBlock", blockNum);
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}

		public async Task<MusResult> GetNumTxnsTxEpoch()
		{
			var req = RequestFactory.New("GetNumTxnsTxEpoch");
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}

		public async Task<MusResult> GetNumTxnsDSEpoch()
		{
			var req = RequestFactory.New("GetNumTxnsDSEpoch");
			var result = await CallMethod(req);
			return ResponseHandler.GetResult(ref result);
		}

		#endregion

		#region Helpers

		private HttpClient GetClient()
        {
            HttpClient httpClient = null;
            lock (requestLock)
            {
                httpClient = new HttpClient();

                //specify to use TLS 1.2 as default connection
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
            

            return httpClient;
        }

        /// <summary>
        /// Calls a API method of the Zilliqa API
        /// </summary>
        /// <param name="req">MusRequest object to pass request</param>
        /// <returns></returns>
        private async Task<APIResponse> CallMethod(MusRequest req)
        {
            string result = "";
            var json = req.ToJson();
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = GetClient())
            {
                var response = await httpClient.PostAsync(Url, data);
                result = response.Content.ReadAsStringAsync().Result;
            }
			var musres = JsonConvert.DeserializeObject<APIResponse>(result);

			return musres;
        }
        #endregion
    }
}
