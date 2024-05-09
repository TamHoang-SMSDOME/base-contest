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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.VisualBasic.FileIO;
using System.Threading;
using System.Net;
using System.Data.SqlClient;
using BaseContest_WebForms.Models.Repositories.Utility;

namespace BaseContest_WebForms.Models
{
    public sealed class Repository : GenericFunctions
    {
        #region Singleton Setup
        static readonly Repository _instance = new Repository();
        public static Repository Instance
        {
            get
            {
                return _instance;
            }
        }

        static readonly CustomLogger _logger = CustomLogger.GetInstance(_instance);
        public static CustomLogger Logger
        {
            get
            {
                return _logger;
            }
        }
        Repository()
        {
        }

        #endregion


        #region MMS OR SMS

        public FunctionResult SubmitEntry(Parameters param)
        {
            using (var db = new BaseEntities())
            {
                //try
                //{
                var FunctRes = new FunctionResult(true);
                FunctRes.IsSendSMS = true;
                FunctRes.Entry.IsValid = true;
                FunctRes.Entry.EntrySource = param.EntrySource;

                string errResponse = string.Empty;
                string validResponse = string.Empty;
                //validate response
                if (FunctRes.Entry.EntrySource.Equals("Sms", StringComparison.InvariantCultureIgnoreCase))
                {
                    errResponse = ErrorMessageGeneric;
                    validResponse = ValidMessage;
                }
                else if (FunctRes.Entry.EntrySource.Equals("Whatsapp", StringComparison.InvariantCultureIgnoreCase))
                {
                    errResponse = ErrorMessageWhatsapp;
                    validResponse = ValidMessageWhatsapp;
                }

                var CleanedMessage = CleanMessage(param);

                var CreatedOn = param.CreatedOn != null && param.CreatedOn != "" ? DateTime.Parse(param.CreatedOn.ToString()).ToUniversalTime() : System.DateTime.UtcNow;//jfield.ContainsKey("createdOn") ? JsonConvert.DeserializeObject<DateTime>(StandardContest.Models.Helpers.SerializerHelper(jfield["createdOn"].ToString()))  : System.DateTime.UtcNow;

                //ReturnFail only when message is totally not savable

                //Validation before matchfields
                var ValidationResults = PreMatchFieldValidations(CreatedOn);
                if (!ValidationResults.IsSuccessful)
                {
                    FunctRes.Entry.IsValid = false;
                    FunctRes.IsSendSMS = false;

                    FunctRes.ListOfReasonsForPossibleFailures.Add(ValidationResults.ReasonForFailure);
                    FunctRes.ListOfResponsesForPossibleFailures.Add(ValidationResults.ResponseForFailure);

                }

                #region SpecificRegexMatching
                Regex regex = new Regex(ValidationRegexFull, RegexOptions.IgnoreCase);
                var MatchedMessage = regex.Match(CleanedMessage.Trim());
                // Set Fields which dont require validation
                FunctRes.Entry.DateEntry = CreatedOn;
                FunctRes.Entry.MobileNo = param.MobileNo;
                FunctRes.Entry.EntryText = param.Message.ToString();

                FunctRes.Entry.FileLink = (param.EntrySource == "MMS" || param.EntrySource == "Whatsapp") && (param.FileLink != null && param.FileLink != "")/*in SMS, FileLink should always be empty*/ ? param.FileLink.ToString() : "";

                if (MatchedMessage.Success)
                {

                    //Matched fields will now have a space infront of them because the space is now inside the regex of each field.
                    var MatchedResultList = MatchedMessage.Groups.Cast<Group>().Select(match => match.Value).Skip(1).ToList().Select(s => s.Trim()).ToList();

                    var FieldsL = FieldNames.Split(',').ToList();
                    for (int k = 0; k < FieldsL.Count; k++)
                    {

                        if (FieldsL[k] == "Amount")
                        {
                            //logic will handle invalid or blank amounts
                            var regexAmt = new Regex(@ValidAmount);
                            var AMTmatch = regexAmt.Match(MatchedResultList[k]);
                            if (AMTmatch.Success)
                            {
                                FunctRes.Entry.Amount = Convert.ToDecimal(AMTmatch.Groups[1].Value);
                            }
                        }
                        //Other fields which require special Regex
                        else if (FieldsL[k] == "ReceiptNo")
                        {
                            var regexReceipt = new Regex(@ValidationRegexReceiptNo);
                            Regex rgx = new Regex("[^a-zA-Z0-9]");
                            var str = rgx.Replace(MatchedResultList[k], "");

                            //var AMTmatch = regexReceipt.Match(MatchedResultList[k]);
                            var AMTmatch = regexReceipt.Match(str);

                            if (AMTmatch.Success)
                            {
                                FunctRes.Entry.ReceiptNo = AMTmatch.Value;
                            }
                        }
                        else
                        {
                            //MatchedFields.Add(FieldsL[k], MatchedResultList[k]);

                            var prop = FunctRes.Entry.GetType().GetProperty(FieldsL[k]);
                            prop.SetValue(FunctRes.Entry, MatchedResultList[k]);
                        }
                    }

                    if (!string.IsNullOrEmpty(FunctRes.Entry.NRIC))
                        FunctRes.Entry.NRIC_NoPrefix = Char.IsLetter(FunctRes.Entry.NRIC.FirstOrDefault()) ? FunctRes.Entry.NRIC.Substring(1, FunctRes.Entry.NRIC.Length - 1) : FunctRes.Entry.NRIC;
                    else
                    {
                        FunctRes.Entry.NRIC = string.Empty;
                        FunctRes.Entry.NRIC_NoPrefix = string.Empty;
                    }

                    //Edit Entry Logics

                    FunctRes.Entry = EditEntry(FunctRes.Entry);



                    //Validate LOGIC using MatchedFields
                    ValidationResults = null;
                    ValidationResults = ValidationLogics(FunctRes.Entry, FunctRes.Entry.EntrySource);
                    if (!ValidationResults.IsSuccessful)
                    {
                        FunctRes.Entry.IsValid = false;

                        FunctRes.ListOfReasonsForPossibleFailures.Add(ValidationResults.ReasonForFailure);
                        FunctRes.ListOfResponsesForPossibleFailures.Add(ValidationResults.ResponseForFailure);

                    }

                    FunctRes.IsSavable = true;

                }
                else
                {

                    //If Invalid, for loop form regex on the fly to determine which field have error(use Key) RegexKeyPairValue,

                    var FieldsL = FieldNames.Split(',').ToList();
                    var BuildingRegex = "";
                    for (int k = 0; k < FieldsL.Count; k++)
                    {
                        var CurrentFieldRegex = @System.Configuration.ConfigurationManager.AppSettings["ValidationRegex" + FieldsL[k]];

                        if (BuildingRegex == "")
                        {
                            BuildingRegex = "(" + CurrentFieldRegex.Remove(CurrentFieldRegex.Length - 1).Remove(0, 1) + ")";
                        }
                        else
                        {
                            //check if optional field to omit space
                            //example of UOB regex ^(?: (?i)uob)?$
                            if (ValidationRegexOptionalFields.Contains(FieldsL[k]) /*CurrentFieldRegex.StartsWith("^(?:") && CurrentFieldRegex.EndsWith(")?$")*/)
                            {
                                //if optional field, then the space is already within the field itself
                                BuildingRegex = BuildingRegex + "(" + CurrentFieldRegex.Remove(CurrentFieldRegex.Length - 1).Remove(0, 1) + ")";
                            }
                            else
                            {

                                BuildingRegex = BuildingRegex + "(" + " " + CurrentFieldRegex.Remove(CurrentFieldRegex.Length - 1).Remove(0, 1) + ")";
                            }

                        }

                        var TestingRegex = k == FieldsL.Count - 1 ? "^" + BuildingRegex + "$" : "^" + BuildingRegex + "( ?)";

                        Regex InvalidedRegex = new Regex(TestingRegex, RegexOptions.IgnoreCase);
                        var MatchNowOrNot = InvalidedRegex.Match(CleanedMessage.Trim());

                        if (!MatchNowOrNot.Success)
                        {
                            //Validation has failed at this field, therefore we assume this field to be the one causing the error.
                            FunctRes.ListOfReasonsForPossibleFailures.Add(FieldsL[k] + " Is Not Valid!");
                        }

                    }

                    FunctRes.Entry.IsValid = false;
                    FunctRes.IsSavable = false;

                    if (FunctRes.Entry.EntrySource.Equals("Sms", StringComparison.InvariantCultureIgnoreCase))
                        FunctRes.ListOfResponsesForPossibleFailures.Add(ErrorMessageGeneric);
                    else if (FunctRes.Entry.EntrySource.Equals("Whatsapp", StringComparison.InvariantCultureIgnoreCase))
                        FunctRes.ListOfResponsesForPossibleFailures.Add(ErrorMessageWhatsapp);
                }

                #endregion

                //Validation has completed, if IsValid survives, add it as true

                // CompleteReturnObject.Add("SuccessMessage", WarningList.Count > 0 ? "Matching has not been completed succesfully. " : "Matched Successfully. Please Submit.");

                //upload filelink to azure blob
                if (FunctRes.Entry.FileLink != "" && param.EntrySource == "Whatsapp")
                {
                    //get filename
                    Uri uri = new Uri(FunctRes.Entry.FileLink);
                    string filename = string.Empty;

                    filename = System.IO.Path.GetFileName(uri.LocalPath);

                    using (var webClient = new WebClient())
                    {
                        byte[] imageBytes = webClient.DownloadData(FunctRes.Entry.FileLink);

                        string fl;
                        var fileresult = SendByteToAzureStorage(FunctRes.Entry, imageBytes, filename, out fl);

                        if (!fileresult.Valid)
                        {
                            return fileresult;
                        }

                        FunctRes.Entry.FileLink = fl;
                    }
                }




                //Decide whether to save entryfields based on validity
                int EntryID;

                FunctRes.Entry.Reason = FunctRes.ListOfReasonsForPossibleFailures.Count == 0 ? "" : FunctRes.ListOfReasonsForPossibleFailures[0].ToString();
                FunctRes.Entry.Response = FunctRes.ListOfResponsesForPossibleFailures.Count == 0 ? validResponse : FunctRes.ListOfResponsesForPossibleFailures[0].ToString();

                FunctRes.Entry = SaveEntry(FunctRes.Entry, FunctRes.IsSavable, FunctRes.Entry.EntrySource);
                EntryID = FunctRes.Entry.EntryID;

                FunctRes.message = "Successfully Saved with Entry ID : " + EntryID.ToString();
                return FunctRes;

                //Call Custom Logic Loop of After_Submit

                //}
                //catch (Exception ex)
                //{
                //    return new ReturnFail() { message = ex.ToString(), SendSMS = false };
                //}

            }
        }

