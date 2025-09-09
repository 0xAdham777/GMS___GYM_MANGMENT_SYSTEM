using GYM_BusinessLayer;
using GYM_MS.Members.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GYM_MS.Attendance
{
    public partial class frmAddUpdateAttandec : Form
    {
        private int _AttendanceID = -1;
        private clsAttendance _Attendance;

        private enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;

        public frmAddUpdateAttandec()
        {
            InitializeComponent();
        }

        public frmAddUpdateAttandec(int AttendanceID)
        {
            InitializeComponent();
            _AttendanceID = AttendanceID;
            _Mode = enMode.Update;
        }


        private void _FillWithDefaultValues()
        {
            lblAttendanceID.Text = "[???]";
            lblAttendanceDate.Text = DateTime.Now.ToShortDateString();
            lblAttendanceCheakInTime.Text = "[???]";

            if (_Mode == enMode.AddNew)
            {
                lblTitel.Text = "Add New Attendance";

                _Attendance = new clsAttendance();

            }
            else
            {
                lblTitel.Text = "Update Attendance";

                

            }


        }

        private void _LoadData()
        {
            ctrlMembersCardWithFilter1.LoadMemberInfo(_Attendance.MemberID);

            lblAttendanceID.Text = _Attendance.AttendanceID.ToString();
            lblAttendanceDate.Text = _Attendance.Date.ToShortDateString();
            lblAttendanceCheakInTime.Text = _Attendance.CheckInTime.ToString();
            txtNotes.Text = string.IsNullOrEmpty(_Attendance.Notes) ? "No Notes" : _Attendance.Notes;
        }

        public void LoadAttendanceInfo(int AttendanceID)
        {
            _Attendance = clsAttendance.Find(AttendanceID);
            if (_Attendance == null)
            {
                MessageBox.Show($"No Attendance found with ID {AttendanceID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _FillWithDefaultValues();
                return;
            }
            _LoadData();
        }

        private void ctrlMembersCardWithFilter1_OnMemberSelected(object sender, Members.Controls.ctrlMembersCardWithFilter.MemberSelectedEventArgs e)
        {
            ctrlMembersCardWithFilter1.FilterEnabled = false;

            btnSave.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _Attendance.MemberID = ctrlMembersCardWithFilter1.SelectedMemberID;
            _Attendance.Date = DateTime.Now;
            _Attendance.CheckInTime = DateTime.Now.TimeOfDay;
            _Attendance.Notes = txtNotes.Text;

            if (_Attendance.Save())
            {
                lblAttendanceCheakInTime.Text = _Attendance.CheckInTime.ToString();
                lblAttendanceID.Text = _Attendance.AttendanceID.ToString();


                MessageBox.Show("Attendance Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblAttendanceID.Text = _Attendance.AttendanceID.ToString();
            }
            else
            {
                MessageBox.Show("Error saving Attendance", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddUpdateAttandec_Load(object sender, EventArgs e)
        {
            _FillWithDefaultValues();

            if (_Mode == enMode.Update)
            {
                LoadAttendanceInfo(_AttendanceID);
            }
        }
    }
}
