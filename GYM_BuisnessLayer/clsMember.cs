using GYM_DataAccessLayer;
using System;
using System.Data;

namespace GYM_BuisnessLayer
{
    public class clsMember
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;

        public int MemberID { get; set; }
        public int PersonID { get; set; }
        public decimal Debts { get; set; }
        public int RemainingDays { get; set; }
        public bool Status { get; set; }
        public string Notes { get; set; }

        public clsPerson PersonInfo;

        private bool _AddNewMember()
        {
            this.MemberID = clsMemberData.AddNewMember(PersonID, Debts, RemainingDays, Status, Notes);
            return (this.MemberID != -1);
        }

        private bool _UpdateMember()
        {
            return clsMemberData.UpdateMember(MemberID, PersonID, Debts, RemainingDays, Status, Notes);
        }

        public clsMember()
        {
            this.MemberID = -1;
            this.PersonID = -1;
            this.Debts = 0;
            this.RemainingDays = 0;
            this.Status = true;
            this.Notes = "";
            _Mode = enMode.AddNew;
        }

        public clsMember(int memberID, int personID, decimal debts, int remainingDays, bool status, string notes)
        {
            this.MemberID = memberID;
            this.PersonID = personID;
            this.Debts = debts;
            this.RemainingDays = remainingDays;
            this.Status = status;
            this.Notes = notes;

            PersonInfo = clsPerson.Find(personID);

            _Mode = enMode.Update;
        }

        public static DataTable GetAllMembers()
        {
            return clsMemberData.GetAllMembers();
        }


        public static clsMember Find(int memberID)
        {
            int personID = -1;
            decimal debts = 0;
            int remainingDays = 0;
            bool status = false;
            string notes = "";

            if (clsMemberData.GetMemberByID(memberID, ref personID, ref debts, ref remainingDays, ref status, ref notes))
            {
                return new clsMember(memberID, personID, debts, remainingDays, status, notes);
            }
            else
            {
                return null;
            }
        }


        public static clsMember FindByPersonID(int personID)
        {
            int memberID = -1;
            decimal debts = 0;
            int remainingDays = 0;
            bool status = false;
            string notes = "";

            if (clsMemberData.GetMemberByPersonID(personID, ref memberID, ref debts, ref remainingDays, ref status, ref notes))
            {
                return new clsMember(memberID, personID, debts, remainingDays, status, notes);
            }
            else
            {
                return null;
            }
        }



        public static bool IsExist(int memberID)
        {
            return clsMemberData.IsMemberExistByID(memberID);
        }

        public static bool IsExistByPersonID(int personID)
        {
            return clsMemberData.IsMemberExistByPersonID(personID);
        }


        public static bool Delete(int memberID)
        {
            return clsMemberData.DeleteMember(memberID);
        }


        public static DataTable GetAllMemberInfo()
        {
            return clsMemberData.GetAllMemberInfo();
        }


        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewMember())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateMember();

                default:
                    return false;
            }
        }
    }
}
