<%@ Page Language="C#" MasterPageFile="~/SiteTemplate.Master" AutoEventWireup="true" CodeBehind="ConfirmTB.aspx.cs" Inherits="UBCTextbooks.ConfirmTB" %>
<%@ MasterType VirtualPath="~/SiteTemplate.Master" %>
<asp:Content id="sell" ContentPlaceHolderID="Content" runat="server">
    <div id="div_sell">
        <h1><asp:Literal id="lt_confirmtb" Text="" runat="server" /></h1>
        <p><asp:Literal id="lt_confirmtb_description" Text="" runat="server" /></p>
        <table id="tb_confirmtb">
            <tr>
                <td class="label"><asp:Literal id="lt_title" Text="" runat="server" /></td>
                <td><asp:Literal id="lt_aztitle" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_author" Text="" runat="server" /></td>
                <td><asp:Literal id="lt_azauthor" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_edition" Text="" runat="server" /></td>
                <td><asp:Literal id="lt_azedition" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_isbn" Text="" runat="server" /></td>
                <td><asp:Literal id="lt_azisbn" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_binding" Text="" runat="server" /></td>
                <td><asp:Literal id="lt_azbinding" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_manufacturer" Text="" runat="server" /></td>
                <td><asp:Literal id="lt_azmanufacturer" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_bookimage" Text="" runat="server" /></td>
                <td><asp:Image id="img_book" ImageUrl="~/images/imageua.gif" runat="server" /></td>
            </tr>
            <tr>
                <span style="color:Red;"><asp:Literal id="msg_internalerrortb" Text="" runat="server" /></span>
            </tr>
            <tr>
                <td><asp:Button id="btn_yes" CssClass="button" Text="" runat="server" 
                        onclick="btn_yes_Click" /></td>
                <td><asp:Button id="btn_no" CssClass="button" Text="" runat="server" 
                        onclick="btn_no_Click" /></td>
            </tr>
        </table>
        <p style="display:none;">
            <asp:Literal id="hd_bookprice" Text="" runat="server" />
            <asp:Literal id="hd_bookisbn" Text="" runat="server" />
            <asp:Literal id="hd_bookean" Text="" runat="server" />
            <asp:Literal id="hd_bookid" Text="" runat="server" />
        </p>
    </div>
</asp:Content>

