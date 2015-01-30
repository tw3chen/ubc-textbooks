<%@ Page Language="C#" MasterPageFile="~/SiteTemplate.Master" AutoEventWireup="true" CodeBehind="Activate.aspx.cs" Inherits="UBCTextbooks.Activate" %>
<%@ MasterType VirtualPath="~/SiteTemplate.Master" %>
<asp:Content id="activate" ContentPlaceHolderID="Content" runat="server">
    <div id="div_activate">
        <h1><asp:Literal id="lt_activation" Text="" runat="server" /></h1>
        <p><asp:Literal id="lt_activation_description" Text="" runat="server" /></p>
    </div>
</asp:Content>