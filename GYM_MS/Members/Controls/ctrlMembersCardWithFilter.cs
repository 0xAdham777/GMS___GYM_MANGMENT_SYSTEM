using Guna.UI2.WinForms;
using GYM_BuisnessLayer;
using GYM_MS.Global;
using GYM_MS.People;
using GYM_MS.People.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GYM_MS.Members.Controls
{
    public partial class ctrlMembersCardWithFilter : UserControl
    {
        public ctrlMembersCardWithFilter()
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
                groupBox1.Enabled = _FilterEnabled;
            }
        }


        public ctrlMemberCard ctrlMemberCard
        {
            get
            {
                return ctrlMemberCard1;
            }
        }



        public class MemberSelectedEventArgs : EventArgs
        {
            public clsMember MemberInfo { get; }

            public MemberSelectedEventArgs(clsMember memberInfo)
            {
                MemberInfo = memberInfo;
            }


        }

        public event EventHandler<MemberSelectedEventArgs> OnMemberSelected;
        protected virtual void RaiseOnMemberSelected(MemberSelectedEventArgs e)
        {
            OnMemberSelected?.Invoke(this, e);
        }

        private int _MemberID = -1;

        public int SelectedMemberID
        {
            get
            {
                return _MemberID;
            }
        }


        public void LoadMemberInfo(int MemberID)
        {
            _MemberID = MemberID;


            cbFilterBy.SelectedIndex = 1;
            txtFilterBy.Text = _MemberID.ToString();



            ctrlMemberCard1.LoadMemberInfo(_MemberID);
        }
        private void OnAddNewMember(object sender, int MemberID)
        {
            _MemberID = MemberID;

            ctrlMemberCard1.LoadMemberInfo(MemberID);
            cbFilterBy.SelectedIndex = 1;
            txtFilterBy.Text = _MemberID.ToString();


            RaiseOnMemberSelected(new MemberSelectedEventArgs(clsMember.Find(MemberID)));
        }

        private void btnAddNewMember_Click(object sender, EventArgs e)
        {
            frmAddUpdateMember frmAdd = new frmAddUpdateMember();

            frmAdd.DataBack += OnAddNewMember;

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
                case "Member ID":
                    {
                        if (clsGlobal.IsNumber(txtFilterBy.Text))
                        {
                            ctrlMemberCard1.LoadMemberInfo(Convert.ToInt32(txtFilterBy.Text));
                            _MemberID = (Convert.ToInt32(txtFilterBy.Text));
                        }
                        break;
                    }
                case "Person ID":
                    {
                        var member = clsMember.FindByPersonID(Convert.ToInt32(txtFilterBy.Text));
                        if (member != null)
                        {
                            ctrlMemberCard1.LoadMemberInfo(member.MemberID);
                            _MemberID = member.MemberID;
                        }
                        else
                        {
                            MessageBox.Show("No member found for this Person ID", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            if (ctrlMemberCard1.SelectedMemberInfo != null)
            {
                RaiseOnMemberSelected(new MemberSelectedEventArgs(ctrlMemberCard1.SelectedMemberInfo));
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
