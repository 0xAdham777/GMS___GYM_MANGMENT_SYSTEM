using GYM_BuisnessLayer;
using GYM_BusinessLayer;
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

namespace GYM_MS.Subscriptions_Types.Controls
{
    public partial class ctrlSubscriptionCardInfo : UserControl
    {
        public ctrlSubscriptionCardInfo()
        {
            InitializeComponent();
        }



        private int _SubscriptionTypeID = -1;
        private clsSubscriptionTypes _SubscriptionTypeInfo;

        public clsSubscriptionTypes SelectedSubscriptionTypeInfo
        {
            get { return _SubscriptionTypeInfo; }
        }

        private void _FillWithDefaultValue()
        {
            txtDescription.Text = "[???]";
            txtSubscriptionName.Text = "[???]";
            nudDurationDays.Value = 0;
            nudPrice.Value = 0;
          
        }


      
        private void _LoadData()
        {
            txtDescription.Text = _SubscriptionTypeInfo.Description;
            txtSubscriptionName.Text = _SubscriptionTypeInfo.SubscriptionName;
            nudDurationDays.Value = _SubscriptionTypeInfo.DurationDays;
            nudPrice.Value = _SubscriptionTypeInfo.Price;

        }


     


        public void LoadSubscriptionTypeInfo(int SubscriptionTypeID)
        {
            _SubscriptionTypeID = SubscriptionTypeID;
            _SubscriptionTypeInfo = clsSubscriptionTypes.Find(SubscriptionTypeID);

            if (_SubscriptionTypeInfo == null)
            {
                MessageBox.Show($"This Person With Id {SubscriptionTypeID} IS Not Exsist", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _FillWithDefaultValue();
                return;
            }

            _LoadData();


        }

        private void llEditSubscriptionInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdateSubscriptionType frmAddUpdatePerson = new frmAddUpdateSubscriptionType(_SubscriptionTypeID);
            frmAddUpdatePerson.ShowDialog();

            // for refrach
            LoadSubscriptionTypeInfo(_SubscriptionTypeID);
        }
    }
}
