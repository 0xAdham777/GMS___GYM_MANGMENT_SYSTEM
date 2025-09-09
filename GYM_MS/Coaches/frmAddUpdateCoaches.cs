using GYM_BuisnessLayer;
using GYM_BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GYM_MS;
using GYM_MS.Coach_Spelazations;

namespace GYM_MS.Coaches
{
    public partial class frmAddUpdateCoaches : Form
    {
        public frmAddUpdateCoaches()
        {
            InitializeComponent();
        }

        private void _LoadAllSpelazation()
        {

            DataTable dt = clsCoachSpezalations.GetAll();

            foreach (DataRow dr in dt.Rows)
            {
                cbSpezalation.Items.Add(dr[1].ToString());
            }

            if (dt.Rows.Count > 0)
            {
                cbSpezalation.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("You Have To Add Coach Spelazation First Type", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }


        }


        public delegate void DataBackEventHandler(object sender, int coachID);
        public event DataBackEventHandler DataBack;

        private enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;

        private int _CoachID = -1;
        private clsCoach _CoachInfo;

       
        public frmAddUpdateCoaches(int coachID)
        {
            InitializeComponent();
            _CoachID = coachID;
            _Mode = enMode.Update;
        }

        private void _FillWithDefaultValues()
        {

            _LoadAllSpelazation();

            lblCoachID.Text = "[????]";


            tcCoachInfo.TabPages[1].Enabled = false;

            ctrlPersonCardWithFilter1.FilterEnabled = true;

            if (_Mode == enMode.AddNew)
            {
                lblTitel.Text = "Add New Coach";
                _CoachInfo = new clsCoach();
            }
            else
            {
                lblTitel.Text = "Update Coach";
                _CoachInfo = clsCoach.Find(_CoachID);
                tcCoachInfo.TabPages[1].Enabled = true;
                btnNext.Enabled = true;
            }
        }

        private void _LoadData()
        {
            if (_CoachInfo == null)
            {
                MessageBox.Show("No Coach with ID = " + _CoachID, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            ctrlPersonCardWithFilter1.LoadPersonData(_CoachInfo.PersonID);
            ctrlPersonCardWithFilter1.FilterEnabled = false;

            if (!clsCoachSpezalations.IsExist(_CoachInfo.CoachSpezalationsID))
            {
                MessageBox.Show("No Spezalations with ID = " + _CoachInfo.CoachSpezalationsID, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }


            lblCoachID.Text = _CoachInfo.CoachID.ToString();
            cbSpezalation.SelectedValue = _CoachInfo.CoachSpezalationsInfo.SpelaztionsName;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (clsCoach.IsExistByPersonID(ctrlPersonCardWithFilter1.SelectedPersonID))
            {
                MessageBox.Show("This person is already assigned as a Coach!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            tcCoachInfo.TabPages[1].Enabled = true;
            tcCoachInfo.SelectedTab = tcCoachInfo.TabPages[1];
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _CoachInfo.PersonID = ctrlPersonCardWithFilter1.SelectedPersonID;
            _CoachInfo.CoachSpezalationsID = clsCoachSpezalations.Find(cbSpezalation.Text).CoachSpezalationsID;

            if (_CoachInfo.Save())
            {
                lblCoachID.Text = _CoachInfo.CoachID.ToString();
                _Mode = enMode.Update;
                lblTitel.Text = "Update Coach";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DataBack?.Invoke(this, _CoachInfo.CoachID);
            }
            else
            {
                MessageBox.Show("Error: Data not saved!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddUpdateCoach_Load(object sender, EventArgs e)
        {
            _FillWithDefaultValues();

            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(object sender, People.Controls.ctrlPersonCardWithFilter.PersonSelectedEventArgs e)
        {
            btnNext.Enabled = true;
        }

        private void llShowSpelazationInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowSpelazationInfo frmShowSpelazationInfo = new frmShowSpelazationInfo(clsCoachSpezalations.Find(cbSpezalation.Text).CoachSpezalationsID);
            frmShowSpelazationInfo.ShowDialog();


        }
    }
}
