using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykkeWallet.ApiAccess
{
    public class ApiException : Exception
    {
        public ApiException(ApiErrorModel error) : base(error.Message)
        {
            ErrorModel = error;
        }

        public ApiErrorModel ErrorModel { get; set; }
        

        public static void CheckException(string json)
        {
            var errorModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponseModel>(json);

            if (errorModel.Error != null)
            {
                switch (errorModel.Error.Code)
                {
                    case 0: throw new InvalidInputFieldException(errorModel.Error);
                    case 2:
                    case 3: throw new InvalidUsernameOrPasswordException(errorModel.Error);
                    default: throw new ApiException(errorModel.Error);
                }
            }

        }
    }

    public class InvalidInputFieldException : ApiException
    {
        public InvalidInputFieldException(ApiErrorModel error) : base(error)
        {

        }
    }

    public class InvalidUsernameOrPasswordException : ApiException
    {
        public InvalidUsernameOrPasswordException(ApiErrorModel error) : base(error)
        {

        }
    }
}
