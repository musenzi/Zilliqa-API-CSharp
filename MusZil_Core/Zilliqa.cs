using MusZil_Core.API;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MusZil_Core.Accounts;
using MusZil_Core.Contracts;
using MusZil_Core.Blockchain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using MusZil_Core.Transactions;

namespace MusZil_Core
{
    public class Zilliqa
    {
        private MusZil_APIClient _client;
		const string DEV_URL = "https://dev-api.zilliqa.com/";
		public Zilliqa() : this(DEV_URL) {}
        public Zilliqa(string APIURL)
        {
            _client = new MusZil_APIClient(APIURL);
        }
        
        private async Task<decimal> GetBalanceForAddressAsync(string address)
        {
            var acc = new Account(address);
			var res = await _client.GetBalance(address);
			return  (decimal)res.Result;
        }

		#region Accounts

		public async Task<Balance> GetBalance(string address)
		{
			var res = await _client.GetBalance(address);
			var bal = decimal.Parse((string)res.Result);
			return new Balance(bal);
		}
		public async Task<Balance> GetBalance(Address address)
		{
			return await GetBalance(address.Raw);
		}
		public async Task<Balance> GetBalance(Account acc)
		{
			return await GetBalance(acc.Address.Raw);
		}

		#endregion

		#region BlockChain
		public async Task<int> GetNetworkId()
		{
			var req = await _client.GetNetworkId();
			return (int)req.Result;
		}

		public async Task<BlockchainInfo> GetBlockchainInfo()
		{
			var res = await _client.GetBlockchainInfo();
			var musres = JsonConvert.DeserializeObject<BlockchainInfo>(res.Result.ToString());
			return musres;
		}

		public async Task<ShardingStructure> GetShardingStructure()
		{
			var res = await _client.GetShardingStructure();
			var musres = JsonConvert.DeserializeObject<ShardingStructure>(res.Result.ToString());
			return musres;
		}
		/*

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
			var req = RequestFactory.New("DSBlockListing", "");
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

		public async Task<MusResult> GetCurrentDSEpoch()
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
		}*/

		#endregion

		#region Contracts

		/// <summary>
		/// Gets contractCode, overloaded with Address,Contract
		/// </summary>
		/// <param name="address"></param>
		/// <returns></returns>
		public async Task<string> GetSmartContractCode(string address)
		{
			var res = await _client.GetSmartContractCode(address);
			return res.Result.ToString();
		}
		public async Task<string> GetSmartContractCode(Address address)
		{
			return await GetSmartContractCode(address.Raw);
		}
		public async Task<string> GetSmartContractCode(SmartContract c)
		{
			return await GetSmartContractCode(c.Address.Raw);
		}
		/// <summary>
		/// Gets Contract Balance, overloaded with: Address,Contract
		/// </summary>
		/// <param name="address"></param>
		/// <returns></returns>
		public async Task<Balance> GetContractBalance(string address)
		{
			var res = await _client.GetContractBalance(address);
			var bal = (decimal)res.Result;
			return new Balance(bal);
		}
		public async Task<Balance> GetContractBalance(Address address)
		{
			return await GetContractBalance(address.Raw);
		}
		public async Task<Balance> GetContractBalance(SmartContract con)
		{
			return await GetContractBalance(con.Address.Raw);
		}

		/// <summary>
		/// Gets all contracts for one account
		/// </summary>
		/// <param name="account"></param>
		/// <returns></returns>
		public async Task<List<SmartContract>> GetSmartContracts(string address)
		{
			var res = await _client.GetSmartContracts(address);
			if (res.Result == null)
			{
				throw new ArgumentException(res.Message);
			}
			var l = new List<SmartContract>();
			foreach (var r in (JArray)res.Result)
			{
				l.Add(r.ToObject<SmartContract>());
			}
			return l;
		}
		public async Task<List<SmartContract>> GetSmartContracts(Address address)
		{
			address.SwitchEncoding();
			return await GetSmartContracts(address.Raw);
		}
		public async Task<List<SmartContract>> GetSmartContracts(Account account)
		{
			account.Address.SwitchEncoding();
			return await GetSmartContracts(account.Address.Raw);
		}
		/// <summary>
		/// Gets the contract address from tnx Id
		/// </summary>
		/// <param name="address"></param>
		/// <returns></returns>
		public async Task<string> GetContractAddressFromTransactionID(string address)
		{
			var res = await _client.GetContractAddressFromTransactionID(address);
			return (string)res.Result;
		}
		public async Task<string> GetContractAddressFromTransactionID(Address address)
		{
			address.SwitchEncoding() ;
			return await GetContractAddressFromTransactionID(address.Raw);
		}


		public async Task<StateItem> GetSmartContractState(string address)
		{
			var res = await _client.GetSmartContractState(address);
			var it = ((JToken)res.Result).ToObject<StateItem>();
			it.AllValues = res.Result;
			return it;
		}
		public async Task<string> GetSmartContractSubState(object[] parameters)
		{
			var res = await _client.GetSmartContractSubState(parameters);
			return (string)res.Result;
		}
		public async Task<string> GetSmartContractInit(string address)
		{
			var res = await _client.GetSmartContractInit(address);
			return res.Result.ToString();
		}
		#endregion

		#region Transactions
		public async Task<Info> CreateTransaction(string payload)
		{
			var res = await _client.CreateTransaction(payload);
			return ((JToken)res.Result).ToObject<Info>();
		}

		public async Task<int> GetMinimumGasPrice()
		{
			var res = await _client.GetMinimumGasPrice();
			return int.Parse((string)res.Result);
		}


		public async Task<Transaction> GetTransaction(string hash)
		{
			var res = await _client.GetTransaction(hash);
			return ((JToken)res.Result).ToObject<Transaction>();
		}

		public async Task<List<string>> GetRecentTransactions()
		{
			var res = await _client.GetRecentTransactions();
			return ((JToken)res.Result).ToObject<List<string>>();
		}

		public async Task<List<string[]>> GetTransactionsForTxBlock(int blockNum)
		{
			var res = await _client.GetTransactionsForTxBlock(blockNum.ToString());
			return ((JToken)res.Result).ToObject<List<string[]>>();
		}

		public async Task<int> GetNumTxnsTxEpoch()
		{
			var res = await _client.GetNumTxnsTxEpoch();
			return ((JToken)res.Result).ToObject<int>();
		}

		public async Task<int> GetNumTxnsDSEpoch()
		{
			var res = await _client.GetNumTxnsDSEpoch();
			return ((JToken)res.Result).ToObject<int>();
		}
		public async Task<PendingTransaction> GetPendingTxn(string hash)
		{
			var res = await _client.GetPendingTxn(hash);
			return ((JToken)res.Result).ToObject<PendingTransactionInfo>();
		}
		public async Task<List<PendingTransaction>> GetPendingTxns()
		{
			var res = await _client.GetPendingTxns();
			return ((JToken)res.Result).ToObject<List<PendingTransaction>>();
		}
		public async Task<List<Transaction>> GetTxnBodiesForTxBlock(int blockNum)
		{
			var res = await _client.GetTxnBodiesForTxBlock(blockNum.ToString());
			return ((JToken)res.Result).ToObject<List<Transaction>>();
		}

		#endregion



		#region Custom Functions not part of API
		public static Account MakeAccount(string address)
		{
			return AccountFactory.New(address);
		}
        #endregion

    }
}
