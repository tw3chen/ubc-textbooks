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
    public partial class NewBookList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(bool)Session[USession.IsLoggedIn])
                Response.Redirect("~/login");
            txt_isbn.Text = Request.QueryString[QueryString.ISBN];
            txt_coursecode.Text = Request.QueryString[QueryString.CourseCode];
            if (!IsPostBack)
            {
                Master.LinkSellCss = "activated";
                Master.EnterHookUp = btn_confirm.UniqueID;
                GetStringResource();
                PopulateAreaDDL();
                PopulateConditionDDL();
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
            Master.Localize(lt_author_description);
            Master.Localize(lt_title);
            Master.Localize(lt_author);
            Master.Localize(lt_edition);
            Master.Localize(lt_coursecode);
            Master.Localize(lt_isbn);
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
            BookManager book = new BookManager();
            string na = "N/A";
            string author = txt_author.Text.Trim();
            string edition = txt_edition.Text.Trim();
            if (String.IsNullOrEmpty(author))
                author = na;
            if (String.IsNullOrEmpty(edition))
                edition = na;
            bookid = book.BookInsert(txt_title.Text.Trim(), author, txt_isbn.Text.Trim(),
                        na, na, na, na, edition, na, txt_coursecode.Text.Trim());
            string ubookid = bookid.ToString();
            string area = Request.Form[ddl_clocation.UniqueID];
            string location = txt_clocation.Text.Trim();
            ListManager list = new ListManager();
            Dictionary<string, string> accountInfo = (Dictionary<string, string>)Session[USession.AccountInfo];
            if (area == "UBC")
                location = String.Empty;
            list.ListInsert(accountInfo[UAccount.UserId], ubookid, Request.Form[ddl_ccondition.UniqueID], txt_cprice.Text.Trim(),
                                area, location, txt_cnotes.Text.Trim());
            Response.Redirect("~/mylisting");
        }

        private int? bookid;
    }
}
