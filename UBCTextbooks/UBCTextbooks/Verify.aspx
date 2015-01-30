<%@ Page Language="C#" MasterPageFile="~/SiteTemplate.Master" AutoEventWireup="true" CodeBehind="Verify.aspx.cs" Inherits="UBCTextbooks.Verify" %>
<%@ MasterType VirtualPath="~/SiteTemplate.Master" %>
<asp:Content id="verify" ContentPlaceHolderID="Content" runat="server">
    <div id="div_verify">
        <h1><asp:Literal id="lt_verification" Text="" runat="server" /></h1>
        <p><asp:Literal id="lt_verification_description" Text="" runat="server" /></p>
        <p><asp:Literal id="lt_sendmail" Text="" runat="server" /></p>
        <p><asp:Button id="btn_resendmail" CssClass="button" Text="" runat="server" 
                            onclick="btn_resendmail_Click" /></p>
    </div>
</asp:Content>