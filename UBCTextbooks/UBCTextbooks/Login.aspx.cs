using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UBCTextbooksCore.Constants;
using UBCTextbooksCore.Custom;
using UBCTextbooksCore.Security;

namespace UBCTextbooks
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.LinkLoginCss = "activated";
            Master.EnterHookUp = btn_login.UniqueID;
            GetStringResource();
            if (!IsPostBack)
            {
                HttpCookie accountCookie = Request.Cookies[UCookie.Account];
                if (accountCookie != null && accountCookie[UCookie.EmailAddress] != null)
                    txt_emailaddress.Text = accountCookie[UCookie.EmailAddress];
            }
        }

        private void GetStringResource()
        {
            Master.Localize(lt_login);
            Master.Localize(lt_login_description);
            Master.Localize(lt_emailaddress);
            Master.Localize(lt_password);
            Master.Localize(lt_rememberme);
            Master.Localize(btn_login);
        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            CreateCookie();
            AccountLogin();
        }

        private void CreateCookie()
        {
            if (ch_rememberme.Checked)
            {
                HttpCookie accountCookie = new HttpCookie(UCookie.Account);
                accountCookie[UCookie.EmailAddress] = txt_emailaddress.Text.Trim();
                accountCookie.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(accountCookie);
            }
        }

        private void AccountLogin()
        {
            AccountManager account = new AccountManager();
            emailAddress = txt_emailaddress.Text.Trim();
            password = txt_password.Text.Trim();
            Dictionary<string, string> accountInfo = account.Login(emailAddress, password, Server.MapPath(Paths.KeyFileName));
            if (int.Parse(accountInfo[UAccount.UserId]) > 0)
            {
                Session[USession.IsLoggedIn] = true;
                Session[USession.AccountInfo] = accountInfo;
                if ((string)Session[USession.PageTracker] == "Sell")
                {
                    Session[USession.PageTracker] = "Login";
                    Response.Redirect("~/sell");
                }
                else
                    Response.Redirect("~/account");
            }
            else
            {
                Master.Localize(lt_loginfailed);
            }
        }

        private string emailAddress = String.Empty;
        private string password = String.Empty;
    }
}