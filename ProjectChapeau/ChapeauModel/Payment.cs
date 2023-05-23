﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauModel
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int BillId { get; set; }
        public paymentMethod PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public decimal Tip { get; set; }

        public Payment(int paymentId, int billId, paymentMethod paymentMethod, decimal amount, decimal tip)
        {
            PaymentId = paymentId;
            BillId = billId;
            PaymentMethod = paymentMethod;
            Amount = amount;
            Tip = tip;
        }

        public enum paymentMethod { Cash, Debit, Credit }
    }
}