        #endregion


        #region Online

        public FunctionResult InsertEntry(OnlineEntry Entry, HttpPostedFileBase Pfile)
        {
            try
            {
                bool __lockWasTaken = false;
                //lock object                    
                try
                {
                    Monitor.Enter(Globals.balanceLock, ref __lockWasTaken);
                    //Validation before matchfields
                    var ValidationResults = PreMatchFieldValidations(DateTime.Now);
                    if (!ValidationResults.IsSuccessful)
                    {
                        return new FunctionResult(false) { message = ValidationResults.ReasonForFailure };
                    }

                    var FunctRes = new FunctionResult(true);

                    var regexFullAmt = new Regex(@ValidationRegexAmount);

                    var AMTmatchFUll = regexFullAmt.Match(Entry.Amount.ToString());


                    var regexAmt = new Regex(@ValidAmount);

                    var AMTmatch = regexAmt.Match(Entry.Amount.ToString());

                    Decimal Amt = 0;
                    if (AMTmatchFUll.Success && AMTmatch.Success)
                    {
                        Amt = Convert.ToDecimal(AMTmatch.Groups[1].Value);
                    }

                    FunctRes.Entry.Name = Entry.Name;
                    FunctRes.Entry.NRIC = Entry.NRIC;
                    if (!string.IsNullOrEmpty(FunctRes.Entry.NRIC))
                        FunctRes.Entry.NRIC_NoPrefix = Char.IsLetter(FunctRes.Entry.NRIC.FirstOrDefault()) ? FunctRes.Entry.NRIC.Substring(1, FunctRes.Entry.NRIC.Length - 1) : FunctRes.Entry.NRIC;
                    else
                    {
                        FunctRes.Entry.NRIC = string.Empty;
                        FunctRes.Entry.NRIC_NoPrefix = string.Empty;
                    }


                    //FunctRes.Entry.NRIC = Entry.NRIC;
                    //FunctRes.Entry.NRIC_NoPrefix = Char.IsLetter(Entry.NRIC.FirstOrDefault()) ? Entry.NRIC.Substring(1, Entry.NRIC.Length - 1): Entry.NRIC;

                    FunctRes.Entry.MobileNo = Entry.MobileNo;
                    FunctRes.Entry.ReceiptNo = Entry.ReceiptNo;
                    FunctRes.Entry.Amount = Amt;
                    FunctRes.Entry.Email = Entry.Email;
                    FunctRes.Entry.Retailer = Entry.Retailer;

                    //Gender = OEntry.Gender"].ToString(),
                    //DOB = OEntry.DOB"] ==  null || OEntry.DOB"].ToString() == "" ?
                    //System.DateTime.UtcNow.ToString("dd-MMM-yyyy") :
                    //Convert.ToDateTime(OEntry.DOB"]).ToString("dd-MMM-yyyy") ,

                    //Email = OEntry.Email"].ToString(),

                    //IsOptIn = RC,

                    //Validation Success
                    //Run Additional logics , duplication etc.


                    using (var db = new BaseEntities())
                    {

                        //Validate LOGIC using MatchedFields
                        ValidationResults = ValidationLogics(FunctRes.Entry, "WEB");
                        if (!ValidationResults.IsSuccessful)
                        {
                            return new FunctionResult(false) { message = ValidationResults.ReasonForFailure };
                        }


                        FunctRes.Entry.IsValid = true;


                        //Should make sure Entry is savable before sending to AZS

                        string fl;
                        var fileresult = SendBlobToAzureStorage(Entry, Pfile, Entry.FileName, out fl);

                        if (!fileresult.Valid)
                        {
                            return fileresult;
                        }

                        FunctRes.Entry.FileLink = fl;
                        FunctRes.Entry.Response = ValidMessageOnline;
                        //Save Entry after passing all checks
                        FunctRes.Entry = SaveEntry(FunctRes.Entry, true, "WEB");

                        FunctRes.message = ValidMessageOnline.Replace("{receiptNo}", Entry.ReceiptNo); // "Successfully saved with ID : " + FunctRes.Entry.EntryID.ToString();

                        return FunctRes;
                    }
                }
                finally
                {
                    if (__lockWasTaken) System.Threading.Monitor.Exit(Globals.balanceLock);
                }
            }
            catch (Exception ex)
            {
                return new FunctionResult(false) { exception = ex };
            }


        }


