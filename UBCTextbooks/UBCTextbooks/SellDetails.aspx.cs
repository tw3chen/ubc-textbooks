using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UBCTextbooksCore.Constants;
using UBCTextbooksCore.Custom;
using UBCTextbooksCore.Data;

namespace UBCTextbooks
{
    public partial class SellDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(bool)Session[USession.IsLoggedIn])
                Response.Redirect("~/login");
            if (!IsPostBack)
            {
                int rprice = 0;
                Master.LinkSellCss = "activated";
                Master.EnterHookUp = btn_confirm.UniqueID;
                GetStringResource();
                PopulateAreaDDL();
                PopulateConditionDDL();
                int.TryParse(Request.QueryString[QueryString.Price], out rprice);
                if (rprice != 0)
                {
                    lt_refprice.Text = String.Format(GetGlobalResourceObject(StringResource.FileName, "lt_refprice").ToString(), rprice / 100M);
                }
                hp_previewmap.Attributes.Add("onclick", "previewMap()");
            }
        }

        private void PopulateAreaDDL()
        {
            Settings setting = new Settings();
            foreach (string area in setting.Area())
            {
                ListItem item = new ListItem();
                item.Text = area;
                item.Value = area;
                ddl_clocation.Items.Add(item);
            }
        }

        private void PopulateConditionDDL()
        {
            ddl_ccondition.Items.Add(ConditionItem("brandnew"));
            ddl_ccondition.Items.Add(ConditionItem("excellent"));
            ddl_ccondition.Items.Add(ConditionItem("good"));
            ddl_ccondition.Items.Add(ConditionItem("fair"));
            ddl_ccondition.Items.Add(ConditionItem("poor"));
        }

        private ListItem ConditionItem(string condition)
        {
            ListItem item = new ListItem();
            item.Text = GetGlobalResourceObject(StringResource.FileName, "ddl_cd" + condition).ToString();
            item.Value = condition;
            return item;
        }

        private void GetStringResource()
        {
            Master.Localize(lt_condition);
            Master.Localize(lt_area);
            Master.Localize(btn_confirm);
            Master.Localize(lt_location);
            Master.Localize(lt_location_description);
            Master.Localize(hp_previewmap);
            Master.Localize(lt_price);
            Master.Localize(lt_notes);
            Master.Localize(lt_notes_description);
        }

        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            string area = Request.Form[ddl_clocation.UniqueID];
            string location = txt_clocation.Text.Trim();
            ListManager list = new ListManager();
            Dictionary<string, string> accountInfo = (Dictionary<string, string>)Session[USession.AccountInfo];
            if (area == "UBC")
                location = String.Empty;
            list.ListInsert(accountInfo[UAccount.UserId], Request.QueryString[QueryString.BookId], Request.Form[ddl_ccondition.UniqueID], txt_cprice.Text.Trim(),
                                area, location, txt_cnotes.Text.Trim());
            Response.Redirect("~/mylisting");
        }
    }
}
