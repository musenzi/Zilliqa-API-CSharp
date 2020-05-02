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
			var resp = await _client.GetNetworkId();
			return (int)resp.Result;
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
			var resp = await _client.GetNumDSBlocks();
			return (int)resp.Result;
		}
		public async Task<decimal> GetDSBlockRate()
		{
			var resp = await _client.GetDSBlockRate();
			return (decimal)resp.Result;
		}
		public async Task<List<BlockInfo>> GetDSBlockListing(int pageNumber = 0)
		{
			var resp = await _client.GetDSBlockListing(pageNumber);
			return ((JToken)resp.Result).ToObject<List<BlockInfo>>();
		}
		public async Task<List<BlockInfo>> GetDSBlockListing()
		{
			var resp = await _client.GetDSBlockListing();
			return ((JToken)resp.Result).ToObject<List<BlockInfo>>();
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
			var resp = await _client.GetNumTxBlocks();
			return (int)resp.Result;
		}
		public async Task<decimal> GetTxBlockRate()
		{
			var resp = await _client.GetTxBlockRate();
			return (decimal)resp.Result;
		}
		public async Task<List<BlockInfo>> GetTxBlockListing(int pageNumber)
		{
			var resp = await _client.GetTxBlockListing(pageNumber);
			return ((JToken)resp.Result).ToObject<List<BlockInfo>>();
		}

		public async Task<int> GetNumTransactions()
		{
			var resp = await _client.GetNumTransactions();
			return (int)resp.Result;
		}

		public async Task<decimal> GetTransactionRate()
		{
			var resp = await _client.GetTxBlockRate();
			return (decimal)resp.Result;
		}

		public async Task<int> GetCurrentMiniEpoch()
		{
			var resp = await _client.GetCurrentMiniEpoch();
			return (int)resp.Result;
		}

		public async Task<int> GetCurrentDSEpoch()
		{
			var resp = await _client.GetCurrentDSEpoch();
			return (int)resp.Result;
		}

		public async Task<int> GetPrevDifficulty()
		{
			var resp = await _client.GetPrevDifficulty();
			return (int)resp.Result;
		}

		public async Task<int> GetPrevDSDifficulty()
		{
			var resp = await _client.GetCurrentMiniEpoch();
			return (int)resp.Result;
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