        public FunctionResult CompleteEntry(OnlineEntry OEntry, HttpPostedFileBase Pfile, out string responsestring)
        {
            try
            {

                ////Validation before matchfields
                //var ValidationResults = PreMatchFieldValidations(DateTime.Now);
                //if (!ValidationResults.IsSuccessful)
                //{
                //    return new FunctionResult(false) { message = ValidationResults.ReasonForFailure };
                //}
                ////Validation before matchfields

                var results = new Dictionary<string, object>();

                var EntryID = Convert.ToInt32(OEntry.EntryID);
                var MobileNo = OEntry.MobileNo;


                using (var db = new BaseEntities())
                {
                    //Validate MobileNumber as well
                    var ListOfEntries = db.BC_240501_COKENCP.Where(s => s.EntryID == EntryID && s.MobileNo == MobileNo && s.IsValid).ToList();
                    if (ListOfEntries.Count == 0)
                    {
                        responsestring = "";
                        return new FunctionResult(false) { message = "Unable to find Entry." };
                    }

                    var Entry = ListOfEntries[0];

                    //Should make sure Entry is savable before sending to AZS

                    string fl;
                    var fileresult = SendBlobToAzureStorage(Entry, Pfile, OEntry.FileName, out fl);

                    if (!fileresult.Valid)
                    {
                        responsestring = "";
                        return fileresult;
                    }

                    Entry.FileLink = fl;
                    //Entry.Email = OEntry.Email;
                    db.SaveChanges();
                    //Entry.Response = ValidMessageOnline;
                    //Save Entry after passing all checks
                    responsestring = ValidMessageOnlineCompletion;
                    return new FunctionResult(true);
                }
            }
            catch (Exception ex)
            {
                responsestring = "";
                return new FunctionResult(false) { exception = ex };
            }
        }


        public FunctionResult UploadEntries(HttpPostedFileBase Pfile)
        {

            try
            {
                //Validation Success
                //Run Additional logics , duplication etc.

                var MessageList = new List<string>();

                using (var db = new BaseEntities())
                {
                    if (Pfile != null && Pfile.ContentLength > 0 && Path.GetExtension(Pfile.FileName) == ".csv")
                    {
                        //Current set of data :
                        var CsvData = new List<BC_240501_COKENCP>();

                        using (TextFieldParser parser = new TextFieldParser(Pfile.InputStream))
                        {
                            parser.CommentTokens = new string[] { "#" };
                            parser.SetDelimiters(new string[] { "," });
                            parser.HasFieldsEnclosedInQuotes = true;



                            //  parser.ReadLine();

                            while (!parser.EndOfData)
                            {
                                string[] fields = parser.ReadFields();

                                //Assume Column 1 = UniqueCode, Column 2 = StartDate, Column 3 = End Date
                                DateTime s;

                                // Skip over header line.
                                if (fields[0] == "DateEntry")
                                {
                                    continue;
                                }


                                ////Mini validation

                                //var IsValid = true;
                                //var Reason = "";


                                if (!DateTime.TryParse(fields[0], out s))
                                {
                                    throw new Exception(string.Join(",", fields));
                                }


                                CsvData.Add(new BC_240501_COKENCP()
                                {
                                    //MobileNo,Name,NRIC,ReceiptNo,Amount

                                    DateEntry = FromLocalToUTC(s),
                                    MobileNo = fields[1],
                                    /*
                                    //If Online Submission
                                    EntryText = "",
                                    Name = fields[1],
                                    NRIC = fields[2],
                                    ReceiptNo = fields[3],
                                    Amount = Convert.ToDecimal(fields[4]),
                                    //Chances = s,
                                    //Group = fields[5].ToUpper(),
                                    //Reason = Reason,
                                    //IsValid = IsValid,
                                    EntrySource = "WEB",
                                    */

                                    //If SMS Route
                                    EntryText = fields[2], //fields[0] + " " + fields[1] + " " + fields[2] + " " + fields[3] + " " + fields[4]

                                });

                                if (fields == null)
                                {
                                    break;
                                }

                            }
                        }


                        for (var i = 0; i < CsvData.Count; i++)
                        {
                            MessageList.Add(SubmitEntry(new Parameters()
                            {
                                CreatedOn = CsvData[i].DateEntry.ToString("yyyy-MM-dd HH:mm:ss"),
                                EntrySource = "SMS",
                                FileLink = "",
                                Message = CsvData[i].EntryText,
                                MobileNo = CsvData[i].MobileNo
                            }).message);
                        }

                    }
                    else
                    {
                        return new FunctionResult(false) { message = "File is invalid! Only CSV Files are accepted!" };
                    }

                    return new FunctionResult(true) { message = "CSV Uploaded!" + String.Join("<br/>", MessageList)/**/ };
                }
            }
            catch (Exception ex)
            {
                return new FunctionResult(false) { exception = ex };
            }

        }

