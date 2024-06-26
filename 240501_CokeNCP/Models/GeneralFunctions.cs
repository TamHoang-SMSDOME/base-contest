﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;
using System.Net;
using System.Drawing;
using System.Net.Mail;
using System.Globalization;
using System.Web.Util;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO.Ports;
using System.Collections;
using System.Xml;
using BaseContest_WebForms.Models;
using Newtonsoft.Json;
using Azure.Security.KeyVault.Secrets;
using Azure.Identity;

namespace SmsDome.bll
{
    public static class GeneralFunctions
    {
        public static bool Check(string number)
        {
            string pattern = @"^\d{7}$";

            string data = number;
            if (Regex.IsMatch(data, pattern))
                return true;
            else
                return false;
        }

        public static bool CheckForNumbersOnly(string number)
        {
            string pattern = @"^\d+$";

            string data = number;
            if (Regex.IsMatch(data, pattern))
                return true;
            else
                return false;
        }

        public static bool CheckSixDigitDate(string number)
        {
            string pattern = @"^\d{6}$";

            string data = number;
            if (Regex.IsMatch(data, pattern))
                return true;
            else
                return false;
        }

        public static bool CheckSevenDigit(string number)
        {
            string pattern = @"^\d{7}$";

            string data = number;
            if (Regex.IsMatch(data, pattern))
                return true;
            else
                return false;
        }

        public static bool CheckEightDigitDate(string number)
        {
            string pattern = @"^\d{8}$";

            string data = number;
            if (Regex.IsMatch(data, pattern))
                return true;
            else
                return false;
        }

        public static bool CheckSixteenDigitDate(string number)
        {
            string pattern = @"^\d{16}$";

            string data = number;
            if (Regex.IsMatch(data, pattern))
                return true;
            else
                return false;
        }

        public static string GetOrdinalSuffix(int @this)
        {
            switch (@this % 100)
            {
                case 11:
                case 12:
                case 13:
                    return @this + "th";
                default:
                    switch (@this % 10)
                    {
                        case 1:
                            return @this + "st";
                        case 2:
                            return @this + "nd";
                        case 3:
                            return @this + "rd";
                        default:
                            return @this + "th";
                    }
            }
        }

        public static bool CheckIsAlphabet(string name)
        {
            string pattern = @"^[a-zA-Z]*$";

            string data = name;
            if (Regex.IsMatch(data, pattern))
                return true;
            else
                return false;
        }


