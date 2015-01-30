using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UBCTextbooksCore.Constants;
using UBCTextbooksCore.Custom;
using UBCTextbooksCore.Data;
using UBCTextbooksCore.Feed;

namespace UBCTextbooks
{
    public partial class Sell : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(bool)Session[USession.IsLoggedIn])
            {
                Session[USession.PageTracker] = "Sell";
                Response.Redirect("~/login");
            }
            if (!IsPostBack)
            {
                Master.LinkSellCss = "activated";
                GetStringResource();
                PopulateDepartmentDDL();
                Master.EnterHookUp = btn_next.UniqueID;
            }
        }

        protected void btn_next_Click(object sender, EventArgs e)
        {
            int? bookindb = 0;
            string coursecode = Request.Form[ddl_cdepartment.UniqueID] + txt_cnumber.Text.Trim();
            AmazonBook amazon = new AmazonBook();
            BookManager book = new BookManager();
            string isbn = txt_isbn.Text.Trim();
            bookindb = book.CheckBookInDB(isbn, coursecode);
            if (bookindb > 0)
            {
                Response.Redirect("~/confirmtb?" + QueryString.ISBN + "=" + isbn + "&" +
                                    QueryString.CourseCode + "=" + coursecode + "&" + 
                                    QueryString.InDb + "=" + bookindb);
            }
            else
            {
                Dictionary<string, string> bookInfo = amazon.LookUpBook(isbn);
                if (bookInfo[Book.Name] != null)
                {
                    Session[USession.Book] = bookInfo;
                    Response.Redirect("~/confirmtb?" + QueryString.ISBN + "=" + txt_isbn.Text.Trim() + "&" +
                                    QueryString.CourseCode + "=" + coursecode);
                }
                else
                {
                    Response.Redirect("~/newbooklist?" + QueryString.ISBN + "=" + txt_isbn.Text.Trim() + "&" +
                                    QueryString.CourseCode + "=" + coursecode);
                }
            }
        }

        private void PopulateDepartmentDDL()
        {
            
            Settings setting = new Settings();
            foreach (string department in setting.Department())
            {
                ListItem item = new ListItem();
                item.Text = department;
                item.Value = department;
                ddl_cdepartment.Items.Add(item);
            }
        }

        private void GetStringResource()
        {
            Master.Localize(lt_sell);
            Master.Localize(lt_sell_description);
            Master.Localize(lt_isbn);
            Master.Localize(lt_isbn_description);
            Master.Localize(btn_next);
            Master.Localize(lt_coursecode);
            Master.Localize(lt_coursecode_description);
        }
    }
}
