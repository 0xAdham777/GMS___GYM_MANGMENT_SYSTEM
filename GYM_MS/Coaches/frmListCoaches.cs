using GYM_BusinessLayer;
using GYM_MS.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GYM_MS.Coaches
{
    public partial class frmListCoaches : Form
    {

        private DataTable _coachesTable;

        private void _RefrachCoachesList()
        {
            // نجلب كل المدربين من قاعدة البيانات
            _coachesTable = clsCoach.GetAll();

            if (_coachesTable == null || _coachesTable.Columns.Count == 0)
            {
                _coachesTable = new DataTable(); // جدول فارغ
            }

            dgvListCoaches.DataSource = _coachesTable;
        }


        public frmListCoaches()
        {
            InitializeComponent();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (_coachesTable == null)
                return;

            string filterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "Coach ID":
                    filterColumn = "CoachID";
                    break;
                case "Person ID":
                    filterColumn = "PersonID";
                    break;
                case "Spezalations ID":
                    filterColumn = "CoachSpezalationsID";
                    break;
                case "Full Name":
                    filterColumn = "FullName";
                    break;
                case "Phone Number":
                    filterColumn = "PhoneNumber";
                    break;
                case "Spezalations Name":
                    filterColumn = "SpezalationsName";
                    break;
                case "Spezalations Description":
                    filterColumn = "Description";
                    break;
                default:
                    dgvListCoaches.DataSource = _coachesTable;
                    return;
            }

            DataView dv = _coachesTable.DefaultView;

            if (filterColumn == "CoachID" || filterColumn == "PersonID" || filterColumn == "CoachSpezalationsID")
            {
                if (int.TryParse(txtFilterValue.Text, out int id))
                    dv.RowFilter = $"{filterColumn} = {id}";
                else
                    dv.RowFilter = "1=0";
            }
            else
            {
                if (clsGlobal.IsValidFilter(txtFilterValue.Text))
                    dv.RowFilter = $"{filterColumn} LIKE '%{txtFilterValue.Text.Replace("'", "''")}%' ";
                else
                    dv.RowFilter = "1=0";
            }

            dgvListCoaches.DataSource = dv;
            lblNumberOfRecord.Text = dgvListCoaches.RowCount.ToString();
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

        private void frmListCoaches_Load(object sender, EventArgs e)
        {
            _RefrachCoachesList();
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            lblNumberOfRecord.Text = dgvListCoaches.Rows.Count.ToString();

            if (dgvListCoaches.Rows.Count > 0)
            {
                dgvListCoaches.Columns[0].HeaderText = "Coach ID";
                dgvListCoaches.Columns[0].Width = 100;

                dgvListCoaches.Columns[1].HeaderText = "Person ID";
                dgvListCoaches.Columns[1].Width = 100;

                dgvListCoaches.Columns[2].HeaderText = "Full Name";
                dgvListCoaches.Columns[2].Width = 200;

                dgvListCoaches.Columns[3].HeaderText = "Phone Number";
                dgvListCoaches.Columns[3].Width = 120;

                dgvListCoaches.Columns[4].HeaderText = "Spezalation ID";
                dgvListCoaches.Columns[4].Width = 100;

                dgvListCoaches.Columns[5].HeaderText = "Spezalation Name";
                dgvListCoaches.Columns[5].Width = 200;

                dgvListCoaches.Columns[6].HeaderText = "Spezalation Description";
                dgvListCoaches.Columns[6].Width = 250;
            }
        }


        private void btnAddNewCoach_Click(object sender, EventArgs e)
        {
            frmAddUpdateCoaches frm = new frmAddUpdateCoaches();
            frm.ShowDialog();

            frmListCoaches_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void findMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFindCoach frmFindCoach = new frmFindCoach(); 
            frmFindCoach.ShowDialog();

            frmListCoaches_Load(null, null);
        }

        private void deleteMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this Coach [" + dgvListCoaches.CurrentRow.Cells[0].Value + "]?", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsCoach.Delete((int)dgvListCoaches.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Coach Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefrachCoachesList();
                }
                else
                    MessageBox.Show("Coach was not deleted because it has linked data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            frmListCoaches_Load(null, null);
        }

        private void editMemberInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateCoaches frm = new frmAddUpdateCoaches((int)dgvListCoaches.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmListCoaches_Load(null, null);
        }

        private void addNewMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateCoaches frm = new frmAddUpdateCoaches();
            frm.ShowDialog();

            frmListCoaches_Load(null, null);
        }

        private void showMemberInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowCoachInfo frm = new frmShowCoachInfo((int)dgvListCoaches.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmListCoaches_Load(null, null);
        }

        private void dgvListCoaches_DoubleClick(object sender, EventArgs e)
        {
            frmShowCoachInfo frm = new frmShowCoachInfo((int)dgvListCoaches.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmListCoaches_Load(null, null);
        }
    }
}
