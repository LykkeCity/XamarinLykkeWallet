using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    

}
