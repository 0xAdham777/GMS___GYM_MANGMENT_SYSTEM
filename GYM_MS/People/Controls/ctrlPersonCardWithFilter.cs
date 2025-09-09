using Guna.UI2.WinForms;
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

namespace GYM_MS.People.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        private bool _FilterEnabled = true;

        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }


        public ctrlPersonCard ctrlPersonCard
        {
            get
            {
                return ctrlPersonCard1;
            }
        }



        public class PersonSelectedEventArgs : EventArgs
        {
            public clsPerson PersonInfo { get; }

            public PersonSelectedEventArgs(clsPerson personInfo)
            {
                PersonInfo = personInfo;
            }


        }

        public event EventHandler<PersonSelectedEventArgs> OnPersonSelected;
        protected virtual void RaiseOnPersonSelected(PersonSelectedEventArgs e)
        {
            OnPersonSelected?.Invoke(this, e);
        }

        private int _PersonID = -1;

        public int  SelectedPersonID
        {
            get
            {
                return _PersonID;
            }
        }

       
        public void LoadPersonData(int PersonID)
        {
            _PersonID = PersonID;


            cbFilterBy.SelectedIndex = 1;
            txtFilterBy.Text = _PersonID.ToString();
            

            ctrlPersonCard1.LoadPersonInfo(PersonID);
        }
        private void OnAddNewPerson(object sender, int PersonId)
        {
            _PersonID = PersonId;

            ctrlPersonCard1.LoadPersonInfo(PersonId);
            cbFilterBy.SelectedIndex = 1;
            txtFilterBy.Text = _PersonID.ToString();


            RaiseOnPersonSelected(new PersonSelectedEventArgs(clsPerson.Find(PersonId)));
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frmAdd = new frmAddUpdatePerson();

            frmAdd.DataBack += OnAddNewPerson;

            frmAdd.ShowDialog();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 0)
            {
                txtFilterBy.Enabled = false;
            }
            else
            {
                txtFilterBy.Enabled = true;
                txtFilterBy.Text = "";
                txtFilterBy.Focus();
            }
        }

        public void Foucs()
        {
            txtFilterBy.Focus();
        }


        private void FindNow()
        {
            switch (cbFilterBy.Text)
            {
                case "None":
                    {

                        break;
                    }
                case "Person ID":
                    {
                        if (clsGlobal.IsNumber(txtFilterBy.Text))
                        {
                            ctrlPersonCard1.LoadPersonInfo(Convert.ToInt32(txtFilterBy.Text));
                            _PersonID = (Convert.ToInt32(txtFilterBy.Text));
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            if (ctrlPersonCard1.SelectedPersonInfo != null)
            {
                RaiseOnPersonSelected(new PersonSelectedEventArgs(ctrlPersonCard1.SelectedPersonInfo));
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            FindNow();
        }


        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {

            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
            Guna2TextBox Temp = ((Guna2TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                //e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }

        }

    }
}
