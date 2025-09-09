using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GYM_DataAccessLayer.Global_DB;

namespace GYM_DataAccessLayer
{
    public class clsTrainingSessionsData
    {
        public static int AddNewTrainingSession(int CreateByUserID, int ForMemberID, int CoachID, DateTime StartTime, DateTime EndTime, string Notes)
        {
            int NewTrainingSessionID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewTrainingSession", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@CreateByUserID", CreateByUserID);
                        command.Parameters.AddWithValue("@ForMemberID", ForMemberID);
                        command.Parameters.AddWithValue("@CoachID", CoachID);
                        command.Parameters.AddWithValue("@StartTime", StartTime);
                        command.Parameters.AddWithValue("@EndTime", EndTime);
                        command.Parameters.AddWithValue("@Notes", string.IsNullOrWhiteSpace(Notes) ? (object)DBNull.Value : Notes);

                        SqlParameter outputParam = new SqlParameter("@NewTrainingSessionID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParam);

                        connection.Open();
                        command.ExecuteNonQuery();

                        if (command.Parameters["@NewTrainingSessionID"].Value != DBNull.Value)
                        {
                            NewTrainingSessionID = (int)command.Parameters["@NewTrainingSessionID"].Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobal.SetErrorInEventLog(ex.Message);
            }

            return NewTrainingSessionID;
        }

        public static bool UpdateTrainingSession(int TrainingSessionID, int CreateByUserID, int ForMemberID, int CoachID, DateTime StartTime, DateTime EndTime, string Notes)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateTrainingSession", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TrainingSessionID", TrainingSessionID);
                        command.Parameters.AddWithValue("@CreateByUserID", CreateByUserID);
                        command.Parameters.AddWithValue("@ForMemberID", ForMemberID);
                        command.Parameters.AddWithValue("@CoachID", CoachID);
                        command.Parameters.AddWithValue("@StartTime", StartTime);
                        command.Parameters.AddWithValue("@EndTime", EndTime);
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

        public static bool DeleteTrainingSession(int TrainingSessionID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_DeleteTrainingSession", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TrainingSessionID", TrainingSessionID);

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

        public static bool IsTrainingSessionExist(int TrainingSessionID)
        {
            bool IsExist = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_IsTrainingSessionExist", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TrainingSessionID", TrainingSessionID);

                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            IsExist = Convert.ToBoolean(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsGlobal.SetErrorInEventLog(ex.Message);
            }

            return IsExist;
        }

        public static DataTable GetAllTrainingSessions()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetAllTrainingSessions", connection))
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

        public static DataRow GetTrainingSessionByID(int TrainingSessionID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetTrainingSessionByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TrainingSessionID", TrainingSessionID);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                            }
                            else
                            {
                                return null;
                            }
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
