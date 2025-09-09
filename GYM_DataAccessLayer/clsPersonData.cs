using GYM_DataAccessLayer.Global_DB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GYM_DataAccessLayer
{
    public class clsPersonData
    {

        public static int AddNewPerson(string FirstName,string MidName,string LastName,DateTime BirthDate,string Email,string PhoneNumber,byte Gender,short BloodID,string Address,string PhotoPath)
        {
            int NewPersonID = -1;

            try
            {

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {

                    using (SqlCommand command = new SqlCommand("SP_AddNewPerson", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@FirstName", FirstName);
                        command.Parameters.AddWithValue("@MidName", string.IsNullOrWhiteSpace(MidName) ? (object)DBNull.Value : MidName);
                        command.Parameters.AddWithValue("@LastName", LastName);
                        command.Parameters.AddWithValue("@BirthDate", BirthDate);
                        command.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(Email) ? (object)DBNull.Value : Email);
                        command.Parameters.AddWithValue("@Gender", Gender);
                        command.Parameters.AddWithValue("@BloodID", BloodID);
                        command.Parameters.AddWithValue("@Address", string.IsNullOrWhiteSpace(Address) ? (object)DBNull.Value : Address);
                        command.Parameters.AddWithValue("@PhotoPath", string.IsNullOrWhiteSpace(PhotoPath) ? (object)DBNull.Value : PhotoPath);
                        command.Parameters.AddWithValue("@PhoneNumber", string.IsNullOrWhiteSpace(PhoneNumber) ? (object)DBNull.Value : PhoneNumber);


                        SqlParameter outputParamter = new SqlParameter("@NewPersonID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(outputParamter);



                        connection.Open();

                        command.ExecuteNonQuery();

                        if (command.Parameters["@NewPersonID"].Value != DBNull.Value)
                        {
                            NewPersonID = (int)command.Parameters["@NewPersonID"].Value;
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                clsGlobal.SetErrorInEventLog(ex.Message);
            }

            return NewPersonID;

        }


        public static bool UpdatePerson(int PersonID, string FirstName, string MidName, string LastName, DateTime BirthDate, string Email, string PhoneNumber, byte Gender, short BloodID, string Address, string PhotoPath)
        {

             int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdatePerson", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@FirstName", FirstName);
                        command.Parameters.AddWithValue("@MidName", string.IsNullOrWhiteSpace(MidName) ? (object)DBNull.Value : MidName);
                        command.Parameters.AddWithValue("@LastName", LastName);
                        command.Parameters.AddWithValue("@BirthDate", BirthDate);
                        command.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(Email) ? (object)DBNull.Value : Email);
                        command.Parameters.AddWithValue("@Gender", Gender);
                        command.Parameters.AddWithValue("@BloodID", BloodID);
                        command.Parameters.AddWithValue("@Address", string.IsNullOrWhiteSpace(Address) ? (object)DBNull.Value : Address);
                        command.Parameters.AddWithValue("@PhotoPath", string.IsNullOrWhiteSpace(PhotoPath) ? (object)DBNull.Value : PhotoPath);
                        command.Parameters.AddWithValue("@PhoneNumber", string.IsNullOrWhiteSpace(PhoneNumber) ? (object)DBNull.Value : PhoneNumber);

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


        public static bool DeletePerson(int PersonID)
        {

            int rowsAffected =0 ;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_DeletePerson", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonID);

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


        public static bool IsPersonExist(int PersonID)
        {
            bool IsExist = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_IsPersonExists", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PersonID", PersonID);

                      
                        connection.Open();
                      
                        IsExist = Convert.ToBoolean(command.ExecuteNonQuery());

                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobal.SetErrorInEventLog(ex.Message);
            }

            return IsExist;
        }



        public static DataTable GetAllPersons()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetAllPersons", connection))
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


        public static bool GetPersonByID(int PersonID, ref string FirstName, ref string MidName, ref string LastName,
                ref DateTime BirthDate, ref string Email, ref string PhoneNumber, ref byte Gender,
                ref short BloodID, ref string Address, ref string PhotoPath)
        {
            bool IsFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetPersonByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonID);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;


                                FirstName = (string)reader["FirstName"];

                                if (reader["MidName"] == System.DBNull.Value)
                                {
                                    MidName = "";
                                }
                                else
                                {
                                    MidName = (string)reader["MidName"];

                                }

                                LastName = (string)reader["LastName"];

                                if (reader["BirthDate"] == System.DBNull.Value)
                                {
                                    BirthDate = DateTime.Now;
                                }
                                else
                                {
                                    BirthDate = (DateTime)reader["BirthDate"];

                                }

                                if (reader["Email"] == System.DBNull.Value)
                                {
                                    Email = "";
                                }
                                else
                                {
                                    Email = (string)reader["Email"];

                                }

                                if (reader["PhoneNumber"] == System.DBNull.Value)
                                {
                                    PhoneNumber = "";
                                }
                                else
                                {
                                    PhoneNumber = (string)reader["PhoneNumber"];

                                }



                                Gender = (bool)reader["Gender"] ? (byte)1 : (byte)0;


                                BloodID = (short)reader["BloodID"];

                                if (reader["PhotoPath"] == System.DBNull.Value)
                                {
                                    PhotoPath = "";
                                }
                                else
                                {
                                    PhotoPath = (string)reader["PhotoPath"];

                                }


                                if (reader["Address"] == System.DBNull.Value)
                                {
                                    Address = "";
                                }
                                else
                                {
                                    Address = (string)reader["Address"];

                                }


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
