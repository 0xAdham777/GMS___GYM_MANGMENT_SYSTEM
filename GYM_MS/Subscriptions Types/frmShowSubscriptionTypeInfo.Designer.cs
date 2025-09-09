namespace GYM_MS.Subscriptions_Types
{
    partial class frmShowSubscriptionTypeInfo
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
            this.ctrlSubscriptionCardInfo1 = new GYM_MS.Subscriptions_Types.Controls.ctrlSubscriptionCardInfo();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // ctrlSubscriptionCardInfo1
            // 
            this.ctrlSubscriptionCardInfo1.Location = new System.Drawing.Point(7, -4);
            this.ctrlSubscriptionCardInfo1.Name = "ctrlSubscriptionCardInfo1";
            this.ctrlSubscriptionCardInfo1.Size = new System.Drawing.Size(839, 429);
            this.ctrlSubscriptionCardInfo1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.BorderRadius = 15;
            this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Image = global::GYM_MS.Properties.Resources.close__1_;
            this.btnClose.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnClose.ImageSize = new System.Drawing.Size(30, 30);
            this.btnClose.Location = new System.Drawing.Point(345, 419);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(180, 45);
            this.btnClose.TabIndex = 44;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmShowSubscriptionTypeInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 471);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrlSubscriptionCardInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmShowSubscriptionTypeInfo";
            this.Text = "frmShowSubscriptionTypeInfo";
            this.Load += new System.EventHandler(this.frmShowSubscriptionTypeInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ctrlSubscriptionCardInfo ctrlSubscriptionCardInfo1;
        private Guna.UI2.WinForms.Guna2Button btnClose;
    }
}