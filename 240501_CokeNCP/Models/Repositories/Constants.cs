using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using BaseContest_WebForms.Models;
using System.Collections;
using SmsDome;
using System.Reflection;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.File; //Files
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Data;


namespace BaseContest_WebForms.Models
{
    public class Constants
    {
        //public readonly JavaScriptSerializer js = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };


        //<!--Contest Settings -->

        public readonly string Keyword = @System.Configuration.ConfigurationManager.AppSettings["Keyword"];

        public readonly DateTime TestDate = Convert.ToDateTime(@System.Configuration.ConfigurationManager.AppSettings["TestDate"]).ToUniversalTime();
        public readonly DateTime StartDate = Convert.ToDateTime(@System.Configuration.ConfigurationManager.AppSettings["StartDate"]).ToUniversalTime();
        public readonly DateTime EndDate = Convert.ToDateTime(@System.Configuration.ConfigurationManager.AppSettings["EndDate"]).ToUniversalTime();
        public readonly DateTime TerminationDate = Convert.ToDateTime(@System.Configuration.ConfigurationManager.AppSettings["TerminationDate"]).ToUniversalTime();

        public readonly string UploadLink = @System.Configuration.ConfigurationManager.AppSettings["UploadLink"];
        public readonly string ImageLink = @System.Configuration.ConfigurationManager.AppSettings["ImageLink"];

        public readonly string RepeatedMessageOnline = @System.Configuration.ConfigurationManager.AppSettings["RepeatedMessageOnline"];
        public readonly string RepeatedMessageSMS = @System.Configuration.ConfigurationManager.AppSettings["RepeatedMessageSMS"];

        public readonly string ErrorMessageGeneric = @System.Configuration.ConfigurationManager.AppSettings["GenericErrorMessage"];
        public readonly string ErrorMessageWhatsapp = @System.Configuration.ConfigurationManager.AppSettings["GenericErrorMessageWhatsapp"];
        public readonly string ErrorMessageAmount = @System.Configuration.ConfigurationManager.AppSettings["GenericErrorMessageAmount"];

        public readonly string ValidMessage = @System.Configuration.ConfigurationManager.AppSettings["ValidMessage"];
        public readonly string ValidMessageWhatsapp = @System.Configuration.ConfigurationManager.AppSettings["ValidMessageWhatsapp"];
        public readonly string ValidMessageOnline = @System.Configuration.ConfigurationManager.AppSettings["ValidMessageOnline"];
        public readonly string ValidMessageOnlineCompletion = @System.Configuration.ConfigurationManager.AppSettings["ValidMessageOnlineCompletion"];
        
        public readonly string VerifiedMessage = @System.Configuration.ConfigurationManager.AppSettings["VerifiedMessage"];
        public readonly string VerifiedEmailMessage = @System.Configuration.ConfigurationManager.AppSettings["VerifiedEmailMessage"];
        public readonly string RejectedMessage = @System.Configuration.ConfigurationManager.AppSettings["RejectedMessage"];
        public readonly string ResentMessage = @System.Configuration.ConfigurationManager.AppSettings["ResentMessage"];
        public readonly string MailFrom = @System.Configuration.ConfigurationManager.AppSettings["MailFrom"];

        public readonly IEnumerable<string> EntryExclusionFields = @System.Configuration.ConfigurationManager.AppSettings["EntryExclusionFields"].ToString().Split(',').ToList();
        public readonly IEnumerable<string> WinnerExclusionFields = @System.Configuration.ConfigurationManager.AppSettings["WinnerExclusionFields"].ToString().Split(',').ToList();

        public readonly string IP = @System.Configuration.ConfigurationManager.AppSettings["AuthorizedIP"];

        //       <!--Validations -->

        public readonly string ValidAmount = @System.Configuration.ConfigurationManager.AppSettings["ValidAmount"];
        public readonly decimal TierAmount = Convert.ToDecimal(@System.Configuration.ConfigurationManager.AppSettings["TierAmount"]);
        public readonly decimal TierChance = Convert.ToDecimal(@System.Configuration.ConfigurationManager.AppSettings["TierChance"]);

