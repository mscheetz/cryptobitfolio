using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptobitfolio.Business.Entities.Portfolio
{
    public class Currency : EntityBase
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
