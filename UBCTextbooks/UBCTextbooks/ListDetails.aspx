﻿<%@ Page Language="C#" MasterPageFile="~/SiteTemplate.Master" AutoEventWireup="true" CodeBehind="ListDetails.aspx.cs" Inherits="UBCTextbooks.ListDetails" %>
<%@ MasterType VirtualPath="~/SiteTemplate.Master" %>
<asp:Content id="listdetails" ContentPlaceHolderID="Content" runat="server">
<script type="text/javascript" src="http://www.google.com/jsapi?key=ABQIAAAAvqYYNYZ408xLN59l5OFVyBSY491GEJEiyfy4U63FiP8uySHT8RTClBAbt3rFExXlUP1OOmO80MiTYQ"></script>
    <script type="text/javascript">
        google.load("books", "0");

        function initialize() {
            var viewer = new google.books.DefaultViewer(document.getElementById('book_canvas'));
            viewer.load('ISBN:' + '<%= isbn %>', bookNotFound); //0520202007, 0738531367
        }

        function bookNotFound() {
            document.getElementById('book_canvas').style.display = 'none';
        }

        google.setOnLoadCallback(initialize);
     </script>
    <div id="div_details">
        <h1><asp:Literal id="lt_listdetails" Text="" runat="server" /></h1>
        <table id="tb_confirmtb">
            <tr>
                <td class="label"><asp:Literal id="lt_bookimage" Text="" runat="server" /></td>
                <td><asp:Image id="img_book" ImageUrl="~/images/imageua.gif" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_title" Text="" runat="server" /></td>
                <td><asp:Literal id="lt_ltitle" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_author" Text="" runat="server" /></td>
                <td><asp:Literal id="lt_lauthor" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_edition" Text="" runat="server" /></td>
                <td><asp:Literal id="lt_ledition" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_coursecode" Text="" runat="server" /></td>
                <td><asp:Literal id="lt_lcoursecode" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_isbn" Text="" runat="server" /></td>
                <td><asp:Literal id="lt_lisbn" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_binding" Text="" runat="server" /></td>
                <td><asp:Literal id="lt_lbinding" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_manufacturer" Text="" runat="server" /></td>
                <td><asp:Literal id="lt_lmanufacturer" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_area" Text="" runat="server" /></td>
                <td><asp:Literal id="lt_larea" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_location" Text="" runat="server" /></td>
                <td><asp:Literal id="lt_llocation" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_price" Text="" runat="server" /></td>
                <td><asp:Literal id="lt_lprice" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_refprices" Text="" runat="server" /></td>
                <td><asp:Literal id="lt_lrefprice" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_condition" Text="" runat="server" /></td>
                <td><asp:Literal id="lt_lcondition" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_numberofoffers" Text="" runat="server" /></td>
                <td><asp:Literal id="lt_lnumberofoffers" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_timelisted" Text="" runat="server" /></td>
                <td><asp:Literal id="lt_ltimelisted" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_notes" Text="" runat="server" /></td>
                <td><asp:Literal id="lt_lnotes" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_posterdisplayname" Text="" runat="server" /></td>
                <td><asp:HyperLink id="lt_lposterdisplayname" Text="" runat="server" /></td>
            </tr>
        </table>
        <div id="book_canvas" style="width:500px;height:400px;float:right;"></div>
        <div id="map_canvas" style="width:350px;height:200px"></div>
        <div style="margin:35px;clear:both;">
            <asp:Repeater id="rp_comments" runat="server">
            <ItemTemplate>
                <div class="<%#DataBinder.Eval(Container.DataItem, "OfferOrComment")%>">
                    <div><span class="c_name"><%#DataBinder.Eval(Container.DataItem, "DisplayName")%></span>
                    <span class="c_time"> at <%#DataBinder.Eval(Container.DataItem, "TimePosted")%></span>
                    <span class="c_mailaddress"> contact email: <%#DataBinder.Eval(Container.DataItem, "EmailAddress")%></span></div>
                    <div><%#DataBinder.Eval(Container.DataItem, "Comments").ToString().Replace("\r\n", "<br />")%></div>
                </div>
            </ItemTemplate>
            </asp:Repeater>
        </div>
        <table style="margin:20px;">
            <tr style="font-size:20px;">
                <td colspan="2"><asp:Literal id="lt_offercomments" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_displayname" Text="" runat="server" /></td>
                <td><asp:TextBox id="txt_displayname" CssClass="textbox" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_emailaddress" Text="" runat="server" /></td>
                <td><asp:TextBox id="txt_emailaddress" CssClass="textbox" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_comments" Text="" runat="server" /></td>
                <td><asp:TextBox id="txt_comments" TextMode="MultiLine" Width="600" Height="100" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="2"><asp:Literal id="lt_reload" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td><asp:Button id="btn_offer" CssClass="button" Text="" runat="server" 
                        onclick="btn_offer_Click" /></td>
                <td><asp:Button id="btn_comment" CssClass="button" Text="" runat="server" 
                        onclick="btn_comment_Click" /></td>
            </tr>
        </table>
    </div>
    <script src="http://maps.google.com/maps?file=api&amp;v=2&amp;sensor=false&amp;key=ABQIAAAAvqYYNYZ408xLN59l5OFVyBTfmLT69H07Hcj1PfelCpWB45oz6xRFsmuhpnumKWBkBzAx-bJEoJwHiw"
            type="text/javascript"></script>
    <script type="text/javascript">
        var map = null;
        var geocoder = null;
        var address = null;
        if ('<%= location %>' != '')
            address = '<%= location %>' + ' ' + '<%= area %>' + ' BC Canada';
        else
            address = '<%= area %>' + ' BC Canada';

        initialize();
        showAddress(address);
        
        function initialize() {
            if (GBrowserIsCompatible()) {
                map = new GMap2(document.getElementById("map_canvas"));
                geocoder = new GClientGeocoder();
                map.setUIToDefault();
            }
        }

        function showAddress(address) {
            if (geocoder) {
                geocoder.getLatLng(address,
                                    function(point) {
                                            map.setCenter(point, 13);
                                            var marker = new GMarker(point);
                                            map.addOverlay(marker);
                                    }
                                );
            }
        }
    </script>
</asp:Content>