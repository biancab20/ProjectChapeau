﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauModel
{
    public enum Role { Manager, Barman, Chef, Waiter }
    public enum MenuType { Lunch, Dinner, AllDay }
    public enum FoodType { Starter, MainCourse, Dessert, Drink }
    public enum OrderedItemStatus { Sent, Preparing, Ready, Served }
    public enum PaymentMethod { Cash, Debit, Credit }
    public enum TableStatus { Free, Occupied }
}