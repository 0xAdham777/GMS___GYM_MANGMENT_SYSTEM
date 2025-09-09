using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GYM_MS.Payments
{
    public partial class frmShowPaymentInfo : Form
    {

        private int _PaymentID = -1;

        public frmShowPaymentInfo(int PaymentID)
        {
            InitializeComponent();
            _PaymentID = PaymentID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowPaymentInfo_Load(object sender, EventArgs e)
        {
            ctrlPaymentCard1.LoadPaymentInfo(_PaymentID);
        }
    }
}
