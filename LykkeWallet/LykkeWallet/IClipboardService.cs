using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykkeWallet
{
    public interface IClipboardService
    {
        void CopyToClipboard(string text);
    }
}