        #endregion



        #region Logics

        public BC_240501_COKENCP EditEntry(BC_240501_COKENCP Entry)
        {
            //Edit Entry Logics



            return Entry;
        }


        public ValidationResult PreMatchFieldValidations(DateTime dt)
        {
            //Campaign Date Checking
            if (dt < TestDate || dt > EndDate)
            {

                return new ValidationResult(false)
                {
                    ReasonForFailure = "Not In Campaign Period",
                    ResponseForFailure = "Error : Not within campaign period (From " + TestDate + " to " + EndDate + ")"
                };


            }
            //Campaign Date Checking

            return new ValidationResult(true);

        }
        public ValidationResult ValidationLogics(BC_240501_COKENCP Entry, string type)
        {
            using (var db = new BaseEntities())
            {

                //Validation

                #region Validation

                var regex = new Regex("");
                var Match = regex.Match("");
                string errMessage = string.Empty;

                if (type.Equals("Whatsapp", StringComparison.InvariantCultureIgnoreCase))
                    errMessage = ErrorMessageWhatsapp;
                else if (type.Equals("Sms", StringComparison.InvariantCultureIgnoreCase) || type.Equals("WEB", StringComparison.InvariantCultureIgnoreCase))
                    errMessage = ErrorMessageGeneric;

                if (type == "WEB") //Only online Entries need to validate Field By Field.
                {
                    regex = new Regex(ValidationRegexMobileNo, RegexOptions.IgnoreCase);
                    Match = regex.Match(Entry.MobileNo.Trim());

                    if (!Match.Success)
                    {
                        return new ValidationResult(false) { ReasonForFailure = "Mobile is Invalid!", ResponseForFailure = "Mobile is Invalid!"/*errMessage*/ };
                    }



                    regex = new Regex(ValidationRegexName, RegexOptions.IgnoreCase);
                    Match = regex.Match(Entry.Name.Trim());

                    if (!Match.Success)
                    {
                        return new ValidationResult(false) { ReasonForFailure = "Name is Invalid!", ResponseForFailure = "Name is Invalid!"/*errMessage*/ };
                    }


                    //regex = new Regex(ValidationRegexNRIC, RegexOptions.IgnoreCase);
                    //Match = regex.Match(Entry.NRIC.Trim());

                    //if (!Match.Success)
                    //{
                    //    return new ValidationResult(false) { ReasonForFailure = "NRIC is Invalid!", ResponseForFailure = "NRIC is Invalid!"/*ErrorMessageGeneric*/ };
                    //}


                    //regex = new Regex(ValidationRegexReceiptNo, RegexOptions.IgnoreCase);
                    //Match = regex.Match(Entry.ReceiptNo.Trim());

                    //if (!Match.Success)
                    //{
                    //    return new ValidationResult(false) { ReasonForFailure = "ReceiptNo is Invalid!", ResponseForFailure = ErrorMessageGeneric };
                    //}


                    //regex = new Regex(ValidationRegexEmail, RegexOptions.IgnoreCase);
                    //Match = regex.Match(Entry.Email.Trim());

                    //if (!Match.Success)
                    //{
                    //return new ValidationResult(false) { ReasonForFailure = "Email is Invalid!", ResponseForFailure = ErrorMessageGeneric };
                    //}

                }
                if (type == "Whatsapp")
                {
                    if (string.IsNullOrEmpty(Entry.FileLink))
                    {
                        return new ValidationResult(false) { ReasonForFailure = "Image is required!", ResponseForFailure = errMessage };
                    }
                }

                #endregion


                if (Convert.ToDecimal(Entry.Amount) < TierAmount)
                {
                    return new ValidationResult(false) { ReasonForFailure = "Amount is below " + TierAmount.ToString() + "!", ResponseForFailure = ErrorMessageAmount.Replace("{tierAmount}", TierAmount.ToString()) };
                }

                var EntryMobile = (Entry.MobileNo.Substring(0, 1) == "+" ?/*2*/ Entry.MobileNo.Substring(1, Entry.MobileNo.Length - 1) :/*2*/ Entry.MobileNo);

                //Remove leading 0 on receiptNo
                Entry.ReceiptNo = Entry.ReceiptNo.TrimStart('0');

                //Check for duplicates


                /*Check For Receipt*/
                //var ReceiptCheckQuery = db.BC_240501_COKENCP.Where(s => s.IsValid == true).Where(s => s.ReceiptNo.ToUpper() == Entry.ReceiptNo.ToUpper());
                //var ReceiptCheckCount = ReceiptCheckQuery.Count();
                //if (ReceiptCheckCount > 0)
                //{
                //    /* When repeated message contains {uploadlink} */
                //    return new ValidationResult(false)
                //    {
                //        ReasonForFailure = "Repeated entry!",
                //        ResponseForFailure = RepeatedMessageSMS.Replace("{uploadlink}", UploadLink + "?i=" + ReceiptCheckQuery.FirstOrDefault().VerificationCode)
                //    };

                //    return new ValidationResult(false) { ReasonForFailure = "Repeated entry!", ResponseForFailure = RepeatedMessageSMS };
                //}

                /*Check For Receipt AND Retailer*/
                var ReceiptRetailerCheckQuery = db.BC_240501_COKENCP.Where(s => s.IsValid == true).Where(s => s.ReceiptNo.ToUpper() == Entry.ReceiptNo.ToUpper()
                && s.Retailer == Entry.Retailer);
                var ReceiptRetailerCheckCount = ReceiptRetailerCheckQuery.Count();
                if (ReceiptRetailerCheckCount > 0)
                {
                    /* When repeated message contains {uploadlink} */

                    return new ValidationResult(false)
                    {
                        ReasonForFailure = "Repeated entry!",
                        ResponseForFailure = RepeatedMessageSMS.Replace("{uploadlink}", UploadLink + "?i=" + ReceiptRetailerCheckQuery.FirstOrDefault().VerificationCode)
                    };

                    //return new ValidationResult(false) { ReasonForFailure = "Repeated entry!", ResponseForFailure = RepeatedMessageSMS };
                }

                /*Check For Receipt AND NRIC*/
                //var ReceiptNRICCheckQuery = db.BC_240501_COKENCP.Where(s => s.IsValid == true).Where(s => s.ReceiptNo.ToUpper() == Entry.ReceiptNo.ToUpper()
                //&& s.NRIC_NoPrefix.ToUpper() == Entry.NRIC_NoPrefix.ToUpper());
                //var ReceiptNRICCheckCount = ReceiptNRICCheckQuery.Count();
                //if (ReceiptNRICCheckCount > 0)
                //{
                //    /* When repeated message contains {uploadlink} */
                //    return new ValidationResult(false)
                //    {
                //        ReasonForFailure = "Repeated entry!",
                //        ResponseForFailure = RepeatedMessageSMS.Replace("{uploadlink}", UploadLink + "?i=" + ReceiptNRICCheckQuery.FirstOrDefault().VerificationCode)
                //    };

                //    // return new ValidationResult(false) { ReasonForFailure = "Repeated entry!", ResponseForFailure = RepeatedMessageSMS };
                //}


                //if managed to pass all logics then return true
                return new ValidationResult(true);

            }


        }

