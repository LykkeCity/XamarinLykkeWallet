using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykkeWallet.Models.Api
{
    public class Error
    {
        public int Code { set; get; }
        public string Field { set; get; }
        public string Message { set; get; }
    }
}
