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

namespace GYM_MS.Payments.Controls
{
    public partial class ctrlPaymentCard : UserControl
    {
        public ctrlPaymentCard()
        {
            InitializeComponent();
        }


        private int _PaymentID = -1;
        private clsPayments _Payment;

     

        private void _FillWithDefaultValue()
        {
            ctrlMemberCard1.LoadMemberInfo(-1); // يعرض ??? في كل الحقول
            lblPaymentID.Text = "[???]";
            lblAmounth.Text = "[???]";
            lblPaymentDate.Text = "[???]";
            lblPaymentMethod.Text = "[???]";
        }

        private void _LoadData()
        {
            // أولًا حمّل بيانات العضو من خلال الـ ctrlMemberCard
            ctrlMemberCard1.LoadMemberInfo(_Payment.MemberID);

            // بعدين أضف بيانات الدفع فقط
            lblPaymentID.Text = _Payment.PaymentID.ToString();
            lblAmounth.Text = _Payment.Amount.ToString("0.00");
            lblPaymentDate.Text = _Payment.PaymentDate.ToShortDateString();
            lblPaymentMethod.Text = (_Payment.PaymentMethod == 0) ? "Cash" : "Card";
        }

        public void LoadPaymentInfo(int PaymentID)
        {
            _PaymentID = PaymentID;
            _Payment = clsPayments.Find(PaymentID);

            if (_Payment == null)
            {
                MessageBox.Show($"This Payment With Id {PaymentID} does not exist",
                    "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _FillWithDefaultValue();
                return;
            }

            _LoadData();
        }
    }
}
