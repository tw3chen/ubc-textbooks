<%@ Page Language="C#" MasterPageFile="~/SiteTemplate.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UBCTextbooks.Login" %>
<%@ MasterType VirtualPath="~/SiteTemplate.Master" %>
<asp:Content id="login" ContentPlaceHolderID="Content" runat="server">
    <div id="div_login">
        <h1><asp:Literal id="lt_login" Text="" runat="server" /></h1>
        <p><asp:Literal id="lt_login_description" Text="" runat="server" /></p>
        <table id="tb_login">
            <tr>
                <td class="label"><asp:Literal id="lt_emailaddress" Text="" runat="server" /></td>
                <td><asp:TextBox id="txt_emailaddress" CssClass="textbox" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_password" Text="" runat="server" /></td>
                <td><asp:TextBox id="txt_password" CssClass="textbox" runat="server" TextMode="Password" autocomplete="off" /></td>
            </tr>
            <tr>
                <td class="label"><asp:CheckBox id="ch_rememberme" runat="server" Checked="true" /></td>
                <td><asp:Literal id="lt_rememberme" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"></td>
                <td style="color:red;"><asp:Literal id="lt_loginfailed" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Button id="btn_login" CssClass="button" Text="" runat="server" 
                        onclick="btn_login_Click" /></td>
            </tr>
        </table>
    </div>
</asp:Content>