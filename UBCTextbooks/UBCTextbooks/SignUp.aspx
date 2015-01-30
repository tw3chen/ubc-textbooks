<%@ Page Language="C#" MasterPageFile="~/SiteTemplate.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="UBCTextbooks.SignUp" %>
<%@ MasterType VirtualPath="~/SiteTemplate.Master" %>
<asp:Content id="signup" ContentPlaceHolderID="Content" runat="server">
    <div id="div_signup">
        <h1><asp:Literal id="lt_signup" Text="" runat="server" /></h1>
        <p><asp:Literal id="lt_signup_description" Text="" runat="server" /></p>
        <table id="tb_signup">
            <tr>
                <td class="label"><asp:Literal id="lt_emailaddress" Text="" runat="server" /></td>
                <td><asp:TextBox id="txt_emailaddress" CssClass="textbox" runat="server" />
                    <asp:RequiredFieldValidator ID="req_email" runat="server" ErrorMessage="<%$ Resources: String, msg_requiredfield %>" ControlToValidate="txt_emailaddress" Display="Dynamic" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:RegularExpressionValidator id="vld_email" runat="server" ErrorMessage="<%$ Resources: String, msg_emailnotvalid %>" ValidationExpression="\S+@\S+\.\S+" ControlToValidate="txt_emailaddress" Display="Dynamic" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Literal id="lt_emailaddress_description" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_emailaddress_confirm" Text="" runat="server" /></td>
                <td><asp:TextBox id="txt_emailaddress_confirm" CssClass="textbox" runat="server" />
                    <asp:RequiredFieldValidator ID="req_emailconfirm" runat="server" ErrorMessage="<%$ Resources: String, msg_requiredfield %>" ControlToValidate="txt_emailaddress_confirm" Display="Dynamic" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:CompareValidator id="vld_emailconfirm" runat="server" ErrorMessage="<%$ Resources: String, msg_inputnotmatch %>" ControlToCompare="txt_emailaddress" ControlToValidate="txt_emailaddress_confirm" Display="Dynamic" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_password" Text="" runat="server" /></td>
                <td><asp:TextBox id="txt_password" CssClass="textbox" runat="server" TextMode="Password" autocomplete="off" />
                    <asp:RequiredFieldValidator ID="req_password" runat="server" ErrorMessage="<%$ Resources: String, msg_requiredfield %>" ControlToValidate="txt_password" Display="Dynamic" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:RegularExpressionValidator id="vld_password" runat="server" ErrorMessage="<%$ Resources: String, msg_passwordnotvalid %>" ValidationExpression="\w{6,15}" ControlToValidate="txt_password" Display="Dynamic" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_password_confirm" Text="" runat="server" /></td>
                <td><asp:TextBox id="txt_password_confirm" CssClass="textbox" runat="server" TextMode="Password" autocomplete="off" />
                    <asp:RequiredFieldValidator ID="req_passwordconfirm" runat="server" ErrorMessage="<%$ Resources: String, msg_requiredfield %>" ControlToValidate="txt_password_confirm" Display="Dynamic" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:CompareValidator id="vld_password_confirm" runat="server" ErrorMessage="<%$ Resources: String, msg_inputnotmatch %>" ControlToCompare="txt_password" ControlToValidate="txt_password_confirm" Display="Dynamic" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_displayname" Text="" runat="server" /></td>
                <td><asp:TextBox id="txt_displayname" CssClass="textbox" runat="server" />
                    <asp:RequiredFieldValidator ID="req_displayname" runat="server" ErrorMessage="<%$ Resources: String, msg_requiredfield %>" ControlToValidate="txt_displayname" Display="Dynamic" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:RegularExpressionValidator id="vld_displayname" runat="server" ErrorMessage="<%$ Resources: String, msg_displaynotvalid %>" ValidationExpression="\w{3,15}" ControlToValidate="txt_displayname" Display="Dynamic" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Literal id="lt_displayname_description" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_captchaverification" Text="" runat="server" /></td>
                <td><asp:TextBox id="txt_captchaverification" CssClass="textbox" runat="server" autocomplete="off" />
                    <asp:RequiredFieldValidator ID="vld_captcha" runat="server" ErrorMessage="<%$ Resources: String, msg_requiredfield %>" ControlToValidate="txt_captchaverification" /></td>
            </tr>
            <tr>
                <td></td>
                <td style="color:Red;"><asp:Literal id="msg_captchanotmatch" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Literal id="lt_captcha_description" Text="" runat="server" /><br />
                    <img src="images/captcha.aspx" alt="" /></td>
            </tr>
            <tr>
                <td></td>
                <td style="color:Red;"><asp:Literal id="msg_repeatedemailorname" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Button id="btn_signup" CssClass="button" Text="" runat="server" 
                        onclick="btn_signup_Click" /></td>
            </tr>
        </table>
    </div>
</asp:Content>