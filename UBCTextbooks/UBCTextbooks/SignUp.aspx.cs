using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using UBCTextbooksCore.Constants;
using UBCTextbooksCore.Custom;
using UBCTextbooksCore.Security;

namespace UBCTextbooks
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Context.RewritePath(Request.RawUrl);
            GetStringResource();
            Master.LinkSignUpCss = "activated";
            if (!IsPostBack)
                Session["CaptchaText"] = Captcha.GenerateRandomCode();
            Master.EnterHookUp = btn_signup.UniqueID;
        }

        private void GetStringResource()
        {
            Master.Localize(lt_signup);
            Master.Localize(lt_signup_description);
            Master.Localize(lt_displayname);
            Master.Localize(lt_emailaddress);
            Master.Localize(lt_emailaddress_confirm);
            Master.Localize(lt_password);
            Master.Localize(lt_password_confirm);
            Master.Localize(lt_captchaverification);
            Master.Localize(lt_captcha_description);
            Master.Localize(lt_displayname_description);
            Master.Localize(lt_emailaddress_description);
            Master.Localize(btn_signup);
        }

        protected void btn_signup_Click(object sender, EventArgs e)
        {
            mailtxt = txt_emailaddress.Text.Trim();
            displayName = txt_displayname.Text.Trim();
            if (txt_captchaverification.Text.Trim().Equals(Session[USession.CaptchaText].ToString()))
            {
                AccountManager account = new AccountManager();
                if (!account.IsEmailOrNameRepeated(mailtxt, displayName, 0))
                {
                    CreateAccount(account);
                    SendEmail();
                    Response.Redirect("~/verify?" + QueryString.Email + "=" + mailtxt);
                }
                else
                {
                    Master.Localize(msg_repeatedemailorname);
                }
            }
            else
            {
                Master.Localize(msg_captchanotmatch);
                Session[USession.CaptchaText] = Captcha.GenerateRandomCode();
            }
        }

        private void CreateAccount(AccountManager account)
        {
            userGuid = Guid.NewGuid();
            Session[USession.TempGuid] = userGuid;
            Session[USession.TempToMail] = mailtxt;
            account.CreateAccount(mailtxt, txt_password.Text.Trim(), txt_displayname.Text.Trim(), userGuid.ToString("N"));
        }

        private void SendEmail()
        {
            mail_verificationsubject = GetGlobalResourceObject(StringResource.FileName, "mail_verificationsubject").ToString();
            mail_verificationbody = GetGlobalResourceObject(StringResource.FileName, "mail_verificationbody").ToString();
            activation_link = "http://" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"];
            activation_link += "/activate?" + QueryString.Token + "=" + userGuid.ToString("N");
            mail_verificationbody += "<p><a href=\"" + activation_link + "\">" + activation_link + "</a></p>";
            Email.SendVerificationEmail(mailtxt, mail_verificationsubject, mail_verificationbody);
        }

        private string displayName = String.Empty;
        private string mailtxt;
        private string mail_verificationsubject;
        private string mail_verificationbody;
        private string activation_link;
        private Guid userGuid;
    }
}