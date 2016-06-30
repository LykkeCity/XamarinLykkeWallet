using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykkeWallet.Models.Api;
using LykkeWallet.Services;
using LykkeWallet.ApiAccess;

namespace LykkeWallet.ApiAccess
{
    public partial class WalletApi : IWalletApi
    {
        private readonly ILocalStorage _localStorage;
        public const string TokenName = "Token";

        public WalletApi(ILocalStorage localStorage)
        {
            _localStorage = localStorage;
            CurrentToken = _localStorage.Get(TokenName);
        }

        public WalletApi()
        {
        }

        private void SaveToken(string token)
        {
            CurrentToken = token;
            _localStorage.Save(TokenName, token);
        }


        public Task<AccountExistRespModel> AccountExistAsync(string email)
        {
            return DoGetRequestAsync<AccountExistRespModel>("AccountExist", new { email });
        }

        public async Task RegisterAccount(string email, string fullName, string contactPhone, string password)
        {
            var result = await DoPostRequestAsync<RegsiterAccountRespModel>("Registration",
                new
                {
                    Email = email,
                    FullName = fullName,
                    ContactPhone = contactPhone,
                    Password = password,
                    ClientInfo = "Windows 10"
                });

            SaveToken(result.Token);
        }

        public Task<KycRegistrationStatusModel> CheckRegistrationStatus()
        {
            return DoGetRequestAsync<KycRegistrationStatusModel>("Registration");
        }

        public async Task<KycRegistrationStatusModel> AuthAsync(string email, string password)
        {
            var result = await DoPostRequestAsync<AuthRespModel>("Auth", new { Email = email, Password = password });
            SaveToken(result.Token);
            return result;
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
            return DoPostRequestAsync("EmailVerification", new {Email = email});
        }

        public Task SetPinCodeAsync(string pin)
        {
            return DoPostRequestAsync("PinSecurity", new { Pin = pin });
        }

        public async Task<bool> CheckPinCodeAsync(string pin)
        {
            var result = await DoGetRequestAsync<CheckPinSecurityRespModel>("PinSecurity", new { Pin = pin });
            return result.Passed;
        }

    }
}
