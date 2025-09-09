using Guna.UI2.WinForms;
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

namespace GYM_MS.Coaches.Controls
{
    public partial class ctrlChoachCardWithFilter : UserControl
    {

        public ctrlChoachCardWithFilter()
        {
            InitializeComponent();
        }

        private bool _FilterEnabled = true;
        private int _CoachID = -1;

        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set
            {
                _FilterEnabled = value;
                groupBox1.Enabled = _FilterEnabled;
            }
        }

        public ctrlCoachCard ctrlCoachCard
        {
            get { return ctrlCoachCard1; }
        }

        public class CoachSelectedEventArgs : EventArgs
        {
            public clsCoach CoachInfo { get; }

            public CoachSelectedEventArgs(clsCoach coachInfo)
            {
                CoachInfo = coachInfo;
            }
        }

        public event EventHandler<CoachSelectedEventArgs> OnCoachSelected;
        protected virtual void RaiseOnCoachSelected(CoachSelectedEventArgs e)
        {
            OnCoachSelected?.Invoke(this, e);
        }

        public int SelectedCoachID
        {
            get { return _CoachID; }
        }

        public void LoadCoachInfo(int coachID)
        {
            _CoachID = coachID;

            cbFilterBy.SelectedIndex = 1; // Coach ID
            txtFilterBy.Text = _CoachID.ToString();

            ctrlCoachCard1.LoadCoachInfo(_CoachID);
        }

        private void OnAddNewCoach(object sender, int coachID)
        {
            _CoachID = coachID;
            ctrlCoachCard1.LoadCoachInfo(_CoachID);

            cbFilterBy.SelectedIndex = 1;
            txtFilterBy.Text = _CoachID.ToString();

            RaiseOnCoachSelected(new CoachSelectedEventArgs(clsCoach.Find(_CoachID)));
        }

        private void btnAddNewCoach_Click(object sender, EventArgs e)
        {
            frmAddUpdateCoaches frmAdd = new frmAddUpdateCoaches();
            frmAdd.DataBack += OnAddNewCoach;
            frmAdd.ShowDialog();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 0)
            {
                txtFilterBy.Enabled = false;
            }
            else
            {
                txtFilterBy.Enabled = true;
                txtFilterBy.Text = "";
                txtFilterBy.Focus();
            }
        }

        public void Foucs()
        {
            txtFilterBy.Focus();
        }

        private void FindNow()
        {
            switch (cbFilterBy.Text)
            {
                case "None":
                    break;

                case "Coach ID":
                    if (int.TryParse(txtFilterBy.Text, out int coachID))
                    {
                        ctrlCoachCard1.LoadCoachInfo(coachID);
                        _CoachID = coachID;
                    }
                    break;

                case "Person ID":
                    if (int.TryParse(txtFilterBy.Text, out int personID))
                    {
                        var coach = clsCoach.FindByPersonID(personID);
                        if (coach != null)
                        {
                            ctrlCoachCard1.LoadCoachInfo(coach.CoachID);
                            _CoachID = coach.CoachID;
                        }
                    }
                    break;

                default:
                    break;
            }

            if (ctrlCoachCard1.SelectedCoachInfo != null)
            {
                RaiseOnCoachSelected(new CoachSelectedEventArgs(ctrlCoachCard1.SelectedCoachInfo));
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FindNow();
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            Guna2TextBox Temp = ((Guna2TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }
        }
    }



}
