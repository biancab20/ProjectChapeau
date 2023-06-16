﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using ChapeauModel;
using ChapeauService;

namespace ChapeauUI
{
    public partial class Login : Form
    {
        private EmployeeService employeeService;
        private Employee employee;
        public Login()
        {
            InitializeComponent();
            employeeService = new EmployeeService();
            employee = new Employee();
            this.textBox_login_pincode.PasswordChar = '*';
        }

        private void label_login_forgotPincode_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please call the manager", "Message", MessageBoxButtons.OKCancel);
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            if ((this.textBox_login_name.Text) == "" || (this.textBox_login_pincode.Text) == "")
            {
                MessageBox.Show("Log in failed! Please enter your name and pin code!", "Message", MessageBoxButtons.OK);
            }
            else
            {
                string username = employeeService.GetEmployeeUsername(employee);
                string hashedPincode = employeeService.Hash(this.textBox_login_pincode.Text);
                try
                {
                    employee = employeeService.GetEmployeeByPassword(hashedPincode);
                    username = this.textBox_login_name.Text;
                }
                catch
                {
                    MessageBox.Show("Incorrect pin code or username. Please try again!", "Message", MessageBoxButtons.OK);
                    return;
                }

                if ((employee.Pincode == hashedPincode) && (username == this.textBox_login_name.Text))
                {
                    switch (employee.Occupation)
                    {
                        case ChapeauModel.Role.Manager:
                            this.Hide();
                            ManagerView managerView = new ManagerView(employee);
                            managerView.ShowDialog();
                            this.Close();
                            break;
                        case ChapeauModel.Role.Chef:
                        case ChapeauModel.Role.Barman:
                            this.Hide();
                            BarKitchenView barKitchenView = new BarKitchenView(employee);
                            barKitchenView.ShowDialog();
                            this.Close();
                            break;
                        case ChapeauModel.Role.Waiter:
                            this.Hide();
                            TableView tableView = new TableView(employee);
                            tableView.ShowDialog();
                            this.Close();
                            break;
                    }
                }

                
            }
        }
    }
}
