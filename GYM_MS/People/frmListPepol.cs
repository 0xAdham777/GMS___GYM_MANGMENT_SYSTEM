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

namespace GYM_MS.People
{
    public partial class frmListPepol : Form
    {
        private DataTable _peopleTable;

        // دالة تحميل البيانات وعرضها
        private void _RefrachPepoleList()
        {
            // نجلب كل الأشخاص من قاعدة البيانات
            _peopleTable = clsPerson.GetAllPersons();
            if (_peopleTable != null && _peopleTable.Columns.Count > 0)
            {
                _peopleTable = _peopleTable.DefaultView.ToTable(false,
                    "PersonID", "FirstName", "MidName", "LastName",
                    "BirthDate", "Email", "PhoneNumber", "GendorCaption",
                    "BloodName", "Address");
            }
            else
            {
                _peopleTable = new DataTable(); // جدول فاضي عشان ما يكسرش الكود
            }
            dgvListPepole.DataSource = _peopleTable;
        }

        public frmListPepol()
        {
            InitializeComponent();
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
            if (_peopleTable == null) 
                return; // لو الجدول فاضي، نخرج

            string filterColumn = "";

            // نحدد اسم العمود في الجدول حسب اختيار المستخدم
            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    filterColumn = "PersonID";
                    break;
                case "First Name":
                    filterColumn = "FirstName";
                    break;
                case "Mid Name":
                    filterColumn = "MidName";
                    break;
                case "Last Name":
                    filterColumn = "LastName";
                    break;
                case "Email":
                    filterColumn = "Email";
                    break;
                case "Phone Number":
                    filterColumn = "PhoneNumber";
                    break;
                case "Address":
                    filterColumn = "Address";
                    break;
               
                default:
                    dgvListPepole.DataSource = _peopleTable;
                    return;
            }

            // ننشئ DataView للفلترة
            DataView dv = _peopleTable.DefaultView;

            // لو العمود رقمي مثل PersonID
            if (filterColumn == "PersonID")
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
            dgvListPepole.DataSource = dv;

            lblNumberOfRecord.Text = dgvListPepole.RowCount.ToString();

        }



        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frmAddUpdatePerson = new frmAddUpdatePerson();   
            frmAddUpdatePerson.ShowDialog();    

            this.frmListPepol_Load(null,null);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListPepol_Load(object sender, EventArgs e)
        {

            _RefrachPepoleList();
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            cbFilterBy.SelectedIndex = 0;
            lblNumberOfRecord.Text = dgvListPepole.Rows.Count.ToString();
            if (dgvListPepole.Rows.Count > 0)
            {

                dgvListPepole.Columns[0].HeaderText = "Person ID";
                dgvListPepole.Columns[0].Width = 110;

                dgvListPepole.Columns[1].HeaderText = "First Name";
                dgvListPepole.Columns[1].Width = 120;

                dgvListPepole.Columns[2].HeaderText = "Mid Name";
                dgvListPepole.Columns[2].Width = 140;

                dgvListPepole.Columns[3].HeaderText = "Last Name";
                dgvListPepole.Columns[3].Width = 120;

                dgvListPepole.Columns[4].HeaderText = "Date Of Birth";
                dgvListPepole.Columns[4].Width = 140;

                dgvListPepole.Columns[5].HeaderText = "Email";
                dgvListPepole.Columns[5].Width = 170;

                dgvListPepole.Columns[6].HeaderText = "Phone Number";
                dgvListPepole.Columns[6].Width = 120;

                dgvListPepole.Columns[7].HeaderText = "Gendor";
                dgvListPepole.Columns[7].Width = 120;

                dgvListPepole.Columns[8].HeaderText = "Blood";
                dgvListPepole.Columns[8].Width = 70;

                dgvListPepole.Columns[9].HeaderText = "Address";
                dgvListPepole.Columns[9].Width = 170;



            }



        }

        private void showPersonInfoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frmShowPersonInfo = new frmShowPersonInfo((int)dgvListPepole.CurrentRow.Cells[0].Value);
            frmShowPersonInfo.ShowDialog(); 
            frmListPepol_Load(null,null);
        }

        private void addNewPersonToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            btnAddNewPerson_Click(null, null);
        }

        private void editPersonInfoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frmShowPersonInfo = new frmAddUpdatePerson((int)dgvListPepole.CurrentRow.Cells[0].Value);
            frmShowPersonInfo.ShowDialog();

            this.frmListPepol_Load(null, null);
        }

        private void callPhoneToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Featur Will Be Be Tomoro Brather", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void sendEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Featur Will Be Be Tomoro Brather", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void deletePersonToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to delete Person [" + dgvListPepole.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)

            {

                //Perform Delele and refresh
                if (clsPerson.Delete((int)dgvListPepole.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefrachPepoleList();
                }

                else
                    MessageBox.Show("Person was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            this.frmListPepol_Load(null,null);
        }
    }
}
