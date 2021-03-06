﻿using Cryptobitfolio.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptobitfolio.Business.Contracts.Trade
{
    public class ExchangeApi
    {
        public int ExchangeApiId { get; set; }
        public string ApiKeyName { get; set; }
        public Exchange Exchange { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string ApiExtra { get; set; }
        public string WIF { get; set; }
    }
}
