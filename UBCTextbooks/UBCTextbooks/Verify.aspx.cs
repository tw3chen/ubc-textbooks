using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using UBCTextbooksCore.Custom;
using UBCTextbooksCore.Constants;

namespace UBCTextbooks
{
    public partial class Verify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetStringResource();
            lt_verification_description.Text = String.Format(lt_verification_description.Text, Request.QueryString[QueryString.Email]);
        }

        private void GetStringResource()
        {
            Master.Localize(lt_verification);
            Master.Localize(lt_verification_description);
            Master.Localize(lt_sendmail);
            Master.Localize(btn_resendmail);
        }

        private void SendEmail()
        {
            mailtxt = (String)Session[USession.TempToMail];
            userGuid = (Guid)Session[USession.TempGuid];
            mail_verificationsubject = GetGlobalResourceObject(StringResource.FileName, "mail_verificationsubject").ToString();
            mail_verificationbody = GetGlobalResourceObject(StringResource.FileName, "mail_verificationbody").ToString();
            activation_link = "http://" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"];
            activation_link += "/activate?" + QueryString.Token + "=" + userGuid.ToString("N");
            mail_verificationbody += "<p><a href=\"" + activation_link + "\">" + activation_link + "</a></p>";
            Email.SendVerificationEmail(mailtxt, mail_verificationsubject, mail_verificationbody);
        }

        protected void btn_resendmail_Click(object sender, EventArgs e)
        {
            SendEmail();
        }

        private string mailtxt;
        private string mail_verificationsubject;
        private string mail_verificationbody;
        private string activation_link;
        private Guid userGuid;
    }
}
