using System.Threading.Tasks;

namespace MusZil_Core.API
{
    public interface IZilliqaAPIClient<T>
    {
        string Url { get; }

        Task<T> CreateTransaction(object payload);
        Task<T> GetBalance(string address);
        Task<T> GetBlockchainInfo();
        Task<T> GetContractAddressFromTransactionID(string address);
        Task<T> GetContractBalance(string address);
        Task<T> GetContractCode(string address);
        Task<T> GetCurrentDSEpoch();
        Task<T> GetCurrentMiniEpoch();
        Task<T> GetDsBlock(string blockNumber);
        Task<T> GetDSBlockListing(int blockNum);
        Task<T> GetDSBlockRate();
        Task<T> GetLatestDsBlock();
        Task<T> GetLatestTxBlock();
        Task<T> GetMinimumGasPrice();
        Task<T> GetNetworkId();
        Task<T> GetNumDSBlocks();
        Task<T> GetNumTransactions();
        Task<T> GetNumTxBlocks();
        Task<T> GetNumTxnsDSEpoch();
        Task<T> GetNumTxnsTxEpoch();
        Task<T> GetPendingTxn(string hash);
        Task<T> GetPendingTxns();
        Task<T> GetPrevDifficulty();
        Task<T> GetPrevDSDifficulty();
        Task<T> GetRecentTransactions();
        Task<T> GetSmartContractCode(string address);
        Task<T> GetSmartContractInit(string address);
        Task<T> GetSmartContracts(string address);
        Task<T> GetSmartContractState(string address);
        Task<T> GetSmartContractSubState(object[] parameters);
        Task<T> GetTotalCoinSupply();
        Task<T> GetTransaction(string hash);
        Task<T> GetTransactionRate();
        Task<T> GetTransactionsForTxBlock(string blockNum);
        Task<T> GetTxBlock(string blockNumber);
        Task<T> GetTxBlockListing(int pageNumber);
        Task<T> GetTxBlockRate();
        Task<T> GetTxnBodiesForTxBlock(string blockNum);
    }
}