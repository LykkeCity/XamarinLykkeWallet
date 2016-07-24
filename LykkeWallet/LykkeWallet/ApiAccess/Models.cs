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
        public decimal Accuracy { get; set; }
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

    public class BaseAssetRespModel
    {
        public AssetRespModel Asset { set; get; }
    }

    public class AllBaseAssetsRespModel
    {
        public List<AssetRespModel> Assets { set; get; }
    }

    public class AssetRespModel
    {
        public string Id { set; get; }
        public string Name { set; get; }
        public double Accuracy { set; get; }
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
}