        #endregion



        #region Saving

        public BC_240501_COKENCP SaveEntry(BC_240501_COKENCP Entry, bool savable, string EntryType)
        {
            try
            {
                //DefaultValues Overriding

                if (EntryType == "SMS")
                {
                    Entry.FileLink = "";
                    //Entry.Gender = "";
                    //Entry.Email = "";
                    //Entry.DOB = "";
                    //Entry.IsOptIn = false;
                    //Entry.Retailer = "";
                }
                else if (EntryType == "MMS")
                {
                    //FileLink definitely Present
                }
                else if (EntryType == "WEB")
                {
                    Entry.EntryText = "";
                }
                else if (EntryType == "Whatsapp")
                {
                    //Entry.Email = "";
                }

                //If dont have value then set def value
                Entry.DateEntry = Entry.DateEntry == DateTime.MinValue ? DateTime.UtcNow : Entry.DateEntry;
                Entry.Response = Entry.Response == null || Entry.Response == "" ? "" : Entry.Response;
                Entry.Reason = Entry.Reason == null || Entry.Reason == "" ? "" : Entry.Reason;
                Entry.VerificationCode = "";

                using (var db = new BaseEntities())
                {

                    //Calc Chances

                    if (savable)
                    {
                        Entry.Chances = Entry.IsValid ? 1 : 0; /*ChancesCalcs(Entry); //For Contests with Tier Logic*/
                        //Entry.NRIC_NoPrefix = Char.IsLetter(Entry.NRIC.FirstOrDefault()) ? Entry.NRIC.Substring(1, Entry.NRIC.Length - 1) : Entry.NRIC;
                    }
                    else
                    {
                        Entry.Chances = 0;
                        Entry.NRIC = "";
                        Entry.Name = "";
                        Entry.NRIC_NoPrefix = "";
                        Entry.ReceiptNo = "";
                        Entry.Amount = 0;
                        Entry.FileLink = "";
                    }

                    Entry.EntrySource = EntryType;

                    db.BC_240501_COKENCP.Add(Entry);
                    db.SaveChanges();

                    /* Replacer for Valid Message */
                    if (savable && Entry.IsValid)
                    {
                        //Just create random 4 alphanumeric string and append to EntryID
                        Entry.VerificationCode = Entry.EntryID.ToString() + Guid.NewGuid().ToString().Substring(0, 4);

                        if (Entry.EntrySource.Equals("Sms", StringComparison.InvariantCultureIgnoreCase))
                            Entry.Response = ValidMessage.Replace("{uploadlink}", UploadLink + "?i=" + Entry.VerificationCode);
                        else if (Entry.EntrySource.Equals("Whatsapp", StringComparison.InvariantCultureIgnoreCase))
                            Entry.Response = ValidMessageWhatsapp;
                        db.SaveChanges();
                    }

                    return Entry;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        #endregion



        #region Views

        public FunctionResult GetEntries(Options Options)
        {
            using (var db = new BaseEntities())
            {

                var FunctRes = new FunctionResult(true);


                var Query = db.BC_240501_COKENCP.Where(s => s.DateEntry >= Options.StartDate && s.DateEntry <= Options.EndDate);


                //Filter Validity

                Query = Options.ValidOnly.ToUpper() == "Valid".ToUpper() ? Query.Where(s => (bool)s.IsValid == true) :
                    Options.ValidOnly.ToUpper() == "Invalid".ToUpper() ? Query.Where(s => (bool)s.IsValid == false) :
                    Query;

                //Filter Status

                Query = Options.Status.ToUpper() == "Verified".ToUpper() ? Query.Where(s => (bool)s.IsVerified == true) :
                    Options.Status.ToUpper() == "Void".ToUpper() ? Query.Where(s => (bool)s.IsRejected == true) :
                    Options.Status.ToUpper() == "Resent".ToUpper() ? Query.Where(s => s.DateResent != null) :
                    Query;

                //Other Filters?

                //Filter by UploadStatus
                if (Options.UploadStatus)
                {
                    Query = Query.Where(s => s.FileLink != "");
                }

                var query = Query.OrderByDescending(s => s.EntryID).Skip((Options.Page - 1) * Options.PageSize).Take(Options.PageSize).AsEnumerable();

                //var SAS = GetSASToken();

                var count = query.Count();

                if (count == 0)
                {
                    return new FunctionResult(false) { message = "No Entries Found!" };
                }

                FunctRes.TotalCount = Query.Count();

                FunctRes.DataHeaders = ExcludeHeaders(query.FirstOrDefault(), EntryExclusionFields.ToList());

                FunctRes.DataAsDictionary = ConvertEntriesToDictionary(query, EntryExclusionFields.ToList());

                return FunctRes;

            }
        }

        public FunctionResult GetEntriesCSV(Options Options)
        {
            using (var db = new BaseEntities())
            {
                var FunctRes = new FunctionResult(true);

                var Query = db.BC_240501_COKENCP.Where(s => s.DateEntry >= Options.StartDate && s.DateEntry <= Options.EndDate);

                //Filter Validity

                Query = Options.ValidOnly.ToUpper() == "Valid".ToUpper() ? Query.Where(s => (bool)s.IsValid == true) :
                      Options.ValidOnly.ToUpper() == "Invalid".ToUpper() ? Query.Where(s => (bool)s.IsValid == false) :
                      Query;

                //Filter Status

                Query = Options.Status.ToUpper() == "Verified".ToUpper() ? Query.Where(s => (bool)s.IsVerified == true) :
                    Options.Status.ToUpper() == "Void".ToUpper() ? Query.Where(s => (bool)s.IsRejected == true) :
                    Options.Status.ToUpper() == "Resent".ToUpper() ? Query.Where(s => s.DateResent != null) :
                    Query;

                //Filter by UploadStatus
                if (Options.UploadStatus)
                {
                    Query = Query.Where(s => s.FileLink != "");
                }

                var query = Query.AsEnumerable();

                var Excludes = EntryExclusionFields.ToList();


                //Hide these 2
                Excludes.Add("WinnerID");
                Excludes.Add("EntryID");

                if (query.Count() == 0)
                {
                    return new FunctionResult(false) { message = "No Entries Found!" };
                }

                FunctRes.DataHeaders = ExcludeHeaders(query.FirstOrDefault(), Excludes);

                FunctRes.DataAsDictionary = query.OrderByDescending(s => s.DateEntry).ThenByDescending(s => s.EntryID).Select(s =>
                    ExcludeHeaders(s.GetType().GetProperties().Where(p => p.GetMethod.IsVirtual == false).ToDictionary(prop => prop.Name, prop => prop.GetValue(s, null)), Excludes, true).
                    ToDictionary(prop => prop.Key, prop => prop.Value)).ToList();

                return FunctRes;

            }
        }

        public FunctionResult GetEntriesFileLinkZip(Options Options)
        {
            using (var db = new BaseEntities())
            {
                var FunctRes = new FunctionResult(true);

                var Query = db.BC_240501_COKENCP.Where(s => s.DateEntry >= Options.StartDate && s.DateEntry <= Options.EndDate);

                //Filter Validity

                Query = Options.ValidOnly.ToUpper() == "Valid".ToUpper() ? Query.Where(s => (bool)s.IsValid == true) :
                      Options.ValidOnly.ToUpper() == "Invalid".ToUpper() ? Query.Where(s => (bool)s.IsValid == false) :
                      Query;

                //Filter by UploadStatus
                if (Options.UploadStatus)
                {
                    Query = Query.Where(s => s.FileLink != "");
                }


                var query = Query.ToList();


                if (query.Count() == 0)
                {
                    return new FunctionResult(false) { message = "No FileLink Found!" };
                }

                FunctRes.files = new List<string>();

                for (int j = 0; j < query.Count(); j++)
                {
                    if (query[j].FileLink != "" && query[j].FileLink != null)
                    {
                        FunctRes.files.Add(query[j].FileLink);
                    }
                }

                if (FunctRes.files.Count() > 1000)
                {
                    return new FunctionResult(false) { message = "Max Files to download is 1000, please split the download to multiple filter!" };
                }

                ExtractFileLinkZip(FunctRes.files, "entriesfiles.zip");

                return FunctRes;

            }
        }


        public FunctionResult GetWinners(Options Options)
        {
            using (var db = new BaseEntities())
            {
                var FunctRes = new FunctionResult(true);

                var Query = db.BC_240501_COKENCP_Winners.AsQueryable();

                //WinDate
                //Query = Query.Where(s => s.a.DateWon >= wsDate && s.a.DateWon <= weDate);

                //WinnerGroupFilter
                Query = Options.WinnerGroupName == "Select All" ? Query : Query.Where(s => s.GroupName.ToUpper() == Options.WinnerGroupName.ToUpper());

                //Convert (a,b) to Dictionary
                var query = Query.OrderByDescending(s => s.WinnerID).Skip((Options.Page - 1) * Options.PageSize).Take(Options.PageSize).AsEnumerable();

                //Other Filters?

                var count = query.Count();

                if (count == 0)
                {
                    return new FunctionResult(false) { message = "No Winners Found!" };
                }

                FunctRes.TotalCount = Query.Count();
                FunctRes.DataHeaders = ExcludeHeaders(query.First(), WinnerExclusionFields.ToList());
                FunctRes.DataAsDictionary = ConvertWinnersToDictionary(query, WinnerExclusionFields.ToList());

                return FunctRes;

            }
        }

        public FunctionResult GetWinnersCSV(Options Options)
        {
            using (var db = new BaseEntities())
            {
                var FunctRes = new FunctionResult(true);

                var Query = db.BC_240501_COKENCP_Winners.AsQueryable();

                //WinDate
                //Query = Query.Where(s => s.a.DateWon >= wsDate && s.a.DateWon <= weDate);

                //WinnerGroupFilter
                Query = Options.WinnerGroupName == "Select All" ? Query : Query.Where(s => s.GroupName.ToUpper() == Options.WinnerGroupName.ToUpper());


                var query = Query.OrderByDescending(s => s.DateWon).ThenByDescending(s => s.WinnerID).ThenByDescending(s => s.DateWon).AsEnumerable();

                //Other Filters?

                if (query.Count() == 0)
                {
                    return new FunctionResult(false) { message = "No Winners Found!" };
                }


                var Excludes = WinnerExclusionFields.ToList();

                //Hide these 2
                Excludes.Add("WinnerID");
                Excludes.Add("EntryID");

                FunctRes.DataHeaders = ExcludeHeaders(query.First(), Excludes.ToList());
                FunctRes.DataAsDictionary = ConvertWinnersToDictionary(query, Excludes.ToList());

                return FunctRes;
            }
        }


        #endregion


        #region Picks


        public FunctionResult ConvertEntryToWinner(BC_240501_COKENCP Entry)
        {
            using (var db = new BaseEntities())
            {
                db.BC_240501_COKENCP_Winners.Add(new BC_240501_COKENCP_Winners
                {
                    BC_240501_COKENCP = Entry,
                    DateWon = System.DateTime.UtcNow,
                    GroupName = "Converted on " + System.DateTime.UtcNow.AddHours(AddLocalTimeZone).ToString("yyyy-MMM-dd"),
                    MobileNo = Entry.MobileNo,
                    NRIC_NoPrefix = Entry.NRIC_NoPrefix

                });

                db.SaveChanges();
                return new FunctionResult(true) { message = "Successfully converted Entry of ID : " + Entry.EntryID.ToString() };

            }

        }

        public FunctionResult PickWinner(Options Options)
        {
            using (var db = new BaseEntities())
            {
                var FunctRes = new FunctionResult(true);

                var Query = db.BC_240501_COKENCP.Where(s => s.IsValid);

                Query = Query.Where(s => s.DateEntry >= Options.StartDate && s.DateEntry <= Options.EndDate);


                //Filter Previous Winners
                if (Options.ExcludePastMob)
                {
                    var PrevMob = db.BC_240501_COKENCP_Winners.Select(s => s.MobileNo).ToList();
                    Query = Query.Where(s => !PrevMob.Contains(s.MobileNo));
                }

                if (Options.ExcludePastNRIC)
                {
                    var PrevNRIC = db.BC_240501_COKENCP_Winners.Select(s => s.NRIC_NoPrefix).ToList();
                    Query = Query.Where(s => !PrevNRIC.Contains(s.NRIC_NoPrefix));
                }

                //Filter by EntryType
                Query = Options.EntryType.ToString().ToUpper() == "SELECT ALL".ToUpper() ? Query : Query.Where(s => s.EntrySource.ToUpper() == Options.EntryType);

                //Filter by UploadStatus
                if (Options.UploadStatus)
                {
                    Query = Query.Where(s => s.FileLink != "");
                }


                var query = Query.ToList();
                //Chances
                var AddingList = new List<BC_240501_COKENCP>();

                for (int j = 0; j < query.Count; j++)
                {
                    for (int q = 0; q < query[j].Chances; q++)
                    {
                        AddingList.Add(query[j]);
                    }
                }

                //Pick Winners
                var Winners = new List<BC_240501_COKENCP_Winners>();
                var ResToSel = Convert.ToInt32(Options.QuotaToSelect);
                var random = new Random();
                for (int y = 0; y < ResToSel; y++)
                {
                    var WinningEntry = AddingList.OrderBy(x => random.Next()).Take(1).FirstOrDefault();

                    if (WinningEntry != null)
                    {
                        var W = new BC_240501_COKENCP_Winners()
                        {
                            BC_240501_COKENCP = WinningEntry,
                            MobileNo = WinningEntry.MobileNo,
                            NRIC_NoPrefix = WinningEntry.NRIC_NoPrefix,
                            DateWon = System.DateTime.UtcNow,
                            GroupName = Options.WinnerGroupName,
                        };

                        Winners.Add(W);

                        //Filter Duplicate Winners within Filtered List (Choose 1 from 3)
                        AddingList = AddingList.Where(s => s.MobileNo != WinningEntry.MobileNo).ToList();
                        //AddingList = AddingList.Where(s => s.NRIC != WinningEntry.NRIC)/**/.ToList();
                        //AddingList = AddingList.Where(s => s.EntryID != WinningEntry.EntryID)/**/.ToList();

                    }
                    else
                    {
                        break;
                    }
                };

                //Insert Into Winners with GroupName
                if (Winners.Count > 0)
                {
                    db.BC_240501_COKENCP_Winners.AddRange(Winners);
                    db.SaveChanges();
                }
                else
                {
                    return new FunctionResult(false) { message = "No Winners Found!" };
                }

                var WinningResults = Winners.OrderByDescending(s => s.DateWon).AsEnumerable();

                FunctRes.DataHeaders = ExcludeHeaders(WinningResults.First(), WinnerExclusionFields.ToList());
                FunctRes.DataAsDictionary = ConvertWinnersToDictionary(WinningResults, WinnerExclusionFields.ToList());

                return FunctRes;

            }

        }

        #endregion


        #region Purge



        public FunctionResult PurgeEntries()
        {
            using (var db = new BaseEntities())
            {
                ////Purge Winners
                ////db.Database.ExecuteSqlCommand("TRUNCATE TABLE [BC_160101_BASE_Winners]");

                ////db.Database.ExecuteSqlCommand("TRUNCATE TABLE [BC_160101_BASE]");

                db.BC_240501_COKENCP_Winners.RemoveRange(db.BC_240501_COKENCP_Winners);
                db.SaveChanges();

                db.BC_240501_COKENCP.RemoveRange(db.BC_240501_COKENCP);
                db.SaveChanges();

                return new FunctionResult(true) { message = "Successfully Purged!" };
                ;
            }

        }


        public FunctionResult PurgeSelectedEntries(List<int> EntryIDs)
        {
            using (var db = new BaseEntities())
            {
                var Winners = db.BC_240501_COKENCP_Winners.Where(s => EntryIDs.Contains(s.BC_240501_COKENCP.EntryID));
                db.BC_240501_COKENCP_Winners.RemoveRange(Winners);


                var Entries = db.BC_240501_COKENCP.Where(s => EntryIDs.Contains(s.EntryID));
                db.BC_240501_COKENCP.RemoveRange(Entries);

                db.SaveChanges();
                return new FunctionResult(true) { message = "Successfully Purged!" };

            }

        }

        public FunctionResult PurgeWinner()
        {
            using (var db = new BaseEntities())
            {

                //Purge Winners
                //db.Database.ExecuteSqlCommand("TRUNCATE TABLE [BC_160101_BASE_Winners]");

                db.BC_240501_COKENCP_Winners.RemoveRange(db.BC_240501_COKENCP_Winners);

                db.SaveChanges();
                return new FunctionResult(true) { message = "Successfully Purged!" };

            }

        }

        public FunctionResult PurgeSelectedWinners(List<int> WinnerIDs)
        {
            using (var db = new BaseEntities())
            {
                var Winners = db.BC_240501_COKENCP_Winners.Where(s => WinnerIDs.Contains(s.WinnerID));
                db.BC_240501_COKENCP_Winners.RemoveRange(Winners);


                db.SaveChanges();
                return new FunctionResult(true) { message = "Successfully Purged!" };

            }

        }

        #endregion




        public Dictionary<string, object> ContestInfo()
        {
            //create new object
            //manually define object perperties

            return typeof(Repository)
              .GetFields(BindingFlags.Public | BindingFlags.Static)
              .ToDictionary(f => f.Name,
                            f => f.GetValue(null));
        }

        public List<string> WinnerGroups()
        {
            using (var db = new BaseEntities())
            {

                var wgn = db.BC_240501_COKENCP_Winners.GroupBy(s => s.GroupName.ToUpper()).Select(s => s.FirstOrDefault()).Select(s => s.GroupName).ToList();
                //wgn.Insert(0, "Select All");
                return wgn;

            }
        }

        public FunctionResult GetTransactions(Options Options, bool isExport = false)
        {
            using (var db = new BaseEntities())
            {

                var FunctRes = new FunctionResult(true);


                var Query = db.BC_240501_COKENCP_LOG.Where(s => s.LogDate >= Options.StartDate && s.LogDate <= Options.EndDate);

                var query = Query.OrderByDescending(s => s.LogID).AsEnumerable();
                if (!isExport)
                    query = Query.OrderByDescending(s => s.LogID).Skip((Options.Page - 1) * Options.PageSize).Take(Options.PageSize).AsEnumerable();

                //var SAS = GetSASToken();

                var count = query.Count();

                if (count == 0)
                {
                    return new FunctionResult(false) { message = "No Entries Found!" };
                }


                var Excludes = new List<string>();
                if (isExport)
                {
                    Excludes.Add("LogID");
                }

                FunctRes.TotalCount = Query.Count();

                FunctRes.DataHeaders = query.GetType().GetProperties().Where(p => p.GetMethod.IsVirtual == false).Select(s => s.Name).ToList();

                FunctRes.DataAsDictionary = query.Select(s =>
                    ExcludeHeaders(s.GetType().GetProperties().Where(p => p.GetMethod.IsVirtual == false).ToDictionary(prop => prop.Name, prop => prop.GetValue(s, null)), Excludes, true).
                    ToDictionary(prop => prop.Key, prop => prop.Value)).ToList();

                return FunctRes;

            }
        }

        public int CountNumberTransactions(string logType, Options Options)
        {
            var numTrans = 0;
            using (var db = new BaseEntities())
            {
                var lisLog = db.BC_240501_COKENCP_LOG.Where(s => s.LogType == logType && s.LogDate >= Options.StartDate && s.LogDate <= Options.EndDate).AsEnumerable();

                if (lisLog != null && lisLog.Count() > 0)
                {
                    numTrans = lisLog.Where(s => s.CreditsUsed != "").Sum(s => int.Parse(s.CreditsUsed));
                }
            }
            return numTrans;
        }

        public FunctionResult RemoveTable(string tableName)
        {
            var FunctRes = new FunctionResult(true);
            var lstNameOftable = new List<string>();
            try
            {
                using (var connection = new SqlConnection(DefaultConnectionString))
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        var mainTableName = nameof(BC_240501_COKENCP);
                        command.CommandText = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' and TABLE_NAME like '" + mainTableName + "%'";

                        if (tableName != "")
                        {
                            command.CommandText = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' and TABLE_NAME like '" + tableName + "'";
                        }

                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            lstNameOftable.Add(reader["TABLE_NAME"].ToString());
                        }
                        reader.Close();

                        foreach (var nameOfTable in lstNameOftable)
                        {
                            var lstFKOftable = new List<ForeignKey>();
                            command.CommandText = "sp_fkeys '" + nameOfTable + "'";
                            var readerForeignKey = command.ExecuteReader();
                            while (readerForeignKey.Read())
                            {
                                var foreignKey = new ForeignKey();
                                foreignKey.FkName = readerForeignKey["FK_NAME"].ToString();
                                foreignKey.FkTableName = readerForeignKey["FKTABLE_NAME"].ToString();
                                lstFKOftable.Add(foreignKey);
                            }
                            readerForeignKey.Close();
                            foreach (var fK in lstFKOftable)
                            {
                                command.CommandText = "ALTER TABLE " + fK.FkTableName + " DROP CONSTRAINT " + fK.FkName;
                                command.ExecuteNonQuery();
                            }
                            command.CommandText = "drop table " + nameOfTable;
                            command.ExecuteNonQuery();
                        }
                        FunctRes.message = "OK";
                    }
                }
            }
            catch (SqlException ex)
            {
                FunctRes.message = ex.Message;
            }
            catch (Exception ex)
            {
                FunctRes.message = ex.Message;
            }
            return FunctRes;
        }