        public static string GetUniqueKey(int maxSize)
        {
            char[] chars = "ABCDEFGHIJKLMNPQRSTUVWXYZ123456789".ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        public static string Remove(string name)
        {
            return Regex.Replace(name, "[^a-zA-Z]+", "");
        }

        // http://www.regular-expressions.info/email.html
        public static bool IsValidEmail(string str)
        {
            return Regex.IsMatch(str, "[a-z0-9!#$%&\'\'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&\'\'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\" +
                ".)+(?:[A-Z]{2}|com|org|net|edu|gov|mil|biz|info|mobi|name|aero|asia|jobs|museum)\\b", RegexOptions.IgnoreCase);
        }

        public static bool IsValidNRIC(string str)
        {
            //^[STFG]\\d{7}[A-Z]$
            return Regex.IsMatch(str, "^[A-Za-z]{0,1}([ -/]*)\\d{7}([ -/]*)[A-Za-z]$");
        }

        public static bool IsValidCurrency(string str)
        {
            Decimal Amount = new Decimal();
            return IsValidCurrency(str, ref Amount);
        }

        public static bool IsValidCurrency(string str, ref Decimal Amount)
        {
            bool IsValidCurrency = false;
            //  Allow 123, 123.45, $123, $123.45, S$123,$123.45. 
            //  \p{Sc}? #optional unicode currency symbols
            //  (\.)? # handle ending .
            str = str.Replace(" ", "");
            IsValidCurrency = Regex.IsMatch(str, "^(\\p{Sc}|[Ss]\\$)?(\\d{1,3}(\\,\\d{3})*|(\\d+))(\\.\\d{1,2})?(\\.)?$");
            if (IsValidCurrency)
            {
                Amount = Decimal.Parse(str.Replace("(^\\p{Sc})|(^[Ss]\\$)|(\\.$)", ""));
            }

            return IsValidCurrency;
        }

        public static void WriteToLogFile(string Entry)
        {
            WriteToLogFile(Entry, Assembly.GetExecutingAssembly().GetName().Name);
        }

        public static void WriteToLogFile(string Entry, string LogName)
        {
            try
            {
                StackTrace st = new StackTrace();
                string MethodName = st.GetFrame(1).GetMethod().Name;
                //  Log file goes in application data directory
                string LogFilePath;
                LogFilePath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                LogFilePath = Path.Combine(LogFilePath, "SmsDome");
                LogFilePath = Path.Combine(LogFilePath, (LogName + ("_" + (String.Format(DateTime.UtcNow.ToString(), "yyyyMM") + ".log"))));
                if (!Directory.Exists(Path.GetDirectoryName(LogFilePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(LogFilePath));
                }

                using (StreamWriter writer = new StreamWriter(LogFilePath, true))
                {
                    string messageTemplate = "{0}: [{1}]{2}{3}";
                    writer.WriteLine(messageTemplate, DateTime.UtcNow.ToString("dd MMM yyyy hh:mm:ss tt"), MethodName, '\t', Entry);
                }
            }
            catch
            {
            }
        }

        public static string NormalizeSpaces(string value)
        {
            value = Regex.Replace(value, @"[\n\r\t]", " ");  // Convert all  \n = CR(Carriage Return)   \r = LF(Line Feed)   \t = tab to a single space.
            value = Regex.Replace(value, @"\s+", " ");       // Convert all whitespaces to a single space.
            return value.Trim();
        }

        public static string SendSms(int AppID, Guid AppSecret, string receivers, string content)
        {
            //HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://www.smsdome.com/api/http/sendsms.aspx");

            ////Set values for the request back
            //req.Method = "POST";
            //req.ContentType = "application/x-www-form-urlencoded";
            //byte[] param = Request.BinaryRead(HttpContext.Current.Request.ContentLength);
            //string strRequest = Encoding.ASCII.GetString(param);
            //strRequest = "AppID=" + AppID + "&AppSecret=" + AppSecret + "&receivers=" + receivers + "&content=" + HttpUtility.UrlEncode(content) + "&responseformat=XML";
            //req.ContentLength = strRequest.Length;

            ////Send the request to PayPal and get the response
            //StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            //streamOut.Write(strRequest);
            //streamOut.Close();
            //StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
            //string responseStringa = streamIn.ReadToEnd();

            //XmlReader reader = XmlReader.Create(streamIn);
            //XmlDocument xdoc = new XmlDocument();
            //xdoc.LoadXml(responseStringa);
            //IEnumerator ie2 = xdoc.SelectNodes("root/content").GetEnumerator();
            //int AutoReplyMessagelen = 0;
            //while (ie2.MoveNext())
            //{
            //    AutoReplyMessagelen = Convert.ToInt16((ie2.Current as XmlNode).Attributes["chars"].Value);
            //}

            //streamIn.Close();

            //if (AutoReplyMessagelen > 500)
            //{
            //    isValid = false;
            //    importMessage = "Error : Valid Auto Reply is more than 500 Char length.";
            //}



            try
            {
                // create the web request with the url to the web
                // service with the method name added to the end
                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create("http://www.smsdome.com/api/http/sendsms.aspx");

                // add the parameters as key valued pairs making
                // sure they are URL encoded where needed
                ASCIIEncoding encoding = new ASCIIEncoding();
                //byte[] postData = encoding.GetBytes("CreatedOn=" + dt + "&MobileNo=" + MobileNo + "&Message=" + Message);
                byte[] postData = encoding.GetBytes("AppID=" + AppID + "&AppSecret=" + AppSecret + "&receivers=" + receivers + "&content=" + HttpUtility.UrlEncode(content) + "&responseformat=XML");
                httpReq.ContentType = "application/x-www-form-urlencoded";
                httpReq.Method = "POST";
                httpReq.ContentLength = postData.Length;

                // convert the request to a steeam object and send it on its way
                Stream ReqStrm = httpReq.GetRequestStream();
                ReqStrm.Write(postData, 0, postData.Length);
                ReqStrm.Close();

                // get the response from the web server and
                // read it all back into a string variable
                HttpWebResponse httpResp = (HttpWebResponse)httpReq.GetResponse();
                StreamReader respStrm = new StreamReader(httpResp.GetResponseStream(), Encoding.ASCII);
                string result = respStrm.ReadToEnd();
                httpResp.Close();
                respStrm.Close();

                // Get Credits used for sms
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(result);
                XmlNodeList parentNode = xmlDoc.GetElementsByTagName("receiver");
                var creditsUsed = "";
                foreach (XmlNode node in parentNode)
                {
                    creditsUsed = node.Attributes["credits"].Value;
                }

                using (var db = new BaseEntities())
                {
                    //Write log 
                    var log = new BC_240501_COKENCP_LOG();
                    log.LogDate = DateTime.UtcNow;
                    log.Recipient = receivers;
                    log.Content = content;
                    log.LogType = "SMS";
                    log.CreditsUsed = creditsUsed == "" ? "0": creditsUsed;
                    db.BC_240501_COKENCP_LOG.Add(log);
                    db.SaveChanges();
                }
                return result;
                // show the result the test box for testing purposes
                //string result2 = result;

                ////////////////////////////////////////////////////////

            }
            catch (Exception ex)
            {
                return ex.ToString();
                //WriteToLogFile("Campaign Error: " + ex.Message);
            }
        }

        public static string SendWhatsapp(string mobileNo, string messageType, string messageText)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //call outbound url
                Constants constants = new Constants();
                string uri = constants.OutboundURL;
                using (var db = new BaseEntities())
                {
                    //Write log 
                    var log = new BC_240501_COKENCP_LOG();
                    log.LogDate = DateTime.UtcNow;
                    log.Recipient = mobileNo;
                    log.Content = messageText;
                    log.LogType = "Whatsapp";
                    log.CreditsUsed = "1";
                    db.BC_240501_COKENCP_LOG.Add(log);
                    db.SaveChanges();
                }
                //string uri = "https://api.whatsapp.com/send?phone=whatsappphonenumber&text=urlencodedtext";
                //using (var httpClient = new HttpClient())
                //{
                //    using (var request = new HttpRequestMessage(new HttpMethod("POST"), uri))
                //    {
                //        request.Headers.TryAddWithoutValidation("Accept", "application/json");

                //        //request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {bearerTokenValue}");

                //        OutboundMessage Outbound_webapp = new OutboundMessage
                //        {
                //            ContestId = constants.ContestID,
                //            MobileNo = mobileNo,
                //            MessageType = messageType,
                //            MessageText = messageText
                //        };

                //        string outboundWebapp = JsonConvert.SerializeObject(Outbound_webapp);
                //        request.Content = new StringContent(outboundWebapp, Encoding.UTF8, "application/json");

                //        //var result = Task.Run(() => httpClient.SendAsync(request));
                //        //result.Wait();

                //        var result = await httpClient.SendAsync(request);

                //        string response = await result.Content.ReadAsStringAsync();

                //        if (result.IsSuccessStatusCode)
                //        {
                //            //implement logging here                                
                //            return string.Empty;
                //        }
                //        else
                //        {
                //            return "Err: " + outboundWebapp;
                //        }
                //    }
                //}

                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(uri);

                httpReq.ContentType = "application/json; charset=utf-8";
                httpReq.Method = "POST";
                using (var streamWriter = new StreamWriter(httpReq.GetRequestStream()))
                {
                    OutboundMessage Outbound_webapp = new OutboundMessage
                    {
                        ContestId = constants.ContestID,
                        MobileNo = mobileNo,
                        MessageType = messageType,
                        MessageText = messageText
                    };

                    string outboundWebapp = JsonConvert.SerializeObject(Outbound_webapp);

                    streamWriter.Write(outboundWebapp);
                }
                string content = string.Empty;

                using (var response = (HttpWebResponse)httpReq.GetResponse())
                using (var receiveStream = response.GetResponseStream())
                using (var reader = new StreamReader(receiveStream))
                {
                    content = reader.ReadToEnd();
                    // parse your content, etc.
                }
                return content;

            }
            catch (WebException ex)
            {
                return "Exception : " + ex.Message;
            }
        }

