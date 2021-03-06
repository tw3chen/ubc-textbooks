﻿using System;
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
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.LinkBuyCss = "activated";
                Master.EnterHookUp = btn_search.UniqueID;
                Master.DefaultFocus = txt_search.ClientID;
                PopulateAreaDDL();
                PopulatePriceDDL();
            }
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
                ddl_price.Items.Add(item);
            }
        }

        private void PopulateAreaDDL()
        {
            try
            {
                Settings setting = new Settings();
                ListItem fitem = new ListItem();
                fitem.Text = "All";
                fitem.Value = "All";
                ddl_location.Items.Add(fitem);
                foreach (string area in setting.Area())
                {
                    ListItem item = new ListItem();
                    item.Text = area;
                    item.Value = area;
                    ddl_location.Items.Add(item);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/search?" + QueryString.SearchBy + "=" + Request["rdbtn_searchby"] + "&" +
                                QueryString.Keywords + "=" + Request[txt_search.UniqueID].Trim() + "&" +
                                QueryString.Area + "=" + Request[ddl_location.UniqueID] + "&" +
                                QueryString.PriceRange + "=" + Request[ddl_price.UniqueID] + "&" +
                                QueryString.OldestPostedTime + "=" + Request[txt_date.UniqueID] + "&" +
                                QueryString.SortBy + "=" + Request["rdbtn_sortby"]);
        }
    }
}
