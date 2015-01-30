<%@ Page Language="C#" MasterPageFile="~/SiteTemplate.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="UBCTextbooks.Account" %>
<%@ MasterType VirtualPath="~/SiteTemplate.Master" %>
<asp:Content id="account" ContentPlaceHolderID="Content" runat="server">
    <div id="div_account">
        <h1><asp:Literal id="lt_account" Text="" runat="server" /></h1>
        <p><asp:Literal id="lt_account_description" Text="" runat="server" /></p>
        <table id="tb_account">
            <tr>
                <td class="label"><asp:Literal id="lt_displayname" Text="" runat="server" /></td>
                <td><asp:Literal id="ult_displayname" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_emailaddress" Text="" runat="server" /></td>
                <td><asp:Literal id="ult_emailaddress" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Button id="btn_editaccount" CssClass="button" Text="" runat="server" 
                        onclick="btn_editaccount_Click" /></td>
            </tr>
        </table>
    </div>
</asp:Content>