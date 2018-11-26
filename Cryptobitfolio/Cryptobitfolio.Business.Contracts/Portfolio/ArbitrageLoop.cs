using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptobitfolio.Business.Contracts.Portfolio
{
    public class ArbitrageLoop
    {
        public string[] Path { get; set; }
        public decimal StartingQuantity { get; set; }
        public decimal FinalQuantity { get; set; }

        public ArbitrageLoop()
        {
        }

        public ArbitrageLoop(string[] path, decimal startingQuantity)
        {
            this.Path = path;
        }
    }
}
