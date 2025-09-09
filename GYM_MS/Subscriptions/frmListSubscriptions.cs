using GYM_BuisnessLayer;
using GYM_MS.Global;
using GYM_MS.Members;
using GYM_MS.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GYM_MS.Subscriptions
{
    public partial class frmListSubscriptions : Form
    {
        public frmListSubscriptions()
        {
            InitializeComponent();
        }


        private DataTable _subscriptionsTable;

        private void _RefreshSubscriptionsList()
        {
            _subscriptionsTable = clsSubscriptions.GetAllSubscriptions();

            if (_subscriptionsTable == null || _subscriptionsTable.Columns.Count == 0)
                _subscriptionsTable = new DataTable();

            dgvListSubscriptions.DataSource = _subscriptionsTable;
        }

   
        private void frmListSubscriptions_Load(object sender, EventArgs e)
        {
            _RefreshSubscriptionsList();
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            lblNumberOfRecord.Text = dgvListSubscriptions.Rows.Count.ToString();

            if (dgvListSubscriptions.Rows.Count > 0)
            {
                dgvListSubscriptions.Columns[0].HeaderText = "Subscription ID";
                dgvListSubscriptions.Columns[0].Width = 100;

                dgvListSubscriptions.Columns[1].HeaderText = "Member ID";
                dgvListSubscriptions.Columns[1].Width = 100;

                dgvListSubscriptions.Columns[2].HeaderText = "Person ID";
                dgvListSubscriptions.Columns[2].Width = 100;

                dgvListSubscriptions.Columns[3].HeaderText = "Full Name";
                dgvListSubscriptions.Columns[3].Width = 200;

                dgvListSubscriptions.Columns[4].HeaderText = "Phone Number";
                dgvListSubscriptions.Columns[4].Width = 100;

                dgvListSubscriptions.Columns[5].HeaderText = "Create User ID";
                dgvListSubscriptions.Columns[5].Width = 100;

                dgvListSubscriptions.Columns[6].HeaderText = "Subscription Type ID";
                dgvListSubscriptions.Columns[6].Width = 100;

                dgvListSubscriptions.Columns[7].HeaderText = "Subscription Name";
                dgvListSubscriptions.Columns[7].Width = 150;

                dgvListSubscriptions.Columns[8].HeaderText = "Subscription Date";
                dgvListSubscriptions.Columns[8].Width = 150;

                dgvListSubscriptions.Columns[9].HeaderText = "Start Date";
                dgvListSubscriptions.Columns[9].Width = 150;

                dgvListSubscriptions.Columns[10].HeaderText = "End Date";
                dgvListSubscriptions.Columns[10].Width = 150;
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
            if (_subscriptionsTable == null)
                return;

            string filterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "Subscription ID": filterColumn = "SubscriptionID"; break;
                case "Member ID": filterColumn = "MemberID"; break;
                case "Person ID": filterColumn = "PersonID"; break;
                case "Full Name": filterColumn = "FullName"; break;
                case "Phone Number": filterColumn = "PhoneNumber"; break;
                case "Subscription Name": filterColumn = "SubscriptionName"; break;
                case "Create User ID": filterColumn = "CreateByUserID"; break;
                case "Subscription Type ID": filterColumn = "SubscriptionTypeID"; break;
                case "Subscription Date": filterColumn = "SubscriptionDate"; break;
                case "Start Date": filterColumn = "StartDate"; break;
                case "End Date": filterColumn = "EndDate"; break;
                default: dgvListSubscriptions.DataSource = _subscriptionsTable; return;
            }

            DataView dv = _subscriptionsTable.DefaultView;

            if (filterColumn.EndsWith("ID"))
            {
                if (int.TryParse(txtFilterValue.Text, out int id))
                    dv.RowFilter = $"{filterColumn} = {id}";
                else
                    dv.RowFilter = "1=0";
            }
            else if (filterColumn.EndsWith("Date"))
            {
                if (DateTime.TryParse(txtFilterValue.Text, out DateTime date))
                    dv.RowFilter = $"{filterColumn} = '#{date:MM/dd/yyyy}#'";
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

            dgvListSubscriptions.DataSource = dv;
            lblNumberOfRecord.Text = dgvListSubscriptions.RowCount.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deleteSubscriptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvListSubscriptions.CurrentRow == null)
                return;

            int SubscriptionID = (int)dgvListSubscriptions.CurrentRow.Cells[0].Value;

            if (MessageBox.Show($"Are you sure you want to delete Subscription [{SubscriptionID}]?",
                "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // Perform Delete and refresh
                if (clsPayments.DeletePayment(SubscriptionID))
                {
                    MessageBox.Show("Subscription Deleted Successfully.",
                        "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _RefreshSubscriptionsList(); // إعادة تحميل القائمة
                }
                else
                {
                    MessageBox.Show("Subscription was not deleted because it has linked data.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            frmListSubscriptions_Load(null, null); // لتحديث الفورم بالكامل
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (dgvListSubscriptions.CurrentRow != null)
            {
                int memberID = Convert.ToInt32(dgvListSubscriptions.CurrentRow.Cells["MemberID"].Value);
                frmShowMemberInfo frmShowMemberInfo = new frmShowMemberInfo(memberID);
                frmShowMemberInfo.ShowDialog();
            }
        }

        private void showSubscriptionsInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowSubscriptionInfo frmShowSubscriptionInfo = new frmShowSubscriptionInfo(Convert.ToInt32(dgvListSubscriptions.CurrentRow.Cells[0].Value));
            frmShowSubscriptionInfo.ShowDialog();

            frmListSubscriptions_Load(null, null);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dgvListSubscriptions.CurrentRow != null)
            {
                int userID = Convert.ToInt32(dgvListSubscriptions.CurrentRow.Cells["CreateByUserID"].Value);
                frmShowUserInfo frmShowUserInfo = new frmShowUserInfo(userID);
                frmShowUserInfo.ShowDialog();
            }

        }
    }
}
