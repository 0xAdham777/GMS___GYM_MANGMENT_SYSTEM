using System;
using System.Data;
using GYM_BuisnessLayer;
using GYM_DataAccessLayer;

namespace GYM_BusinessLayer
{
    public class clsCoach
    {
        public int CoachID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo;
        public int CoachSpezalationsID { get; set; }
        public clsCoachSpezalations CoachSpezalationsInfo;
        public clsCoach()
        {
            CoachID = -1;
            PersonID = -1;
            CoachSpezalationsID = -1;
        }

        private clsCoach(int coachID, int personID, int coachSpezalationsID)
        {
            CoachID = coachID;
            PersonID = personID;
            PersonInfo = clsPerson.Find(PersonID);
            CoachSpezalationsID = coachSpezalationsID;
            CoachSpezalationsInfo = clsCoachSpezalations.Find(CoachSpezalationsID);
        }

        public bool Save()
        {
            if (CoachID == -1)
            {
                // Insert
                CoachID = clsCoachData.AddNewCoach(PersonID, CoachSpezalationsID);
                return CoachID != -1;
            }
            else
            {
                // Update
                return clsCoachData.UpdateCoach(CoachID, PersonID, CoachSpezalationsID);
            }
        }

        public static bool Delete(int coachID)
        {
            return clsCoachData.DeleteCoach(coachID);
        }

        public static bool Exists(int coachID)
        {
            return clsCoachData.IsCoachExist(coachID);
        }


        public static bool IsExistByPersonID(int personID)
        {
            return clsCoachData.IsCoachExistByPersonID(personID);
        }
        public static DataTable GetAll()
        {
            return clsCoachData.GetAllCoaches();
        }

        public static clsCoach FindByPersonID(int personID)
        {
            int coachID = -1, coachSpezalationsID = -1;

            if (clsCoachData.GetCoachByPersonID(personID, ref coachID, ref coachSpezalationsID))
            {
                return new clsCoach(coachID, personID, coachSpezalationsID);
            }
            else
            {
                return null;
            }
        }

        public static clsCoach Find(int coachID)
        {
            DataRow row = clsCoachData.GetCoachByID(coachID);
            if (row != null)
            {
                return new clsCoach(
                    (int)row["CoachID"],
                    (int)row["PersonID"],
                    (int)row["CoachSpezalationsID"]
                );
            }
            else
            {
                return null;
            }
        }
    }
}
