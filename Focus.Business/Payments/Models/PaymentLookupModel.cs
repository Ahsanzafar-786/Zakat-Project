﻿using System;

namespace Focus.Business.Payments.Models
{
    public class PaymentLookupModel
    {
        public Guid? Id { get; set; }
        public Guid? BenificayId { get; set; }
        public decimal Amount { get; set; }
        public string UserId { get; set; }
        public DateTime? Month { get; set; }
        public string Year { get; set; }
        public string Period { get; set; }
        public string BenificaryName { get; set; }
    }
}