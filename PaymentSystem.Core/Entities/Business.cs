using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentSystem.Core.Entities
{
    public  class Business : Entity
    {
        public Guid ContactId { get; set; }
        public Contact? Contact { get; set; }
        public DateTime DateOfEstablishment { get; set; }
        public string? MerchantNumber { get; set; }
        public float TransactionVolume { get; set; }
    }
}
