using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UBCTextbooksCore.Custom;
using UBCTextbooksCore.Constants;
using UBCTextbooksCore.Security;

namespace UBCTextbooks
{
    public partial class EditAcc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.CheckAccountSession();
            if (!IsPostBack)
                GetAccountInformation();
            GetStringResource();
            Master.LinkAccountCss = "activated";
        }

        private void GetStringResource()
        {
            Master.Localize(lt_editaccount);
            Master.Localize(lt_editaccount_description);
            Master.Localize(lt_displayname);
            Master.Localize(lt_emailaddress);
            Master.Localize(lt_password);
            Master.Localize(lt_password_confirm);
            Master.Localize(btn_confirm);
            Master.Localize(btn_cancel);
        }

        private void GetAccountInformation()
        {
            Dictionary<string, string> accountInfo = (Dictionary<string, string>)Session[USession.AccountInfo];
            txt_editemail.Text = accountInfo[UAccount.EmailAddress];
            txt_editdisplay.Text = accountInfo[UAccount.DisplayName];
            txt_editpassword.Text = accountInfo[UAccount.Password];
            txt_editpasswordconfirm.Text = accountInfo[UAccount.Password];
            lb_hiddenid.Text = accountInfo[UAccount.UserId];
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/account");
        }

        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> accountInfo = (Dictionary<string, string>)Session[USession.AccountInfo];
            mailtxt = txt_editemail.Text.Trim();
            displayName = txt_editdisplay.Text.Trim();
            userid = int.Parse(accountInfo[UAccount.UserId]);
            AccountManager account = new AccountManager();
            if (!account.IsEmailOrNameRepeated(mailtxt, displayName, userid))
            {
                EditUAccount(account);
                accountInfo[UAccount.DisplayName] = txt_editdisplay.Text;
                accountInfo[UAccount.EmailAddress] = txt_editemail.Text;
                accountInfo[UAccount.Password] = txt_editpassword.Text;
                Response.Redirect("~/account");
            }
            else
            {
                Master.Localize(msg_repeatedemailorname);
            }
        }

        private void EditUAccount(AccountManager account)
        {
            Master.CheckAccountSession();
            account.EditAccount(userid, mailtxt, txt_editpassword.Text.Trim(), txt_editdisplay.Text.Trim());
        }

        private int userid = 0;
        private string displayName = String.Empty;
        private string mailtxt;
    }
}
