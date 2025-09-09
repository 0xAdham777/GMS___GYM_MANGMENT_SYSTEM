namespace GYM_MS.Coaches
{
    partial class frmAddUpdateCoaches
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddUpdateCoaches));
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.lblTitel = new System.Windows.Forms.Label();
            this.ctrlPersonCardWithFilter1 = new GYM_MS.People.Controls.ctrlPersonCardWithFilter();
            this.btnNext = new Guna.UI2.WinForms.Guna2Button();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.tpPersonInfo = new System.Windows.Forms.TabPage();
            this.tpCoachInfo = new System.Windows.Forms.TabPage();
            this.lblCoachID = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.llShowSpelazationInfo = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSpezalation = new Guna.UI2.WinForms.Guna2ComboBox();
            this.tcCoachInfo = new Guna.UI2.WinForms.Guna2TabControl();
            this.tpPersonInfo.SuspendLayout();
            this.tpCoachInfo.SuspendLayout();
            this.tcCoachInfo.SuspendLayout();
            this.SuspendLayout();
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
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnClose.Location = new System.Drawing.Point(843, 659);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(180, 45);
            this.btnClose.TabIndex = 41;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTitel
            // 
            this.lblTitel.AutoSize = true;
            this.lblTitel.Font = new System.Drawing.Font("Ravie", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitel.ForeColor = System.Drawing.Color.Crimson;
            this.lblTitel.Location = new System.Drawing.Point(275, -3);
            this.lblTitel.Name = "lblTitel";
            this.lblTitel.Size = new System.Drawing.Size(587, 76);
            this.lblTitel.TabIndex = 39;
            this.lblTitel.Text = "Add New Coach";
            // 
            // ctrlPersonCardWithFilter1
            // 
            this.ctrlPersonCardWithFilter1.FilterEnabled = true;
            this.ctrlPersonCardWithFilter1.Location = new System.Drawing.Point(0, 0);
            this.ctrlPersonCardWithFilter1.Name = "ctrlPersonCardWithFilter1";
            this.ctrlPersonCardWithFilter1.Size = new System.Drawing.Size(935, 522);
            this.ctrlPersonCardWithFilter1.TabIndex = 40;
            this.ctrlPersonCardWithFilter1.OnPersonSelected += new System.EventHandler<GYM_MS.People.Controls.ctrlPersonCardWithFilter.PersonSelectedEventArgs>(this.ctrlPersonCardWithFilter1_OnPersonSelected);
            // 
            // btnNext
            // 
            this.btnNext.BorderRadius = 15;
            this.btnNext.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnNext.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnNext.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnNext.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnNext.Enabled = false;
            this.btnNext.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.ImageAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.btnNext.Location = new System.Drawing.Point(665, 523);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(180, 45);
            this.btnNext.TabIndex = 39;
            this.btnNext.Text = "Next";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnSave
            // 
            this.btnSave.BorderRadius = 15;
            this.btnSave.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSave.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(669, 510);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(180, 45);
            this.btnSave.TabIndex = 38;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tpPersonInfo
            // 
            this.tpPersonInfo.Controls.Add(this.ctrlPersonCardWithFilter1);
            this.tpPersonInfo.Controls.Add(this.btnNext);
            this.tpPersonInfo.Location = new System.Drawing.Point(184, 4);
            this.tpPersonInfo.Name = "tpPersonInfo";
            this.tpPersonInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpPersonInfo.Size = new System.Drawing.Size(935, 573);
            this.tpPersonInfo.TabIndex = 0;
            this.tpPersonInfo.Text = "Person Info ";
            this.tpPersonInfo.UseVisualStyleBackColor = true;
            // 
            // tpCoachInfo
            // 
            this.tpCoachInfo.Controls.Add(this.lblCoachID);
            this.tpCoachInfo.Controls.Add(this.label2);
            this.tpCoachInfo.Controls.Add(this.llShowSpelazationInfo);
            this.tpCoachInfo.Controls.Add(this.label1);
            this.tpCoachInfo.Controls.Add(this.cbSpezalation);
            this.tpCoachInfo.Controls.Add(this.btnSave);
            this.tpCoachInfo.Location = new System.Drawing.Point(184, 4);
            this.tpCoachInfo.Name = "tpCoachInfo";
            this.tpCoachInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpCoachInfo.Size = new System.Drawing.Size(935, 573);
            this.tpCoachInfo.TabIndex = 1;
            this.tpCoachInfo.Text = "Coach Info";
            this.tpCoachInfo.UseVisualStyleBackColor = true;
            // 
            // lblCoachID
            // 
            this.lblCoachID.AutoSize = true;
            this.lblCoachID.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCoachID.Location = new System.Drawing.Point(230, 26);
            this.lblCoachID.Name = "lblCoachID";
            this.lblCoachID.Size = new System.Drawing.Size(88, 45);
            this.lblCoachID.TabIndex = 43;
            this.lblCoachID.Text = "[???]";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(30, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 45);
            this.label2.TabIndex = 42;
            this.label2.Text = "Coach ID :";
            // 
            // llShowSpelazationInfo
            // 
            this.llShowSpelazationInfo.AutoSize = true;
            this.llShowSpelazationInfo.Location = new System.Drawing.Point(719, 171);
            this.llShowSpelazationInfo.Name = "llShowSpelazationInfo";
            this.llShowSpelazationInfo.Size = new System.Drawing.Size(169, 20);
            this.llShowSpelazationInfo.TabIndex = 41;
            this.llShowSpelazationInfo.TabStop = true;
            this.llShowSpelazationInfo.Text = "Show Spelazation Info";
            this.llShowSpelazationInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llShowSpelazationInfo_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(321, 45);
            this.label1.TabIndex = 40;
            this.label1.Text = "Coach Spelaztions :";
            // 
            // cbSpezalation
            // 
            this.cbSpezalation.BackColor = System.Drawing.Color.Transparent;
            this.cbSpezalation.BorderRadius = 15;
            this.cbSpezalation.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbSpezalation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSpezalation.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbSpezalation.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbSpezalation.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cbSpezalation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cbSpezalation.ItemHeight = 30;
            this.cbSpezalation.Location = new System.Drawing.Point(128, 162);
            this.cbSpezalation.Name = "cbSpezalation";
            this.cbSpezalation.Size = new System.Drawing.Size(565, 36);
            this.cbSpezalation.TabIndex = 39;
            // 
            // tcCoachInfo
            // 
            this.tcCoachInfo.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tcCoachInfo.Controls.Add(this.tpPersonInfo);
            this.tcCoachInfo.Controls.Add(this.tpCoachInfo);
            this.tcCoachInfo.ItemSize = new System.Drawing.Size(180, 40);
            this.tcCoachInfo.Location = new System.Drawing.Point(0, 72);
            this.tcCoachInfo.Name = "tcCoachInfo";
            this.tcCoachInfo.SelectedIndex = 0;
            this.tcCoachInfo.Size = new System.Drawing.Size(1123, 581);
            this.tcCoachInfo.TabButtonHoverState.BorderColor = System.Drawing.Color.Empty;
            this.tcCoachInfo.TabButtonHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.tcCoachInfo.TabButtonHoverState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tcCoachInfo.TabButtonHoverState.ForeColor = System.Drawing.Color.White;
            this.tcCoachInfo.TabButtonHoverState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.tcCoachInfo.TabButtonIdleState.BorderColor = System.Drawing.Color.Empty;
            this.tcCoachInfo.TabButtonIdleState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.tcCoachInfo.TabButtonIdleState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tcCoachInfo.TabButtonIdleState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(160)))), ((int)(((byte)(167)))));
            this.tcCoachInfo.TabButtonIdleState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.tcCoachInfo.TabButtonSelectedState.BorderColor = System.Drawing.Color.Empty;
            this.tcCoachInfo.TabButtonSelectedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(37)))), ((int)(((byte)(49)))));
            this.tcCoachInfo.TabButtonSelectedState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tcCoachInfo.TabButtonSelectedState.ForeColor = System.Drawing.Color.White;
            this.tcCoachInfo.TabButtonSelectedState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(132)))), ((int)(((byte)(255)))));
            this.tcCoachInfo.TabButtonSize = new System.Drawing.Size(180, 40);
            this.tcCoachInfo.TabIndex = 40;
            this.tcCoachInfo.TabMenuBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            // 
            // frmAddUpdateCoaches
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.ClientSize = new System.Drawing.Size(1153, 710);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tcCoachInfo);
            this.Controls.Add(this.lblTitel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAddUpdateCoaches";
            this.Load += new System.EventHandler(this.frmAddUpdateCoach_Load);
            this.tpPersonInfo.ResumeLayout(false);
            this.tpCoachInfo.ResumeLayout(false);
            this.tpCoachInfo.PerformLayout();
            this.tcCoachInfo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button btnClose;
        private System.Windows.Forms.Label lblTitel;
        private Guna.UI2.WinForms.Guna2TabControl tcCoachInfo;
        private System.Windows.Forms.TabPage tpPersonInfo;
        private People.Controls.ctrlPersonCardWithFilter ctrlPersonCardWithFilter1;
        private Guna.UI2.WinForms.Guna2Button btnNext;
        private System.Windows.Forms.TabPage tpCoachInfo;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private System.Windows.Forms.LinkLabel llShowSpelazationInfo;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2ComboBox cbSpezalation;
        private System.Windows.Forms.Label lblCoachID;
        private System.Windows.Forms.Label label2;
    }
}