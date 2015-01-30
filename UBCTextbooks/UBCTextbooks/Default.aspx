<%@ Page Language="C#" MasterPageFile="~/SiteTemplate.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="UBCTextbooks.Default" %>

<%@ MasterType VirtualPath="~/SiteTemplate.Master" %>
<asp:Content ID="home" ContentPlaceHolderID="Content" runat="server">

    <script type="text/javascript" src="javascript/epoch_classes.js"></script>

    <div id="div_buy">
        <h1>
            Buy Textbooks</h1>
        <div id="div_searchnav" style="width: 600px; text-align: center; margin: 60px;">
            <div style="text-align: left;">
                <asp:TextBox ID="txt_search" Font-Size="16" Height="24" Width="500" CssClass="textbox"
                    runat="server" />
                <asp:Button ID="btn_search" CssClass="button" Style="margin-top: 0px; margin-bottom: 0px;"
                    Text="Search" runat="server" OnClick="btn_search_Click" /><br />
                Search by:
                <input type="radio" id="rdb_coursecode" name="rdbtn_searchby" value="CourseCode"
                    checked="checked" />Course Code
                <input type="radio" id="rdb_title" name="rdbtn_searchby" value="Title" />Title
                <input type="radio" id="rdb_author" name="rdbtn_searchby" value="Author" />Author
                <input type="radio" id="rdb_isbn" name="rdbtn_searchby" value="ISBN" />ISBN
                <div id="div_criteriaheader">
                    More Search Critieria ▼</div>
                <div id="div_criteria">
                    <table>
                        <tr>
                            <td class="label">
                                Trade Area
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddl_location" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Price Range
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddl_price" />
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Oldest Posted Date
                            </td>
                            <td>
                                <asp:TextBox ID="txt_date" CssClass="textbox" runat="server" Width="150" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">Sort by:
                                <input type="radio" name="rdbtn_sortby" value="PostedTime" checked="checked" />Posted
                                Date
                                <input type="radio" name="rdbtn_sortby" value="Price" />Price</td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        UBCTextbooks is an online community for buying and selling UBC textbooks. Hopefully
        you will find it useful.
        <table>
            <tr>
                <td>
                    Features:
                </td>
                <td>
                    Countless bugs
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    Lots of rectangles
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    0 browser compatibility
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    Please report any <i>major</i> bug(otherwise I'd hang myself) to wedoqad@gmail.com. Flames are also welcome.
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
