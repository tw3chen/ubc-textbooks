using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
    public partial class ListDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((bool)Session[USession.IsLoggedIn])
            {
                Dictionary<string, string> accountInfo = (Dictionary<string, string>)Session[USession.AccountInfo];
                txt_displayname.Text = accountInfo[UAccount.DisplayName];
                txt_emailaddress.Text = accountInfo[UAccount.EmailAddress];
            }
            Master.LinkBuyCss = "activated";
            Master.EnterHookUp = btn_offer.UniqueID;
            GetStringResource();
            PopulateListInfo();
            PopulateComments();
        }

        private void PopulateComments()
        {
            CommentManager comment = new CommentManager();
            rp_comments.DataSource = comment.RetrieveComments(Request.QueryString[QueryString.ListId]);
            rp_comments.DataBind();
        }

        protected void btn_offer_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                CommentManager comment = new CommentManager();
                comment.InsertCommentOrOffer(Request.QueryString[QueryString.ListId], txt_displayname.Text, txt_emailaddress.Text, "Offer", txt_comments.Text);
                Response.Redirect(Request.Url.ToString());
            }
        }

        protected void btn_comment_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                CommentManager comment = new CommentManager();
                comment.InsertCommentOrOffer(Request.QueryString[QueryString.ListId], txt_displayname.Text, txt_emailaddress.Text, "Comment", txt_comments.Text);
                Response.Redirect(Request.Url.ToString());
            }
        }

        private void GetStringResource()
        {
            Master.Localize(lt_location);
            Master.Localize(lt_listdetails);
            Master.Localize(lt_author);
            Master.Localize(lt_title);
            Master.Localize(lt_manufacturer);
            Master.Localize(lt_binding);
            Master.Localize(lt_isbn);
            Master.Localize(lt_edition);
            Master.Localize(lt_area);
            Master.Localize(lt_price);
            Master.Localize(lt_coursecode);
            Master.Localize(lt_refprices);
            Master.Localize(lt_condition);
            Master.Localize(lt_numberofoffers);
            Master.Localize(lt_timelisted);
            Master.Localize(lt_posterdisplayname);
            Master.Localize(btn_offer);
            Master.Localize(btn_comment);
            Master.Localize(lt_notes);
            Master.Localize(lt_displayname);
            Master.Localize(lt_emailaddress);
            Master.Localize(lt_offercomments);
            Master.Localize(lt_comments);
            Master.Localize(lt_reload);
        }

        private void PopulateListInfo()
        {
            ListManager list = new ListManager();
            SqlDataReader reader = list.RetrieveCompleteList(Request.QueryString[QueryString.ListId]);
            reader.Read();
            string imageUrl = reader[UParameter.ImageUrl].ToString();
            if (imageUrl != "N/A")
                        img_book.ImageUrl = imageUrl;
            lt_larea.Text = reader[UParameter.Area].ToString();
            lt_llocation.Text = reader[UParameter.Location].ToString();
            lt_ltitle.Text = reader[UParameter.BookName].ToString();
            lt_lauthor.Text = reader[UParameter.AuthorNames].ToString();
            lt_lbinding.Text = reader[UParameter.BookBinding].ToString();
            lt_lmanufacturer.Text = reader[UParameter.BookManufacturer].ToString();
            lt_ledition.Text = reader[UParameter.Edition].ToString();
            lt_lcoursecode.Text = reader[UParameter.CourseList].ToString();
            if (reader[UParameter.EAN].ToString() != "N/A")
                lt_lisbn.Text = "10 digit: " + reader[UParameter.ISBN].ToString() + " / " + "13 digit: " + reader[UParameter.EAN].ToString();
            else
                lt_lisbn.Text = reader[UParameter.ISBN].ToString();
            float price = Common.SafeFloatParse(reader[UParameter.Price].ToString());
            lt_lprice.Text = String.Format("${0:F2}", price);
            float refprice = Common.SafeFloatParse(reader[UParameter.RefPrice].ToString());
            if (!(refprice > 0))
                lt_lrefprice.Text = "N/A";
            else
                lt_lrefprice.Text = String.Format("${0:F2}", refprice);
            lt_lcondition.Text = GetGlobalResourceObject("String", "ddl_cd" + reader[UParameter.Condition].ToString()).ToString();
            lt_lnumberofoffers.Text = reader[UParameter.NumberOfOffers].ToString();
            lt_lposterdisplayname.Text = reader[UParameter.DisplayName].ToString();
            lt_ltimelisted.Text = reader[UParameter.TimeListed].ToString();
            lt_lposterdisplayname.NavigateUrl = "userdetails?" + QueryString.UserId + "=" + reader[UParameter.UserId].ToString();
            lt_lnotes.Text = reader[UParameter.Notes].ToString().Replace("\r\n", "<br />");
            lt_llocation.Text = reader[UParameter.Location].ToString();
            area = lt_larea.Text;
            location = lt_llocation.Text;
            isbn = reader[UParameter.ISBN].ToString();
        }

        public string isbn;
        public string area;
        public string location;
    }
}