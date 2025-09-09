using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GYM_DataAccessLayer.Global_DB
{
    public  class clsGlobal
    {
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


        static public string HashingText(string input)
        {
            using (SHA256 sHA256 = SHA256.Create())
            {
                Byte[] hashBytes = sHA256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

    }
}
