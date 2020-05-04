using MusZil_Core.API;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusZil_Core.Accounts;
using MusZil_Core.Contracts;
using MusZil_Core.Blockchain;
using Newtonsoft.Json.Linq;
using MusZil_Core.Transactions;
using Newtonsoft.Json;

namespace MusZil_Core
{
	public class Zilliqa
    {
        private IZilliqaAPIClient<MusResult> _client;
		public static readonly string TESTNET = "https://dev-api.zilliqa.com/";
		public static readonly string MAINNET = "https://api.zilliqa.com/";
		public Zilliqa(bool test = true) {
			_client = test ?  new MusZil_APIClient(TESTNET): new MusZil_APIClient(MAINNET);
		}
        public Zilliqa(string APIURL)
        {
            _client = new MusZil_APIClient(APIURL);
        }
        
        private async Task<decimal> GetBalanceForAddressAsync(string address)
        {
            var acc = new Account(address);
			var res = await _client.GetBalance(address);
			return decimal.Parse((string)res.Result);
		}

		#region Accounts
		/// <summary>
		/// Gets balance from account
		/// </summary>
		/// <param name="address"></param>
		/// <returns></returns>
		public async Task<Balance> GetBalance(string address)
		{
			var res = await _client.GetBalance(address);
			return ((JObject)res.Result).ToObject<Balance>();
		}
		public async Task<Balance> GetBalance(Address address)
		{
			address.SwitchEncoding();
			return await GetBalance(address.Raw);
		}
		public async Task<Balance> GetBalance(Account acc)
		{
			acc.Address.SwitchEncoding();
			return await GetBalance(acc.Address.Raw);
		}
		#endregion

		#region BlockChain
		public async Task<int> GetNetworkId()
		{
			var res = await _client.GetNetworkId();
			return int.Parse((string)res.Result);
		}

		public async Task<BlockchainInfo> GetBlockchainInfo()
		{
			var resp = await _client.GetBlockchainInfo();
			return ((JToken)resp.Result).ToObject<BlockchainInfo>();
		}

		public async Task<DSBlock> GetDsBlock(string blockNumber)
		{
			var resp = await _client.GetDsBlock(blockNumber);
			return ((JToken)resp.Result).ToObject<DSBlock>();
		}
		public async Task<DSBlock> GetLatestDsBlock()
		{
			var resp = await _client.GetLatestDsBlock();
			return ((JToken)resp.Result).ToObject<DSBlock>();
		}
		public async Task<int> GetNumDSBlocks()
		{
			var res = await _client.GetNumDSBlocks();
			return int.Parse((string)res.Result);
		}
		public async Task<double> GetDSBlockRate()
		{
			var resp = await _client.GetDSBlockRate();
			return(double)resp.Result;
		}
		public async Task<BlockListing> GetDSBlockListing(int pageNumber = 1)
		{
			var resp = await _client.GetDSBlockListing(pageNumber);
			return ((JToken)resp.Result).ToObject<BlockListing>();
		}
		public async Task<TxBlock> GetTxBlock(string blockNumber)
		{
			var resp = await _client.GetLatestDsBlock();
			return ((JToken)resp.Result).ToObject<TxBlock>();
		}
		public async Task<TxBlock> GetLatestTxBlock()
		{
			var resp = await _client.GetLatestDsBlock();
			return ((JToken)resp.Result).ToObject<TxBlock>();
		}
		public async Task<int> GetNumTxBlocks()
		{
			var res = await _client.GetNumTxBlocks();
			return int.Parse((string)res.Result);
		}
		public async Task<double> GetTxBlockRate()
		{
			var resp = await _client.GetTxBlockRate();
			return (double)resp.Result;
		}
		public async Task<BlockListing> GetTxBlockListing(int pageNumber)
		{
			var resp = await _client.GetTxBlockListing(pageNumber);
			return ((JToken)resp.Result).ToObject<BlockListing>();
		}

		public async Task<int> GetNumTransactions()
		{
			var res = await _client.GetNumTransactions();
			return int.Parse((string)res.Result);
		}

		public async Task<double> GetTransactionRate()
		{
			var resp = await _client.GetTxBlockRate();
			return (double)resp.Result;
		}

		public async Task<int> GetCurrentMiniEpoch()
		{
			var resp = await _client.GetCurrentMiniEpoch();
			return int.Parse((string)resp.Result);
		}

		public async Task<int> GetCurrentDSEpoch()
		{
			var resp = await _client.GetCurrentDSEpoch();
			return int.Parse((string)resp.Result);
		}

		public async Task<Int64> GetPrevDifficulty()
		{
			var resp = await _client.GetPrevDifficulty();
			return (Int64)resp.Result;
		}

		public async Task<int> GetPrevDSDifficulty()
		{
			var resp = await _client.GetCurrentMiniEpoch();
			return int.Parse((string)resp.Result);
		}

		public async Task<decimal> GetTotalCoinSupply()
		{
			var resp = await _client.GetTotalCoinSupply();
			return decimal.Parse((string)resp.Result);
		}
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
		/// Gets all contracts for one address
		/// </summary>
		/// <param name="address"></param>
		/// <returns></returns>
		public async Task<List<SmartContract>> GetSmartContracts(string address)
		{
			var res = await _client.GetSmartContracts(address);
			if (res.Error)
			{
				throw new ArgumentException(res.Message);
			}
			var l = new List<SmartContract>();
			if (res.Result != null)
			{
				foreach (var r in (JArray)res.Result)
				{
					l.Add(r.ToObject<SmartContract>());
				}
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
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task<Address> GetContractAddressFromTransactionID(string id)
		{
			var res = await _client.GetContractAddressFromTransactionID(id);
			if (res.Error)
			{
				throw new Exception(res.Message);
			}
			var address = new Address(res.Result.ToString());
			return address;
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
		public async Task<Transaction.Info> CreateTransaction(TransactionPayload payload)
		{
			
			var res = await _client.CreateTransaction(payload);
			if (res.Error)
			 throw new Exception(res.Message); 

			return ((JToken)res.Result).ToObject<Transaction.Info>();
		}
		public async Task<Transaction.Info> CreateTransaction(string payload)
		{
			var res = await _client.CreateTransaction(payload);
			return ((JToken)res.Result).ToObject<Transaction.Info>();
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
			var list = new List<string[]>();
			if (!res.Error)
			{ 
				list = ((JToken)res.Result).ToObject<List<string[]>>();
			}
			else if (res.Message != "TxBlock has no transactions")
			{
				throw new Exception(res.Message);
			}

			return list;
		}

		public async Task<int> GetNumTxnsTxEpoch()
		{
			var res = await _client.GetNumTxnsTxEpoch();
			return int.Parse((string)res.Result);
		}

		public async Task<int> GetNumTxnsDSEpoch()
		{
			var res = await _client.GetNumTxnsDSEpoch();
			return int.Parse((string)res.Result);
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
			var list = new List<Transaction>();
			if (!res.Error)
			{
				list = ((JToken)res.Result).ToObject<List<Transaction>>();
			}
			else if (res.Message != "TxBlock has no transactions")
			{
				throw new Exception(res.Message);
			}
			return list;
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
