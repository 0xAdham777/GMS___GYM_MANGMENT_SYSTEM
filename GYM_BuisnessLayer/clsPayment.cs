using GYM_DataAccessLayer;
using System;
using System.Data;
using System.Reflection;

namespace GYM_BuisnessLayer
{
    public class clsPayments
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;

        public int PaymentID { get; set; }
        public int MemberID { get; set; }
        public clsMember MemberInfo;
        public int SubscriptionID { get; set; }
        public clsSubscriptions SubscriptionInfo;
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public byte PaymentMethod { get; set; }
        public string Notes { get; set; }

        private bool _AddNewPayment()
        {
            this.PaymentID = clsPaymentsData.AddNewPayment(MemberID, SubscriptionID, Amount, PaymentDate, PaymentMethod, Notes);
            return (this.PaymentID != -1);
        }

        private bool _UpdatePayment()
        {
            return clsPaymentsData.UpdatePayment(PaymentID, MemberID, SubscriptionID, Amount, PaymentDate, PaymentMethod, Notes);
        }

        public clsPayments()
        {
            PaymentID = -1;
            MemberID = -1;
            SubscriptionID = -1;
            Amount = 0;
            PaymentDate = DateTime.Now;
            PaymentMethod = 0;
            Notes = "";
            _Mode = enMode.AddNew;
        }

        private clsPayments(int paymentID, int memberID, int subscriptionID, decimal amount, DateTime paymentDate, byte paymentMethod,string notes)
        {
            PaymentID = paymentID;
            MemberID = memberID;
            MemberInfo = clsMember.Find(memberID);
            SubscriptionID = subscriptionID;
            SubscriptionInfo = clsSubscriptions.Find(subscriptionID);
            Amount = amount;
            PaymentDate = paymentDate;
            PaymentMethod = paymentMethod;
            Notes = notes;
            _Mode = enMode.Update;
        }

        public static DataTable GetAllPayments()
        {
            return clsPaymentsData.GetAllPayments();
        }

        public static clsPayments Find(int paymentID)
        {
            int memberID = -1, subscriptionID = -1;
            decimal amount = 0;
            DateTime paymentDate = DateTime.Now;
            byte paymentMethod = 0;
            string notes = "";

            if (clsPaymentsData.GetPaymentInfoByID(paymentID, ref memberID, ref subscriptionID, ref amount, ref paymentDate, ref paymentMethod,ref notes))
                return new clsPayments(paymentID, memberID, subscriptionID, amount, paymentDate, paymentMethod, notes);
            return null;
        }

        public static bool IsPaymentExist(int paymentID)
        {
            return clsPaymentsData.IsPaymentExist(paymentID);
        }

        public static bool DeletePayment(int paymentID)
        {
            return clsPaymentsData.DeletePayment(paymentID);
        }


        public static clsPayments GetLastPaymentOfMember(int memberID)
        {
            DataRow row = clsPaymentsData.GetLastPaymentOfMember(memberID);

            if (row != null)
            {
                return new clsPayments(
                    (int)row["PaymentID"],
                    (int)row["ForMemberID"],          // memberID
                    (int)row["SubscriptionID"],       // subscriptionID
                    (decimal)row["Amounth"],           // amount
                    (DateTime)row["PaymentDate"],     // paymentDate
                    Convert.ToByte(row["PaymentMethod"]), //  convert bool/bit to byte
                    row["Notes"] == DBNull.Value ? "" : (string)row["Notes"]
                );
            }
            else
            {
                return null;
            }
        }



        public bool Save()
        {
            if (_Mode == enMode.AddNew)
            {
                if (_AddNewPayment()) { _Mode = enMode.Update; return true; }
                return false;
            }
            return _UpdatePayment();
        }
    }
}