        public static void SendEmail(Email email)
        {
            using (var db = new BaseEntities())
            {
                //Write log 
                var log = new BC_240501_COKENCP_LOG();
                log.LogDate = DateTime.UtcNow;
                log.Recipient = email.ToEmail;
                log.Content = email.Body;
                log.LogType = "Email";
                log.CreditsUsed = "1";
                db.BC_240501_COKENCP_LOG.Add(log);
                db.SaveChanges();
            }

            try
            {
                Constants constants = new Constants();
                var Kvclient = new SecretClient(new Uri(constants.AzureKeyVaultUri), new DefaultAzureCredential());
                var apiKey = Kvclient.GetSecret("basecontest-sendgrid-apikey").Value.Value;
                MailMessage mail = new MailMessage();
                mail.To.Add(email.ToEmail);
                mail.From = new MailAddress(constants.MailFrom);
                mail.Subject = constants.Keyword + " - Receipt Verified";
                mail.Body = email.Body;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.sendgrid.net";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("apikey", apiKey);
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                throw ;
            }

            //var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            //var client = new SendGridClient(apiKey);
            //var from = new EmailAddress("test@example.com", "Example User");
            //var subject = "Sending with SendGrid is Fun";
            //var to = new EmailAddress("test@example.com", "Example User");
            //var plainTextContent = "and easy to do anywhere, even with C#";
            //var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            //var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            //var response = await client.SendEmailAsync(msg);
        }

