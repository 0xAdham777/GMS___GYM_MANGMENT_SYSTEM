namespace GYM_MS.Coaches.Controls
{
    partial class ctrlCoachCard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ctrlPersonCard1 = new GYM_MS.People.Controls.ctrlPersonCard();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ef = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCoachID = new System.Windows.Forms.Label();
            this.lblCoachSpelazationName = new System.Windows.Forms.Label();
            this.llEditCoachInfo = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrlPersonCard1
            // 
            this.ctrlPersonCard1.Location = new System.Drawing.Point(0, 0);
            this.ctrlPersonCard1.Name = "ctrlPersonCard1";
            this.ctrlPersonCard1.Size = new System.Drawing.Size(860, 433);
            this.ctrlPersonCard1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.llEditCoachInfo);
            this.groupBox1.Controls.Add(this.lblCoachSpelazationName);
            this.groupBox1.Controls.Add(this.lblCoachID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ef);
            this.groupBox1.Location = new System.Drawing.Point(9, 430);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(840, 145);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Coach Info";
            // 
            // ef
            // 
            this.ef.AutoSize = true;
            this.ef.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ef.Location = new System.Drawing.Point(6, 32);
            this.ef.Name = "ef";
            this.ef.Size = new System.Drawing.Size(130, 32);
            this.ef.TabIndex = 0;
            this.ef.Text = "Coach ID :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(310, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Coach Spelazation Name :";
            // 
            // lblCoachID
            // 
            this.lblCoachID.AutoSize = true;
            this.lblCoachID.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCoachID.Location = new System.Drawing.Point(151, 32);
            this.lblCoachID.Name = "lblCoachID";
            this.lblCoachID.Size = new System.Drawing.Size(65, 32);
            this.lblCoachID.TabIndex = 2;
            this.lblCoachID.Text = "[???]";
            // 
            // lblCoachSpelazationName
            // 
            this.lblCoachSpelazationName.AutoSize = true;
            this.lblCoachSpelazationName.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCoachSpelazationName.Location = new System.Drawing.Point(322, 85);
            this.lblCoachSpelazationName.Name = "lblCoachSpelazationName";
            this.lblCoachSpelazationName.Size = new System.Drawing.Size(65, 32);
            this.lblCoachSpelazationName.TabIndex = 3;
            this.lblCoachSpelazationName.Text = "[???]";
            // 
            // llEditCoachInfo
            // 
            this.llEditCoachInfo.AutoSize = true;
            this.llEditCoachInfo.Location = new System.Drawing.Point(713, 116);
            this.llEditCoachInfo.Name = "llEditCoachInfo";
            this.llEditCoachInfo.Size = new System.Drawing.Size(119, 20);
            this.llEditCoachInfo.TabIndex = 2;
            this.llEditCoachInfo.TabStop = true;
            this.llEditCoachInfo.Text = "Edit Coach Info";
            this.llEditCoachInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llEditCoachInfo_LinkClicked);
            // 
            // ctrlCoachCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ctrlPersonCard1);
            this.Name = "ctrlCoachCard";
            this.Size = new System.Drawing.Size(860, 583);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private People.Controls.ctrlPersonCard ctrlPersonCard1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel llEditCoachInfo;
        private System.Windows.Forms.Label lblCoachSpelazationName;
        private System.Windows.Forms.Label lblCoachID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ef;
    }
}
