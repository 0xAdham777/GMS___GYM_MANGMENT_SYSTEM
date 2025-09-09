using Guna.UI2.WinForms;
using GYM_BuisnessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters;

namespace GYM_MS.Users
{
    public partial class frmAddUpdateUser : Form
    {

        public delegate void DataBackEventHandler(object sender, int UserID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;



        private enum enMode { AddNew =0 , Update =1}
        private enMode _Mode = enMode.AddNew;

        private int _UserID = -1;

        private clsUser _UserInfo ;

        public frmAddUpdateUser()
        {
            InitializeComponent();
            
        }

        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            _Mode = enMode.Update;  
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {

            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
            Guna2TextBox Temp = ((Guna2TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                //e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }

        }


        private void ValidateUserNameExsist(object sender, CancelEventArgs e)
        {

            ValidateEmptyTextBox(sender, e);    

            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
            Guna2TextBox Temp = ((Guna2TextBox)sender);
            if (clsUser.IsExist(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This User Name Are Realy Exsist!!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }

        }


        private void ValidatePassword(object sender, CancelEventArgs e)
        {

            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
            Guna2TextBox Temp = ((Guna2TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                //e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }

            // Second We Valid the Password in txt 1 is Elso In txt2

            if (txtConfirmePassword.Text != txtPassword.Text)
            {
                //e.Cancel = true;
                errorProvider1.SetError(Temp, "The Password Does Not Match");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }


        }


        private void _FillWithDefaultValues()
        {
            txtConfirmePassword.Enabled = true;
            txtPassword.Enabled = true;
            tcUserInfo.TabPages[1].Enabled = false;


            btnNext.Enabled = false;
            ctrlPersonCardWithFilter1.FilterEnabled = true;
            lblUserID.Text = "[????]";
            txtConfirmePassword.Text = "";
            txtPassword.Text = "";
            txtUserName.Text = "";
        }


        private void _DefaultValues()
        {

            _FillWithDefaultValues();


            if (_Mode == enMode.AddNew)
            {
                ctrlPersonCardWithFilter1.Focus();
                lblTitel.Text = "Add New User";

                _UserInfo = new clsUser();

            }
            else
            {
                _UserInfo = clsUser.Find(_UserID);
                lblTitel.Text = "Update User";
                btnNext.Enabled = true;
                txtConfirmePassword.Enabled = false;
                txtPassword.Enabled = false;

                ctrlPersonCardWithFilter1.LoadPersonData(_UserInfo.PersonID);

            }
        }

        private void _LoadData()
        {

            if (_UserInfo == null)
            {
                MessageBox.Show("No Person with ID = " + _UserID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            ctrlPersonCardWithFilter1.ctrlPersonCard.LoadPersonInfo(_UserInfo.PersonID);
            ctrlPersonCardWithFilter1.FilterEnabled = false;


            lblUserID.Text = _UserInfo.UserID.ToString();
            txtUserName.Text = _UserInfo.UserName;
            chkIsActive.Checked = _UserInfo.IsActive;
            txtPassword.Text = "***********";
            txtConfirmePassword.Text = "***********";
        
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.AddNew &&  clsUser.IsExistByPersonID(ctrlPersonCardWithFilter1.SelectedPersonID))
            {
                MessageBox.Show("Error: Data Is not Saved Successfully Chose Another Person This IsExsist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            tcUserInfo.TabPages[1].Enabled = true;
            tcUserInfo.SelectedTab = tcUserInfo.TabPages[1];
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(object sender, People.Controls.ctrlPersonCardWithFilter.PersonSelectedEventArgs e)
        {
            btnNext.Enabled = true;
 
        }

        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            _DefaultValues();
            if (_Mode == enMode.Update)
            {
                _LoadData();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            _UserInfo.PersonID = ctrlPersonCardWithFilter1.SelectedPersonID;
            _UserInfo.UserName = txtUserName.Text;
            _UserInfo.Password = txtPassword.Text;
            _UserInfo.IsActive = chkIsActive.Checked;
           



            if (_UserInfo.Save())
            {
                lblUserID.Text = _UserInfo.UserID.ToString();
                //change form mode to update.
                _Mode = enMode.Update;
                lblTitel.Text = "Update User";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);


                // Trigger the event to send data back to the caller form.
                DataBack?.Invoke(this, _UserInfo.UserID);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);



        }
    }
}
