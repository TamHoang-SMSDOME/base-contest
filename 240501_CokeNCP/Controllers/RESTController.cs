using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SmsDome.bll;
using BaseContest_WebForms.Models;
using System.Text.RegularExpressions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Collections.Specialized;
using System.Net.Http.Formatting;
using System.Web;

namespace BaseContest_WebForms.Controllers
{
    [AllowAnonymous]
    public class RESTController : ApiController
    {
        Repository Repo = Repository.Instance;


        [HttpGet]
        [AllowAnonymous]
        [Route("REST/Add/")]
        public string Add(string createdon, string MobileNo, string Message, string FileLink = "")
        {
            return GetAndPostFunction(new Parameters
            {
                CreatedOn = createdon,
                MobileNo = MobileNo,
                Message = Message,
                FileLink = FileLink,
                EntrySource = (FileLink != null && FileLink.ToString() != "") ? "MMS" : "SMS",
                SendResponse = true,
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("REST/Add/")]
        public string Add([FromBody] FormDataCollection body)
        {
            return GetAndPostFunction(new Parameters
            {
                CreatedOn = body.Get("CreatedOn") == null ? null : body["CreatedOn"].ToString(),
                MobileNo = body.Get("MobileNo") == null ? null : body["MobileNo"].ToString(),
                Message = body.Get("Message") == null ? null : body["Message"].ToString(),
                FileLink = body.Get("FileLink") == null ? null : body["FileLink"].ToString(),
                EntrySource = (body["FileLink"] != null && body["FileLink"].ToString() != "") ? "MMS" : "SMS",
                SendResponse = true,
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("rest/contest-management/entries")]
        public string AddWhatsappEntry([FromBody] InboundMessage Inbound_webapp)
        {
            return GetAndPostFunction(new Parameters
            {
                CreatedOn = Inbound_webapp?.CreatedOn.ToString() ?? string.Empty,
                MobileNo = Inbound_webapp.MobileNo ?? string.Empty,
                Message = Inbound_webapp.Message ?? string.Empty,
                FileLink = Inbound_webapp.FileLink ?? string.Empty,
                EntrySource = Inbound_webapp.EntrySource ?? string.Empty,
                SendResponse = true,
            });
        }

        public string GetAndPostFunction(Parameters body)
        {
            try
            {
                //AuthorizedIP
                string SourceIP = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();

                if (Repo.IP != "" && SourceIP != Repo.IP)
                    throw new ArgumentException("Unauthorized access from " + SourceIP);
                //AuthorizedIP

                //UserID and Keyword
                DateTime dt = DateTime.UtcNow;

                var Result = Repo.SubmitEntry(body);
                string response = string.Empty;

                if (body.SendResponse && (Result.IsSendSMS && Repo.AppID != "" && Repo.AppSecret != ""))/* && body.EntrySource !="API" */ //UnComment this to prevent API Entries from sending responses. 
                {
                    if ((Repo.AppID != "" && Repo.AppSecret != "") &&
                            Result.Entry.EntrySource.Equals("SMS", StringComparison.InvariantCultureIgnoreCase))
                    {
                        response = GeneralFunctions.SendSms(Convert.ToInt32(Repo.AppID), new Guid(Repo.AppSecret), body.MobileNo.ToString(),
                                    Result.Entry.Response);                        
                    }
                    else if (Result.Entry.EntrySource.Equals("Whatsapp", StringComparison.InvariantCultureIgnoreCase))
                    {
                        response = GeneralFunctions.SendWhatsapp(body.MobileNo.ToString(),
                                  "text", Result.Entry.Response);                        
                    }
                        
                }

                return "OK";

            }
            catch (Exception ex)
            {
                var ErrMsg = "Error : " + ex.Message + ex.StackTrace.ToString();
                Repo.ErrorLog(ErrMsg);
                return ErrMsg;
            }
        }



        [HttpGet]
        [AllowAnonymous]
        [Route("REST/Submit/")]
        public string Submit(string Createdon, string MobileNo, string MessagePart1, string MessagePart2,  string FileLink = "")
        {
            return GetAndPostFunction(new Parameters
            {
                CreatedOn = DateTime.Parse(Createdon).AddHours(Repo.SubtractLocalTimeZone).ToString(),
                MobileNo = MobileNo,
                Message = MessagePart1 + " " + MessagePart2, //form Message from parameters
                FileLink = FileLink,
                EntrySource = "API",
                SendResponse = true,
            });
        }


        [HttpPost]
        [HttpGet]
        [AllowAnonymous]
        [Route("REST/AutoPickWinner/")]
        public string AutoPickWinner(string Createdon = "")
        {
            using (var db = new BaseEntities())
            {
                //DateTime localNow = (Createdon == "") ? DateTime.UtcNow.AddHours(Repo.AddLocalTimeZone) : Convert.ToDateTime(Createdon).AddHours(Repo.AddLocalTimeZone);

                //var list = db.BC_240501_COKENCP_AutoPickWinner.Where(s => s.DrawDate == localNow.Date).ToList();

                ////To prevent expired date of autopickwinner to be picked
                //DateTime enddate = DateTime.Parse("2023-02-06"); //YYYY-MM-DD

                //if (localNow.Date < enddate.Date && list.Count != 0)
                //{
                //    for (int i = 0; i < list.Count; i++)
                //    {
                //        DateTime utcStartDate = Convert.ToDateTime(list[i].StartDate);
                //        DateTime utcEndDate = Convert.ToDateTime(list[i].EndDate);
                //        int NoOfWinner = Convert.ToInt32(list[i].NoOfWinner);
                //        int NoOfReservedWinner = Convert.ToInt32(list[i].NoOfReserveWinner);
                //        var GroupName = list[i].GroupName;

                //        var options = new Options
                //        {

                //            //Assume Dates coming in are in UTC
                //            StartDate = utcStartDate,
                //            EndDate = utcEndDate,
                //            EntryType = "Select All",
                //            ExcludePastMob = true,
                //            UploadStatus = true,
                //            QuotaToSelect = NoOfWinner,
                //            WinnerGroupName = "Auto Pick Winner " + GroupName,
                //        };


                //        var FR = Repo.PickWinner(options);
                //        if (!FR.Valid)
                //        {

                //            Repo.ErrorLog(FR.message);

                //        }


                //        var options2 = new Options
                //        {

                //            //Assume Dates coming in are in UTC
                //            StartDate = utcStartDate,
                //            EndDate = utcEndDate,
                //            EntryType = "Select All",
                //            ExcludePastMob = true,
                //            UploadStatus = true,
                //            QuotaToSelect = NoOfReservedWinner,
                //            WinnerGroupName = "Auto Pick Reserved Winner " + GroupName,
                //        };


                //        var FR2 = Repo.PickWinner(options2);
                //        if (!FR2.Valid)
                //        {

                //            Repo.ErrorLog(FR2.message);

                //        }
                //    }

                //    return "OK";
                //}
                //else
                //    return "Expired";
                return "";
            }
        }



        //[HttpGet]
        //[AllowAnonymous]
        //[Route("REST/SetupSettings/")]
        //public string SetupSettings()
        //{
        //    return Repo.SetupSettings().message;
        //}

    }
}
