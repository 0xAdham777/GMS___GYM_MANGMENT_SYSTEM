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

namespace GYM_MS.Payments
{
    public partial class frmListPayments : Form
    {

        private DataTable _paymentsTable;

        private void _RefreshPaymentsList()
        {
            _paymentsTable = clsPayments.GetAllPayments();

            if (_paymentsTable == null || _paymentsTable.Columns.Count == 0)
                _paymentsTable = new DataTable();

            dgvListPayments.DataSource = _paymentsTable;
        }


        public frmListPayments()
        {
            InitializeComponent();
        }

        private void frmListPayments_Load(object sender, EventArgs e)
        {
            _RefreshPaymentsList();
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            lblNumberOfRecord.Text = dgvListPayments.Rows.Count.ToString();

            decimal totalAmount = 0;

            foreach (DataRow row in ((DataTable)dgvListPayments.DataSource).Rows)
            {
                if (row["Amounth"] != DBNull.Value)
                    totalAmount += Convert.ToDecimal(row["Amounth"]);
            }

            lblTotalAmounth.Text = totalAmount.ToString("0.00");


            if (dgvListPayments.Rows.Count > 0)
            {
                dgvListPayments.Columns[0].HeaderText = "Payment ID";
                dgvListPayments.Columns[0].Width = 100;

                dgvListPayments.Columns[1].HeaderText = "Subscription ID";
                dgvListPayments.Columns[1].Width = 100;

                dgvListPayments.Columns[2].HeaderText = "Subscription Name";
                dgvListPayments.Columns[2].Width = 100;

                dgvListPayments.Columns[3].HeaderText = "For Member ID";
                dgvListPayments.Columns[3].Width = 100;

                dgvListPayments.Columns[4].HeaderText = "Person ID";
                dgvListPayments.Columns[4].Width = 100;

                dgvListPayments.Columns[5].HeaderText = "Full Name";
                dgvListPayments.Columns[5].Width = 200;

                dgvListPayments.Columns[6].HeaderText = "Phone Number";
                dgvListPayments.Columns[6].Width = 100;

                dgvListPayments.Columns[7].HeaderText = "Payment Date";
                dgvListPayments.Columns[7].Width = 120;

                dgvListPayments.Columns[8].HeaderText = "Amounth";
                dgvListPayments.Columns[8].Width = 150;

                dgvListPayments.Columns[9].HeaderText = "Payment Method";
                dgvListPayments.Columns[9].Width = 100;


                dgvListPayments.Columns[10].HeaderText = "Notes";
                dgvListPayments.Columns[10].Width = 100;
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
            if (_paymentsTable == null)
                return;

            string filterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "Payment ID": filterColumn = "PaymentID"; break;
                case "Member ID": filterColumn = "MemberID"; break;
                case "Person ID": filterColumn = "PersonID"; break;
                case "Full Name": filterColumn = "FullName"; break;
                case "Phone Number": filterColumn = "PhoneNumber"; break;
                case "Subscription Name": filterColumn = "SubscriptionName"; break;
                case "Payment Method": filterColumn = "PaymentMethod"; break;
                case "Amount": filterColumn = "Amounth"; break;
                case "Payment Date": filterColumn = "PaymentDate"; break;
                case "Status": filterColumn = "Status"; break;
                default:
                    dgvListPayments.DataSource = _paymentsTable;
                    return;
            }

            DataView dv = _paymentsTable.DefaultView;

            if (filterColumn == "PaymentID" || filterColumn == "MemberID" || filterColumn == "PersonID")
            {
                if (int.TryParse(txtFilterValue.Text, out int id))
                    dv.RowFilter = $"{filterColumn} = {id}";
                else
                    dv.RowFilter = "1=0";
            }
            else if (filterColumn == "Amount")
            {
                if (decimal.TryParse(txtFilterValue.Text, out decimal amount))
                    dv.RowFilter = $"{filterColumn} = {amount}";
                else
                    dv.RowFilter = "1=0";
            }
            else if (filterColumn == "PaymentDate")
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

                dgvListPayments.DataSource = dv;
                lblNumberOfRecord.Text = dgvListPayments.RowCount.ToString();


            decimal totalAmount = 0;

            foreach (DataRowView row in dv)
            {
                if (row["Amounth"] != DBNull.Value)
                    totalAmount += Convert.ToDecimal(row["Amounth"]);
            }

            lblTotalAmounth.Text = totalAmount.ToString("0.00");
        }

        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deletePaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvListPayments.CurrentRow == null)
                return;

            int paymentID = (int)dgvListPayments.CurrentRow.Cells[0].Value;

            if (MessageBox.Show($"Are you sure you want to delete Payment [{paymentID}]?",
                "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // Perform Delete and refresh
                if (clsPayments.DeletePayment(paymentID))
                {
                    MessageBox.Show("Payment Deleted Successfully.",
                        "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _RefreshPaymentsList(); // إعادة تحميل القائمة
                }
                else
                {
                    MessageBox.Show("Payment was not deleted because it has linked data.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            frmListPayments_Load(null, null); // لتحديث الفورم بالكامل
        }

        private void showPaymentInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPaymentInfo frmShowPaymentInfo = new frmShowPaymentInfo((int)dgvListPayments.CurrentRow.Cells[0].Value);
            frmShowPaymentInfo.ShowDialog();

            frmListPayments_Load(null, null); // لتحديث الفورم بالكامل
        }
    }
}
