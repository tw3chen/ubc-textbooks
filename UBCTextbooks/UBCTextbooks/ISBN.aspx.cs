using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UBCTextbooks
{
    public partial class ISBN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetStringResource();
                Master.LinkSellCss = "activated";
            }
        }

        private void GetStringResource()
        {
            Master.Localize(lt_isbn);
            Master.Localize(lt_isbn_details);
        }
    }
}