        public readonly string FieldNames = @System.Configuration.ConfigurationManager.AppSettings["Fields"];
        public readonly string ValidationRegexFull = @System.Configuration.ConfigurationManager.AppSettings["ValidationRegexFull"];
        public readonly string ValidationRegexMobileNo = @System.Configuration.ConfigurationManager.AppSettings["ValidationRegexMobileNo"];
        public readonly string ValidationRegexNRIC = @System.Configuration.ConfigurationManager.AppSettings["ValidationRegexNRIC"];
        public readonly string ValidationRegexName = @System.Configuration.ConfigurationManager.AppSettings["ValidationRegexName"];
        public readonly string ValidationRegexAmount = @System.Configuration.ConfigurationManager.AppSettings["ValidationRegexAmount"];
        public readonly string ValidationRegexReceiptNo = @System.Configuration.ConfigurationManager.AppSettings["ValidationRegexReceiptNo"];
        public readonly string ValidationRegexEmail = @System.Configuration.ConfigurationManager.AppSettings["ValidationRegexEmail"];
        public IEnumerable<string> ValidationRegexOptionalFields = @System.Configuration.ConfigurationManager.AppSettings["ValidationRegexOptionalFields"].ToString().Split(',').ToList();

        //    <!--Login Information -->

        public readonly string ContestUser = @System.Configuration.ConfigurationManager.AppSettings["ContestUser"];
        public readonly string ContestPWHash = @System.Configuration.ConfigurationManager.AppSettings["ContestPWHash"];
        public readonly string kvIdentityPW = @System.Configuration.ConfigurationManager.AppSettings["kvIdentityPW"];
        public readonly string kvIdentityUser = @System.Configuration.ConfigurationManager.AppSettings["kvIdentityUser"];
        public readonly string kvIdentityAdmin = @System.Configuration.ConfigurationManager.AppSettings["kvIdentityAdmin"];
        public readonly string ContestAdminUser = @System.Configuration.ConfigurationManager.AppSettings["ContestAdminUser"];
        public readonly string ContestAdminPWHash = @System.Configuration.ConfigurationManager.AppSettings["ContestAdminPWHash"];


        //     <!--Azure Storage Info-->

        public readonly string AzureStorageLink = @System.Configuration.ConfigurationManager.AppSettings["AzureStorageLink"].ToString(); // @Properties.Settings.Default.AzureStorageLink.ToString(); 
        public readonly string AzureStorageContainer = @System.Configuration.ConfigurationManager.AppSettings["AzureStorageContainer"].ToString();   //@Properties.Settings.Default.AzureStorageContainer.ToString();
        public readonly string AzureStorageAccountName = @System.Configuration.ConfigurationManager.AppSettings["AzureStorageAccountName"].ToString();
        public readonly string AzureStorageAccountKey = @System.Configuration.ConfigurationManager.AppSettings["AzureStorageAccountKey"].ToString();
        public readonly CloudStorageAccount storageAccount = CloudStorageAccount.Parse(@System.Configuration.ConfigurationManager.ConnectionStrings["StorageConnectionString"].ToString());


        //  <!--Azure Key Vault Info-->

        public readonly string AzureKeyVaultUri = @System.Configuration.ConfigurationManager.AppSettings["AzureKeyVaultUri"].ToString();

        //    <!-- SMS Account Settings -->

        public readonly string AppID = @System.Configuration.ConfigurationManager.AppSettings["AppID"];
        public readonly string AppSecret = @System.Configuration.ConfigurationManager.AppSettings["AppSecret"];

        //    <!-- Whatsapp Account Settings -->

        public readonly string OutboundURL = @System.Configuration.ConfigurationManager.AppSettings["OutboundURL"];
        public readonly int ContestID = Convert.ToInt32(@System.Configuration.ConfigurationManager.AppSettings["ContestID"]);

        //    <!-- Other Settings -->

