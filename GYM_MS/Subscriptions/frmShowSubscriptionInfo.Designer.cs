namespace GYM_MS.Subscriptions
{
    partial class frmShowSubscriptionInfo
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
            this.ctrlSubscriptionCard1 = new GYM_MS.Subscriptions.Controls.ctrlSubscriptionCard();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // ctrlSubscriptionCard1
            // 
            this.ctrlSubscriptionCard1.Location = new System.Drawing.Point(3, -1);
            this.ctrlSubscriptionCard1.Name = "ctrlSubscriptionCard1";
            this.ctrlSubscriptionCard1.Size = new System.Drawing.Size(866, 868);
            this.ctrlSubscriptionCard1.TabIndex = 1;
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
            this.btnClose.Location = new System.Drawing.Point(352, 856);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(205, 45);
            this.btnClose.TabIndex = 40;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmShowSubscriptionInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 910);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrlSubscriptionCard1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmShowSubscriptionInfo";
            this.Text = "frmShowSubscriptionInfo";
            this.Load += new System.EventHandler(this.frmShowSubscriptionInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ctrlSubscriptionCard ctrlSubscriptionCard1;
        private Guna.UI2.WinForms.Guna2Button btnClose;
    }
}