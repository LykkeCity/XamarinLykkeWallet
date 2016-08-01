using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LykkeWallet.ApiAccess
{
    public class AccountExistRespModel
    {
        public bool IsEmailRegistered { get; set; }
    }


    public class RegsiterAccountRespModel
    {
        public string Token { get; set; }
    }

    public class AuthRespModel : KycRegistrationStatusModel
    {
        public string Token { get; set; }
        public string EncodedPrivateKey { set; get; }
    }


    public class PersonalDataRespModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
    }

    public class DocumentsToUploadRespoModel
    {
        public bool IdCard { get; set; }
        public bool ProofOfAddress { get; set; }
        public bool Selfie { get; set; }
    }

    public class CheckPinSecurityRespModel
    {
        public bool Passed { get; set; }
    }
    public class CheckEmailVerificationRespModel
    {
        public bool Passed { get; set; }
    }
    public class CheckMobilePhoneRespModel
    {
        public bool Passed { get; set; }
    }

    public class WalletRespModel
    {
        public WalletLykkeRespModel Lykke { set; get; }
        public List<WalletBankCardRespModel> BankCards { set; get; }
        public string MultiSig { set; get; }
        public string ColoredMultiSig { set; get; }
    }

    public class WalletLykkeAssetRespModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string Symbol { get; set; }
        public string AssetPairId { get; set; }
        public bool HideIfZero { get; set; }
        public string IssuerId { get; set; }
        public int Accuracy { get; set; }
    }


    public class WalletLykkeRespModel
    {
        public List<WalletLykkeAssetRespModel> Assets { get; set; }
        public double Equity { get; set; }
    }
    public class WalletBankCardRespModel
    {
        public string Id { set; get; }
        public string Type { set; get; }
        public string LastDigits { set; get; }
        public int MonthTo { set; get; }
        public int YearTo { set; get; }
    }




    public class AssetPairRatesRespModel
    {
        public List<AssetPairRateModel> Rates { set; get; }
    }

    public class AssetPairRateRespModel
    {
        public AssetPairRateModel Rate { set; get; }
    }

    public class AssetPairRateModel
    {
        public double PChng { set; get; }
        public double[] ChngGrph { set; get; }
        public bool Inverted { set; get; }
        public string Id { set; get; }
        public string Bid { set; get; }
        public string Ask { set; get; }
    }

    public class AssetsRespModel
    {
        public List<AssetModel> Assets { set; get; }
    }

    public class AssetRespModel
    {
        public AssetModel Asset { set; get; }
    }

    public class BaseAssetRespModel
    {
        public AssetModel Asset { set; get; }
    }

    public class AllBaseAssetsRespModel
    {
        public List<AssetModel> Assets { set; get; }
    }

    public class AssetModel
    {
        public string Id { set; get; }
        public string Name { set; get; }
        public int Accuracy { set; get; }
        public string Symbol { set; get; }
        public bool HideWithdraw { set; get; }
        public bool HideDeposit { set; get; }
    }

    public class AssetDescriptionRespModel
    {
        public string Id { set; get; }
        public string AssetClass { set; get; }
        public int PopIndex { set; get; }
        public string Description { set; get; }
        public string Issuer { set; get; }
        public string NumberOfCoins { set; get; }
        public string MarketCapitalization { set; get; }
        public string AssetDescriptionUrl { set; get; }
    }

    public class AssetPairDetailedRateRespModel
    {
        [JsonProperty(ItemConverterType = typeof(JsonCustomDateTimeConverter))]
        public DateTime FixingTime { set; get; }

        public decimal LastPrice { set; get; }
        public AssetPairDetailRateItemRespModel Rate { set; get; }
        public bool Inverted { set; get; }

        [JsonProperty(ItemConverterType = typeof(JsonCustomDateTimeConverter))]
        public DateTime StartTime { set; get; }

        [JsonProperty(ItemConverterType = typeof(JsonCustomDateTimeConverter))]
        public DateTime EndTime { set; get; }
    }

    public class AssetPairDetailRateItemRespModel
    {
        public decimal PChange { set; get; }
        public List<double> ChngGrph { set; get; }
    }

    public class AssetPairRespModel
    {
        public AssetPairItemRespModel AssetPair { set; get; }
    }

    public class AssetPairItemRespModel
    {
        public string Group { set; get; }
        public string Id { set; get; }
        public string Name { set; get; }
        public int Accuracy { set; get; }
        public int InvertedAccuracy { set; get; }
        public string BaseAssetId { set; get; }
        public string QuotingAssetId { set; get; }
        public bool Inverted { set; get; }
    }

    public class GraphPeriodsRespModel
    {
        public List<GraphPeriodsAvailablePeriodsItem> AvailablePeriods { set; get; }
    }

    public class GraphPeriodsAvailablePeriodsItem
    {
        public string Name { set; get; }
        public string Value { set; get; }
    }

    public class TransactionsRespModel
    {
        public TransactionsCashInOutModel CashInOut { set; get; }
        public List<TransactionTradeModel> Trades { set; get; }
        public List<TrasactionTransferModel> Transfers { set; get; }
        public List<TransactionCashOutAttemptsModel> CashOutAttempts { set; get; }
        public List<TransactionCashOutCancelledModel> CashOutCancelled { set; get; }
        public List<TransactionCashOutDoneModel> CashOutDone { set; get; }
    }

    public class TransactionsCashInOutModel
    {
        public string Id { set; get; }
        public decimal Amount { set; get; }
        [JsonProperty(ItemConverterType = typeof(JsonCustomDateTimeConverter))]
        public DateTime DateTime { set; get; }
        public string Asset { set; get; }
        public string IconId { set; get; }
        public string BlockChainHash { set; get; }
        public bool IsRefund { set; get; }
        public string AddressFrom { set; get; }
        public string AddressTo { set; get; }
    }

    public class TransactionTradeModel
    {
        public string Id { set; get; }
        [JsonProperty(ItemConverterType = typeof(JsonCustomDateTimeConverter))]
        public DateTime DateTime { set; get; }
        public string Asset { set; get; }
        public decimal Volume { set; get; }
        public string IconId { set; get; }
        public string BlockChainHash { set; get; }
        public string AddressFrom { set; get; }
        public string AddressTo { set; get; }
        public TransactionMarketOrderModel MarketOrder { set; get; }

    }

    public class TransactionMarketOrderModel
    {
        public string Id { set; get; }
        [JsonProperty(ItemConverterType = typeof(JsonCustomDateTimeConverter))]
        public DateTime DateTime { set; get; }
        public string OrderType { set; get; }
        public decimal Volume { set; get; }
        public decimal Price { set; get; }
        public string BaseAsset { set; get; }
        public string AssetPair { set; get; }
        public decimal TotalCost { set; get; }
        public decimal Comission { set; get; }
        public decimal Position { set; get; }
        public int Accuracy { set; get; }
    }

    public class TrasactionTransferModel
    {
        public string Id { set; get; }
        [JsonProperty(ItemConverterType = typeof(JsonCustomDateTimeConverter))]
        public DateTime DateTime { set; get; }
        public string Asset { set; get; }
        public decimal Volume { set; get; }
        public string IconId { set; get; }
        public string BlockChainHash { set; get; }
        public string AddressFrom { set; get; }
        public string AddressTo { set; get; }
    }

    public class TransactionCashOutAttemptsModel
    {
        public string Id { set; get; }
        [JsonProperty(ItemConverterType = typeof(JsonCustomDateTimeConverter))]
        public DateTime DateTime { set; get; }
        public string Asset { set; get; }
        public decimal Volume { set; get; }
        public string IconId { set; get; }
    }

    public class TransactionCashOutCancelledModel
    {
        public string Id { set; get; }
        [JsonProperty(ItemConverterType = typeof(JsonCustomDateTimeConverter))]
        public DateTime DateTime { set; get; }
        public string Asset { set; get; }
        public decimal Volume { set; get; }
        public string IconId { set; get; }
    }

    public class TransactionCashOutDoneModel
    {
        public string Id { set; get; }
        [JsonProperty(ItemConverterType = typeof(JsonCustomDateTimeConverter))]
        public DateTime DateTime { set; get; }
        public string Asset { set; get; }
        public decimal Volume { set; get; }
        public string IconId { set; get; }
    }

    public class HistoryItemModel
    {
        public string Id { set; get; }
        [JsonProperty(ItemConverterType = typeof(JsonCustomDateTimeConverter))]
        public DateTime DateTime { set; get; }
        public TransactionsCashInOutModel CashInOut { set; get; }
        public TransactionTradeModel Trade { set; get; }
        public TrasactionTransferModel Transfer { set; get; }
        public TransactionCashOutAttemptsModel CashOutAttemp { set; get; }
        public TransactionCashOutCancelledModel CashOutCancelled { set; get; }
        public TransactionCashOutDoneModel CashOutDone { set; get; }
    }

    public class HistoryRespModel
    {
        
    }

    public class BcnTransactionRespModel
    {
        public BcnTransactionModel Transaction { get; set; }
    }

    public class BcnTransactionModel
    {
        public string Hash { set; get; }
        [JsonProperty(ItemConverterType = typeof(JsonCustomDateTimeConverter))]
        public DateTime Date { set; get; }
        public int Confirmations { set; get; }
        public string Block { set; get; }
        public string Height { set; get; }
        public string SenderId { set; get; }
        public string AssetId { set; get; }
        public string Quantity { set; get; }
        public string Url { set; get; }
    }

}