        public readonly int AddLocalTimeZone = 8;
        public readonly int SubtractLocalTimeZone = -8;
        public readonly string DateTimeFormat = "dd MMM yyyy HH:mm:ss";


        public readonly string DefaultMobileNo = @System.Configuration.ConfigurationManager.AppSettings["DefaultMobileNo"];
        public readonly string DefaultTestMessage = @System.Configuration.ConfigurationManager.AppSettings["DefaultTestMessage"];

        public readonly string DefaultConnectionString = @System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        //Read from DB?

        /*
    public BC_240501_COKENCP_Settings GetSettings() {
        using (var db = new BaseEntities()) {
            return db.BC_240501_COKENCP_Settings.FirstOrDefault();
        }
    }

    public FunctionResult SetupSettings()
    {
        using (var db = new BaseEntities()) {
            db.BC_240501_COKENCP_Settings.Add(new BC_240501_COKENCP_Settings()
            {
                Keyword = "TEST",
                TestDate = Convert.ToDateTime("2016-01-01 16:00:00"),
                StartDate = Convert.ToDateTime("2017-01-01 16:00:00"),
                EndDate = Convert.ToDateTime("2017-12-31 15:59:00"),
                UploadLink = "http://smsdome-contest-base-sea-web-prd-development.azurewebsites.net/Views/OnlineCompletion",
                ImageLink = "https://www.smsdome.com/wp-content/uploads/2017/01/SmsDome_Official_Logo_2017-500px.png",
                RepeatedMessageOnline = "Repeated entry detected. We have received your previous entry well. Please input a new receipt number.",
                RepeatedMessageSMS = "Repeated entry detected. Note that you can only SMS in each receipt number once.",
                GenericErrorMessage = "Incomplete fields detected. Kindly SMS in all fields again. BASE <space> Name <space> NRIC <space> Receipt Number <space> Amount Spent (Min $25)",
                ValidMessage = "Thank you for your participation. If you wish to be kept updated about our future promotions via SMS , kindly SMS Y1 to 90933211 to opt in. Please visit {uploadlink} to complete your entry.",
                ValidMessageOnline = "Thank you for your participation. Your entry is well received.",

                EntryExclusionFields = "Response,NRIC_NoPrefix,Response,VerificationCode",
                WinnerExclusionFields = "Response,NRIC_NoPrefix,Response,VerificationCode",


                TierAmount = (decimal)25.00,
                TierChance = 1,

                Fields = "MSISDN,Name,NRIC,ReceiptNo,Amount",
                ValidationRegexFull = @"^(\+*\d+)( [\S ]+)( (?:S|T|s|t)?\d{7}[a-zA-Z])( \S+)( (?:[Ss]?\$)?(?:\d{1,10}(?:\,\d{3})*|(?:\d+))(?:\.\d{0,2})?)$",
                ValidationRegexMSISDN = @"^\+*\d+$",
                ValidationRegexNRIC = @"^(?:S|T|s|t)?\d{7}[a-zA-Z]$",
                ValidationRegexName = @"^[\S ]+$",
                ValidationRegexAmount = @"^(?:[Ss]?\$)?(?:\d{1,10}(?:\,\d{3})*|(?:\d+))(?:\.\d{0,2})?$",
                ValidationRegexAmount2 = @"([\d,.]+)",
                ValidationRegexReceiptNo = @"^\S+$",
                ValidationRegexEmail = "^$",

                ContestUser = "TEST",
                ContestAdminPW = "SMSDOME",
                ContestPW = "170101",
                IdentityUser = "BaseContestUser",
                IdentityAdmin = "BaseContestAdmin",
                IdentityPW = "f3b396a7-a95e-4a84-ade1-f475f42fa893",
                AzureStorageLink = "https://smsdomedevseasa.blob.core.windows.net/",
                AzureStorageContainer = "basecontestcontainer",
                AppID = 670,
                AppSecret = "822910db-cc3b-4649-b7ce-aba3fec73eb6",

            });

            db.SaveChanges();
        }

        return new FunctionResult(true) {message = "Successfully saved!" };

    }*/
    }
}