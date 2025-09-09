using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using GYM_DataAccessLayer.Global_DB;

namespace GYM_DataAccessLayer
{
    public class clsMemberData
    {
        public static int AddNewMember(int PersonID, decimal Debts, int RemainingDays, bool Status, string Notes)
        {
            int NewMemberID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_AddMember", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@Debts", Debts);
                        command.Parameters.AddWithValue("@RemainingDays", RemainingDays);
                        command.Parameters.AddWithValue("@Status", Status);
                        command.Parameters.AddWithValue("@Notes", string.IsNullOrWhiteSpace(Notes) ? (object)DBNull.Value : Notes);

                        SqlParameter outputParamter = new SqlParameter("@NewMemberID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputParamter);

                        connection.Open();
                        command.ExecuteNonQuery();

                        if (command.Parameters["@NewMemberID"].Value != DBNull.Value)
                        {
                            NewMemberID = (int)command.Parameters["@NewMemberID"].Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobal.SetErrorInEventLog(ex.Message);
            }

            return NewMemberID;
        }


        public static int AddNewMemberWithAllInformation(int personID, decimal debts, int remainingDays, bool status, string notes,
                int createByUserID, DateTime startDate, DateTime endDate, int subscriptionTypeID,
                decimal amount, bool paymentMethod, string paymentsNotes)
        {
            int newMemberID = -1;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_AddMemberWithAllInfo", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PersonID", personID);
                    cmd.Parameters.AddWithValue("@Debts", debts);
                    cmd.Parameters.AddWithValue("@RemainingDays", remainingDays);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@MemberNotes", notes ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreateByUserID", createByUserID);
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);
                    cmd.Parameters.AddWithValue("@SubscriptionTypeID", subscriptionTypeID);
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                    cmd.Parameters.AddWithValue("@PaymentsNotes", paymentsNotes ?? (object)DBNull.Value);

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



        public static bool UpdateMember(int MemberID, int PersonID, decimal Debts, int RemainingDays, bool Status, string Notes)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateMember", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@MemberID", MemberID);
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@Debts", Debts);
                        command.Parameters.AddWithValue("@RemainingDays", RemainingDays);
                        command.Parameters.AddWithValue("@Status", Status);
                        command.Parameters.AddWithValue("@Notes", string.IsNullOrWhiteSpace(Notes) ? (object)DBNull.Value : Notes);

                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobal.SetErrorInEventLog(ex.Message);
            }

            return rowsAffected > 0;
        }

        public static bool DeleteMember(int MemberID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteMember", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MemberID", MemberID);

                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobal.SetErrorInEventLog(ex.Message);
            }

            return rowsAffected > 0;
        }

        public static bool IsMemberExistByID(int MemberID)
        {
            bool IsExist = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_IsMemberExists", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MemberID", MemberID);

                        connection.Open();
                        IsExist = Convert.ToBoolean(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobal.SetErrorInEventLog(ex.Message);
            }

            return IsExist;
        }

        public static DataTable GetAllMembers()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetAllMembers", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                            }
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



        public static bool GetMemberByPersonID(int personID,
    ref int memberID, ref decimal debts, ref int remainingDays, ref bool status, ref string notes)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetMemberByPersonID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", personID);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                memberID = (int)reader["MemberID"];
                                debts = (decimal)reader["Debts"];
                                remainingDays = (int)reader["RemainingDays"];
                                status = (bool)reader["Status"];
                                notes = reader["Notes"] == DBNull.Value ? null : (string)reader["Notes"];
                                isFound = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobal.SetErrorInEventLog(ex.Message);
            }

            return isFound;
        }


        public static DataTable GetAllMemberInfo()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetAllMemberInfo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                            }
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


        public static bool GetMemberByID(int MemberID, ref int PersonID, ref decimal Debts, ref int RemainingDays, ref bool Status, ref string Notes)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetMemberByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MemberID", MemberID);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;

                                PersonID = (int)reader["PersonID"];
                                Debts = (decimal)reader["Debts"];
                                Status = (bool)reader["Status"];
                                Notes = (reader["Notes"] == DBNull.Value)? "" : (string)reader["Notes"];


                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobal.SetErrorInEventLog(ex.Message);
            }

            return IsFound;
        }


        public static bool IsMemberExistByPersonID(int personID)
        {
            bool isExist = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_IsMemberExistByPersonID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", personID);

                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                            isExist = Convert.ToInt32(result) == 1;
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobal.SetErrorInEventLog(ex.Message);
            }

            return isExist;
        }


    }
}
