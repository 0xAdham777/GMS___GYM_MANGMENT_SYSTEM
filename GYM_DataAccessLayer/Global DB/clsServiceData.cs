using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GYM_DataAccessLayer.Global_DB
{
    public class clsServiceData
    {


        public static int AddNewMemberWithSubscriptionAndPayment(int personID, decimal debts, int remainingDays, bool status, string notes,
    int createByUserID, DateTime startDate, DateTime endDate, int subscriptionTypeID,
    decimal amount, bool paymentMethod, string paymentsNotes)
        {
            int newMemberID = -1;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_AddMemberWithSubscriptionAndPayment", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PersonID", personID);
                    cmd.Parameters.AddWithValue("@Debts", debts);
                    cmd.Parameters.AddWithValue("@RemainingDays", remainingDays);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@MemberNotes", string.IsNullOrEmpty(notes) ? (object)DBNull.Value : notes);
                    cmd.Parameters.AddWithValue("@CreateByUserID", createByUserID);
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);
                    cmd.Parameters.AddWithValue("@SubscriptionTypeID", subscriptionTypeID);
                    cmd.Parameters.AddWithValue("@Amounth", amount);
                    cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                    cmd.Parameters.AddWithValue("@PaymentsNotes", string.IsNullOrEmpty(paymentsNotes) ? (object)DBNull.Value : paymentsNotes);

                    SqlParameter outputIdParam = new SqlParameter("@NewMemberID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputIdParam);

                    conn.Open();
                    cmd.ExecuteNonQuery();



                    if (cmd.Parameters["@NewMemberID"].Value != DBNull.Value)
                    {
                        newMemberID = (int)cmd.Parameters["@NewMemberID"].Value;
                    }

                }
            }

            return newMemberID;
        }

        public static bool UpdateMemberWithSubscriptionAndPayment(
     int memberID, int personID, decimal debts, int remainingDays, bool status, string memberNotes,
     int subscriptionID, int createByUserID, DateTime startDate, DateTime endDate, int subscriptionTypeID,
     int paymentID, decimal amount, bool paymentMethod, string paymentsNotes)
        {
            bool result = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateMemberWithSubscriptionAndPayment", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Member parameters
                        command.Parameters.AddWithValue("@MemberID", memberID);
                        command.Parameters.AddWithValue("@PersonID", personID);
                        command.Parameters.AddWithValue("@Debts", debts);
                        command.Parameters.AddWithValue("@RemainingDays", remainingDays);
                        command.Parameters.AddWithValue("@Status", status);
                        command.Parameters.AddWithValue("@MemberNotes", string.IsNullOrEmpty(memberNotes) ? (object)DBNull.Value : memberNotes);

                        // Subscription parameters
                        command.Parameters.AddWithValue("@SubscriptionID", subscriptionID);
                        command.Parameters.AddWithValue("@CreateByUserID", createByUserID);
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);
                        command.Parameters.AddWithValue("@SubscriptionTypeID", subscriptionTypeID);

                        // Payment parameters
                        command.Parameters.AddWithValue("@PaymentID", paymentID);
                        command.Parameters.AddWithValue("@Amount", amount);
                        command.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                        command.Parameters.AddWithValue("@PaymentsNotes", string.IsNullOrEmpty(paymentsNotes) ? (object)DBNull.Value : paymentsNotes);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        result = rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobal.SetErrorInEventLog(ex.Message);
            }

            return result;
        }



        public static bool BackupDatabase(string filePath)
        {
            bool isSuccess = false;
        try
        {

            
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_BackupDatabase", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FilePath", filePath);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    clsGlobal.SetErrorInEventLog(ex.Message);
                }
            }
        }
        catch (Exception ex)
        {
                clsGlobal.SetErrorInEventLog(ex.Message);
        }
            return isSuccess;
        }



    }
}
