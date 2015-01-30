using System;
using System.Collections;
using System.Collections.Generic;
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
using UBCTextbooksCore.Feed;

namespace UBCTextbooks
{
    public partial class ConfirmTB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(bool)Session[USession.IsLoggedIn])
                Response.Redirect("~/login");
            hd_bookisbn.Text = Request.QueryString[QueryString.ISBN];
            Master.LinkSellCss = "activated";
            Master.EnterHookUp = btn_yes.UniqueID;
            GetStringResource();
            PopulateBookInfo();
        }

        protected void btn_yes_Click(object sender, EventArgs e)
        {
            int bookindb = int.Parse(Request.QueryString[QueryString.InDb] ?? "0");
            if (bookindb > 0)
            {
                bookid = int.Parse(hd_bookid.Text);
                if (bookindb < 2)
                {
                    BookManager book = new BookManager();
                    book.CourseInsert(Request.QueryString[QueryString.CourseCode], hd_bookid.Text.Trim());
                }
                Response.Redirect("~/selldetails" + "?" + QueryString.BookId + "=" + bookid + "&" + QueryString.Price + "=" + hd_bookprice.Text.Trim());
            }
            else
            {
                BookManager book = new BookManager();
                bookid = book.BookInsert(lt_aztitle.Text.Trim(), lt_azauthor.Text.Trim(), hd_bookisbn.Text.Trim(),
                    img_book.ImageUrl.Trim(), hd_bookprice.Text.Trim(), lt_azbinding.Text.Trim(), lt_azmanufacturer.Text.Trim(),
                    lt_azedition.Text.Trim(), hd_bookean.Text.Trim(), Request.QueryString[QueryString.CourseCode]);
                if (bookid != null)
                {
                    Response.Redirect("~/selldetails" + "?" + QueryString.BookId + "=" + bookid + "&" + QueryString.Price + "=" + hd_bookprice.Text.Trim());
                }
                else
                {
                    msg_internalerrortb.Text = GetGlobalResourceObject(StringResource.FileName, "msg_internalerrortb").ToString();
                }
            }
        }

        protected void btn_no_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/sell");
        }

        private void GetStringResource()
        {
            Master.Localize(lt_author);
            Master.Localize(lt_title);
            Master.Localize(lt_manufacturer);
            Master.Localize(lt_binding);
            Master.Localize(lt_isbn);
            Master.Localize(lt_edition);
            Master.Localize(lt_confirmtb);
            Master.Localize(lt_confirmtb_description);
            Master.Localize(btn_yes);
            Master.Localize(btn_no);
        }

        private void PopulateBookInfo()
        {
            bookindb = int.Parse(Request.QueryString[QueryString.InDb] ?? "0");
            uisbn = Request.QueryString[QueryString.ISBN];
            if (bookindb > 0)
            {
                BookManager book = new BookManager();
                Dictionary<string, string> bookInfo = book.RetrieveBookByISBN(uisbn);
                hd_bookid.Text = bookInfo[Book.Id];
                string na = GetGlobalResourceObject(StringResource.FileName, "lt_na").ToString();
                string isbn = bookInfo[Book.ISBN] ?? na;
                string ean = bookInfo[Book.EAN] ?? na;
                lt_azauthor.Text = bookInfo[Book.AuthorNames] ?? na;
                lt_aztitle.Text = bookInfo[Book.Name] ?? na;
                lt_azedition.Text = bookInfo[Book.Edition] ?? na;
                lt_azbinding.Text = bookInfo[Book.Binding] ?? na;
                lt_azmanufacturer.Text = bookInfo[Book.Manufacturer] ?? na;
                if (ean != "N/A")
                    lt_azisbn.Text = String.Format(GetGlobalResourceObject(StringResource.FileName, "lt_tenorthirteendigit").ToString(), isbn, ean);
                else
                    lt_azisbn.Text = isbn;
                if (bookInfo[Book.ImageUrl] != "N/A")
                    img_book.ImageUrl = bookInfo[Book.ImageUrl];
                hd_bookprice.Text = bookInfo[Book.Price] ?? "0";
                hd_bookprice.Text = (float.Parse(hd_bookprice.Text) * 100).ToString();
                hd_bookisbn.Text = isbn;
                hd_bookean.Text = ean;
            }
            else
            {
                AmazonBook amazon = new AmazonBook();
                Dictionary<string, string> bookInfo = amazon.LookUpBook(uisbn);
                if (bookInfo != null)
                {
                    string na = "N/A";
                    string isbn = bookInfo[Book.ISBN] ?? na;
                    string ean = bookInfo[Book.EAN] ?? na;
                    lt_azauthor.Text = bookInfo[Book.AuthorNames] ?? na;
                    lt_aztitle.Text = bookInfo[Book.Name] ?? na;
                    lt_azedition.Text = bookInfo[Book.Edition] ?? na;
                    lt_azbinding.Text = bookInfo[Book.Binding] ?? na;
                    lt_azmanufacturer.Text = bookInfo[Book.Manufacturer] ?? na;
                    lt_azisbn.Text = String.Format(GetGlobalResourceObject(StringResource.FileName, "lt_tenorthirteendigit").ToString(), isbn, ean);
                    if (!String.IsNullOrEmpty(bookInfo[Book.ImageUrl]))
                        img_book.ImageUrl = bookInfo[Book.ImageUrl];
                    hd_bookprice.Text = bookInfo[Book.Price] ?? "0";
                    hd_bookisbn.Text = isbn;
                    hd_bookean.Text = ean;
                }
            }
        }

        private int? bookid;
        private int? bookindb;
        private string uisbn;
    }
}