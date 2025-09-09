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
using GYM_MS.Global;
using GYM_MS.People;

namespace GYM_MS.Users
{
    public partial class frmListUsers : Form
    {
        private DataTable _UsersTable;

        // دالة تحميل البيانات وعرضها
        private void _RefrachUsersList()
        {
            // نجلب كل الأشخاص من قاعدة البيانات
            _UsersTable = clsUser.GetAllUsers();
            if (_UsersTable != null && _UsersTable.Columns.Count > 0)
            {
                _UsersTable = _UsersTable.DefaultView.ToTable(false,"UserID", "PersonID","FullName", "UserName","IsActive");
            }
            else
            {
                _UsersTable = new DataTable(); // جدول فاضي عشان ما يكسرش الكود
            }
            dgvListUsers.DataSource = _UsersTable;
        }




        public frmListUsers()
        {
            InitializeComponent();
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frmAddUpdateUser = new frmAddUpdateUser();
            frmAddUpdateUser.ShowDialog();  

            _RefrachUsersList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListUsers_Load(object sender, EventArgs e)
        {
            _RefrachUsersList();
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            cbFilterBy.SelectedIndex = 0;
            lblNumberOfRecord.Text = dgvListUsers.Rows.Count.ToString();
            if (dgvListUsers.Rows.Count > 0)
            {

                dgvListUsers.Columns[0].HeaderText = "User ID";
                dgvListUsers.Columns[0].Width = 230;

                dgvListUsers.Columns[1].HeaderText = "Person ID";
                dgvListUsers.Columns[1].Width = 200;


                dgvListUsers.Columns[2].HeaderText = "Full Name";
                dgvListUsers.Columns[2].Width = 300;

                dgvListUsers.Columns[3].HeaderText = "User Name";
                dgvListUsers.Columns[3].Width = 200;

                dgvListUsers.Columns[4].HeaderText = "Is Active";
                dgvListUsers.Columns[4].Width = 200;

               


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
            if (_UsersTable == null)
                return; // لو الجدول فاضي، نخرج

            string filterColumn = "";

            // نحدد اسم العمود في الجدول حسب اختيار المستخدم
            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    filterColumn = "PersonID";
                    break;
                case "User ID":
                    filterColumn = "UserID";
                    break;
                case "Full Name":
                    filterColumn = "FullName";
                    break;
                case "User Name":
                    filterColumn = "UserName";
                    break;
                default:
                    dgvListUsers.DataSource = _UsersTable;
                    return;
            }

            // ننشئ DataView للفلترة
            DataView dv = _UsersTable.DefaultView;

            // لو العمود رقمي مثل PersonID
            if (filterColumn == "PersonID" || filterColumn == "UserID")
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
            dgvListUsers.DataSource = dv;

            lblNumberOfRecord.Text = dgvListUsers.RowCount.ToString();

        }

        private void showPersonInfoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmShowUserInfo frmShowUserInfo = new frmShowUserInfo((int)dgvListUsers.CurrentRow.Cells[0].Value);    
            frmShowUserInfo.ShowDialog();

            frmListUsers_Load(null,null);
        }

        private void addNewPersonToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm  = new frmAddUpdateUser();
            frm.ShowDialog();

            frmListUsers_Load(null, null);

        }

        private void editPersonInfoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser((int)dgvListUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            frmListUsers_Load(null, null);

        }

  
        private void callPhoneToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Featur Will Be Be Tomoro Brather", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void sendEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Featur Will Be Be Tomoro Brather", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void tsmChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword frmChangePassword = new frmChangePassword((int)dgvListUsers.CurrentRow.Cells[0].Value);
        
            frmChangePassword.ShowDialog();
        
        }

        private void deletePersonToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvListUsers.CurrentRow.Cells[0].Value;
            if (clsUser.Delete(UserID))
            {
                MessageBox.Show("User has been deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmListUsers_Load(null, null);
            }

            else
                MessageBox.Show("User is not delted due to data connected to it.", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);




        }
    }
}
