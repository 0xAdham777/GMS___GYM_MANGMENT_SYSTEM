using GYM_MS.Attendance;
using GYM_MS.Coach_Spelazations;
using GYM_MS.Coaches;
using GYM_MS.Data_Base_Back_Up;
using GYM_MS.Global;
using GYM_MS.Login;
using GYM_MS.Members;
using GYM_MS.Payments;
using GYM_MS.People;
using GYM_MS.Subscriptions;
using GYM_MS.Subscriptions_Types;
using GYM_MS.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GYM_MS
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblUserName.Text = clsGlobal.CurrentUser.UserName;
            lblTime.Text = DateTime.Now.ToString();
        }

        private void btnMembers_Click(object sender, EventArgs e)
        {
            frmListPepol frmListPepol = new frmListPepol();
            frmListPepol.ShowDialog();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            frmListUsers frmListUsers = new frmListUsers();
            frmListUsers.ShowDialog();  
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            frmListSubscriptionsTypes frm   =   new frmListSubscriptionsTypes();
            frm.ShowDialog();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            frmListMember frmListMember = new frmListMember();  
            frmListMember.ShowDialog();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            frmListSpelazation frmListSpelazation = new frmListSpelazation();
            frmListSpelazation.ShowDialog();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            frmListCoaches frm  = new frmListCoaches();
            frm.ShowDialog();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            frmListPayments frm = new frmListPayments();
            frm.ShowDialog();
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            frmListSubscriptions frmListSubscriptions = new frmListSubscriptions();
            frmListSubscriptions.ShowDialog();
        }

        private void label2_DoubleClick(object sender, EventArgs e)
        {
            frmShowUserInfo frmShowUserInfo = new frmShowUserInfo(clsGlobal.CurrentUser.UserID);
            frmShowUserInfo.ShowDialog();
        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            frmChangePassword frmChangePassword = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frmChangePassword.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString();
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            frmDataBaseBackUp frm = new frmDataBaseBackUp();
            frm.ShowDialog();
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            frmListAttendance frm = new frmListAttendance();
            frm.ShowDialog();
        }
    }
}
