<%@ Page Language="C#" MasterPageFile="~/SiteTemplate.Master" AutoEventWireup="true" CodeBehind="EditAcc.aspx.cs" Inherits="UBCTextbooks.EditAcc" %>
<%@ MasterType VirtualPath="~/SiteTemplate.Master" %>
<asp:Content id="editaccount" ContentPlaceHolderID="Content" runat="server">
    <div id="div_editaccount">
        <h1><asp:Literal id="lt_editaccount" Text="" runat="server" /></h1>
        <p><asp:Literal id="lt_editaccount_description" Text="" runat="server" /></p>
        <table id="tb_editaccount">
            <tr>
                <td class="label"><asp:Literal id="lt_displayname" Text="" runat="server" /></td>
                <td><asp:TextBox id="txt_editdisplay" CssClass="textbox" runat="server" />
                    <asp:RequiredFieldValidator ValidationGroup="Confirm" ID="req_displayname" runat="server" ErrorMessage="<%$ Resources: String, msg_requiredfield %>" ControlToValidate="txt_editdisplay" Display="Dynamic" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:RegularExpressionValidator id="vld_displayname" runat="server" ErrorMessage="<%$ Resources: String, msg_displaynotvalid %>" ValidationExpression="\w{3,15}" ControlToValidate="txt_editdisplay" Display="Dynamic" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_emailaddress" Text="" runat="server" /></td>
                <td><asp:TextBox id="txt_editemail" CssClass="textbox" runat="server" />
                    <asp:RequiredFieldValidator ValidationGroup="Confirm" ID="req_email" runat="server" ErrorMessage="<%$ Resources: String, msg_requiredfield %>" ControlToValidate="txt_editemail" Display="Dynamic" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:RegularExpressionValidator id="vld_email" runat="server" ErrorMessage="<%$ Resources: String, msg_emailnotvalid %>" ValidationExpression="\S+@\S+\.\S+" ControlToValidate="txt_editemail" Display="Dynamic" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_password" Text="" runat="server" /></td>
                <td><asp:TextBox id="txt_editpassword" CssClass="textbox" runat="server" />
                    <asp:RequiredFieldValidator ValidationGroup="Confirm" ID="req_password" runat="server" ErrorMessage="<%$ Resources: String, msg_requiredfield %>" ControlToValidate="txt_editpassword" Display="Dynamic" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:RegularExpressionValidator ValidationGroup="Confirm" id="vld_password" runat="server" ErrorMessage="<%$ Resources: String, msg_passwordnotvalid %>" ValidationExpression="\w{6,15}" ControlToValidate="txt_editpassword" Display="Dynamic" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_password_confirm" Text="" runat="server" /></td>
                <td><asp:TextBox id="txt_editpasswordconfirm" CssClass="textbox" runat="server" />
                    <asp:RequiredFieldValidator ValidationGroup="Confirm" ID="req_passwordconfirm" runat="server" ErrorMessage="<%$ Resources: String, msg_requiredfield %>" ControlToValidate="txt_editpasswordconfirm" Display="Dynamic" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:CompareValidator id="vld_password_confirm" runat="server" ErrorMessage="<%$ Resources: String, msg_inputnotmatch %>" ControlToCompare="txt_editpassword" ControlToValidate="txt_editpasswordconfirm" Display="Dynamic" /></td>
            </tr>
            <tr>
                <td></td>
                <td style="color:Red;"><asp:Literal id="msg_repeatedemailorname" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td><asp:Button id="btn_confirm" ValidationGroup="Confirm" CssClass="button" Text="" runat="server" 
                        onclick="btn_confirm_Click" /></td>
                <td><asp:Button id="btn_cancel" CssClass="button" Text="" runat="server" 
                        onclick="btn_cancel_Click" /></td>
            </tr>
            <asp:Label id="lb_hiddenid" Style="display:none;" Text="" runat="server" />
        </table>
    </div>
</asp:Content>