        public static void ExportToCsv(DataTable dt, string Header, string Filename)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            context.Response.Buffer = true;
            context.Response.Charset = "";
            context.Response.ContentType = "text/csv";
            context.Response.AppendHeader("content-disposition", String.Format("attachment; filename={0}.csv", Filename));
            if (Header == "")
            {
                foreach (DataColumn col in dt.Columns)
                {
                    context.Response.Write(string.Format("\"{0}\"{1}", col.ColumnName, ","));
                }
            }
            else
            {
                context.Response.Write(Header);
            }

            foreach (DataRow row in dt.Rows)
            {
                context.Response.Write(Environment.NewLine);
                for (int i = 0; i <= dt.Columns.Count - 1; i++)
                {
                    string FormattedCell = "";

                    bool a = true;
                    DateTime datetime = DateTime.UtcNow;
                    if (row[i].GetType() == a.GetType())
                    {
                        if (Convert.ToBoolean(row[i].ToString()) == true)
                        {
                            FormattedCell = "Y";
                        }
                        else
                        {
                            FormattedCell = "N";
                        }
                    }
                    else if (row[i].GetType() == DateTime.Now.GetType())
                    {
                        FormattedCell = (Convert.ToDateTime(row[i]).ToString("dd MMM yyyy HH:mm:ss"));
                    }
                    else
                    {
                        FormattedCell = row[i].ToString().Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ");
                    }
                    // Escape field if it contains comma
                    if (FormattedCell.Contains(",") || FormattedCell.Contains("\""))
                    {
                        // Escape field if it contains double quote
                        if (row[i].GetType() == datetime.GetType())
                        {
                            FormattedCell = Convert.ToDateTime(row[i]).ToString("dd MMM yyyy HH:mm:ss");
                        }
                        else
                        {
                            FormattedCell = row[i].ToString().Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ");
                        }
                        context.Response.Write(string.Format("\"{0}\"{1}", FormattedCell, ","));
                    }
                    else
                    {
                        context.Response.Write(String.Format("{0}{1}", FormattedCell, ","));
                    }
                }
                context.Response.Flush();

            }
            context.Response.End();
        }

        public static DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
        {
            DataTable dt = new DataTable();


            PropertyInfo[] columns = null;

            if (Linqlist == null) return dt;

            foreach (T Record in Linqlist)
            {

                if (columns == null)
                {
                    columns = ((Type)Record.GetType()).GetProperties();
                    foreach (PropertyInfo GetProperty in columns)
                    {
                        Type colType = GetProperty.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dt.Columns.Add(new DataColumn(GetProperty.Name, colType));
                    }
                }

                DataRow dr = dt.NewRow();

                foreach (PropertyInfo pinfo in columns)
                {
                    dr[pinfo.Name] = pinfo.GetValue(Record, null) == null ? DBNull.Value : pinfo.GetValue
                    (Record, null);
                }

                dt.Rows.Add(dr);
            }
            return dt;
        }

