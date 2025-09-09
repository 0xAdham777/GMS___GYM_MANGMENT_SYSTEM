using GYM_BusinessLayer;
using GYM_DataAccessLayer;
using System;
using System.Data;

namespace GYM_BuisnessLayer
{
    public class clsSubscriptions
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;

        public int SubscriptionID { get; set; }
        public int MemberID { get; set; }  
        public clsMember ForMemberInfo;
        public int CreateByUserID { get; set; }
        public clsUser CreateByUserInfo;

   

        public int SubscriptionTypeID { get; set; }
        public clsSubscriptionTypes SubscriptionTypeInfo;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime SubscriptionDate { get; set; }

        private bool _AddNew()
        {
            this.SubscriptionID = clsSubscriptionsData.AddNewSubscription(
                CreateByUserID,
                MemberID,
                SubscriptionDate,
                StartDate,
                EndDate,
                SubscriptionTypeID);

            return (this.SubscriptionID != -1);
        }

        private bool _Update()
        {
            return clsSubscriptionsData.UpdateSubscription(
                SubscriptionID,
                CreateByUserID,
                MemberID,
                SubscriptionDate,
                StartDate,
                EndDate,
                SubscriptionTypeID);
        }

        public clsSubscriptions()
        {
            SubscriptionID = -1;
            MemberID = -1;
            CreateByUserID = -1;  
            SubscriptionTypeID = -1;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            SubscriptionDate = DateTime.Now;
            _Mode = enMode.AddNew;
        }

        public clsSubscriptions(int subscriptionID, int memberID, int createByUserID,
            DateTime subscriptionDate, DateTime startDate, DateTime endDate, int subscriptionTypeID)
        {
            SubscriptionID = subscriptionID;
            MemberID = memberID;
            CreateByUserID = createByUserID;   
            ForMemberInfo = clsMember.Find(MemberID);

            SubscriptionTypeID = subscriptionTypeID;
            SubscriptionTypeInfo = clsSubscriptionTypes.Find(SubscriptionTypeID);

            StartDate = startDate;
            EndDate = endDate;
            SubscriptionDate = subscriptionDate;

            _Mode = enMode.Update;
        }

        public static DataTable GetAllSubscriptions()
        {
            return clsSubscriptionsData.GetAllSubscriptions();
        }

        public static clsSubscriptions Find(int subscriptionID)
        {
            int memberID = -1, subscriptionTypeID = -1, createByUserID = -1;
            DateTime startDate = DateTime.Now, endDate = DateTime.Now, subDate = DateTime.Now;
            string status = "";

            if (clsSubscriptionsData.GetSubscriptionInfoByID(subscriptionID,
                ref createByUserID,
                ref memberID,
                ref subscriptionTypeID,
                ref startDate,
                ref endDate,
                ref status))
            {
                
                return new clsSubscriptions(subscriptionID, memberID, createByUserID,
                    subDate, startDate, endDate, subscriptionTypeID);
            }
            return null;
        }

        public static bool IsSubscriptionExist(int subscriptionID)
        {
            return clsSubscriptionsData.IsSubscriptionExist(subscriptionID);
        }

        public static bool DeleteSubscription(int subscriptionID)
        {
            return clsSubscriptionsData.DeleteSubscription(subscriptionID);
        }

        public static clsSubscriptions GetLastSubscriptionOfMember(int memberID)
        {
            DataRow row = clsSubscriptionsData.GetLastSubscriptionOfMember(memberID);

            if (row != null)
            {
                return new clsSubscriptions(
                    (int)row["SubscriptionID"],
                    (int)row["ForMemberID"],
                    (int)row["CreateByUserID"],
                    (DateTime)row["SubscriptionDate"],
                    (DateTime)row["StartDate"],
                    (DateTime)row["EndDate"],
                    (int)row["SubscriptionTypeID"]
                );
            }
            else
            {
                return null;
            }
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
