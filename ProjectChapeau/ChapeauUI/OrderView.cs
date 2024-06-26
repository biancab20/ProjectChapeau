﻿using ChapeauDAL;
using ChapeauModel;
using ChapeauService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ChapeauUI
{
    public partial class OrderView : Form
    {
        private Order order;

        private OrderItemService orderItemService;
        private InventoryItemService inventoryItemService;
        private MenuItemService menuItemService;

        private List<MenuItem> currentMenuItems;
        private List<InventoryItem> currentInventoryItems;

        private FoodType currentCourseType;
        private MenuType currentMenuType;
        private MenuType otherMenuType;

        private string currentMenuLabel;

        public OrderView(Table table, Employee employee)
        {
            InitializeComponent();

            string tableId = table.TableId.ToString();

            currentCourseType = FoodType.Starter;
            currentMenuType = MenuType.Lunch;
            otherMenuType = MenuType.Dinner;

            currentMenuLabel = "Starters";

            orderItemService = new OrderItemService();
            inventoryItemService = new InventoryItemService();
            menuItemService = new MenuItemService();

            labelTableNumber.Text = $"Table {tableId}";

            FillMenuItemList(currentCourseType, currentMenuType);
            order = CreateOrder(table, employee);
        }

        private void FillMenuItemList(FoodType foodType, MenuType menuType)
        {
            currentMenuItems = new List<MenuItem>();
            currentInventoryItems = new List<InventoryItem>();

            try
            {
                currentMenuItems.AddRange(menuItemService.GetCourseMenuType(foodType, menuType));
                currentInventoryItems.AddRange(inventoryItemService.GetInventoryItems());
            }
            catch (Exception e)
            {
                MessageBox.Show("An error occurred.\n" + e.Message);
            }

            DisplayItems(currentMenuItems, currentInventoryItems);
        }

        // Displays items in listViewMenuItems
        private void DisplayItems(List<MenuItem> menuItems, List<InventoryItem> inventoryItems)
        {
            try
            {
                // clear the listview items before filling it
                listViewMenuItems.Items.Clear();

                foreach (MenuItem menuItem in menuItems)
                {
                    ListViewItem listViewItem = new ListViewItem(menuItem.Name);
                    listViewItem.SubItems.Add(menuItem.Description);
                    listViewItem.Tag = menuItem;

                    // Find the corresponding InventoryItem for the current MenuItem
                    InventoryItem inventoryItem = FindInventorytemById(inventoryItems, menuItem.MenuItemId);

                    // Set the text colour based on the inStock value
                    if (inventoryItem != null)
                    {
                        if (inventoryItem.InStock > 0)
                        {
                            listViewItem.ForeColor = Color.Black;
                        }
                        else
                        {
                            listViewItem.ForeColor = Color.Red;
                        }

                        listViewMenuItems.Items.Add(listViewItem);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Something went wrong when trying to display the MenuItems \n {e}.", "Error message");
            }
        }

        private void buttonCategoryStarters_Click(object sender, EventArgs e)
        {
            buttonSwitchMenu.Show();
            currentCourseType = FoodType.Starter;
            currentMenuLabel = "Starters";

            FillMenuItemList(currentCourseType, currentMenuType);
            SwitchMenuLabel(currentMenuLabel, currentMenuType.ToString());
        }

        private void buttonCategoryMainDish_Click(object sender, EventArgs e)
        {
            buttonSwitchMenu.Show();
            currentCourseType = FoodType.MainCourse;
            currentMenuLabel = "Main Dish";

            FillMenuItemList(currentCourseType, currentMenuType);
            SwitchMenuLabel(currentMenuLabel, currentMenuType.ToString());
        }

        private void buttonCategoryDesserts_Click(object sender, EventArgs e)
        {
            buttonSwitchMenu.Show();
            currentCourseType = FoodType.Dessert;
            currentMenuLabel = "Desserts";

            FillMenuItemList(currentCourseType, currentMenuType);
            SwitchMenuLabel(currentMenuLabel, currentMenuType.ToString());
        }

        private void buttonCategoryDrinks_Click(object sender, EventArgs e)
        {
            buttonSwitchMenu.Hide();
            currentCourseType = FoodType.Drink;
            currentMenuLabel = "Drinks";

            FillMenuItemList(currentCourseType, MenuType.AllDay);
            SwitchMenuLabel(currentMenuLabel, "All Day");
        }

        private void buttonSwitchMenu_Click(object sender, EventArgs e)
        {
            SwitchMenuType();
        }

        private void buttonCloseOrder_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonFinaliseOrder_Click(object sender, EventArgs e)
        {
            OrderFinalise orderFinalise = new OrderFinalise(order);
            DialogResult dialogResult = orderFinalise.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                order = orderFinalise.Order;

                AddOrderItem(order);
                Close();
            }
            else if (dialogResult == DialogResult.Cancel)
            {
                order = orderFinalise.Order;
            }
        }

        private void SwitchMenuType()
        {
            if (currentMenuType == MenuType.Lunch)
            {
                currentMenuType = MenuType.Dinner;
                otherMenuType = MenuType.Lunch;
            }
            else
            {
                currentMenuType = MenuType.Lunch;
                otherMenuType = MenuType.Dinner;
            }

            buttonSwitchMenu.Text = $"Switch To {otherMenuType.ToString()} Menu";

            FillMenuItemList(currentCourseType, currentMenuType);
            SwitchMenuLabel(currentMenuLabel, currentMenuType.ToString());
        }

        private void SwitchMenuLabel(string menuType, string menuTime)
        {
            labelMenuType.Text = menuType;
            labelMenuTime.Text = menuTime;
        }

        // When ListViewMenuItems item is selected it will create the OrderPopup form
        private void listViewMenuItems_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected && listViewMenuItems.SelectedItems.Count == 1)
            {
                ListViewItem selectedItem = e.Item;

                if (e.Item.ForeColor == Color.Red)
                {
                    MessageBox.Show("This item is out of stock.", "Out of Stock");
                }
                else
                {
                    // Retrieve the MenuItem object from the Tag property
                    MenuItem menuItem = selectedItem.Tag as MenuItem;

                    if (menuItem != null)
                    {
                        // Create form
                        OrderPopup orderPopup = new OrderPopup(menuItem.Name, menuItem.Description);
                        DialogResult dialogResult = orderPopup.ShowDialog();

                        if (dialogResult == DialogResult.OK)
                        {
                            CreateOrderItem(menuItem, orderPopup.Comment, orderPopup.Amount);
                        }
                    }
                }

                // Prevent double selection
                selectedItem.Selected = true;
            }
        }

        private void CreateOrderItem(MenuItem menuItem, string comment, int amount)
        {
            bool existingOrderItem = CheckIfOrderItemExists(menuItem, comment, amount);

            if (!existingOrderItem)
            {
                OrderItem orderItem = new OrderItem();

                orderItem.OrderItemId = order.OrderId;
                orderItem.MenuItem = menuItem;
                orderItem.Comment = comment;
                orderItem.Amount = amount;
                orderItem.Status = OrderedItemStatus.Sent;
                orderItem.PreparedAt = null;

                // Add to list
                order.OrderedItems.Add(orderItem);
                MessageBox.Show($"{orderItem.MenuItem.Name} has been added to the ordering list {orderItem.Amount} times.", "Order added");
            }
        }

        private bool CheckIfOrderItemExists(MenuItem menuItem, string comment, int increaseAmount)
        {
            foreach (OrderItem orderItem in order.OrderedItems)
            {
                if (orderItem.MenuItem.MenuItemId == menuItem.MenuItemId)
                {
                    orderItem.Amount += increaseAmount;
                    orderItem.Comment += $" {comment}";

                    MessageBox.Show($"Increased {orderItem.MenuItem.Name} amount by {increaseAmount}", "Order amount increased");
                    
                    return true;
                }
            }

            return false;
        }

        private MenuItem FindMenuItemById(List<MenuItem> menuItems, int menuId)
        {
            foreach (MenuItem item in menuItems)
            {
                if (item.MenuItemId == menuId)
                {
                    return item;
                }
            }

            return null;
        }

        private InventoryItem FindInventorytemById(List<InventoryItem> inventoryItems, int menuItemId)
        {
            foreach (InventoryItem item in inventoryItems)
            {
                if (item.MenuItemID == menuItemId)
                {
                    return item;
                }
            }

            return null;
        }

        public Order CreateOrder(Table table, Employee employee)
        {
            Order order = new Order();
            order.Table = table;
            order.Time = DateTime.Now;
            order.Employee = employee;
            order.IsPaid = false;
            order.OrderedItems = new List<OrderItem>();

            // Creates order in the database and gets id
            order.OrderId = orderItemService.CreateOrder(order);

            return order;
        }

        // Add Orders to database & Calls method ReduceStockQuery
        public void AddOrderItem(Order order)
        {
            foreach (OrderItem orderItem in order.OrderedItems)
            {
                orderItemService.AddOrderItem(orderItem);
                ReduceStockQuery(orderItem.MenuItem.MenuItemId, orderItem.Amount);
            }
            MessageBox.Show("Orders have been sent to the Kitchen / Bar", "Order sent");
        }

        // Reduce Stock Amount
        public void ReduceStockQuery(int menuItemId, int amount)
        {
            inventoryItemService.DecreaseInventoryItemStock(menuItemId, amount);
        }
    }
}
