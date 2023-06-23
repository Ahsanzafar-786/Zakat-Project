﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class PaymentWiseListLookupModel
    {
        public Guid? Id { get; set; }
        public string PaymentId { get; set; }
        public string BeneficaryId { get; set; }
        public Guid? Beneficary { get; set; }
        public Guid? UserId { get; set; }
        public string BeneficaryName { get; set; }
        public string CashierName { get; set; }
        public string PaymentType { get; set; }
        public string PaymentDate { get; set; }
        public DateTime? Date { get; set; }
        public string Note { get; set; }
        public decimal Amount { get; set; }

    }
}