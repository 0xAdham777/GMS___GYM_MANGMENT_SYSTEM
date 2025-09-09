using Guna.UI2.WinForms;
using GYM_BuisnessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GYM_MS.Coach_Spelazations
{
    public partial class frmAddUpdateSpelazation : Form
    {
        public frmAddUpdateSpelazation()
        {
            InitializeComponent();
        }

        public frmAddUpdateSpelazation(int SpelazationID)
        {
            InitializeComponent();
            _SpezalationID = SpelazationID;
            _Mode = enMode.Update;
        }



        private int _SpezalationID = -1;
        private clsCoachSpezalations _SpezalationInfo;
        private enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;

      

        private void _ResetDefaults()
        {
            if (_Mode == enMode.AddNew)
            {
                lblTitel.Text = "Add New Spezalation";
                _SpezalationInfo = new clsCoachSpezalations();
            }
            else
                lblTitel.Text = "Update Spezalation";

            txtSpezalationName.Text = "";
            txtSpezalationDescription.Text = "";
            lblSpezalationID.Text = "????";
        }

        private void _LoadData()
        {
            _SpezalationInfo = clsCoachSpezalations.Find(_SpezalationID);

            if (_SpezalationInfo == null)
            {
                MessageBox.Show("No Spezalation with ID = " + _SpezalationID, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            lblSpezalationID.Text = _SpezalationInfo.CoachSpezalationsID.ToString();
            txtSpezalationName.Text = _SpezalationInfo.SpelaztionsName;
            txtSpezalationDescription.Text = _SpezalationInfo.Description;
        }


        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {

            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
            Guna2TextBox Temp = ((Guna2TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }



            if (clsCoachSpezalations.IsExistByName(txtSpezalationName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This SpezalationName is Exsist !");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Validation Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _SpezalationInfo.SpelaztionsName = txtSpezalationName.Text.Trim();
            _SpezalationInfo.Description = txtSpezalationDescription.Text.Trim();

            if (_SpezalationInfo.Save())
            {
                lblSpezalationID.Text = _SpezalationInfo.CoachSpezalationsID.ToString();
                _Mode = enMode.Update;
                lblTitel.Text = "Update Spezalation";
                MessageBox.Show("Saved Successfully!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Not Saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void frmAddUpdateSpelazation_Load(object sender, EventArgs e)
        {
            _ResetDefaults();
            if (_Mode == enMode.Update)
            {
                _LoadData();
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();



    }
}
