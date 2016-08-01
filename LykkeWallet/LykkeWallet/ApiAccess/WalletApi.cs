using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.Models.Api;
using LykkeWallet.ApiAccess;
using LykkeWallet.LocalKeyStorageAccess;
using NBitcoin;

namespace LykkeWallet.ApiAccess
{
    public partial class WalletApi : IWalletApi
    {
        private readonly ILocalKeyAccess _localKeyAccess;

        public WalletApi(ILocalKeyAccess localKeyAccess)
        {
            _localKeyAccess = localKeyAccess;
            CurrentToken = _localKeyAccess.GetToken();
        }
        /*
        public WalletApi()
        {
            _localKeyStorage = null;
            CurrentToken = null;
        }
        */
        private void SaveToken(string token)
        {
            CurrentToken = token;
            _localKeyAccess.SaveToken(token);
        }


        public Task<AccountExistRespModel> AccountExistAsync(string email)
        {
            return DoGetRequestAsync<AccountExistRespModel>("AccountExist", new { email });
        }

        public async Task RegisterAccount(string email, string fullName, string contactPhone, string password, string hint, string clientInfo)
        {
            var result = await DoPostRequestAsync<RegsiterAccountRespModel>("Registration",
                new
                {
                    Email = email,
                    FullName = fullName,
                    ContactPhone = contactPhone,
                    Password = password,
                    Hint = hint,
                    ClientInfo = clientInfo
                });

            SaveToken(result.Token);
        }

        public Task<KycRegistrationStatusModel> CheckRegistrationStatus()
        {
            return DoGetRequestAsync<KycRegistrationStatusModel>("Registration");
        }

        public async Task<AuthRespModel> AuthAsync(string email, string password)
        {
            var result = await DoPostRequestAsync<AuthRespModel>("Auth", new { Email = email, Password = password });
            SaveToken(result.Token);
            return result;
        }

        public Task PostClientKeys(string pubKey, string encodedPrivateKey)
        {
            return DoPostRequestAsync("ClientKeys", new { pubKey, encodedPrivateKey });
        }

        public Task<PersonalDataRespModel> GetPersonalDataAsync()
        {
            return DoGetRequestAsync<PersonalDataRespModel>("PersonalData");
        }

        public Task<DocumentsToUploadRespoModel> CheckDocumentsToUploadAsync()
        {
            return DoGetRequestAsync<DocumentsToUploadRespoModel>("CheckDocumentsToUpload");
        }

        public Task UploadDocumentAsync(string type, string ext, string data)
        {
            return DoPostRequestAsync<DocumentsToUploadRespoModel>("KycDocuments", new { Type = type, Ext = ext, Data = data });
        }

        public Task StartCheckingDocumentsAsync()
        {
            return DoPostRequestAsync("KycStatus");
        }

        public Task<KycRegistrationStatusModel> GetKycStatusAsync()
        {
            return DoGetRequestAsync<KycRegistrationStatusModel>("KycStatus");
        }

        public Task<CheckEmailVerificationRespModel> GetEmailVerification(string email, string code)
        {

            return DoGetRequestAsync<CheckEmailVerificationRespModel>("EmailVerification", new { email = email, code = code });
        }

        public Task PostEmailVerification(string email)
        {
            return DoPostRequestAsync("EmailVerification", new { Email = email });
        }
        public Task<CheckMobilePhoneRespModel> GetCheckMobilePhone(string phone, string code)
        {
            return DoGetRequestAsync<CheckMobilePhoneRespModel>("CheckMobilePhone", new { phoneNumber = phone, code = code });
        }

        public Task PostCheckMobilePhone(string phone)
        {
            return DoPostRequestAsync("CheckMobilePhone ", new { PhoneNumber = phone });
        }

        public Task PostClientFullName(string fullName)
        {
            return DoPostRequestAsync("ClientFullName", new { FullName = fullName });
        }
        public Task SetPinCodeAsync(string pin)
        {
            return DoPostRequestAsync("PinSecurity", new { Pin = pin });
        }

        public Task<CheckPinSecurityRespModel> CheckPinCodeAsync(string pin)
        {
            var result = DoGetRequestAsync<CheckPinSecurityRespModel>("PinSecurity", new { Pin = pin });
            return result;
        }

        public Task<WalletRespModel> GetWallets()
        {
            var result = DoGetRequestAsync<WalletRespModel>("Wallets");
            return result;
        }

        public Task<AssetPairRatesRespModel> GetAssetPairRates()
        {
            return DoGetRequestAsync<AssetPairRatesRespModel>("AssetPairRates");
        }

        public Task<AssetPairRateRespModel> GetAssetPairRates(string id)
        {
            return DoGetRequestAsync<AssetPairRateRespModel>("AssetPairRates", new { Id = id });
        }

        public Task<AssetRespModel> GetAsset(string id)
        {
            return DoGetRequestAsync<AssetRespModel>("Assets", new { id });
        }

        public Task<BaseAssetRespModel> GetBaseAsset()
        {
            return DoGetRequestAsync<BaseAssetRespModel>("BaseAsset");
        }
        public Task SetBaseAsset(string id)
        {
            return DoPostRequestAsync("BaseAsset", new { Id = id });
        }

        public Task<AllBaseAssetsRespModel> GetAllBaseAssets()
        {
            return DoGetRequestAsync<AllBaseAssetsRespModel>("BaseAssets");
        }

        public Task<AssetDescriptionRespModel> GetAssetDescription(string id)
        {
            return DoGetRequestAsync<AssetDescriptionRespModel>("AssetDescription", new { id });
        }

        public Task<AssetPairDetailedRateRespModel> GetAssetPairDetailedRates(string assetId, string period, int points)
        {
            return DoGetRequestAsync<AssetPairDetailedRateRespModel>("AssetPairDetailedRates", new { period, assetId, points });
        }

        public Task PostInvertAssetPair(string id, bool inverted)
        {
            return DoPostRequestAsync("InvertedAssetPairs", new { AssetPairId = id, Inverted = inverted });
        }

        public Task<AssetPairRespModel> GetAssetPair(string id)
        {
            return DoGetRequestAsync<AssetPairRespModel>("AssetPair", new { id });
        }

        public Task<GraphPeriodsRespModel> GetGraphPeriods()
        {
            return DoGetRequestAsync<GraphPeriodsRespModel>("GraphPeriods");
        }

        public Task PostInvertAssetPair(string id, string inverted)
        {
            return DoPostRequestAsync("InvertedAssetPairs", new { AssetPairId = id, Inverted = inverted });
        }

        public Task<BcnTransactionRespModel> GetBcnTransaction(string id)
        {
            return DoGetRequestAsync<BcnTransactionRespModel>("BcnTransaction", new { id });
        }

        public Task<List<HistoryItemModel>> GetHistory()
        {
            return DoGetRequestAsync<List<HistoryItemModel>>("History");
        }
    }
}
