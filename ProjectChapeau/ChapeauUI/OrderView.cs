﻿using ChapeauModel;
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

namespace ChapeauUI
{
    public partial class OrderView : Form
    {
        const int DinnerMenuStart = 12;
        const int DinnerMenuEnd = 4;

        MenuItemService menuItemService = new MenuItemService();

        List<MenuItem> starterLunchItems = new List<MenuItem>();
        List<MenuItem> starterDinnerItems = new List<MenuItem>();
        List<MenuItem> mainCourseLunchItems = new List<MenuItem>();
        List<MenuItem> mainCourseDinnerItems = new List<MenuItem>();
        List<MenuItem> dessertLunchItems = new List<MenuItem>();
        List<MenuItem> dessertDinnerItems = new List<MenuItem>();
        List<MenuItem> drinkItems = new List<MenuItem>();

        MenuType currentMenuType;
        MenuType otherMenuType;

        bool HideDrinkMenu;

        string currentMenuLabel;

        public OrderView()
        {
            currentMenuLabel = FoodType.Starter.ToString();

            InitializeComponent();

            // Get Data
            starterLunchItems.AddRange(menuItemService.GetCourseMenuType(FoodType.Starter.ToString(), MenuType.Lunch.ToString()));
            starterDinnerItems.AddRange(menuItemService.GetCourseMenuType(FoodType.Starter.ToString(), MenuType.Dinner.ToString()));
            mainCourseLunchItems.AddRange(menuItemService.GetCourseMenuType(FoodType.MainCourse.ToString(), MenuType.Lunch.ToString()));
            mainCourseDinnerItems.AddRange(menuItemService.GetCourseMenuType(FoodType.MainCourse.ToString(), MenuType.Dinner.ToString()));
            dessertLunchItems.AddRange(menuItemService.GetCourseMenuType(FoodType.Dessert.ToString(), MenuType.Lunch.ToString()));
            dessertDinnerItems.AddRange(menuItemService.GetCourseMenuType(FoodType.Dessert.ToString(), MenuType.Dinner.ToString()));
            drinkItems.AddRange(menuItemService.GetCourseMenuType(FoodType.Drink.ToString(), MenuType.AllDay.ToString()));

            // Automatically determine Menu Type based on time
            #region AutomaticTime
            DateTime dateTime = DateTime.Now;

            TimeSpan dinnerMenuStart = new TimeSpan(DinnerMenuStart, 0, 0);
            TimeSpan dinnerMenuEnd = new TimeSpan(DinnerMenuEnd, 0, 0);
            TimeSpan timeOfDay = dateTime.TimeOfDay;

            // Set default values
            currentMenuType = MenuType.Lunch;
            otherMenuType = MenuType.Dinner;

            if (timeOfDay >= dinnerMenuStart || timeOfDay <= dinnerMenuEnd)
            {
                SwitchMenuType();
            }
            #endregion

            // Starting Displays
            DisplayItems(starterLunchItems);
        }

        // Displays items in listViewMenuItems
        private void DisplayItems(List<MenuItem> items)
        {
            // clear the listview before filling it
            listViewMenuItems.Items.Clear();

            foreach (MenuItem item in items) 
            {
                ListViewItem listViewItem = new ListViewItem(item.Name);
                listViewItem.SubItems.Add(item.Description);

                listViewMenuItems.Items.Add(listViewItem);
            }
        }

        #region CategoryButtons
        private void buttonCategoryStarters_Click(object sender, EventArgs e)
        {
            currentMenuLabel = FoodType.Starter.ToString();
            HideDrinkMenu = true;

            if (currentMenuType == MenuType.Lunch) 
            { 
                DisplayItems(starterLunchItems);
            }
            else
            {
                DisplayItems(starterDinnerItems);
            }

            SwitchMenuLabel(currentMenuLabel.ToString(), currentMenuType.ToString());
        }

        private void buttonCategoryMainDish_Click(object sender, EventArgs e)
        {
            currentMenuLabel = FoodType.MainCourse.ToString();
            HideDrinkMenu = true;

            if (currentMenuType == MenuType.Lunch)
            {
                DisplayItems(mainCourseLunchItems);
            }
            else
            {
                DisplayItems(mainCourseDinnerItems);
            }

            SwitchMenuLabel(currentMenuLabel, currentMenuType.ToString());
        }

        private void buttonCategoryDesserts_Click(object sender, EventArgs e)
        {
            currentMenuLabel = FoodType.Dessert.ToString();
            HideDrinkMenu = true;

            if (currentMenuType == MenuType.Lunch)
            {
                DisplayItems(dessertLunchItems);
            }
            else
            {
                DisplayItems(dessertDinnerItems);
            }

            SwitchMenuLabel(currentMenuLabel, currentMenuType.ToString());
        }

        private void buttonCategoryDrinks_Click(object sender, EventArgs e)
        {
            currentMenuLabel = FoodType.Drink.ToString();

            buttonSwitchMenu.Hide();
            buttonGoBackDrinksMenu.Show();
            HideDrinkMenu = false;

            DisplayItems(drinkItems);
            SwitchMenuLabel(currentMenuLabel, "Category");
        }
        #endregion
        #region BottomButtons
        private void buttonSwitchMenu_Click(object sender, EventArgs e)
        {
            SwitchMenuType();
        }

        private void buttonGoBackDrinksMenu_Click(object sender, EventArgs e)
        {
            currentMenuLabel = "Drinks";

            SwitchMenuLabel("Drinks", "Category");
        }

        private void buttonCloseOrder_Click(object sender, EventArgs e)
        {
            TableView tableView = new TableView();
            this.Hide();
            tableView.ShowDialog();
            this.Close();
        }

        private void buttonFinaliseOrder_Click(object sender, EventArgs e)
        {
            OrderFinalise orderFinalise = new OrderFinalise();
            this.Hide();
            orderFinalise.ShowDialog();
            this.Show();
        }
        #endregion

        void SwitchMenuType()
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
            SwitchMenuLabel(currentMenuLabel, currentMenuType.ToString());
        }

        void SwitchMenuLabel(string menuType, string menuTime)
        {
            if (HideDrinkMenu == true)
            {
                switch (currentMenuType)
                {
                    case MenuType.Lunch:
                        break;
                    case MenuType.Dinner:
                        break;
                    case MenuType.AllDay:
                        break;
                    default:
                        break;
                }

                buttonSwitchMenu.Show();
                buttonGoBackDrinksMenu.Hide();
                HideDrinkMenu = false;
            }

            labelMenuType.Text = menuType;
            labelMenuTime.Text = menuTime;
        }

        // When ListViewMenuItems is selected it will create the OrderPopup form
        private void listViewMenuItems_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected && listViewMenuItems.SelectedItems.Count == 1)
            {
                // Detach the event handler temporarily
                listViewMenuItems.ItemSelectionChanged -= listViewMenuItems_ItemSelectionChanged;

                // Retrieve information from item
                string name = e.Item.SubItems[0].Text;
                string description = e.Item.SubItems[1].Text;

                // Create form
                OrderPopup orderPopup = new OrderPopup(name, description);
                orderPopup.FormClosed += (s, ev) =>
                {
                    // Reattach the event handler after the form is closed
                    listViewMenuItems.ItemSelectionChanged += listViewMenuItems_ItemSelectionChanged;
                };
                orderPopup.ShowDialog();
            }
        }
    }
}
