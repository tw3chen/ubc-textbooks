using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UBCTextbooksCore.Constants;

namespace UBCTextbooks
{
    public partial class Account : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.CheckAccountSession();
                GetAccountInformation();
                GetStringResource();
                Master.LinkAccountCss = "activated";
            }
        }

        private void GetStringResource()
        {
            Master.Localize(lt_account);
            Master.Localize(lt_account_description);
            Master.Localize(lt_displayname);
            Master.Localize(lt_emailaddress);
            Master.Localize(btn_editaccount);
        }

        private void GetAccountInformation()
        {
            Dictionary<string, string> accountInfo = (Dictionary<string, string>)Session[USession.AccountInfo];
            ult_displayname.Text = accountInfo[UAccount.DisplayName];
            ult_emailaddress.Text = accountInfo[UAccount.EmailAddress];
        }

        protected void btn_editaccount_Click(object sender, EventArgs e)
        {
            Master.CheckAccountSession();
            Response.Redirect("~/editaccount");
        }
    }
}
