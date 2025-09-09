using GYM_DataAccessLayer;
using GYM_DataAccessLayer.Global_DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYM_BuisnessLayer
{
    public class clsService
    {

        public static int AddMemberWithSubscriptionAndPayment(
       int personID, decimal debts, int remainingDays, bool status, string notes,
       int createByUserID, DateTime startDate, DateTime endDate, int subscriptionTypeID,
       decimal amount, bool paymentMethod, string paymentsNotes)
        {
            return clsServiceData.AddNewMemberWithSubscriptionAndPayment(
                personID, debts, remainingDays, status, notes,
                createByUserID, startDate, endDate, subscriptionTypeID,
                amount, paymentMethod, paymentsNotes) ;
        }


        public static bool UpdateMemberWithSubscriptionAndPayment(
          int memberID, int personID, decimal debts, int remainingDays, bool status, string memberNotes,
          int subscriptionID, int createByUserID, DateTime startDate, DateTime endDate, int subscriptionTypeID,
          int paymentID, decimal amount, bool paymentMethod, string paymentsNotes)
        {
            return clsServiceData.UpdateMemberWithSubscriptionAndPayment(
                memberID, personID, debts, remainingDays, status, memberNotes,
                subscriptionID, createByUserID, startDate, endDate, subscriptionTypeID,
                paymentID, amount, paymentMethod, paymentsNotes
            );
        }



        public static bool BackupDatabase(string filePath)
        {
            return clsServiceData.BackupDatabase(filePath);
        }


    }
}
