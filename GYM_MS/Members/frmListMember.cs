using GYM_BuisnessLayer;
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

namespace GYM_MS.Members
{
    public partial class frmListMember : Form
    {

        private DataTable _membersTable;

        private void _RefrachMemberList()
        {
            // نجلب كل الأعضاء من قاعدة البيانات
            _membersTable = clsMember.GetAllMemberInfo();

            if (_membersTable == null || _membersTable.Columns.Count == 0)
            {
                _membersTable = new DataTable(); // جدول فارغ عشان ما يكسرش الكود
            }

            dgvListMembers.DataSource = _membersTable;
        }



        public frmListMember()
        {
            InitializeComponent();
        }

        private void btnAddNewMember_Click(object sender, EventArgs e)
        {
            frmAddUpdateMember frmAddUpdateMember = new frmAddUpdateMember();
            frmAddUpdateMember.ShowDialog();

            frmListMember_Load(null,null);
        }

        private void frmListMember_Load(object sender, EventArgs e)
        {


            _RefrachMemberList();
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            lblNumberOfRecord.Text = dgvListMembers.Rows.Count.ToString();

            if (dgvListMembers.Rows.Count > 0)
            {
                dgvListMembers.Columns[0].HeaderText = "Member ID";
                dgvListMembers.Columns[0].Width = 100;

                dgvListMembers.Columns[1].HeaderText = "Person ID";
                dgvListMembers.Columns[1].Width = 100;

                dgvListMembers.Columns[2].HeaderText = "Full Name";
                dgvListMembers.Columns[2].Width = 200;

                dgvListMembers.Columns[3].HeaderText = "Phone Number";
                dgvListMembers.Columns[3].Width = 120;

                dgvListMembers.Columns[4].HeaderText = "Start Date";
                dgvListMembers.Columns[4].Width = 120;

                dgvListMembers.Columns[5].HeaderText = "End Date";
                dgvListMembers.Columns[5].Width = 120;

                dgvListMembers.Columns[6].HeaderText = "Remaining Days";
                dgvListMembers.Columns[6].Width = 100;

                dgvListMembers.Columns[7].HeaderText = "Status";
                dgvListMembers.Columns[7].Width = 100;

                dgvListMembers.Columns[8].HeaderText = "Debts";
                dgvListMembers.Columns[8].Width = 100;

                dgvListMembers.Columns[9].HeaderText = "Subscription";
                dgvListMembers.Columns[9].Width = 150;
            }

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

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (_membersTable == null)
                return;

            string filterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "Member ID":
                    filterColumn = "MemberID";
                    break;
                case "Person ID":
                    filterColumn = "PersonID";
                    break;
                case "Full Name":
                    filterColumn = "FullName";
                    break;
                case "Phone Number":
                    filterColumn = "PhoneNumber";
                    break;
                case "Subscription Name":
                    filterColumn = "SubscriptionName";
                    break;
                case "Status":
                    filterColumn = "Status";
                    break;

                default:
                    dgvListMembers.DataSource = _membersTable;
                    return;
            }

            DataView dv = _membersTable.DefaultView;

            if (filterColumn == "MemberID" || filterColumn == "PersonID")
            {
                if (int.TryParse(txtFilterValue.Text, out int id))
                    dv.RowFilter = $"{filterColumn} = {id}";
                else
                    dv.RowFilter = "1=0";
            }
            else
            {
                if (clsGlobal.IsValidFilter(txtFilterValue.Text))
                    dv.RowFilter = $"{filterColumn} LIKE '%{txtFilterValue.Text.Replace("'", "''")}%'";
                else
                    dv.RowFilter = "1=0";
            }

            dgvListMembers.DataSource = dv;
            lblNumberOfRecord.Text = dgvListMembers.RowCount.ToString();

        }

        private void addNewMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateMember frmAddMember = new frmAddUpdateMember();
            frmAddMember.ShowDialog();

            frmListMember_Load(null,null);
        }

        private void editMemberInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateMember frmAddMember = new frmAddUpdateMember((int)dgvListMembers.CurrentRow.Cells[0].Value);
            frmAddMember.ShowDialog();

            frmListMember_Load(null, null);
        }

        private void deleteMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to delete Member [" + dgvListMembers.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)

            {

                //Perform Delele and refresh
                if (clsMember.Delete((int)dgvListMembers.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Member Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefrachMemberList();
                }

                else
                    MessageBox.Show("Member was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            frmListMember_Load(null, null);
        }

        private void showMemberInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowMemberInfo frm = new frmShowMemberInfo((int)dgvListMembers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmListMember_Load(null, null);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvListMembers_DoubleClick(object sender, EventArgs e)
        {
            frmShowMemberInfo frmShowMemberInfo = new frmShowMemberInfo((int)dgvListMembers.CurrentRow.Cells[0].Value);
            frmShowMemberInfo.ShowDialog();

            frmListMember_Load(null, null);
        }

        private void findMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFindMember frmFindMember = new frmFindMember();
            frmFindMember.ShowDialog();

            frmListMember_Load(null, null);

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAddUpdateMember frmAddMember = new frmAddUpdateMember((int)dgvListMembers.CurrentRow.Cells[0].Value);
            frmAddMember.ShowDialog();

            frmListMember_Load(null, null);
        }

        private void cmsListMembers_Opening(object sender, CancelEventArgs e)
        {
            if ((string)dgvListMembers.CurrentRow.Cells["Status"].Value == "Not Active")
            {
                tmsRenew.Enabled = true;
            }
            else
            {
                tmsRenew.Enabled = false;
            }
        }
    }
}
