﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauModel
{
    public class Order
    {
        public int OrderId { get; set; }

        public Table Table { get; set; }

        public DateTime Time { get; set; }

        public string Comment { get; set; }

        public bool IsActive { get; set; }

        public Employee Employee { get; set; }

        public List<OrderItem> OrderedItems { get; set; }

        public Order(int orderId, Table table, DateTime time, Employee employee)
        {
            OrderId = orderId;
            Table = table; Time = time;
            Employee = employee;

            IsActive = true;
            OrderedItems = new List<OrderItem>();
        }
    }
}