namespace GYM_MS.Attendance
{
    partial class frmAddUpdateAttandec
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddUpdateAttandec));
            this.ctrlMembersCardWithFilter1 = new GYM_MS.Members.Controls.ctrlMembersCardWithFilter();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.lblAttendanceDate = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNotes = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.lblAttendanceCheakInTime = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblAttendanceID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTitel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlMembersCardWithFilter1
            // 
            this.ctrlMembersCardWithFilter1.FilterEnabled = true;
            this.ctrlMembersCardWithFilter1.Location = new System.Drawing.Point(0, 71);
            this.ctrlMembersCardWithFilter1.Name = "ctrlMembersCardWithFilter1";
            this.ctrlMembersCardWithFilter1.Size = new System.Drawing.Size(874, 886);
            this.ctrlMembersCardWithFilter1.TabIndex = 0;
            this.ctrlMembersCardWithFilter1.OnMemberSelected += new System.EventHandler<GYM_MS.Members.Controls.ctrlMembersCardWithFilter.MemberSelectedEventArgs>(this.ctrlMembersCardWithFilter1_OnMemberSelected);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.MediumTurquoise;
            this.guna2Panel1.BorderRadius = 15;
            this.guna2Panel1.Location = new System.Drawing.Point(887, 92);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(10, 854);
            this.guna2Panel1.TabIndex = 1;
            // 
            // lblAttendanceDate
            // 
            this.lblAttendanceDate.AutoSize = true;
            this.lblAttendanceDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAttendanceDate.Location = new System.Drawing.Point(944, 270);
            this.lblAttendanceDate.Name = "lblAttendanceDate";
            this.lblAttendanceDate.Size = new System.Drawing.Size(65, 26);
            this.lblAttendanceDate.TabIndex = 60;
            this.lblAttendanceDate.Text = "[???]";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(903, 214);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(302, 45);
            this.label7.TabIndex = 59;
            this.label7.Text = "Attendance Date :";
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.Image = global::GYM_MS.Properties.Resources.notes;
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(1058, 432);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(37, 38);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 58;
            this.guna2PictureBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(903, 425);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 45);
            this.label6.TabIndex = 55;
            this.label6.Text = "Notes   :";
            // 
            // txtNotes
            // 
            this.txtNotes.BorderRadius = 15;
            this.txtNotes.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNotes.DefaultText = "";
            this.txtNotes.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtNotes.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtNotes.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNotes.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtNotes.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtNotes.Location = new System.Drawing.Point(949, 475);
            this.txtNotes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.PlaceholderText = "";
            this.txtNotes.SelectedText = "";
            this.txtNotes.Size = new System.Drawing.Size(505, 415);
            this.txtNotes.TabIndex = 57;
            // 
            // btnSave
            // 
            this.btnSave.BorderRadius = 15;
            this.btnSave.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSave.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSave.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(1274, 912);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(180, 45);
            this.btnSave.TabIndex = 56;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
            this.btnClose.Location = new System.Drawing.Point(1051, 912);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(180, 45);
            this.btnClose.TabIndex = 54;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblAttendanceCheakInTime
            // 
            this.lblAttendanceCheakInTime.AutoSize = true;
            this.lblAttendanceCheakInTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAttendanceCheakInTime.Location = new System.Drawing.Point(944, 381);
            this.lblAttendanceCheakInTime.Name = "lblAttendanceCheakInTime";
            this.lblAttendanceCheakInTime.Size = new System.Drawing.Size(65, 26);
            this.lblAttendanceCheakInTime.TabIndex = 62;
            this.lblAttendanceCheakInTime.Text = "[???]";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(903, 307);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(451, 45);
            this.label2.TabIndex = 61;
            this.label2.Text = "Attendance Cheak In Time :";
            // 
            // lblAttendanceID
            // 
            this.lblAttendanceID.AutoSize = true;
            this.lblAttendanceID.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAttendanceID.Location = new System.Drawing.Point(944, 179);
            this.lblAttendanceID.Name = "lblAttendanceID";
            this.lblAttendanceID.Size = new System.Drawing.Size(65, 26);
            this.lblAttendanceID.TabIndex = 64;
            this.lblAttendanceID.Text = "[???]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(903, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(264, 45);
            this.label3.TabIndex = 63;
            this.label3.Text = "Attendance ID :";
            // 
            // lblTitel
            // 
            this.lblTitel.AutoSize = true;
            this.lblTitel.Font = new System.Drawing.Font("Ravie", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitel.ForeColor = System.Drawing.Color.Crimson;
            this.lblTitel.Location = new System.Drawing.Point(350, 13);
            this.lblTitel.Name = "lblTitel";
            this.lblTitel.Size = new System.Drawing.Size(817, 76);
            this.lblTitel.TabIndex = 65;
            this.lblTitel.Text = "Add New Attendance";
            // 
            // frmAddUpdateAttandec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1499, 964);
            this.Controls.Add(this.lblTitel);
            this.Controls.Add(this.lblAttendanceID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblAttendanceCheakInTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblAttendanceDate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.guna2PictureBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.ctrlMembersCardWithFilter1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAddUpdateAttandec";
            this.Text = "frmAddUpdateAttandec";
            this.Load += new System.EventHandler(this.frmAddUpdateAttandec_Load);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Members.Controls.ctrlMembersCardWithFilter ctrlMembersCardWithFilter1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label lblAttendanceDate;
        private System.Windows.Forms.Label label7;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private System.Windows.Forms.Label label6;
        private Guna.UI2.WinForms.Guna2TextBox txtNotes;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private Guna.UI2.WinForms.Guna2Button btnClose;
        private System.Windows.Forms.Label lblAttendanceCheakInTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblAttendanceID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTitel;
    }
}