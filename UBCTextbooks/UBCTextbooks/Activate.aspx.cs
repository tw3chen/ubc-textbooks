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
using UBCTextbooksCore.Constants;
using UBCTextbooksCore.Custom;

namespace UBCTextbooks
{
    public partial class Activate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AccountManager account = new AccountManager();
                GetStringResource();
                if (account.ActivateAccount(Request.QueryString[QueryString.Token]))
                {
                    lt_activation_description.Text = GetGlobalResourceObject(StringResource.FileName, "lt_activation_description_success").ToString();
                }
                else
                {
                    lt_activation_description.Text = GetGlobalResourceObject(StringResource.FileName, "lt_activation_description_success").ToString();
                }
            }
        }

        private void GetStringResource()
        {
            Master.Localize(lt_activation);
        }
    }
}
