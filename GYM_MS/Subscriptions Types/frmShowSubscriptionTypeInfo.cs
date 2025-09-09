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
    public partial class frmShowSubscriptionTypeInfo : Form
    {
        private int _SubscriptionTypeID = -1;

        public frmShowSubscriptionTypeInfo(int SubscriptionTypeID)
        {
            InitializeComponent();
            _SubscriptionTypeID = SubscriptionTypeID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowSubscriptionTypeInfo_Load(object sender, EventArgs e)
        {
            ctrlSubscriptionCardInfo1.LoadSubscriptionTypeInfo(_SubscriptionTypeID);
        }
    }
}