        public IEnumerable<DropDownItemData> LoadAllNameOfTables()
        {
            var lstNameOfTables = new List<DropDownItemData>();
            var mainTableName = nameof(BC_240501_COKENCP);
            using (var connection = new SqlConnection(DefaultConnectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {

                    command.CommandText = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' and TABLE_NAME like '" + mainTableName + "%'";

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        lstNameOfTables.Add(new DropDownItemData() { Code = reader["TABLE_NAME"].ToString(), Text = reader["TABLE_NAME"].ToString() });
                    }
                }
            }
            return lstNameOfTables;
        }
        public IEnumerable<DropDownItemData> LoadAllRetailers()
        {
            using (var db = new BaseEntities())
            {
                var allRetailers = db.BC_240501_COKENCP_Retailer.Select(r =>
                    new DropDownItemData { Code = r.Code, Text = r.Text });
                return allRetailers.ToList();
            }
        }

        public bool CheckMinSpendOfRetailer(string retailer, decimal amount)
        {
            using (var db = new BaseEntities())
            {
                var retailersWithNoMinSpend = db.BC_240501_COKENCP_Retailer.Where(p => p.HaveMinSpend == false).ToList();
                var isRetailerWithNoMinSpend = retailersWithNoMinSpend.Where(p => p.Code == retailer).FirstOrDefault();
                if (isRetailerWithNoMinSpend == null)
                {
                    if (amount < 5)
                    {
                        return false;
                    }
                    return true;
                }
                return true;
            }
        }
        public bool SavePassword(string accountType, string password)
        {
            using (var db = new BaseEntities())
            {
                var account = db.BC_240501_COKENCP_Account.Where(p => p.AccountType.ToUpper() == accountType.ToUpper()).FirstOrDefault();
                if(account != null)
                {
                    account.Password = password;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        public IEnumerable<BC_240501_COKENCP_Account> LoadAccount()
        {
            using (var db = new BaseEntities())
            {
                return db.BC_240501_COKENCP_Account.ToList();
            }
        }
    }
}