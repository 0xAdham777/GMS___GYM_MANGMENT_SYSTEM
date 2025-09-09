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

namespace GYM_MS.Coach_Spelazations
{
    public partial class frmListSpelazation : Form
    {
        public frmListSpelazation()
        {
            InitializeComponent();
        }



        private DataTable _SpezalationsTable;

        private void _RefrachSpezalationList()
        {
            _SpezalationsTable = clsCoachSpezalations.GetAll();
            if (_SpezalationsTable != null && _SpezalationsTable.Columns.Count > 0)
            {
                _SpezalationsTable = _SpezalationsTable.DefaultView.ToTable(false,
                    "CoachSpezalationsID", "SpezalationsName", "Description");
            }
            else
            {
                _SpezalationsTable = new DataTable();
            }
            dgvListSpezalation.DataSource = _SpezalationsTable;
        }

        private void frmListSpelazation_Load(object sender, EventArgs e)
        {
            _RefrachSpezalationList();
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            cbFilterBy.SelectedIndex = 0;
            lblNumberOfRecord.Text = dgvListSpezalation.Rows.Count.ToString();

            if (dgvListSpezalation.Rows.Count > 0)
            {
                dgvListSpezalation.Columns[0].HeaderText = "Spezalation ID";
                dgvListSpezalation.Columns[0].Width = 150;

                dgvListSpezalation.Columns[1].HeaderText = "Spezalation Name";
                dgvListSpezalation.Columns[1].Width = 200;

                dgvListSpezalation.Columns[2].HeaderText = "Description";
                dgvListSpezalation.Columns[2].Width = 300;
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (_SpezalationsTable == null) return;

            string filterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "Spezalation ID":
                    filterColumn = "CoachSpezalationsID";
                    break;
                case "Spezalation Name":
                    filterColumn = "SpezalationsName";
                    break;
                case "Description":
                    filterColumn = "Description";
                    break;
                default:
                    dgvListSpezalation.DataSource = _SpezalationsTable;
                    return;
            }

            DataView dv = _SpezalationsTable.DefaultView;

            if (filterColumn == "CoachSpezalationsID")
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

            dgvListSpezalation.DataSource = dv;
            lblNumberOfRecord.Text = dgvListSpezalation.RowCount.ToString();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdateSpelazation frm = new frmAddUpdateSpelazation();
            frm.ShowDialog();
            frmListSpelazation_Load(null, null);
        }

        private void editPersonInfoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAddUpdateSpelazation frm = new frmAddUpdateSpelazation((int)dgvListSpezalation.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListSpelazation_Load(null, null);
        }

        private void deletePersonToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this Spezalation [" +
       dgvListSpezalation.CurrentRow.Cells[0].Value + "]", "Confirm Delete",
       MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsCoachSpezalations.Delete((int)dgvListSpezalation.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefrachSpezalationList();
                }
                else
                {
                    MessageBox.Show("This Spezalation could not be deleted because it is linked to other data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void showPersonInfoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmShowSpelazationInfo frmShow = new frmShowSpelazationInfo((int)dgvListSpezalation.CurrentRow.Cells[0].Value);
            frmShow.ShowDialog();
            frmListSpelazation_Load(null, null);

        }

        private void addNewPersonToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAddUpdateSpelazation frmAddUpdateSpelazation = new frmAddUpdateSpelazation();
            frmAddUpdateSpelazation.ShowDialog();

            frmListSpelazation_Load(null, null);

        }
    }
}
