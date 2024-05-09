using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using BaseContest_WebForms.Models;
using System.Globalization;
using System.Data;
using SmsDome.bll;
using System.Net.Http;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Identity.Core;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using System.Configuration;
using System.Web.Configuration;
using BaseContest_WebForms.Models.Repositories.Utility;

namespace BaseContest_WebForms.Views
{
    public partial class Roles : System.Web.UI.Page
    {
        private readonly PasswordHasherCompatibilityMode _compatibilityMode;
        private readonly int _iterCount =10000;
        private readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();

        Repository repo = Repository.Instance;
        CustomLogger logger = Repository.Logger;
        //Becoz u donno if some pages will need to have a different PageSize
        static readonly int PageSize = 50;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Buffer = true;
            Response.CacheControl = "no-cache";
            Response.AddHeader("Pragma", "no-cache");
            Response.AppendHeader("pragma", "no-cache");
            Response.Expires = -1441;
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();

            if (!Page.IsPostBack)
            {
                if (User.Identity.IsAuthenticated && User.IsInRole("Superusers"))
                {

                }
                else
                {
                    Response.Redirect("/Views/Login.aspx");
                }
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            logger.Info("Roles Page.  Start Set Password Method.");
            string user = ddlRole.SelectedValue;
            if (PassWord.Text == "")
            {
                lblModal.Text = "New password must not empty!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
                return;
            }
            string password = PassWord.Text;

            string PasswordHash = HashPassword(password);
            bool rtn = false;
            if (user.ToUpper() == "ADMIN")
            {
                rtn = repo.SavePassword("ADMIN", PasswordHash);
            }
            else if (user.ToUpper() == "USER")
            {
                rtn = repo.SavePassword("USER", PasswordHash);
            }
            else if(user.ToUpper() == "IdentityPW".ToUpper())
            {
                rtn = repo.SavePassword("IdentityPW", PasswordHash);
            }
            if(rtn == true)
            {
                logger.Info("Roles Page.  Setting Password For" + ddlRole.SelectedValue + "Successfully!  Finish Set Password Method");
                lblModal.Text = "New password updated!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myModal", "$('#divPopUp').modal('show');", true);
                return;
            }
        }
        
        public virtual string HashPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            return Convert.ToBase64String(HashPasswordV3(password, _rng));
        }

        private byte[] HashPasswordV3(string password, RandomNumberGenerator rng)
        {
            return HashPasswordV3(password, rng,
                prf: KeyDerivationPrf.HMACSHA256,
                iterCount: _iterCount,
                saltSize: 128 / 8,
                numBytesRequested: 256 / 8);
        }

        private static byte[] HashPasswordV3(string password, RandomNumberGenerator rng, KeyDerivationPrf prf, int iterCount, int saltSize, int numBytesRequested)
        {
            // Produce a version 3 (see comment above) text hash.
            byte[] salt = new byte[saltSize];
            rng.GetBytes(salt);
            byte[] subkey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, numBytesRequested);

            var outputBytes = new byte[13 + salt.Length + subkey.Length];
            outputBytes[0] = 0x01; // format marker
            WriteNetworkByteOrder(outputBytes, 1, (uint)prf);
            WriteNetworkByteOrder(outputBytes, 5, (uint)iterCount);
            WriteNetworkByteOrder(outputBytes, 9, (uint)saltSize);
            System.Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
            System.Buffer.BlockCopy(subkey, 0, outputBytes, 13 + saltSize, subkey.Length);
            return outputBytes;
        }
        private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
        {
            buffer[offset + 0] = (byte)(value >> 24);
            buffer[offset + 1] = (byte)(value >> 16);
            buffer[offset + 2] = (byte)(value >> 8);
            buffer[offset + 3] = (byte)(value >> 0);
        }
    }
}