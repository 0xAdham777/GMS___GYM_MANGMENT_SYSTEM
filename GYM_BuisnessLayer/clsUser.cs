using GYM_DataAccessLayer;
using GYM_DataAccessLayer.Global_DB;
using System;
using System.Data;

namespace GYM_BuisnessLayer
{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public clsPerson PersonInfo;

        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(PersonID, UserName, Password, IsActive);
            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {
      
            return clsUserData.UpdateUser(UserID, PersonID, UserName, Password, IsActive);
        }

        public clsUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = true;
            _Mode = enMode.AddNew;
        }

        private clsUser(int userID, int personID, string userName, string password, bool isActive)
        {
            this.UserID = userID;
            this.PersonID = personID;
            this.UserName = userName;
            this.Password = password; 
            this.IsActive = isActive;

            PersonInfo = clsPerson.Find(personID);

            _Mode = enMode.Update;
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        public static clsUser Find(int userID)
        {
            int personID = -1;
            string userName = "";
            string password = "";
            bool isActive = false;

            if (clsUserData.GetUserByID(userID, ref personID, ref userName, ref password, ref isActive))
            {
                return new clsUser(userID, personID, userName, password, isActive);
            }
            else
            {
                return null;
            }
        }


        public static clsUser Find(string UserName,string Password)
        {
            int personID = -1;
            int userID = -1;
            bool isActive = false;

            if (clsUserData.GetUserByUsernameAndPassword(ref userID, ref personID, UserName, Password, ref isActive))
            {
                return new clsUser(userID, personID, UserName, Password, isActive);
            }
            else
            {
                return null;
            }
        }

        public static bool IsExistByPersonID(int personID)
        {
            return clsUserData.IsUserExistByPersonID(personID);
        }



        public static bool IsExist(int userID)
        {
            return clsUserData.IsUserExist(userID);
        }

        public static bool IsExist(string USerName)
        {
            return clsUserData.IsUserExist(USerName);
        }

        public static bool Delete(int userID)
        {
            return clsUserData.DeleteUser(userID);
        }


        public static bool IsPasswordCorrect(int userID,string Password)
        {
            return clsUserData.IsPasswordCorrect(userID,Password);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateUser();

                default:
                    return false;
            }
        }
    }
}
