using GYM_DataAccessLayer;
using System;
using System.Data;

namespace GYM_BuisnessLayer
{
    public class clsPerson
    {
        public enum enGender { Male = 0, Female = 1 }
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;

        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return $"{FirstName} {MidName} {LastName}"; }
        }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public enGender Gender { get; set; }
        public string Address { get; set; }
        public short BloodID { get; set; }

        private string _ImagePath;

        public string PhotoPath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }

        public clsBloods BloodInfo;

        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(
                FirstName, MidName, LastName, BirthDate,
                Email, PhoneNumber, (byte)Gender, BloodID, Address, PhotoPath);

            return (this.PersonID != -1);
        }

        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(
                PersonID, FirstName, MidName, LastName,
                BirthDate, Email, PhoneNumber, (byte)Gender,
                BloodID, Address, PhotoPath);
        }

        public clsPerson()
        {
            this.PersonID = -1;
            this.FirstName = "";
            this.MidName = "";
            this.LastName = "";
            this.BirthDate = DateTime.Now;
            this.Email = "";
            this.PhoneNumber = "";
            this.Gender = enGender.Male;
            this.Address = "";
            this.BloodID = -1;
            this.PhotoPath = "";

            _Mode = enMode.AddNew;
        }

        private clsPerson(int PersonID, string FirstName, string MidName, string LastName,
            DateTime BirthDate, string Email, string PhoneNumber, byte Gender,
            short BloodID, string Address, string PhotoPath)
        {
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.MidName = MidName;
            this.LastName = LastName;
            this.BirthDate = BirthDate;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            this.Gender = (enGender)Gender;
            this.Address = Address;
            this.BloodID = BloodID;
            BloodInfo = clsBloods.Find(BloodID);
            this.PhotoPath = PhotoPath;

            _Mode = enMode.Update;
        }

        public static DataTable GetAllPersons()
        {
            return clsPersonData.GetAllPersons();
        }

        public static clsPerson Find(int PersonID)
        {
            string FirstName = "", MidName = "", LastName = "",
                   Email = "", PhoneNumber = "", Address = "", PhotoPath = "";
            DateTime BirthDate = DateTime.Now;
            byte Gender = 0;
            short BloodID = -1;

            if (clsPersonData.GetPersonByID(PersonID, ref FirstName, ref MidName, ref LastName,
                ref BirthDate, ref Email, ref PhoneNumber, ref Gender,
                ref BloodID, ref Address, ref PhotoPath))
            {
                return new clsPerson(PersonID, FirstName, MidName, LastName,
                    BirthDate, Email, PhoneNumber, Gender, BloodID, Address, PhotoPath);
            }
            else
            {
                return null;
            }
        }

        public static bool IsExist(int PersonID)
        {
            return clsPersonData.IsPersonExist(PersonID);
        }

        public static bool Delete(int PersonID)
        {
            return clsPersonData.DeletePerson(PersonID);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdatePerson();

                default:
                    return false;
            }
        }
    }
}
