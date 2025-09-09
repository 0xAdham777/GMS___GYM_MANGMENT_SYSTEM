using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GYM_DataAccessLayer.Global_DB;

namespace GYM_DataAccessLayer
{
    public class clsSubscriptionTypesData
    {
        public static int AddNewSubscriptionType(string SubscriptionName, decimal Price, int DurationDays,string Description)
        {
            int NewSubscriptionTypeID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewSubscriptionType", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@SubscriptionName", SubscriptionName);
                        command.Parameters.AddWithValue("@Price", Price);
                        command.Parameters.AddWithValue("@Description",(string.IsNullOrEmpty(Description))? (object)DBNull.Value : Description);
                        command.Parameters.AddWithValue("@DurationDays", DurationDays);

                        SqlParameter outputParam = new SqlParameter("@NewSubscriptionTypeID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);

                        connection.Open();
                        command.ExecuteNonQuery();

                        if (command.Parameters["@NewSubscriptionTypeID"].Value != DBNull.Value)
                            NewSubscriptionTypeID = (int)command.Parameters["@NewSubscriptionTypeID"].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobal.SetErrorInEventLog(ex.Message);
            }

            return NewSubscriptionTypeID;
        }

        public static bool UpdateSubscriptionType(int SubscriptionTypeID, string SubscriptionName, decimal Price, int DurationDays,string Description)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateSubscriptionType", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@SubscriptionTypeID", SubscriptionTypeID);
                        command.Parameters.AddWithValue("@SubscriptionName", SubscriptionName);
                        command.Parameters.AddWithValue("@Price", Price);
                        command.Parameters.AddWithValue("@DurationDays", DurationDays);
                        command.Parameters.AddWithValue("@Description", (string.IsNullOrEmpty(Description)) ? (object)DBNull.Value : Description);

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

        public static bool DeleteSubscriptionType(int SubscriptionTypeID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteSubscriptionType", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SubscriptionTypeID", SubscriptionTypeID);

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

        public static bool IsSubscriptionTypeExist(int SubscriptionTypeID)
        {
            bool isExist = false;

            try
            {
                using (SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_IsSubscriptionTypeExist", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SubscriptionTypeID", SubscriptionTypeID);

                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                            isExist = Convert.ToBoolean(result);
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobal.SetErrorInEventLog(ex.Message);
            }

            return isExist;
        }

        public static DataTable GetAllSubscriptionTypes()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetAllSubscriptionTypes", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                dt.Load(reader);
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


        public static DataRow GetSubscriptionTypeByName(string SubscriptionName)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetSubscriptionTypeByName", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SubscriptionName", SubscriptionName);

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



        public static DataRow GetSubscriptionTypeByID(int SubscriptionTypeID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetSubscriptionTypeByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SubscriptionTypeID", SubscriptionTypeID);

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
