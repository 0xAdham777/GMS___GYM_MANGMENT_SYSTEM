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
    public class clsBloodData
    {
        public static short AddNewBlood(string BloodType)
        {
            short NewBloodID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewBlood", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BloodType", BloodType);

                        SqlParameter outputParamter = new SqlParameter("@NewBloodID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputParamter);

                        connection.Open();
                        command.ExecuteNonQuery();

                        if (command.Parameters["@NewBloodID"].Value != DBNull.Value)
                        {
                            NewBloodID = (short)command.Parameters["@NewBloodID"].Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobal.SetErrorInEventLog(ex.Message);
            }

            return NewBloodID;
        }

        public static bool UpdateBlood(int BloodID, string BloodType)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateBlood", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BloodID", BloodID);
                        command.Parameters.AddWithValue("@BloodType", BloodType);

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

        public static bool DeleteBlood(int BloodID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteBlood", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BloodID", BloodID);

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

        public static bool IsBloodExist(int BloodID)
        {
            bool IsExist = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_IsBloodExist", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BloodID", BloodID);

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

        public static DataTable GetAllBloods()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetAllBloods", connection))
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

        public static bool GetBloodByID(int BloodID,ref string BloodType)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetBloodByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BloodID", BloodID);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;

                                BloodType = reader["BloodName"] != DBNull.Value? reader["BloodName"].ToString() : "";

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



        public static bool GetBloodByBloodName(ref short BloodID,  string BloodName)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetBloodByBloodName", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@BloodName", BloodName);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;

                                BloodID = (short)reader["BloodID"];

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
    }
}
