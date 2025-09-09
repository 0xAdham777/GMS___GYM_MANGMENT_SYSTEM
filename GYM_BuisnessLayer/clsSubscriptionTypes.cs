using System;
using System.Data;
using GYM_DataAccessLayer;

namespace GYM_BusinessLayer
{
    public class clsSubscriptionTypes
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode = enMode.AddNew;

        public int SubscriptionTypeID { get; set; }
        public string SubscriptionName { get; set; }
        public decimal Price { get; set; }
        public int DurationDays { get; set; }
        public string Description { get; set; }


        private bool _AddNew()
        {
            this.SubscriptionTypeID = clsSubscriptionTypesData.AddNewSubscriptionType(
                this.SubscriptionName,
                this.Price,
                this.DurationDays,
                this.Description
            );

            return (this.SubscriptionTypeID != -1);
        }

        private bool _Update()
        {
            return clsSubscriptionTypesData.UpdateSubscriptionType(
                this.SubscriptionTypeID,
                this.SubscriptionName,
                this.Price,
                this.DurationDays,
                this.Description
            );
        }

        public clsSubscriptionTypes()
        {
            this.SubscriptionTypeID = -1;
            this.SubscriptionName = "";
            this.Price = 0;
            this.DurationDays = 0;
            this.Description = "";

            _Mode = enMode.AddNew;
        }

        private clsSubscriptionTypes(int subscriptionTypeID, string subscriptionName, decimal price, int durationDays, string description)
        {
            this.SubscriptionTypeID = subscriptionTypeID;
            this.SubscriptionName = subscriptionName;
            this.Price = price;
            this.DurationDays = durationDays;
            this.Description = description;

            _Mode = enMode.Update;
            Description = description;
        }

        public static DataTable GetAll()
        {
            return clsSubscriptionTypesData.GetAllSubscriptionTypes();
        }

        public static clsSubscriptionTypes Find(int subscriptionTypeID)
        {
            DataRow row = clsSubscriptionTypesData.GetSubscriptionTypeByID(subscriptionTypeID);

            if (row != null)
            {
                return new clsSubscriptionTypes(
                    (int)row["SubscriptionTypeID"],
                    (string)row["SubscriptionName"],
                    (decimal)row["Price"],
                    (int)row["DurationDays"],
                    (string)row["Description"]
                );
            }
            else
            {
                return null;
            }
        }



        public static clsSubscriptionTypes Find(string SubscriptionName)
        {
            DataRow row = clsSubscriptionTypesData.GetSubscriptionTypeByName(SubscriptionName);

            if (row != null)
            {
                return new clsSubscriptionTypes(
                    (int)row["SubscriptionTypeID"],
                    (string)row["SubscriptionName"],
                    (decimal)row["Price"],
                    (int)row["DurationDays"],
                    (string)row["Description"]
                );
            }
            else
            {
                return null;
            }
        }


        public static bool IsExist(int subscriptionTypeID)
        {
            return clsSubscriptionTypesData.IsSubscriptionTypeExist(subscriptionTypeID);
        }

        public static bool Delete(int subscriptionTypeID)
        {
            return clsSubscriptionTypesData.DeleteSubscriptionType(subscriptionTypeID);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _Update();

                default:
                    return false;
            }
        }
    }
}
