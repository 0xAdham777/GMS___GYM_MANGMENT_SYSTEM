using Guna.UI2.WinForms;
using GYM_BuisnessLayer;
using GYM_BusinessLayer;
using GYM_MS.Global_Classes;
using GYM_MS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters;

namespace GYM_MS.Subscriptions_Types
{
    public partial class frmAddUpdateSubscriptionType : Form
    {
        private int _SubscriptionTypeID = -1;
        private clsSubscriptionTypes _SubscriptionTypeInfo;


        private enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;


        public frmAddUpdateSubscriptionType()
        {
            InitializeComponent();
        }
        public frmAddUpdateSubscriptionType(int SubscriptionTypeID)
        {
            InitializeComponent();
            _SubscriptionTypeID = SubscriptionTypeID;
            _Mode = enMode.Update;
        }
       

        private void _ResetDefualtValues()
        {

            if (_Mode == enMode.AddNew)
            {
                lblTitel.Text = "Add New Subscription Type";
                _SubscriptionTypeInfo = new clsSubscriptionTypes();
            }
            else
            {
                lblTitel.Text = "Update Subscription Type";
            }

          
            txtDescription.Text = "";
            txtSubscriptionName.Text = "";
            nudDurationDays.Text = "0";
            nudPrice.Text = "0.00";
            lblSubscriptionTypeID.Text = "????";


        }

        private void _LoadData()
        {

            _SubscriptionTypeInfo = clsSubscriptionTypes.Find(_SubscriptionTypeID);

            if (_SubscriptionTypeInfo == null)
            {
                MessageBox.Show("No Person with ID = " + _SubscriptionTypeID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            //the following code will not be executed if the person was not found
            lblSubscriptionTypeID.Text = _SubscriptionTypeID.ToString();
            txtSubscriptionName.Text = _SubscriptionTypeInfo.SubscriptionName;
            txtDescription.Text = _SubscriptionTypeInfo.Description ;
            nudPrice.Value = _SubscriptionTypeInfo.Price;
            nudDurationDays.Value = _SubscriptionTypeInfo.DurationDays;

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

      

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }



            _SubscriptionTypeInfo.Description = txtDescription.Text.Trim();
            _SubscriptionTypeInfo.SubscriptionName = txtSubscriptionName.Text.Trim();
            _SubscriptionTypeInfo.Price = nudPrice.Value;
            _SubscriptionTypeInfo.DurationDays = Convert.ToInt16(nudDurationDays.Value);





            if (_SubscriptionTypeInfo.Save())
            {
                lblSubscriptionTypeID.Text = _SubscriptionTypeInfo.SubscriptionTypeID.ToString();
                //change form mode to update.
                _Mode = enMode.Update;
                lblTitel.Text = "Update Subscription Type";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);



        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddUpdateSubscriptionType_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (_Mode == enMode.Update)
            {
                _LoadData();
            }

        }
    }
}
