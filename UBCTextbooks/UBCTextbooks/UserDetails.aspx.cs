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
using UBCTextbooksCore.Data;

namespace UBCTextbooks
{
    public partial class UserDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lt_huserid.Text = Request.QueryString[QueryString.UserId];
            AccountManager account = new AccountManager();
            SqlDataReader reader = account.RetrieveUserInfo(Request.QueryString[QueryString.UserId]);
            reader.Read();
            lt_userdisplay.Text = "Display Name: " + reader[UParameter.DisplayName].ToString();
            lt_useremail.Text = "Email Address: " + reader[UParameter.EmailAddress].ToString();
            if (!IsPostBack)
            {
                Master.LinkBuyCss = "activated";
                CreatePager();
                PopulateMyListing();
                GetStringResource();
            }
        }

        private void PopulateMyListing()
        {
            rp_mylisting.DataSource = pagedData;
            rp_mylisting.DataBind();
        }

        private void CreatePager()
        {
            ListManager list = new ListManager();
            arrayList = list.RetrieveList(Request.QueryString[QueryString.UserId]);
            if (!int.TryParse(Request.QueryString[QueryString.Page], out currentPage))
                currentPage = 1;
            pagedData.DataSource = arrayList;
            pagedData.AllowPaging = true;
            pagedData.PageSize = rowPerPage;
            pagedData.CurrentPageIndex = currentPage - 1;
            totalRowCount = arrayList.Count;
            totalPage = pagedData.PageCount;
            CreatePagingButtons();
        }

        private void CreatePagingButtons()
        {
            string pageUrl = Request.Url.GetLeftPart(UriPartial.Path);
            HyperLink startLnk = new HyperLink();
            startLnk.ID = "startLnk";
            startLnk.Text = "<<";
            startLnk.NavigateUrl = pageUrl + "?" + QueryString.UserId + "=" + lt_huserid.Text + "&" + QueryString.Page + "=1";
            ph_pager.Controls.Add(startLnk);
            HyperLink prevLnk = new HyperLink();
            prevLnk.ID = "prevLnk";
            prevLnk.Text = "<";
            prevLnk.NavigateUrl = pageUrl + "?" + QueryString.UserId + "=" + lt_huserid.Text + "&" + QueryString.Page + "=" + (currentPage - 1);
            ph_pager.Controls.Add(prevLnk);

            int startDisplayPage = currentPage, endDisplayPage = currentPage;
            while (startDisplayPage % numPageToDisplay != 1)
            {
                startDisplayPage -= 1;
            }
            while (endDisplayPage % numPageToDisplay != 0)
            {
                endDisplayPage += 1;
            }
            for (int i = 0; i < numPageToDisplay; i++)
            {
                int num = startDisplayPage + i;
                if (num > totalPage)
                    break;
                HyperLink numLnk = new HyperLink();
                numLnk.ID = "numLnk" + num;
                numLnk.Text = num.ToString();
                numLnk.NavigateUrl = pageUrl + "?" + QueryString.UserId + "=" + lt_huserid.Text + "&" + QueryString.Page + "=" + num;
                if (num == currentPage)
                    numLnk.Enabled = false;
                ph_pager.Controls.Add(numLnk);
            }

            HyperLink nextLnk = new HyperLink();
            nextLnk.ID = "nextLnk";
            nextLnk.Text = ">";
            nextLnk.NavigateUrl = pageUrl + "?" + QueryString.UserId + "=" + lt_huserid.Text + "&" + QueryString.Page + "=" + (currentPage + 1);
            ph_pager.Controls.Add(nextLnk);
            HyperLink endLnk = new HyperLink();
            endLnk.ID = "endLnk";
            endLnk.Text = ">>";
            endLnk.NavigateUrl = pageUrl + "?" + QueryString.UserId + "=" + lt_huserid.Text + "&" + QueryString.Page + "=" + totalPage;
            ph_pager.Controls.Add(endLnk);
            if (currentPage == 1)
            {
                startLnk.Enabled = false;
                prevLnk.Enabled = false;
            }
            if (currentPage == totalPage)
            {
                nextLnk.Enabled = false;
                endLnk.Enabled = false;
            }
        }

        private void GetStringResource()
        {
            Master.Localize(lt_userdetails);
            lt_numresults.Text = "Total number of results: " + totalRowCount;
        }

        private const int rowPerPage = 6;
        private int totalPage;
        private int totalRowCount;
        private const int numPageToDisplay = 5;
        private ArrayList arrayList;
        private int currentPage = 1;
        private PagedDataSource pagedData = new PagedDataSource();
    }
}
