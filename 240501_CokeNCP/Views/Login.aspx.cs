using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseContest_WebForms.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using AjaxControlToolkit;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Azure.Security.KeyVault.Secrets;
using Azure.Identity;
using BaseContest_WebForms.Models.Repositories.Utility;

namespace BaseContest_WebForms.Views
{
    public partial class Login : System.Web.UI.Page
    {
        Repository repo = Repository.Instance;
        CustomLogger logger = Repository.Logger;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Buffer = true;
            //Response.CacheControl = "no-cache";
            //Response.AddHeader("Pragma", "no-cache");            
            //Response.Expires = -1441;
            //Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            //Response.Cache.SetNoStore();

            Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1
            Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0
            Response.AppendHeader("Expires", "0"); // Proxies

            if (!Page.IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                {
                    Response.Redirect("Entries.aspx");
                }
                else
                {
                    Version.InnerText = typeof(Login).Assembly.GetName().Version.ToString();
                    lblDT.Text = @System.DateTime.Now.Year.ToString();
                }
            }
        }


        protected void Login_Click(object sender, EventArgs e)
        {
            //Validation
            logger.Info("Login Page.  Start Login Method.");
            if (UserName.Text == "" || PassWord.Text == "")
            {
                logger.Error("Login page.  Invalid Username Or Password!  Finish Login Method");
                lblModal.Text = "Please key in proper login values!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "var myModal = new bootstrap.Modal(document.getElementById('divPopUp'), {});myModal.show();", true);
                return;
            }




            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new IdentityDbContext()));
            if (!rm.RoleExists("Superusers"))
            {
                var roleResult = rm.Create(new IdentityRole("Superusers"));
            }
            if (!rm.RoleExists("Users"))
            {
                var roleResult2 = rm.Create(new IdentityRole("Users"));
            }

            var dictuser = new Dictionary<string, object>()
            {
                { "UserName", UserName.Text},
                { "PassWord", PassWord.Text }
            };


            //get keyvault secret
            var Kvclient = new SecretClient(new Uri(repo.AzureKeyVaultUri), new DefaultAzureCredential());
            //"BaseContestUser"
            //"BaseContestAdmin"
            //"f3b396a7-a95e-4a84-ade1-f475f42fa893"
            //"SMSDOME"
            //"AQAAAAEAACcQAAAAECXBfyZIfS3+hi3KMG7RoQk0o/uO0w0STg/GhDnPCu32m1QLpLH6iaNlvKFeQFfbpQ=="
            var account = repo.LoadAccount();
            if(account == null) 
            {
                logger.Error("Login Page.  Login Failed!  Password Is Incorrect!  Finish Login Method");
                lblModal.Text = "Login failed!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
                return;
            }
            var kvIdentityUser = repo.kvIdentityUser;
            var kvIdentityAdmin = repo.kvIdentityAdmin;
            var kvIdentityPW = account.Where(p=>p.AccountType == "IdentityPW").FirstOrDefault()?.Password;
            var kvContestAdmin = repo.ContestAdminUser;
            var kvContestAdminPWHash = account.Where(p => p.AccountType == "ADMIN").FirstOrDefault()?.Password;
            //var kvIdentityUser = Kvclient.GetSecret("basecontest-identity-user-uid").Value.Value;
            //var kvIdentityAdmin = Kvclient.GetSecret("basecontest-identity-admin-uid").Value.Value;
            //var kvIdentityPW = Kvclient.GetSecret("basecontest-identity-password").Value.Value;
            //var kvContestAdmin = Kvclient.GetSecret("basecontest-login-admin-uid").Value.Value;
            //var kvContestAdminPWHash = Kvclient.GetSecret("basecontest-login-admin-password").Value.Value;


            //User Login will verify against Contest Login
            if (dictuser["UserName"].ToString().ToUpper() == kvContestAdmin.ToUpper())
            {
                byte[] decodedHashedPassword = Convert.FromBase64String(kvContestAdminPWHash);
                string password = PassWord.Text;

                if (VerifyHashedPasswordV3(decodedHashedPassword, password) != true)
                {
                    logger.Error("Login Page.  Login Failed!  Password Is Incorrect!  Finish Login Method");
                    lblModal.Text = "Login failed!";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
                    return;
                }
            }
            else if (dictuser["UserName"].ToString().ToUpper() == repo.ContestUser.ToUpper())
            {
                //check termination date
                if (DateTime.UtcNow > repo.TerminationDate)
                {
                    logger.Error("Login Page.  Login Failed - Terminated!  Finish Login Method");
                    lblModal.Text = "Login Failed - Terminated!";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
                    return;
                }

                byte[] decodedHashedPassword = Convert.FromBase64String(account.Where(p=>p.AccountType == "USER").FirstOrDefault()?.Password);
                string password = PassWord.Text;

                if (VerifyHashedPasswordV3(decodedHashedPassword, password) != true)
                {
                    logger.Error("Login Page.  Login Failed!  Password Is Incorrect!  Finish Login Method");
                    lblModal.Text = "Login failed!";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
                    return;
                }
            }
            else
            {
                logger.Error("Login page.  Login Failed!  Username Not Found!  Finish Login Method");
                lblModal.Text = "Login failed!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
                return;
            }

            //Then Api will Login to Identity Automatically

            //Try to create User.
            // Default UserStore constructor uses the default connection string named: DefaultConnection
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);

            var createuser = new IdentityUser() { UserName = dictuser["UserName"].ToString() == kvContestAdmin ? kvIdentityAdmin : kvIdentityUser };
            IdentityResult result = manager.Create(createuser, kvIdentityPW);

            if (result.Succeeded)
            {
                //Assign Roles  

                var userManager = new UserManager<IdentityUser>(userStore);
                var user = userManager.Find(dictuser["UserName"].ToString() == kvContestAdmin ? kvIdentityAdmin : kvIdentityUser,
                    kvIdentityPW);

                if (user != null)
                {
                    if (dictuser["UserName"].ToString() == kvContestAdmin)
                    {
                        userManager.AddToRole(user.Id, "Superusers");
                    }
                    else
                    {
                        userManager.AddToRole(user.Id, "Users");
                    }

                    var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                    var userIdentity = userManager.CreateIdentity(createuser, DefaultAuthenticationTypes.ApplicationCookie);
                    authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, userIdentity);
                    logger.Error("Login Page.  Login Successfully!  Finish Login Method");
                    Response.Redirect("Entries.aspx");
                    //return dictuser["PassWord"].ToString().ToUpper() == SArepo.ContestAdminPW ? "Server has been setup, User : " + SArepo.IdentityAdmin + " has successfully Logged In"
                    //: "Server has been setup, User : " + SArepo.ContestUser + " has successfully Logged In";
                }
                else
                {
                    logger.Error("Login Page.  Login Failed!  Finish Login Method");
                    lblModal.Text = "Login failed!";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
                    return;
                }
            }
            else
            {
                var userManager = new UserManager<IdentityUser>(userStore);
                var user = userManager.Find(dictuser["UserName"].ToString() == kvContestAdmin ? kvIdentityAdmin : kvIdentityUser,
                     kvIdentityPW);

                if (user != null)
                {
                    userManager.AddToRole(user.Id, dictuser["UserName"].ToString() == kvContestAdmin ? "Superusers" : "Users");

                    var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                    var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, userIdentity);
                    logger.Info("Login Page.  Login Successfully!  Finish Login Method");
                    Response.Redirect("Entries.aspx");

                    //Response.StatusCode = 200;
                    //return dictuser["PassWord"].ToString().ToUpper() == SArepo.ContestAdminPW ? "User : " + SArepo.IdentityAdmin + " has successfully Logged In"
                    //    : "User : " + SArepo.ContestUser + " has successfully Logged In";
                }
                else
                {
                    logger.Error("Login Page.  Login Failed!  Finish Login Method");
                    lblModal.Text = "Login failed!";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
                    return;
                    //Response.StatusCode = 500;
                    //return "Server Login has failed! Please contact admin!";
                }
            }



        }
        private static bool VerifyHashedPasswordV3(byte[] hashedPassword, string password)
        {
            int iterCount = default(int);

            try
            {
                // Read header information
                KeyDerivationPrf prf = (KeyDerivationPrf)ReadNetworkByteOrder(hashedPassword, 1);
                iterCount = (int)ReadNetworkByteOrder(hashedPassword, 5);
                int saltLength = (int)ReadNetworkByteOrder(hashedPassword, 9);

                // Read the salt: must be >= 128 bits
                if (saltLength < 128 / 8)
                {
                    return false;
                }
                byte[] salt = new byte[saltLength];
                System.Buffer.BlockCopy(hashedPassword, 13, salt, 0, salt.Length);

                // Read the subkey (the rest of the payload): must be >= 128 bits
                int subkeyLength = hashedPassword.Length - 13 - salt.Length;
                if (subkeyLength < 128 / 8)
                {
                    return false;
                }
                byte[] expectedSubkey = new byte[subkeyLength];
                System.Buffer.BlockCopy(hashedPassword, 13 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

                // Hash the incoming password and verify it
                byte[] actualSubkey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, subkeyLength);

                return ByteArraysEqual(actualSubkey, expectedSubkey);
            }
            catch
            {
                // This should never occur except in the case of a malformed payload, where
                // we might go off the end of the array. Regardless, a malformed payload
                // implies verification failed.
                return false;
            }
        }
        private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
        {
            return ((uint)(buffer[offset + 0]) << 24)
                | ((uint)(buffer[offset + 1]) << 16)
                | ((uint)(buffer[offset + 2]) << 8)
                | ((uint)(buffer[offset + 3]));
        }
        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }
            return areSame;
        }
    }
}