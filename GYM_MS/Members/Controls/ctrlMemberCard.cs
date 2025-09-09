using GYM_BuisnessLayer;
using GYM_MS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GYM_MS.Members
{
    public partial class ctrlMemberCard : UserControl
    {
        public ctrlMemberCard()
        {
            InitializeComponent();
        }



        private int _MemberID = -1;
        private clsMember _Member;
        private clsSubscriptions _Subscription;
        private clsPayments _Payment;

        public clsMember SelectedMemberInfo
        {
            get { return _Member; }
        }

        private void _FillWithDefaultValue()
        {
            lblDebts.Text = "[???]";
            lblEndDate.Text = "[???]";
            lblMemberID.Text = "[???]";
            lblNotes.Text = "[???]";
            lblRemainingDays.Text = "[???]";
            lblStartDate.Text = "[???]";
            lblStatus.Text = "[???]";
            lblSubscriptionDurationDays.Text = "[???]";
            lblSubscriptionPrice.Text = "[???]";
            lblSubscriptionTypeName.Text = "[???]";

        }


     


        private void _LoadData()
        {
            ctrlPersonCard1.LoadPersonInfo(_Member.PersonID);

            _Subscription = clsSubscriptions.GetLastSubscriptionOfMember(_MemberID);
            _Payment = clsPayments.GetLastPaymentOfMember(_MemberID);

            // Member Info
            lblMemberID.Text = _Member.MemberID.ToString();
            lblDebts.Text = _Member.Debts.ToString("0.00");
            lblRemainingDays.Text = _Member.RemainingDays.ToString();
            lblStatus.Text = _Member.Status ? "Active" : "Inactive";
            lblNotes.Text = string.IsNullOrEmpty(_Member.Notes) ? "No Notes" : _Member.Notes;

            // Subscription Info
            lblStartDate.Text = _Subscription.StartDate.ToShortDateString();
            lblEndDate.Text = _Subscription.EndDate.ToShortDateString();

            // Subscription Type Info
            lblSubscriptionTypeName.Text = _Subscription.SubscriptionTypeInfo.SubscriptionName;
            lblSubscriptionPrice.Text = _Subscription.SubscriptionTypeInfo.Price.ToString("0.00");
            lblSubscriptionDurationDays.Text = _Subscription.SubscriptionTypeInfo.DurationDays.ToString();

        }




        public void LoadMemberInfo(int MemberID)
        {
            _MemberID = MemberID;
            _Member = clsMember.Find(MemberID);


            if (_Member == null)
            {
                MessageBox.Show($"This Member With Id {MemberID} IS Not Exsist", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _FillWithDefaultValue();
                return;
            }

            _LoadData();


        }

      
        private void llEditMemberInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdateMember frm = new frmAddUpdateMember(_MemberID);
            frm.ShowDialog();


            // for refrach
            LoadMemberInfo(_MemberID);
        }
    }
}
