using System;
using System.Data;
using GYM_BuisnessLayer;
using GYM_DataAccessLayer;

namespace GYM_BusinessLayer
{
    public class clsTrainingSessions
    {
        public int TrainingSessionID { get; set; }
        public int CreateByUserID { get; set; }
        public clsUser CreateByUserInfo;
        public int ForMemberID { get; set; }
        public clsMember ForMemberInfo;

        public int CoachID { get; set; }
        public clsCoach CoachInfo;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Notes { get; set; }

        public clsTrainingSessions()
        {
            TrainingSessionID = -1;
            CreateByUserID = -1;
            ForMemberID = -1;
            CoachID = -1;
            StartTime = DateTime.Now;
            EndTime = DateTime.Now;
            Notes = string.Empty;
        }

        private clsTrainingSessions(int trainingSessionID, int createByUserID, int forMemberID, int coachID, DateTime startTime, DateTime endTime, string notes)
        {
            TrainingSessionID = trainingSessionID;
            CreateByUserID = createByUserID;
            CreateByUserInfo = clsUser.Find(createByUserID);
            ForMemberID = forMemberID;
            ForMemberInfo = clsMember.Find(ForMemberID);
            CoachID = coachID;
            CoachInfo = clsCoach.Find(CoachID);
            StartTime = startTime;
            EndTime = endTime;
            Notes = notes;
        }

        public bool Save()
        {
            if (TrainingSessionID == -1)
            {
                TrainingSessionID = clsTrainingSessionsData.AddNewTrainingSession(CreateByUserID, ForMemberID, CoachID, StartTime, EndTime, Notes);
                return TrainingSessionID != -1;
            }
            else
            {
                return clsTrainingSessionsData.UpdateTrainingSession(TrainingSessionID, CreateByUserID, ForMemberID, CoachID, StartTime, EndTime, Notes);
            }
        }

        public static bool Delete(int trainingSessionID)
        {
            return clsTrainingSessionsData.DeleteTrainingSession(trainingSessionID);
        }

        public static bool Exists(int trainingSessionID)
        {
            return clsTrainingSessionsData.IsTrainingSessionExist(trainingSessionID);
        }

        public static DataTable GetAll()
        {
            return clsTrainingSessionsData.GetAllTrainingSessions();
        }

        public static clsTrainingSessions Find(int trainingSessionID)
        {
            DataRow row = clsTrainingSessionsData.GetTrainingSessionByID(trainingSessionID);

            if (row != null)
            {
                return new clsTrainingSessions(
                    (int)row["TrainingSessionID"],
                    (int)row["CreateByUserID"],
                    (int)row["ForMemberID"],
                    (int)row["CoachID"],
                    (DateTime)row["StartTime"],
                    (DateTime)row["EndTime"],
                    row["Notes"] != DBNull.Value ? (string)row["Notes"] : string.Empty
                );
            }
            return null;
        }
    }
}
