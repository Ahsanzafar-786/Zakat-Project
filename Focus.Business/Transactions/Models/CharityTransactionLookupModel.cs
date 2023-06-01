using System;

namespace Focus.Business.Transactions.Models
{
    public class CharityTransactionLookupModel 
    {
        public Guid? Id { get; set; }
        public Guid? DoucmentId { get; set; }
        public Guid? BenificayId { get; set; }
        public int DoucmentCode { get; set; }
        public decimal Amount { get; set; }
        public DateTime? DoucmentDate { get; set; }
        public DateTime? Month { get; set; }
        public DateTime? CharityTransactionDate { get; set; }
        public string Year { get; set; }


    }
}
