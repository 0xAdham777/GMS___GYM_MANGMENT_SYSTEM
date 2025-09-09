using GYM_BuisnessLayer;
using GYM_BusinessLayer;
using GYM_MS.Global;
using GYM_MS.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GYM_MS.Subscriptions_Types
{
    public partial class frmListSubscriptionsTypes : Form
    {
        public frmListSubscriptionsTypes()
        {
            InitializeComponent();
        }

        private DataTable _SubscriptionTypesTable;

        // دالة تحميل البيانات وعرضها
        private void _RefrachSubscriptionTypesList()
        {
            // نجلب كل الأشخاص من قاعدة البيانات
            _SubscriptionTypesTable = clsSubscriptionTypes.GetAll();
            if (_SubscriptionTypesTable != null && _SubscriptionTypesTable.Columns.Count > 0)
            {
                _SubscriptionTypesTable = _SubscriptionTypesTable.DefaultView.ToTable(false,
                    "SubscriptionTypeID", "SubscriptionName", "Price", "DurationDays",
                    "Description");
            }
            else
            {
                _SubscriptionTypesTable = new DataTable(); // جدول فاضي عشان ما يكسرش الكود
            }
            dgvListSpezalation.DataSource = _SubscriptionTypesTable;
        }


        private void frmListSubscriptionsTypes_Load(object sender, EventArgs e)
        {


            _RefrachSubscriptionTypesList();
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            cbFilterBy.SelectedIndex = 0;
            lblNumberOfRecord.Text = dgvListSpezalation.Rows.Count.ToString();
            if (dgvListSpezalation.Rows.Count > 0)
            {

                dgvListSpezalation.Columns[0].HeaderText = "Subscription Type ID";
                dgvListSpezalation.Columns[0].Width = 200;

                dgvListSpezalation.Columns[1].HeaderText = "Subscription Name";
                dgvListSpezalation.Columns[1].Width = 250;

                dgvListSpezalation.Columns[2].HeaderText = "Price";
                dgvListSpezalation.Columns[2].Width = 150;

                dgvListSpezalation.Columns[3].HeaderText = "Duration Days";
                dgvListSpezalation.Columns[3].Width = 150;

                dgvListSpezalation.Columns[4].HeaderText = "Description";
                dgvListSpezalation.Columns[4].Width = 300;


            }



        }

        private void txtFilterValue_TextChanged_1(object sender, EventArgs e)
        {
            if (_SubscriptionTypesTable == null)
                return; // لو الجدول فاضي، نخرج

            string filterColumn = "";

            // نحدد اسم العمود في الجدول حسب اختيار المستخدم
            switch (cbFilterBy.Text)
            {
                case "Subscription Type ID":
                    filterColumn = "SubscriptionTypeID";
                    break;
                case "Subscription Name":
                    filterColumn = "SubscriptionName";
                    break;
                case "Duration Days":
                    filterColumn = "DurationDays";
                    break;
                case "Description":
                    filterColumn = "Description";
                    break;
                default:
                    dgvListSpezalation.DataSource = _SubscriptionTypesTable;
                    return;
            }

            // ننشئ DataView للفلترة
            DataView dv = _SubscriptionTypesTable.DefaultView;

            // لو العمود رقمي مثل PersonID
            if (filterColumn == "PersonID" || filterColumn == "DurationDays")
            {
                if (int.TryParse(txtFilterValue.Text, out int id))
                    dv.RowFilter = $"{filterColumn} = {id}";
                else
                    dv.RowFilter = "1=0"; // لا يطابق شيء لو الإدخال مو رقم
            }

            // باقي الأعمدة النصية
            else
            {
                if (clsGlobal.IsValidFilter(txtFilterValue.Text))
                    dv.RowFilter = $"{filterColumn} LIKE '%{txtFilterValue.Text.Replace("'", "''")}%'";
                else
                    dv.RowFilter = "1=0"; // لا يطابق شيء 
            }

            // نعرض النتيجة
            dgvListSpezalation.DataSource = dv;

            lblNumberOfRecord.Text = dgvListSpezalation.RowCount.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged_1(object sender, EventArgs e)
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

        private void showPersonInfoToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            frmShowSubscriptionTypeInfo frmShowPersonInfo = new frmShowSubscriptionTypeInfo((int)dgvListSpezalation.CurrentRow.Cells[0].Value);
            frmShowPersonInfo.ShowDialog();
            frmListSubscriptionsTypes_Load(null, null);
        }

        private void addNewPersonToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            frmAddUpdateSubscriptionType frmShowPersonInfo = new frmAddUpdateSubscriptionType();
            frmShowPersonInfo.ShowDialog();
            frmListSubscriptionsTypes_Load(null, null);
        }

        private void editPersonInfoToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            frmAddUpdateSubscriptionType frmShowPersonInfo = new frmAddUpdateSubscriptionType((int)dgvListSpezalation.CurrentRow.Cells[0].Value);
            frmShowPersonInfo.ShowDialog();

            this.frmListSubscriptionsTypes_Load(null, null);
        }

        private void deletePersonToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete This Subscription Types [" + dgvListSpezalation.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)

            {

                //Perform Delele and refresh
                if (clsSubscriptionTypes.Delete((int)dgvListSpezalation.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Subscription Types Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefrachSubscriptionTypesList();
                }

                else
                    MessageBox.Show("Subscription Types was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            this.frmListSubscriptionsTypes_Load(null, null);
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdateSubscriptionType frmAddUpdateSubscriptionType = new frmAddUpdateSubscriptionType();
            frmAddUpdateSubscriptionType.ShowDialog();

            this.frmListSubscriptionsTypes_Load(null,null);
        }

        private void dgvListSubscriptionTypes_DoubleClick(object sender, EventArgs e)
        {
            frmShowSubscriptionTypeInfo frmShowPersonInfo = new frmShowSubscriptionTypeInfo((int)dgvListSpezalation.CurrentRow.Cells[0].Value);
            frmShowPersonInfo.ShowDialog();
            frmListSubscriptionsTypes_Load(null, null);
        }
    }
}
