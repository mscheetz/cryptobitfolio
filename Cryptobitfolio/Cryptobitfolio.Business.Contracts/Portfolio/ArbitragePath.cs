using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptobitfolio.Business.Contracts.Portfolio
{
    public class ArbitragePath
    {
        public int ArbitragePathId { get; set; }
        public DateTime Created { get; set; }
        public string Path { get; set; }
    }
}
