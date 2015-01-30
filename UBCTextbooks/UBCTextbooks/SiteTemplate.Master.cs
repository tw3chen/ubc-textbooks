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

namespace UBCTextbooks
{
    public partial class SiteTemplate : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeLinks();
            GetStringResource();
            AppendAppropriateLinks();
        }

        public string DefaultFocus
        {
            get { return this.fm_main.DefaultFocus; }
            set { this.fm_main.DefaultFocus = value; }
        }

        public string EnterHookUp
        {
            get { return this.fm_main.DefaultButton; }
            set { this.fm_main.DefaultButton = value; }
        }

        public string LinkMyListingCss
        {
            get { return this.hp_mylisting.CssClass; }
            set { this.hp_mylisting.CssClass = value; }
        }
        public string LinkBuyCss
        {
            get { return this.hp_buytextbooks.CssClass; }
            set { this.hp_buytextbooks.CssClass = value; }
        }
        public string LinkSellCss
        {
            get { return this.hp_selltextbooks.CssClass; }
            set { this.hp_selltextbooks.CssClass = value; }
        }
        public string LinkLoginCss
        {
            get { return this.hp_login.CssClass; }
            set { this.hp_login.CssClass = value; }
        }
        public string LinkSignUpCss
        {
            get { return this.hp_signup.CssClass; }
            set { this.hp_signup.CssClass = value; }
        }
        public string LinkAccountCss
        {
            get { return this.hp_myaccount.CssClass; }
            set { this.hp_myaccount.CssClass = value; }
        }

        private void InitializeLinks()
        {
            hp_login.ID = "hp_login";
            hp_login.NavigateUrl = "~/login";
            hp_signup.ID = "hp_signup";
            hp_signup.NavigateUrl = "~/signup";
            hp_mylisting.ID = "hp_mylisting";
            hp_mylisting.NavigateUrl = "~/mylisting";
            hp_myaccount.ID = "hp_myaccount";
            hp_myaccount.NavigateUrl = "~/account";
            hp_logout.ID = "hp_logout";
            hp_logout.Click += new EventHandler(hp_logout_Click);
            hp_logout.CausesValidation = false;
        }

        protected void hp_logout_Click(Object sender, EventArgs e)
        {
            Session[USession.IsLoggedIn] = false;
            Session[USession.AccountInfo] = null;
            Response.Redirect("~/login");
        }


        private void AppendAppropriateLinks()
        {
            if ((bool)Session[USession.IsLoggedIn])
            {
                navigation.Controls.Add(hp_mylisting);
                AppendSeperator();
                navigation.Controls.Add(hp_myaccount);
                AppendSeperator();
                navigation.Controls.Add(hp_logout);
                Dictionary<string, string> accountInfo = (Dictionary<string, string>)Session[USession.AccountInfo];
                lb_logininfo.Text = String.Format(lb_logininfo.Text, (String)accountInfo[UAccount.DisplayName]);
                lb_logininfo.CssClass = "visible";
            }
            else
            {
                navigation.Controls.Add(hp_login);
                AppendSeperator();
                navigation.Controls.Add(hp_signup);
            }
        }

        private void AppendSeperator()
        {
            Literal lt_linkseperator = new Literal();
            lt_linkseperator.Text = " | ";
            navigation.Controls.Add(lt_linkseperator);
        }

        private void GetStringResource()
        {
            Localize(hp_buytextbooks);
            Localize(hp_selltextbooks);
            Localize(hp_login);
            Localize(hp_signup);
            Localize(hp_mylisting);
            Localize(hp_myaccount);
            Localize(hp_logout);
            Localize(lb_logininfo);
            Page.Title = GetGlobalResourceObject(strResource, "pagetitle").ToString();
        }

        public void Localize(HyperLink hyperlink)
        {
            hyperlink.Text = GetGlobalResourceObject(strResource, hyperlink.ID).ToString();
        }
        public void Localize(Literal literal)
        {
            literal.Text = GetGlobalResourceObject(strResource, literal.ID).ToString();
        }
        public void Localize(Button button)
        {
            button.Text = GetGlobalResourceObject(strResource, button.ID).ToString();
        }
        public void Localize(Label label)
        {
            label.Text = GetGlobalResourceObject(strResource, label.ID).ToString();
        }
        public void Localize(LinkButton linkb)
        {
            linkb.Text = GetGlobalResourceObject(strResource, linkb.ID).ToString();
        }

        public void CheckAccountSession()
        {
            if (Session[USession.IsLoggedIn] == null || !(bool)Session[USession.IsLoggedIn] || Session[USession.AccountInfo] == null)
                Response.Redirect("~/login");
        }

        private const string strResource = StringResource.FileName;
        private HyperLink hp_signup = new HyperLink();
        private HyperLink hp_login = new HyperLink();
        private HyperLink hp_mylisting = new HyperLink();
        private HyperLink hp_myaccount = new HyperLink();
        private LinkButton hp_logout = new LinkButton();
    }
}
