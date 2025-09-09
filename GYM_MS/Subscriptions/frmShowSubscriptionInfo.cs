using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GYM_MS.Subscriptions
{
    public partial class frmShowSubscriptionInfo : Form
    {
        private int _SubscriptionID = -1;

        public frmShowSubscriptionInfo(int SubscriptionID)
        {
            InitializeComponent();
            _SubscriptionID = SubscriptionID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowSubscriptionInfo_Load(object sender, EventArgs e)
        {
            ctrlSubscriptionCard1.LoadSubscriptionInfo(_SubscriptionID);
        }
    }
}
