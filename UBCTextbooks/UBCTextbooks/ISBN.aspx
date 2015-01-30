<%@ Page Language="C#" MasterPageFile="~/SiteTemplate.Master" AutoEventWireup="true" CodeBehind="ISBN.aspx.cs" Inherits="UBCTextbooks.ISBN" %>
<%@ MasterType VirtualPath="~/SiteTemplate.Master" %>
<asp:Content id="isbn" ContentPlaceHolderID="Content" runat="server">
    <div id="div_isbn">
        <h1><asp:Literal id="lt_isbn" Text="" runat="server" /></h1>
        <p><asp:Image id="img_isbn" ImageUrl="./images/isbpic.gif" runat="server" /></p>
        <p><asp:Literal id="lt_isbn_details" Text="" runat="server" /></p>
    </div>
</asp:Content>

