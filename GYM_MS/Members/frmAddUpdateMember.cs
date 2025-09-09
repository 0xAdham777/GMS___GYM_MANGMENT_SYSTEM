using Guna.UI2.WinForms;
using GYM_BuisnessLayer;
using GYM_BusinessLayer;
using GYM_MS.Global;
using GYM_MS.Members;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GYM_MS
{
    public partial class frmAddUpdateMember : Form
    {


        public delegate void DataBackEventHandler(object sender, int MemberID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;

        private enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;

        private int _MemberID = -1;
        private int _SubscriptionID = -1;
        private int _PaymentID = -1;

        private clsMember _MemberInfo;
        private clsSubscriptions _SubscriptionInfo;
        private clsSubscriptionTypes _SubscriptionTypeInfo;
        private clsPayments _PaymentInfo;



        public frmAddUpdateMember()
        {
            InitializeComponent();
        }


        public frmAddUpdateMember(int MemberID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _MemberID = MemberID;

        }


        private void frmAddUpdateMember_Load(object sender, EventArgs e)
        {
            _DefaultValues();
            if (_Mode == enMode.Update)
            {
                _LoadData();
            }
        }



        private void _LoadAllSubscriptionsTypes()
        {

            DataTable dt = clsSubscriptionTypes.GetAll();

            foreach (DataRow dr in dt.Rows)
            {
                cmbSubscriptionType.Items.Add(dr[1].ToString());
            }

            if (dt.Rows.Count > 0)
            {
                lblDurationDays.Text = dt.Rows[0]["DurationDays"].ToString();
                lblSubscriptionPrice.Text = dt.Rows[0]["Price"].ToString();


            }
            else
            {
                MessageBox.Show("You Have To Add Subscription Type", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
        

        }


        private bool _Save()
        {


            if (_Mode == enMode.AddNew)
            {


                _MemberID = clsService.AddMemberWithSubscriptionAndPayment(_MemberInfo.PersonID, _MemberInfo.Debts, _MemberInfo.RemainingDays, _MemberInfo.Status, _MemberInfo.Notes, _SubscriptionInfo.CreateByUserID, _SubscriptionInfo.StartDate, _SubscriptionInfo.EndDate, _SubscriptionInfo.SubscriptionTypeID, _PaymentInfo.Amount, (_PaymentInfo.PaymentMethod != 0), _PaymentInfo.Notes);
                this._MemberInfo.MemberID = _MemberID;

                if (_MemberID != -1) 
                { 
                    _Mode = enMode.Update; 
                    return true;
                }
                return false;
            }
            return clsService.UpdateMemberWithSubscriptionAndPayment(
                _MemberID,
                _MemberInfo.PersonID,
                _MemberInfo.Debts,
                _MemberInfo.RemainingDays,
                _MemberInfo.Status,
                _MemberInfo.Notes,
                _SubscriptionInfo.SubscriptionID,         
                _SubscriptionInfo.CreateByUserID,
                _SubscriptionInfo.StartDate,
                _SubscriptionInfo.EndDate,
                _SubscriptionInfo.SubscriptionTypeID,
                _PaymentInfo.PaymentID,                  
                _PaymentInfo.Amount,
                (_PaymentInfo.PaymentMethod != 0),
                _PaymentInfo.Notes
            );


        }

        private void _FillWithDefaultValues()
        {
            lblDurationDays.Text = "[????]";
            lblSubscriptionPrice.Text = "[????]";

            _LoadAllSubscriptionsTypes();

            dtpStartDate.Value = DateTime.Now;
            dtpEndData.Value = dtpStartDate.Value.AddDays(Convert.ToInt32(lblDurationDays.Text)* (int)nupNumberOfMounth.Value);


            cmbSubscriptionType.SelectedIndex = 0;

            lblPaymentDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtMemberNotes.Text = "";
            txtPaymentsNotes.Text = "";

            tcMemberInfo.TabPages[1].Enabled = false;
            tcMemberInfo.TabPages[2].Enabled = false;

        }


        private void _DefaultValues()
        {

            _FillWithDefaultValues();


            if (_Mode == enMode.AddNew)
            {
                ctrlPersonCardWithFilter1.Focus();
                lblTitel.Text = "Add New Member";


                _MemberInfo = new clsMember();
                _SubscriptionInfo = new clsSubscriptions();
                _PaymentInfo = new clsPayments();

            }
            else
            {
                _MemberInfo = clsMember.Find(_MemberID);
                _SubscriptionInfo = clsSubscriptions.GetLastSubscriptionOfMember(_MemberID);
                _PaymentInfo = clsPayments.GetLastPaymentOfMember(_MemberID);

                lblTitel.Text = "Update Member";

                btnNextToPaymentInfo.Enabled = true;
                btnNextToSubscriptionInfo.Enabled = true;

                tcMemberInfo.TabPages[1].Enabled = true;
                tcMemberInfo.TabPages[2].Enabled = true;

            }
        }


        private void _LoadData()
        {


            ctrlPersonCardWithFilter1.LoadPersonData(_MemberInfo.PersonID);
            ctrlPersonCardWithFilter1.FilterEnabled = false;


            txtMemberNotes.Text = _MemberInfo.Notes;
            txtPaymentsNotes.Text = _PaymentInfo.Notes == "NULL" ? "" : _PaymentInfo.Notes;

            nudDebts.Value = _MemberInfo.Debts;

            rbCachMethod.Checked = (_PaymentInfo.PaymentMethod == 0 )? true : false;
            rbCardMethod.Checked = (_PaymentInfo.PaymentMethod == 0 )? false : true;


            lblPaymentDate.Text = _PaymentInfo.PaymentDate.ToString(); 
            
            cmbSubscriptionType.SelectedIndex = cmbSubscriptionType.FindString(_SubscriptionInfo.SubscriptionTypeInfo.SubscriptionName);

            lblDurationDays.Text = _SubscriptionInfo.SubscriptionTypeInfo.DurationDays.ToString();
            lblSubscriptionPrice.Text = _SubscriptionInfo.SubscriptionTypeInfo.Price.ToString();

        
        
            dtpStartDate.Value = _SubscriptionInfo.StartDate;
              dtpEndData.Value = _SubscriptionInfo.EndDate;
    
          
        }

    

        private void btnSave_Click_1(object sender, EventArgs e)
        {


            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }


            _MemberInfo.PersonID = ctrlPersonCardWithFilter1.SelectedPersonID;
            _MemberInfo.Debts = nudDebts.Value;
            _MemberInfo.RemainingDays = (dtpEndData.Value - dtpStartDate.Value).Days;
            _MemberInfo.Status = _MemberInfo.RemainingDays > 0;
            _MemberInfo.Notes = txtMemberNotes.Text;


            _SubscriptionInfo.StartDate = dtpStartDate.Value;
            _SubscriptionInfo.EndDate = dtpEndData.Value;
            _SubscriptionInfo.SubscriptionDate = DateTime.Now;
            _SubscriptionInfo.MemberID = _MemberInfo.MemberID;
            _SubscriptionInfo.SubscriptionTypeID = clsSubscriptionTypes.Find(cmbSubscriptionType.Text).SubscriptionTypeID;
            _SubscriptionInfo.CreateByUserID = clsGlobal.CurrentUser.UserID;


            _PaymentInfo.SubscriptionID = _SubscriptionInfo.SubscriptionID;
            _PaymentInfo.PaymentMethod = (rbCachMethod.Checked) ? (byte)0 : (byte)1;
            _PaymentInfo.MemberID = _MemberInfo.MemberID;
            _PaymentInfo.PaymentDate = DateTime.Now;
            _PaymentInfo.Amount = Convert.ToDecimal(lblSubscriptionPrice.Text) - nudDebts.Value;
            _PaymentInfo.Notes = txtPaymentsNotes.Text;


            if (_Save())
            {
               

                //change form mode to update.
                _Mode = enMode.Update;
                lblTitel.Text = "Update Member";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);


                // Trigger the event to send data back to the caller form.
                DataBack?.Invoke(this, _MemberInfo.MemberID);

                this.Close();
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnNextToSubscriptionInfo_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.AddNew)
            {
                if (clsMember.IsExistByPersonID(ctrlPersonCardWithFilter1.SelectedPersonID))
                {
                    MessageBox.Show("Error: Data Is not Saved Successfully Chose Another Person This IsExsist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
           
            tcMemberInfo.SelectedTab = tcMemberInfo.TabPages[1];
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(object sender, People.Controls.ctrlPersonCardWithFilter.PersonSelectedEventArgs e)
        {

            btnNextToSubscriptionInfo.Enabled = true;
            tcMemberInfo.TabPages[1].Enabled = true;
            tcMemberInfo.TabPages[2].Enabled = true;
        }

        private void cmbSubscriptionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _SubscriptionTypeInfo = clsSubscriptionTypes.Find(cmbSubscriptionType.Text);

            if (_SubscriptionTypeInfo == null )
            {
                MessageBox.Show("You Have To Add Subscription Type","Error Message",MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }


            lblDurationDays.Text = (_SubscriptionTypeInfo.DurationDays * (int)nupNumberOfMounth.Value).ToString();
            lblSubscriptionPrice.Text = (_SubscriptionTypeInfo.Price * (int)nupNumberOfMounth.Value).ToString();

            dtpStartDate.Value = DateTime.Now;
            dtpEndData.Value = dtpStartDate.Value.AddDays(Convert.ToInt32(lblDurationDays.Text)* (int)nupNumberOfMounth.Value);

        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            dtpEndData.MinDate = dtpStartDate.Value.AddDays(Convert.ToInt32(lblDurationDays.Text));
            dtpEndData.Value = dtpStartDate.Value.AddDays(Convert.ToInt32(lblDurationDays.Text));

        }

        private void btnNextToPaymentInfo_Click(object sender, EventArgs e)
        {
            tcMemberInfo.SelectedTab = tcMemberInfo.TabPages[2];
        }

        private void guna2NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            lblDurationDays.Text = (_SubscriptionTypeInfo.DurationDays * (int)nupNumberOfMounth.Value).ToString();
            dtpEndData.Value = dtpStartDate.Value.AddDays(30 * (int)nupNumberOfMounth.Value);
            lblSubscriptionPrice.Text = (_SubscriptionTypeInfo.Price * (int)nupNumberOfMounth.Value).ToString();
        }
    }
}
