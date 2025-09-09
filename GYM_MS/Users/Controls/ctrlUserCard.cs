using GYM_BuisnessLayer;
using GYM_MS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GYM_MS.Users.Controls
{
    public partial class ctrlUserCard : UserControl
    {


        private int _UserID = -1;
        private clsUser _User;

        public clsUser SelectedUserInfo
        {
            get { return _User; }
        }


        public int SelectedUserID
        {
            get { return _UserID; }
        }

        private void _FillWithDefaultValue()
        {

            lblIsActive.Text = "[????]";
            lblUserName.Text = "[????]";
            lblUserID.Text = "[????]";
        }


     


        private void _LoadData()
        {

            lblIsActive.Text = (_User.IsActive) ? "Yes" : "No";
            lblUserName.Text = _User.UserName;
            lblUserID.Text = _User.UserID.ToString();

            ctrlPersonCard1.LoadPersonInfo(_User.PersonID);
        }


      

        public void LoadUserInfo(int UserID)
        {
            _UserID = UserID;
            _User = clsUser.Find(_UserID);

            if (_User == null)
            {
                MessageBox.Show($"This User With Id {UserID} IS Not Exsist", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _FillWithDefaultValue();
                return;
            }

            _LoadData();


        }

    

        public ctrlUserCard()
        {
            InitializeComponent();
        }

        private void llEditUserInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser(_UserID);
            frm.ShowDialog();

            // for refrach
            LoadUserInfo(_UserID);
        }
    }
}
