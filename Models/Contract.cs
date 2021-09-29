using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace contracts_api.Models
{
    public class Contract
    {
        public string Id { get; set; }
        public string ContractName { get; set; }
        public string ContractNumber { get; set; }
        public string ContractType { get; set; }
    }
}
