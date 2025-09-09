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
    public class clsUserData
    {
        public static int AddNewUser(int PersonID, string UserName, string Password, bool IsActive)
        {
            int NewUserID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@UserName", UserName);
                        command.Parameters.AddWithValue("@Password", clsGlobal.HashingText(Password));
                        command.Parameters.AddWithValue("@IsActive", IsActive);

                        SqlParameter outputParamter = new SqlParameter("@NewUserID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputParamter);

                        connection.Open();

                        command.ExecuteNonQuery();

                        if (command.Parameters["@NewUserID"].Value != DBNull.Value)
                        {
                            NewUserID = (int)command.Parameters["@NewUserID"].Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobal.SetErrorInEventLog(ex.Message);
            }

            return NewUserID;
        }

        public static bool UpdateUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@UserID", UserID);
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@UserName", UserName);
                        command.Parameters.AddWithValue("@Password", clsGlobal.HashingText(Password));
                        command.Parameters.AddWithValue("@IsActive", IsActive);

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

        public static bool DeleteUser(int UserID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", UserID);

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

        public static bool IsUserExist(int UserID)
        {
            bool IsExist = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_IsUserExist", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", UserID);

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

        public static bool IsUserExistByPersonID(int personID)
        {
            bool isExist = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_IsUserExistByPersonID", connection))
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


        public static bool IsUserExist(string UserName)
        {
            bool IsExist = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_IsUserExistByUserName", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserName", UserName);

                        connection.Open();


                        object result = command.ExecuteScalar();
                        IsExist = (result != null && Convert.ToInt32(result) == 1);
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobal.SetErrorInEventLog(ex.Message);
            }

            return IsExist;
        }


        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetAllUsers", connection))
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

        public static bool GetUserByID(int UserID, ref int PersonID, ref string UserName, ref string Password, ref bool IsActive)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetUserByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", UserID);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;

                                PersonID = (int)reader["PersonID"];
                                UserName = (string)reader["UserName"];
                                Password = (string)reader["Password"];
                                IsActive = (bool)reader["IsActive"];
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

        

        public static bool GetUserByUsernameAndPassword(ref int UserID, ref int PersonID, string UserName, string Password, ref bool IsActive)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetUserByUserNameAndPassword", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserName", UserName);
                        command.Parameters.AddWithValue("@Password", clsGlobal.HashingText( Password));

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;

                                UserID = (int)reader["UserID"];
                                PersonID = (int)reader["PersonID"];
                                IsActive = (bool)reader["IsActive"];
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


        public static bool IsPasswordCorrect(int UserID, string Password)
        {

            bool IsCorrect = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_IsPasswordCorrect", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", UserID);
                         command.Parameters.AddWithValue("@Password",clsGlobal.HashingText(Password));


                        connection.Open();


                        object result = command.ExecuteScalar();
                        IsCorrect = (result != null && Convert.ToInt32(result) == 1);
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobal.SetErrorInEventLog(ex.Message);
            }

            return IsCorrect;




        }

    }
}
