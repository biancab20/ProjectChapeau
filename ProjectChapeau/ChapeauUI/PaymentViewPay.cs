﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChapeauModel;
using ChapeauService;


namespace ChapeauUI
{
    public partial class PaymentViewPay : Form
    {
        private Bill bill;
        private List<OrderItem> items;
        PaymentMethod method;

        decimal subTotal;
        float vat9, vat21;
        public decimal Total
        {
            get
            {
                return subTotal + (decimal)vat9 + (decimal)vat21 + bill.TotalTip;
            }
        }
        public PaymentViewPay(Bill bill, PaymentMethod method, List<OrderItem> items)
        {
            InitializeComponent();
            this.bill = bill;
            this.method = method;
            this.items = items;

            //Get all items, combine, display
            InitializeDisplay();

            ProcessPayment();

        }
        private void InitializeDisplay()
        {
            DisplayItems(items);
            UpdateLabels();
            StyleListView();
        }

        private void ProcessPayment()
        {
            if(method == PaymentMethod.Cash)
            {
                HideInputFields();
                DoPayment();
            }
            else
            {
                HideSuccessFields();
            }
        }

        private void DoPayment()
        {
            try
            {
                PaymentService paymentService = new PaymentService();
                //Generate a bill and corresponding payment for non-split payment 
                //and update all statuses to mark order as complete.

                //paymentService.CreateBill(bill);
                //bill.BillId = paymentService.GetCurrentBillId();

                //Payment payment = new Payment(bill.BillId, method, bill.TotalAmount, bill.TotalTip);
                //paymentService.AddPayment(payment);

                //paymentService.UpdateOrderPaidStatus(bill.Table);  
                //paymentService.UpdateTableStatusToFree(bill.Table);

                HideInputFields();
                ShowSuccessFields();
            }
            catch (Exception ex)
            {

                MessageBox.Show($"ERROR finalizing payment! \nERROR: {ex.Message}!");
            }


        }

        private void DisplayItems(List<OrderItem> items)
        {
            //Display each item the table ordered
            int index = 0;

            foreach (OrderItem item in items)
            {
                listviewItems.Items.Add(item.MenuItem.Name);

                decimal price = CalculateItemPrice(item);
                float vat = CalculateItemVat(item);

                listviewItems.Items[index].SubItems.Add(item.Amount.ToString());
                listviewItems.Items[index].SubItems.Add(price.ToString("€0.00"));
                listviewItems.Items[index].SubItems.Add(vat.ToString("€0.00"));
                index++;
            }

            if(bill.TotalTip.ToString() != "0.00")
            {
                listviewItems.Items.Add("Customer tip");
                listviewItems.Items[index].SubItems.Add("-");
                listviewItems.Items[index].SubItems.Add(bill.TotalTip.ToString("€0.00"));
                listviewItems.Items[index].SubItems.Add("€0.00");
            }
        }

        private decimal CalculateItemPrice(OrderItem item)
        {
            //Calculate price for display and add it to the running total
            decimal price = item.MenuItem.Price * item.Amount;
            subTotal += price;
            return price;
        }

        private float CalculateItemVat(OrderItem item)
        {
            //Calculate vat for display and add it to the running total for later
            float vat = (float)item.MenuItem.Price * item.MenuItem.Vat;
            if (item.MenuItem.Vat == (float)0.09) { vat9 += vat; } else { vat21 += vat; }
            return vat;
        }
        private void UpdateLabels()
        {
            lblTitle.Text = $"Table {bill.Table.TableId} - {DateTime.Now.ToString("d/M/yy")} - {DateTime.Now.ToString("h:mm tt")}";

            lblSubtotal.Text = subTotal.ToString("€0.00");
            lblvat9.Text = vat9.ToString("€0.00");
            lblvat21.Text = vat21.ToString("€0.00");
            lblTotal.Text = Total.ToString("€0.00");
            lblTotalTip.Text = bill.TotalTip.ToString("€0.00");
        }

        private void StyleListView()
        {
            foreach (ListViewItem item in listviewItems.Items)
            {
                if (item.Index % 2 == 0)
                {
                    item.BackColor = Color.White;
                }
                else
                {
                    item.BackColor = Color.FromArgb(255, 210, 210, 210);
                }

            }

            //add padding between each item, don't ask me how it works
            int itemHeight = 25;
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, itemHeight);
            listviewItems.SmallImageList = imgList;

        }
        private void listviewItems_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            //Change heading background to red and text to white
            SolidBrush brush = new SolidBrush(Color.FromArgb(255, 204, 68, 75)); //red background brush
            Font font = new Font("Segoe UI", 9, FontStyle.Bold); //header text
            StringFormat format = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
            };

            e.Graphics.FillRectangle(brush, e.Bounds);
            e.Graphics.DrawString(e.Header.Text, font, Brushes.White, e.Bounds, format);
        }

        private void listviewItems_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            PaymentView paymentView = new PaymentView(bill.Table);
            paymentView.ShowDialog();
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            DoPayment();
        }

        private void listviewItems_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //stop selection from turning blue when clicking on it
            if (e.IsSelected) { e.Item.Selected = false; }
        }
        private void HideInputFields()
        {
            lblCardName.Visible= false;
            txtCardName.Visible= false;
            lblCardNumber.Visible= false;
            txtCardNumber.Visible= false;
            lblExpDate.Visible= false;
            txtExpDate.Visible= false;
            lblCVV.Visible= false;
            txtCVV.Visible= false;

            btnCancel.Visible= false;
            btnConfirm.Visible= false;
        }

        private void HideSuccessFields()
        {
            pictureCheck.Visible= false;
            lblSuccess.Visible= false;
            lblComeAgain.Visible= false;
            btnClose.Visible= false;
        }

        private void ShowSuccessFields()
        {
            pictureCheck.Visible = true;
            lblSuccess.Visible = true;
            lblComeAgain.Visible = true;
            btnClose.Visible = true;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            TableView tableView = new TableView(); //Do I need to pass an employee? Or launch loginview instead?
            tableView.ShowDialog();
            this.Close();
        }
    }
}