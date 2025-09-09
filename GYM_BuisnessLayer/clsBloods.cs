using GYM_DataAccessLayer;
using System;
using System.Data;

namespace GYM_BuisnessLayer
{
    public class clsBloods
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;

        public short BloodID { get; set; }
        public string BloodType { get; set; }

        private bool _AddNewBlood()
        {
            this.BloodID = clsBloodData.AddNewBlood(BloodType);
            return (this.BloodID != -1);
        }

        private bool _UpdateBlood()
        {
            return clsBloodData.UpdateBlood(BloodID, BloodType);
        }

        private clsBloods()
        {
            this.BloodID = -1;
            this.BloodType = "";
            _Mode = enMode.AddNew;
        }

        public clsBloods(short BloodID, string BloodType)
        {
            this.BloodID = BloodID;
            this.BloodType = BloodType;
            _Mode = enMode.Update;
        }

        public static DataTable GetAllBloods()
        {
            return clsBloodData.GetAllBloods();
        }

        public static clsBloods Find(short BloodID)
        {
            string BloodType = "";

            if (clsBloodData.GetBloodByID(BloodID, ref BloodType))
            {
                return new clsBloods(BloodID, BloodType);
            }
            else
            {
                return null;
            }
        }


        public static clsBloods Find(string BloodName)
        {
            short BloodID = -1;

            if (clsBloodData.GetBloodByBloodName(ref BloodID, BloodName))
            {
                return new clsBloods(BloodID, BloodName);
            }
            else
            {
                return null;
            }
        }

        public static bool IsExist(short BloodID)
        {
            return clsBloodData.IsBloodExist(BloodID);
        }

        public static bool Delete(short BloodID)
        {
            return clsBloodData.DeleteBlood(BloodID);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewBlood())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateBlood();

                default:
                    return false;
            }
        }
    }
}
