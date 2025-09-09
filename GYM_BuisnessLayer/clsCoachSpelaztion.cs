using GYM_DataAccessLayer;
using System;
using System.Data;

namespace GYM_BuisnessLayer
{
    public class clsCoachSpezalations
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;

        public int CoachSpezalationsID { get; set; }
        public string SpelaztionsName { get; set; }
        public string Description { get; set; }

        private bool _AddNew()
        {
            this.CoachSpezalationsID =
                clsCoachSpezalationsData.AddNewCoachSpezalation(SpelaztionsName,Description);
            return (this.CoachSpezalationsID != -1);
        }

        private bool _Update()
        {
            return clsCoachSpezalationsData.UpdateCoachSpezalation(
                CoachSpezalationsID, SpelaztionsName,Description);
        }

        public clsCoachSpezalations()
        {
            CoachSpezalationsID = -1;
            SpelaztionsName = "";
            Description = "";
            _Mode = enMode.AddNew;
        }

        private clsCoachSpezalations(int coachSpezalationsID, string spelaztionsName,string description)
        {
            CoachSpezalationsID = coachSpezalationsID;
            SpelaztionsName = spelaztionsName;
            Description = description;
            _Mode = enMode.Update;
        }

        public static DataTable GetAll()
        {
            return clsCoachSpezalationsData.GetAllCoachSpezalations();
        }

        public static clsCoachSpezalations Find(int coachSpezalationsID)
        {
            string name = "", description ="";
            if (clsCoachSpezalationsData.GetCoachSpezalationByID(coachSpezalationsID, ref name,ref description))
                return new clsCoachSpezalations(coachSpezalationsID, name,description);
            return null;
        }

        public static clsCoachSpezalations Find(string spezalationName)
        {
            DataRow row = clsCoachSpezalationsData.GetCoachSpezalationByName(spezalationName);

            if (row != null)
            {
                return new clsCoachSpezalations(
                    (int)row["CoachSpezalationsID"],
                    (string)row["SpezalationsName"],
                    row["Description"] == DBNull.Value ? null : (string)row["Description"]
                );
            }
            else
            {
                return null;
            }
        }


        public static bool IsExist(int coachSpezalationsID)
        {
            return clsCoachSpezalationsData.IsCoachSpezalationExist(coachSpezalationsID);
        }

        public static bool IsExistByName(string spezalationsName)
        {
            return clsCoachSpezalationsData.IsCoachSpelazationExistByName(spezalationsName);
        }


        public static bool Delete(int coachSpezalationsID)
        {
            return clsCoachSpezalationsData.DeleteCoachSpezalation(coachSpezalationsID);
        }

        public bool Save()
        {
            if (_Mode == enMode.AddNew)
            {
                if (_AddNew()) 
                {
                    _Mode = enMode.Update; 
                    return true; 
                }
                return false;
            }
            return _Update();
        }
    }
}
