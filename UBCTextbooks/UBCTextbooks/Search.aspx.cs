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
using UBCTextbooksCore.Data;

namespace UBCTextbooks
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.LinkBuyCss = "activated";
                CreatePager();
                PopulateSearch();
                GetStringResource();
                PopulateAreaDDL();
                PopulatePriceDDL();
                Master.EnterHookUp = btn_search.UniqueID;
                PopulateInputFields();
            }
        }

        private void PopulateInputFields()
        {
            txt_search.Text = Request.QueryString[QueryString.Keywords];
            ddl_location.SelectedIndex = ddl_lcount;
            ddl_price.SelectedIndex = ddl_pcount;
            switch (Request.QueryString[QueryString.SearchBy])
            {
                case "CourseCode" :
                    rdb_searchby.SelectedIndex = 0;
                    break;
                case "Title" :
                    rdb_searchby.SelectedIndex = 1;
                    break;
                case "Author":
                    rdb_searchby.SelectedIndex = 2;
                    break;
                case "ISBN":
                    rdb_searchby.SelectedIndex = 3;
                    break;
            }
            switch (Request.QueryString[QueryString.SortBy])
            {
                case "PostedTime":
                    rdb_sortby.SelectedIndex = 0;
                    break;
                case "Price":
                    rdb_sortby.SelectedIndex = 1;
                    break;
            }
            txt_date.Text = Request.QueryString[QueryString.OldestPostedTime];
        }

        private void PopulatePriceDDL()
        {
            int price = 0;
            ListItem fitem = new ListItem();
            fitem.Text = "No Constraint";
            fitem.Value = "1000";
            ddl_price.Items.Add(fitem);
            for (int i = 0; i < 8; i++)
            {
                price += 25;
                ListItem item = new ListItem();
                item.Text = "<= " + price;
                item.Value = price.ToString();
                if (item.Value == Request.QueryString[QueryString.PriceRange])
                    ddl_pcount = i + 1;
                ddl_price.Items.Add(item);
            }
        }

        private void PopulateAreaDDL()
        {
            Settings setting = new Settings();
            ListItem fitem = new ListItem();
            fitem.Text = "All";
            fitem.Value = "All";
            ddl_location.Items.Add(fitem);
            int i = 0;
            foreach (string area in setting.Area())
            {
                i++;
                ListItem item = new ListItem();
                item.Text = area;
                item.Value = area;
                if (item.Value == Request.QueryString[QueryString.Area])
                    ddl_lcount = i;
                ddl_location.Items.Add(item);
            }
        }

        private void PopulateSearch()
        {
            rp_mylisting.DataSource = pagedData;
            rp_mylisting.DataBind();
        }

        private void CreatePager()
        {
            ListManager list = new ListManager();
            string oldestPostedTime = "2009-01-01";
            if (!String.IsNullOrEmpty(Request.QueryString[QueryString.OldestPostedTime]))
                oldestPostedTime = Request.QueryString[QueryString.OldestPostedTime];
            arrayList = list.RetrieveSearch(Request.QueryString[QueryString.SearchBy], Request.QueryString[QueryString.Keywords],
                            Request.QueryString[QueryString.Area], Request.QueryString[QueryString.PriceRange],
                            oldestPostedTime, Request.QueryString[QueryString.SortBy]);
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
            string baseQueryString = "?" + QueryString.SearchBy + "=" + Request.QueryString[QueryString.SearchBy] + "&" +
                                QueryString.Keywords + "=" + Request.QueryString[QueryString.Keywords].ToUpper() + "&" +
                                QueryString.Area + "=" + Request.QueryString[QueryString.Area] + "&" +
                                QueryString.PriceRange + "=" + Request.QueryString[QueryString.PriceRange] + "&" +
                                QueryString.OldestPostedTime + "=" + Request.QueryString[QueryString.OldestPostedTime] + "&" +
                                QueryString.SortBy + "=" + Request.QueryString[QueryString.SortBy];
            HyperLink startLnk = new HyperLink();
            startLnk.ID = "startLnk";
            startLnk.Text = "<<";
            startLnk.NavigateUrl = pageUrl + baseQueryString + "&" + QueryString.Page + "=1";
            ph_pager.Controls.Add(startLnk);
            HyperLink prevLnk = new HyperLink();
            prevLnk.ID = "prevLnk";
            prevLnk.Text = "<";
            prevLnk.NavigateUrl = pageUrl + baseQueryString + "&" + QueryString.Page + "=" + (currentPage - 1);
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
                numLnk.NavigateUrl = pageUrl + baseQueryString + "&" + QueryString.Page + "=" + num;
                if (num == currentPage)
                    numLnk.Enabled = false;
                ph_pager.Controls.Add(numLnk);
            }

            HyperLink nextLnk = new HyperLink();
            nextLnk.ID = "nextLnk";
            nextLnk.Text = ">";
            nextLnk.NavigateUrl = pageUrl + baseQueryString + "&" + QueryString.Page + "=" + (currentPage + 1);
            ph_pager.Controls.Add(nextLnk);
            HyperLink endLnk = new HyperLink();
            endLnk.ID = "endLnk";
            endLnk.Text = ">>";
            endLnk.NavigateUrl = pageUrl + baseQueryString + "&" + QueryString.Page + "=" + totalPage;
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
            Master.Localize(lt_search);
            lt_numresults.Text = "Total number of results: " + totalRowCount;
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/search?" + QueryString.SearchBy + "=" + Request[rdb_searchby.UniqueID] + "&" +
                                QueryString.Keywords + "=" + Request[txt_search.UniqueID].Trim() + "&" +
                                QueryString.Area + "=" + Request[ddl_location.UniqueID] + "&" +
                                QueryString.PriceRange + "=" + Request[ddl_price.UniqueID] + "&" +
                                QueryString.OldestPostedTime + "=" + Request[txt_date.UniqueID] + "&" +
                                QueryString.SortBy + "=" + Request[rdb_sortby.UniqueID]);
        }

        private const int rowPerPage = 6;
        private int totalPage;
        private int totalRowCount;
        private const int numPageToDisplay = 5;
        private ArrayList arrayList;
        private int currentPage = 1;
        private PagedDataSource pagedData = new PagedDataSource();
        private int ddl_lcount = 0;
        private int ddl_pcount = 0;
    }
}
