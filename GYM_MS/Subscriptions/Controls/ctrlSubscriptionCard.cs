using GYM_BuisnessLayer;
using GYM_MS.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GYM_MS.Subscriptions.Controls
{
    public partial class ctrlSubscriptionCard : UserControl
    {
        private int _SubscriptionID = -1;
        private clsSubscriptions _subscription;

        public ctrlSubscriptionCard()
        {
            InitializeComponent();
        }

        private void _FillWithDefaultValue()
        {
            ctrlMemberCard1.LoadMemberInfo(-1); // يعرض ??? في كل الحقول
            lblUserID.Text = "[???]";
           
        }

        private void _LoadData()
        {
            // أولًا حمّل بيانات العضو من خلال الـ ctrlMemberCard
            ctrlMemberCard1.LoadMemberInfo(_subscription.MemberID);

            // بعدين أضف بيانات الدفع فقط
            lblUserID.Text = _subscription.CreateByUserID.ToString();
           
        }

        public void LoadSubscriptionInfo(int SubscriptionID)
        {
            _SubscriptionID = SubscriptionID;
            _subscription = clsSubscriptions.Find(SubscriptionID);

            if (_subscription == null)
            {
                MessageBox.Show($"This Subscription With Id {SubscriptionID} does not exist",
                    "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _FillWithDefaultValue();
                return;
            }

            _LoadData();
        }

        private void llShowUserInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowUserInfo frmShowUserInfo = new frmShowUserInfo(_subscription.CreateByUserID);
            frmShowUserInfo.ShowDialog();
        }
    }
}
