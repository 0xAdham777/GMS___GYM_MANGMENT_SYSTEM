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

namespace GYM_MS.Coach_Spelazations.Controls
{
    public partial class ctrlSpelazationCard : UserControl
    {


        private int _CoachSpecializationID = -1;
        private clsCoachSpezalations _CoachSpecializationInfo;


        public clsCoachSpezalations SelectedCoachSpecializationInfo
        {
            get { return _CoachSpecializationInfo; }
        }



        public ctrlSpelazationCard()
        {
            InitializeComponent();
        }




        private void _FillWithDefaultValue()
        {
            lblSpezalationID.Text = "[???]";
            lblSpezalationName.Text = "[???]";
            lblSpezalationDescription.Text = "[???]";
        }

        private void _LoadData()
        {
            lblSpezalationID.Text = _CoachSpecializationInfo.CoachSpezalationsID.ToString();
            lblSpezalationName.Text = _CoachSpecializationInfo.SpelaztionsName;
            lblSpezalationDescription.Text = string.IsNullOrEmpty(_CoachSpecializationInfo.Description)
                ? "[No Description]"
                : _CoachSpecializationInfo.Description;
        }

        public void LoadCoachSpecializationInfo(int specializationID)
        {
            _CoachSpecializationID = specializationID;
            _CoachSpecializationInfo = clsCoachSpezalations.Find(specializationID);

            if (_CoachSpecializationInfo == null)
            {
                MessageBox.Show($"This Specialization with ID {specializationID} does not exist",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                _FillWithDefaultValue();
                return;
            }

            _LoadData();
        }

        private void llEditSpecialization_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdateSpelazation frm = new frmAddUpdateSpelazation(_CoachSpecializationID);
            frm.ShowDialog();

            // Refresh
            LoadCoachSpecializationInfo(_CoachSpecializationID);
        }
    }
}
