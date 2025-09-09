using GYM_BuisnessLayer;
using GYM_MS.Global;
using GYM_MS.Global_Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace GYM_MS.Global
{
    public class clsGlobal
    {
        static public clsUser CurrentUser;


        public static bool RememberUsernameAndPassword(string UserName,string Password)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\GMS");
                if (key != null)
                {
                    key.SetValue("UserName", UserName, RegistryValueKind.String);
                    key.SetValue("Password", Password, RegistryValueKind.String);
                    key.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                // ممكن تكتب اللوج في EventLog زي ما عامل
                SetErrorInEventLog(ex.Message);
                return false;
            }

        }


        public static bool GetStoredCredential(ref string UserName,ref string Password)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\GMS");
                if (key != null)
                {
                    UserName = key.GetValue("UserName", "").ToString();
                    Password = key.GetValue("Password", "").ToString();
                    key.Close();

                    return (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password));
                }
            }
            catch (Exception ex)
            {
                SetErrorInEventLog(ex.Message);
            }

            return false;

        }


        public static bool IsValidFilter(string input)
        {
            // Regex: أحرف عربية + إنجليزية + أرقام + مسافات فقط
            string pattern = @"^[\u0621-\u064Aa-zA-Z0-9 ]*$";

            return Regex.IsMatch(input, pattern);
        }

        public static bool ValidateEmail(string emailAddress)
        {
            var pattern = @"^[a-zA-Z0-9.!#$%&'*+-/=?^_`{|}~]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";

            var regex = new Regex(pattern);

            return regex.IsMatch(emailAddress);
        }

        public static bool ValidateInteger(string Number)
        {
            var pattern = @"^[0-9]*$";

            var regex = new Regex(pattern);

            return regex.IsMatch(Number);
        }

        public static bool ValidateFloat(string Number)
        {
            var pattern = @"^[0-9]*(?:\.[0-9]*)?$";

            var regex = new Regex(pattern);

            return regex.IsMatch(Number);
        }

        public static bool IsNumber(string Number)
        {
            return (ValidateInteger(Number) || ValidateFloat(Number));
        }

        static public string HashingText(string input)
        {
            using (SHA256 sHA256 = SHA256.Create())
            {
                Byte[] hashBytes = sHA256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }


        static public string Source = "GYM_DB_LOG";

        static public void SetErrorInEventLog(string Error)
        {
            if (!EventLog.SourceExists(Source))
            {
                // Create Event 
                EventLog.CreateEventSource(Source, "Application");

            }


            EventLog.WriteEntry(Source, "Error Message " + Error, EventLogEntryType.Error);
        }

    }
}
