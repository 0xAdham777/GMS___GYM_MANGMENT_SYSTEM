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

namespace GYM_MS.Attendance
{
    public partial class frmListAttendance : Form
    {


        private DataTable _attendanceTable;

        private void _RefreshAttendanceList()
        {
            _attendanceTable = clsAttendance.GetAll();
            if (_attendanceTable == null || _attendanceTable.Columns.Count == 0)
                _attendanceTable = new DataTable();

            dgvListAttendance.DataSource = _attendanceTable;
        }


        public frmListAttendance()
        {
            InitializeComponent();
        }

       
        private void btnAddNewAttendance_Click(object sender, EventArgs e)
        {
            frmAddUpdateAttandec frmAddUpdateAttandec = new frmAddUpdateAttandec();
            frmAddUpdateAttandec.ShowDialog();

            frmListAttendance_Load(null,null);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            frmAddUpdateAttandec frmAddUpdateAttandec = new frmAddUpdateAttandec((int)dgvListAttendance.CurrentRow.Cells[0].Value);
            frmAddUpdateAttandec.ShowDialog();

            frmListAttendance_Load(null,null);

        }

        private void frmListAttendance_Load(object sender, EventArgs e)
        {
            _RefreshAttendanceList();
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            lblNumberOfRecord.Text = dgvListAttendance.Rows.Count.ToString();

            if (dgvListAttendance.Rows.Count > 0)
            {
                dgvListAttendance.Columns[0].HeaderText = "Attendance ID";
                dgvListAttendance.Columns[0].Width = 100;

                dgvListAttendance.Columns[1].HeaderText = "Member ID";
                dgvListAttendance.Columns[1].Width = 100;

                dgvListAttendance.Columns[2].HeaderText = "Person ID";
                dgvListAttendance.Columns[2].Width = 100;

                dgvListAttendance.Columns[3].HeaderText = "Full Name";
                dgvListAttendance.Columns[3].Width = 200;

                dgvListAttendance.Columns[4].HeaderText = "Phone Number";
                dgvListAttendance.Columns[4].Width = 150;

                dgvListAttendance.Columns[5].HeaderText = "Date";
                dgvListAttendance.Columns[5].Width = 150;

                dgvListAttendance.Columns[6].HeaderText = "Check In Time";
                dgvListAttendance.Columns[6].Width = 150;

                dgvListAttendance.Columns[7].HeaderText = "Notes";
                dgvListAttendance.Columns[7].Width = 250;
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (_attendanceTable == null) return;

            string filterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "Attendance ID": filterColumn = "AttendanceID"; break;
                case "Member ID": filterColumn = "MemberID"; break;
                case "Person ID": filterColumn = "PersonID"; break;
                case "Full Name": filterColumn = "FullName"; break;
                case "Phone Number": filterColumn = "PhoneNumber"; break;
                case "Date": filterColumn = "Date"; break;
                default:
                    dgvListAttendance.DataSource = _attendanceTable;
                    return;
            }

            DataView dv = _attendanceTable.DefaultView;

            if (filterColumn == "AttendanceID" || filterColumn == "MemberID" || filterColumn == "PersonID")
            {
                if (int.TryParse(txtFilterValue.Text, out int id))
                    dv.RowFilter = $"{filterColumn} = {id}";
                else
                    dv.RowFilter = "1=0";
            }
            else if (filterColumn == "Date")
            {
                if (DateTime.TryParse(txtFilterValue.Text, out DateTime date))
                    dv.RowFilter = $"CONVERT([{filterColumn}], System.String) LIKE '%{date:yyyy-MM-dd}%'";
                else
                    dv.RowFilter = "1=0";
            }
            else
            {
                dv.RowFilter = $"{filterColumn} LIKE '%{txtFilterValue.Text.Replace("'", "''")}%' ";
            }

            dgvListAttendance.DataSource = dv;
            lblNumberOfRecord.Text = dgvListAttendance.RowCount.ToString();
        }

        private void addNewMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateAttandec frmAddUpdateAttandec = new frmAddUpdateAttandec();
            frmAddUpdateAttandec.ShowDialog();

            frmListAttendance_Load(null, null);
        }

        private void deleteMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvListAttendance.CurrentRow == null)
                return;

            int attendanceID = (int)dgvListAttendance.CurrentRow.Cells[0].Value;

            if (MessageBox.Show($"Are you sure you want to delete Attendance [{attendanceID}]?",
                "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsAttendance.Delete(attendanceID))
                {
                    MessageBox.Show("Attendance Deleted Successfully.",
                        "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _RefreshAttendanceList(); // إعادة تحميل القائمة
                }
                else
                {
                    MessageBox.Show("Attendance was not deleted because it has linked data.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            frmListAttendance_Load(null, null); // Refresh كامل للفورم
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
