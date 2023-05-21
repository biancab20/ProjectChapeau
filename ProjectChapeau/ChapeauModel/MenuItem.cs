﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapeauModel
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public Vat Vat { get; set; }

        public List<MenuItem> Ingredients { get; set; } //? should the name for this property be "Ingredients"?

        public MenuItem(int menuItemId, string name, double price, Vat vat)
        {
            MenuItemId = menuItemId;
            Name = name;
            Price = price;
            Vat = vat;
            Ingredients = new List<MenuItem>();
        }
    }
}