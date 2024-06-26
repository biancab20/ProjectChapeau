﻿namespace ChapeauUI
{
    partial class NewEmployee
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewEmployee));
            pictureBox1 = new PictureBox();
            lblHeading = new Label();
            lblFirstName = new Label();
            lblLastName = new Label();
            txtBoxFirstName = new TextBox();
            txtBoxLastName = new TextBox();
            lblOccupation = new Label();
            lblPinCode = new Label();
            lblConfirmPinCode = new Label();
            radBtnWaiter = new RadioButton();
            radBtnChef = new RadioButton();
            radBtnBartender = new RadioButton();
            btnCancel = new Button();
            btnConfirm = new Button();
            txtBoxPin1 = new TextBox();
            txtBoxPin2 = new TextBox();
            panel1 = new Panel();
            panel4 = new Panel();
            panel3 = new Panel();
            panel5 = new Panel();
            lblSubHeading = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(178, 103);
            pictureBox1.TabIndex = 16;
            pictureBox1.TabStop = false;
            // 
            // lblHeading
            // 
            lblHeading.AutoSize = true;
            lblHeading.Font = new Font("Segoe UI", 30F, FontStyle.Regular, GraphicsUnit.Point);
            lblHeading.Location = new Point(229, 31);
            lblHeading.Name = "lblHeading";
            lblHeading.Size = new Size(491, 67);
            lblHeading.TabIndex = 15;
            lblHeading.Text = "Add a new employee";
            // 
            // lblFirstName
            // 
            lblFirstName.AutoSize = true;
            lblFirstName.Location = new Point(76, 259);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new Size(83, 20);
            lblFirstName.TabIndex = 17;
            lblFirstName.Text = "First Name:";
            // 
            // lblLastName
            // 
            lblLastName.AutoSize = true;
            lblLastName.Location = new Point(339, 259);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(79, 20);
            lblLastName.TabIndex = 18;
            lblLastName.Text = "Last Name";
            // 
            // txtBoxFirstName
            // 
            txtBoxFirstName.Location = new Point(76, 314);
            txtBoxFirstName.Name = "txtBoxFirstName";
            txtBoxFirstName.Size = new Size(125, 27);
            txtBoxFirstName.TabIndex = 19;
            // 
            // txtBoxLastName
            // 
            txtBoxLastName.Location = new Point(339, 314);
            txtBoxLastName.Name = "txtBoxLastName";
            txtBoxLastName.Size = new Size(125, 27);
            txtBoxLastName.TabIndex = 20;
            // 
            // lblOccupation
            // 
            lblOccupation.AutoSize = true;
            lblOccupation.Location = new Point(76, 407);
            lblOccupation.Name = "lblOccupation";
            lblOccupation.Size = new Size(88, 20);
            lblOccupation.TabIndex = 21;
            lblOccupation.Text = "Occupation:";
            // 
            // lblPinCode
            // 
            lblPinCode.AutoSize = true;
            lblPinCode.Location = new Point(76, 560);
            lblPinCode.Name = "lblPinCode";
            lblPinCode.Size = new Size(139, 20);
            lblPinCode.TabIndex = 22;
            lblPinCode.Text = "Four digit Pin Code:";
            // 
            // lblConfirmPinCode
            // 
            lblConfirmPinCode.AutoSize = true;
            lblConfirmPinCode.Location = new Point(339, 560);
            lblConfirmPinCode.Name = "lblConfirmPinCode";
            lblConfirmPinCode.Size = new Size(126, 20);
            lblConfirmPinCode.TabIndex = 23;
            lblConfirmPinCode.Text = "Confirm Pin code:";
            // 
            // radBtnWaiter
            // 
            radBtnWaiter.AutoSize = true;
            radBtnWaiter.Location = new Point(76, 468);
            radBtnWaiter.Name = "radBtnWaiter";
            radBtnWaiter.Size = new Size(73, 24);
            radBtnWaiter.TabIndex = 24;
            radBtnWaiter.TabStop = true;
            radBtnWaiter.Text = "Waiter";
            radBtnWaiter.UseVisualStyleBackColor = true;
            // 
            // radBtnChef
            // 
            radBtnChef.AutoSize = true;
            radBtnChef.Location = new Point(339, 468);
            radBtnChef.Name = "radBtnChef";
            radBtnChef.Size = new Size(60, 24);
            radBtnChef.TabIndex = 25;
            radBtnChef.TabStop = true;
            radBtnChef.Text = "Chef";
            radBtnChef.UseVisualStyleBackColor = true;
            // 
            // radBtnBartender
            // 
            radBtnBartender.AutoSize = true;
            radBtnBartender.Location = new Point(567, 468);
            radBtnBartender.Name = "radBtnBartender";
            radBtnBartender.Size = new Size(95, 24);
            radBtnBartender.TabIndex = 26;
            radBtnBartender.TabStop = true;
            radBtnBartender.Text = "Bartender";
            radBtnBartender.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(86, 748);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(172, 102);
            btnCancel.TabIndex = 29;
            btnCancel.Text = "cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnConfirm
            // 
            btnConfirm.Location = new Point(543, 743);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(177, 107);
            btnConfirm.TabIndex = 30;
            btnConfirm.Text = "confirm";
            btnConfirm.UseVisualStyleBackColor = true;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // txtBoxPin1
            // 
            txtBoxPin1.Location = new Point(76, 600);
            txtBoxPin1.Name = "txtBoxPin1";
            txtBoxPin1.Size = new Size(125, 27);
            txtBoxPin1.TabIndex = 31;
            txtBoxPin1.UseSystemPasswordChar = true;
            txtBoxPin1.TextChanged += txtBoxPin1_TextChanged;
            // 
            // txtBoxPin2
            // 
            txtBoxPin2.Location = new Point(340, 600);
            txtBoxPin2.Name = "txtBoxPin2";
            txtBoxPin2.Size = new Size(125, 27);
            txtBoxPin2.TabIndex = 32;
            txtBoxPin2.UseSystemPasswordChar = true;
            txtBoxPin2.TextChanged += txtBoxPin2_TextChanged;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Green;
            panel1.Location = new Point(0, 118);
            panel1.Name = "panel1";
            panel1.Size = new Size(839, 10);
            panel1.TabIndex = 54;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Green;
            panel4.Location = new Point(0, 1018);
            panel4.Name = "panel4";
            panel4.Size = new Size(839, 38);
            panel4.TabIndex = 56;
            // 
            // panel3
            // 
            panel3.BackColor = Color.IndianRed;
            panel3.Controls.Add(panel5);
            panel3.Location = new Point(0, 118);
            panel3.Name = "panel3";
            panel3.Size = new Size(839, 46);
            panel3.TabIndex = 55;
            // 
            // panel5
            // 
            panel5.BackColor = Color.Green;
            panel5.Location = new Point(0, 34);
            panel5.Name = "panel5";
            panel5.Size = new Size(839, 10);
            panel5.TabIndex = 50;
            // 
            // lblSubHeading
            // 
            lblSubHeading.AutoSize = true;
            lblSubHeading.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            lblSubHeading.Location = new Point(76, 192);
            lblSubHeading.Name = "lblSubHeading";
            lblSubHeading.Size = new Size(673, 46);
            lblSubHeading.TabIndex = 57;
            lblSubHeading.Text = "Please fill in this form to edit the menu item";
            // 
            // NewEmployee
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(836, 1055);
            Controls.Add(lblSubHeading);
            Controls.Add(panel1);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(txtBoxPin2);
            Controls.Add(txtBoxPin1);
            Controls.Add(btnConfirm);
            Controls.Add(btnCancel);
            Controls.Add(radBtnBartender);
            Controls.Add(radBtnChef);
            Controls.Add(radBtnWaiter);
            Controls.Add(lblConfirmPinCode);
            Controls.Add(lblPinCode);
            Controls.Add(lblOccupation);
            Controls.Add(txtBoxLastName);
            Controls.Add(txtBoxFirstName);
            Controls.Add(lblLastName);
            Controls.Add(lblFirstName);
            Controls.Add(pictureBox1);
            Controls.Add(lblHeading);
            Name = "NewEmployee";
            Text = "NewEmployee";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label lblHeading;
        private Label lblFirstName;
        private Label lblLastName;
        private TextBox txtBoxFirstName;
        private TextBox txtBoxLastName;
        private Label lblOccupation;
        private Label lblPinCode;
        private Label lblConfirmPinCode;
        private RadioButton radBtnWaiter;
        private RadioButton radBtnChef;
        private RadioButton radBtnBartender;
        private Button btnCancel;
        private Button btnConfirm;
        private TextBox txtBoxPin1;
        private TextBox txtBoxPin2;
        private Panel panel1;
        private Panel panel4;
        private Panel panel3;
        private Panel panel5;
        private Label lblSubHeading;
    }
}