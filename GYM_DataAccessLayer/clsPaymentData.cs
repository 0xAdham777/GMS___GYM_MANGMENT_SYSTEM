using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GYM_DataAccessLayer.Global_DB;

namespace GYM_DataAccessLayer
{
    public class clsPaymentsData
    {
        public static int AddNewPayment(int MemberID, int SubscriptionID, decimal Amount, DateTime PaymentDate, byte PaymentMethod,string Notes)
        {
            int NewPaymentID = -1;

            try
            {
                using (SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewPayment", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@MemberID", MemberID);
                        command.Parameters.AddWithValue("@SubscriptionID", SubscriptionID);
                        command.Parameters.AddWithValue("@Amount", Amount);
                        command.Parameters.AddWithValue("@PaymentDate", PaymentDate);
                        command.Parameters.AddWithValue("@Notes", string.IsNullOrWhiteSpace(Notes) ? (object)DBNull.Value : Notes);
                        command.Parameters.AddWithValue("@PaymentMethod", PaymentMethod);


                        var output = new SqlParameter("@NewPaymentID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(output);

                        connection.Open();
                        command.ExecuteNonQuery();

                        if (command.Parameters["@NewPaymentID"].Value != DBNull.Value)
                            NewPaymentID = (int)command.Parameters["@NewPaymentID"].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobal.SetErrorInEventLog(ex.Message);
            }

            return NewPaymentID;
        }

        public static bool UpdatePayment(int PaymentID, int MemberID, int SubscriptionID, decimal Amount, DateTime PaymentDate, byte PaymentMethod,string Notes)
        {
            int rows = 0;
            try
            {
                using (SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdatePayment", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PaymentID", PaymentID);
                        command.Parameters.AddWithValue("@MemberID", MemberID);
                        command.Parameters.AddWithValue("@SubscriptionID", SubscriptionID);
                        command.Parameters.AddWithValue("@Amount", Amount);
                        command.Parameters.AddWithValue("@PaymentDate", PaymentDate);
                        command.Parameters.AddWithValue("@Notes", string.IsNullOrWhiteSpace(Notes) ? (object)DBNull.Value : Notes);
                        command.Parameters.AddWithValue("@PaymentMethod", PaymentMethod);

                        connection.Open();
                        rows = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobal.SetErrorInEventLog(ex.Message);
            }
            return rows > 0;
        }

        public static bool DeletePayment(int PaymentID)
        {
            int rows = 0;
            try
            {
                using (SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_DeletePayment", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PaymentID", PaymentID);

                        connection.Open();
                        rows = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobal.SetErrorInEventLog(ex.Message);
            }
            return rows > 0;
        }

        public static bool IsPaymentExist(int PaymentID)
        {
            bool exists = false;
            try
            {
                using (SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_IsPaymentExist", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PaymentID", PaymentID);

                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                            exists = Convert.ToBoolean(result);
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobal.SetErrorInEventLog(ex.Message);
            }
            return exists;
        }

        public static DataTable GetAllPayments()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetAllPayments", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows) dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobal.SetErrorInEventLog(ex.Message);
            }
            return dt;
        }

        public static bool GetPaymentInfoByID(int PaymentID, ref int MemberID, ref int SubscriptionID, ref decimal Amount, ref DateTime PaymentDate, ref byte PaymentMethod, ref string Notes)
        {
            bool found = false;
            try
            {
                using (SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetPaymentByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PaymentID", PaymentID);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                MemberID = (int)reader["MemberID"];
                                SubscriptionID = (int)reader["SubscriptionID"];
                                Amount = (decimal)reader["Amount"];
                                PaymentDate = (DateTime)reader["PaymentDate"];
                                PaymentMethod = (byte)((bool)reader["PaymentMethod"] ? 1 : 0);

                                Notes = ((string)reader["Notes"] == (object)DBNull.Value) ? "" : (string)reader["Notes"];

                                found = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobal.SetErrorInEventLog(ex.Message);
            }
            return found;
        }



        public static DataRow GetLastPaymentOfMember(int memberID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetLastPaymentOfMember", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MemberID", memberID);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                dt.Load(reader);
                            else
                                return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobal.SetErrorInEventLog(ex.Message);
                return null;
            }

            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }



    }
}
