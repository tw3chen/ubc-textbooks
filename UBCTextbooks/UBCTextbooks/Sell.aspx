<%@ Page Language="C#" MasterPageFile="~/SiteTemplate.Master" AutoEventWireup="true" CodeBehind="Sell.aspx.cs" Inherits="UBCTextbooks.Sell" %>
<%@ MasterType VirtualPath="~/SiteTemplate.Master" %>
<asp:Content id="sell" ContentPlaceHolderID="Content" runat="server">
    <div id="div_sell">
        <h1><asp:Literal id="lt_sell" Text="" runat="server" /></h1>
        <p><asp:Literal id="lt_sell_description" Text="" runat="server" /></p>
        <table id="tb_sell">
            <tr>
                <td class="label"><asp:Literal id="lt_coursecode" Text="" runat="server" /></td>
                <td><asp:DropDownList style="background-color:#F0F0F0;" id="ddl_cdepartment" runat="server" />
                    <asp:TextBox id="txt_cnumber" CssClass="textbox" runat="server" Width="100" />
                    <asp:RequiredFieldValidator ID="req_coursecode" runat="server" ErrorMessage="<%$ Resources: String, msg_requiredfield %>" ControlToValidate="txt_cnumber" Display="Dynamic" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Literal id="lt_coursecode_description" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:RegularExpressionValidator id="vld_coursecode" runat="server" ErrorMessage="<%$ Resources: String, msg_numbernotvalid %>" ValidationExpression="\d{2,4}" ControlToValidate="txt_cnumber" Display="Dynamic" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_isbn" Text="" runat="server" /></td>
                <td><asp:TextBox id="txt_isbn" CssClass="textbox" runat="server" />
                    <asp:RequiredFieldValidator ID="req_isbn" runat="server" ErrorMessage="<%$ Resources: String, msg_requiredfield %>" ControlToValidate="txt_isbn" Display="Dynamic" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Literal id="lt_isbn_description" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:RegularExpressionValidator id="vld_isbn" runat="server" ErrorMessage="<%$ Resources: String, msg_isbnnotvalid %>" ValidationExpression="\d{10,13}" ControlToValidate="txt_isbn" Display="Dynamic" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Button id="btn_next" CssClass="button" Text="" runat="server"
                        onclick="btn_next_Click" /></td>
            </tr>
        </table>
    </div>
</asp:Content>
