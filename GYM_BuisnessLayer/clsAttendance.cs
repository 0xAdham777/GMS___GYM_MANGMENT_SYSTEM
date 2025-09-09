using GYM_BuisnessLayer;
using GYM_DataAccessLayer;
using System;
using System.Data;
using System.Reflection;

namespace GYM_BusinessLayer
{
    public class clsAttendance
    {
        public int AttendanceID { get; set; }
        public int MemberID { get; set; }
        public clsMember MemberInfo;
        public DateTime Date { get; set; }
        public TimeSpan CheckInTime { get; set; }
        public string Notes { get; set; }

        public clsAttendance()
        {
            AttendanceID = -1;
            MemberID = -1;
            Date = DateTime.Now.Date;
            CheckInTime = TimeSpan.Zero;
            Notes = string.Empty;
        }

        private clsAttendance(int attendanceID, int memberID, DateTime date, TimeSpan checkIn, string notes)
        {
            AttendanceID = attendanceID;
            MemberID = memberID;
            MemberInfo = clsMember.Find(memberID);
            Date = date;
            CheckInTime = checkIn;
            Notes = notes;
        }

        public bool Save()
        {
            if (AttendanceID == -1)
            {
                AttendanceID = clsAttendanceData.AddNewAttendance(MemberID, Date, CheckInTime, Notes);
                return AttendanceID != -1;
            }
            else
            {
                return clsAttendanceData.UpdateAttendance(AttendanceID, MemberID, Date, CheckInTime, Notes);
            }
        }

        public static bool Delete(int attendanceID)
        {
            return clsAttendanceData.DeleteAttendance(attendanceID);
        }

        public static bool Exists(int attendanceID)
        {
            return clsAttendanceData.IsAttendanceExist(attendanceID);
        }

        public static DataTable GetAll()
        {
            return clsAttendanceData.GetAllAttendances();
        }

        public static clsAttendance Find(int attendanceID)
        {
            DataRow row = clsAttendanceData.GetAttendanceByID(attendanceID);

            if (row != null)
            {
                return new clsAttendance(
                    (int)row["AttendanceID"],
                    (int)row["MemberID"],
                    (DateTime)row["Date"],
                    (TimeSpan)row["CheckInTime"],
                    row["Notes"] != DBNull.Value ? (string)row["Notes"] : string.Empty
                );
            }
            return null;
        }
    }
}