        public static bool IsValidNRICForPrefixSorT(string X, string NNNNNNN, string Y)
        {
            bool result = false;
            int Weightage = 0;

            if (X == "T")
                Weightage = 4;

            int S1 = (Convert.ToInt16(NNNNNNN[0].ToString()) * 2) + (Convert.ToInt16(NNNNNNN[1].ToString()) * 7) + (Convert.ToInt16(NNNNNNN[2].ToString()) * 6) + (Convert.ToInt16(NNNNNNN[3].ToString()) * 5) + (Convert.ToInt16(NNNNNNN[4].ToString()) * 4) + (Convert.ToInt16(NNNNNNN[5].ToString()) * 3) + (Convert.ToInt16(NNNNNNN[6].ToString()) * 2) + Weightage;

            int R1 = S1 % 11;
            int P = 11 - R1;

            string CheckDigit = "";

            switch (P)
            {
                case 1:
                    CheckDigit = "A"; break;
                case 2:
                    CheckDigit = "B"; break;
                case 3:
                    CheckDigit = "C"; break;
                case 4:
                    CheckDigit = "D"; break;
                case 5:
                    CheckDigit = "E"; break;
                case 6:
                    CheckDigit = "F"; break;
                case 7:
                    CheckDigit = "G"; break;
                case 8:
                    CheckDigit = "H"; break;
                case 9:
                    CheckDigit = "I"; break;
                case 10:
                    CheckDigit = "Z"; break;
                case 11:
                    CheckDigit = "J"; break;
            }
            if (CheckDigit == Y)
                result = true;

            return result;
        }

        public static void ExportToCsvHandler(List<Transaction> listTran, string Filename)
        {
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            context.Response.Buffer = true;
            context.Response.Charset = "";
            context.Response.ContentType = "text/csv";
            context.Response.AppendHeader("content-disposition", String.Format("attachment; filename={0}.csv", Filename));
            var rowItem = 0;
            foreach (var item in listTran)
            {
                var dt = item.Table;
                var Header = item.Header;
                if (rowItem > 0)
                    context.Response.Write(Environment.NewLine);
                if (Header == "")
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        context.Response.Write(string.Format("\"{0}\"{1}", col.ColumnName, ","));
                    }
                }
                else
                {
                    context.Response.Write(Header);
                }
                rowItem++;

                foreach (DataRow row in dt.Rows)
                {
                    context.Response.Write(Environment.NewLine);
                    for (int i = 0; i <= dt.Columns.Count - 1; i++)
                    {
                        string FormattedCell = "";

                        bool a = true;
                        DateTime datetime = DateTime.UtcNow;
                        if (row[i].GetType() == a.GetType())
                        {
                            if (Convert.ToBoolean(row[i].ToString()) == true)
                            {
                                FormattedCell = "Y";
                            }
                            else
                            {
                                FormattedCell = "N";
                            }
                        }
                        else if (row[i].GetType() == DateTime.Now.GetType())
                        {
                            FormattedCell = (Convert.ToDateTime(row[i]).ToString("dd MMM yyyy HH:mm:ss"));
                        }
                        else
                        {
                            FormattedCell = row[i].ToString().Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ");
                        }
                        // Escape field if it contains comma
                        if (FormattedCell.Contains(",") || FormattedCell.Contains("\""))
                        {
                            // Escape field if it contains double quote
                            if (row[i].GetType() == datetime.GetType())
                            {
                                FormattedCell = Convert.ToDateTime(row[i]).ToString("dd MMM yyyy HH:mm:ss");
                            }
                            else
                            {
                                FormattedCell = row[i].ToString().Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ");
                            }
                            context.Response.Write(string.Format("\"{0}\"{1}", FormattedCell, ","));
                        }
                        else
                        {
                            context.Response.Write(String.Format("{0}{1}", FormattedCell, ","));
                        }
                    }
                    context.Response.Flush();
                }
            }

            context.Response.End();

        }
    }
}

