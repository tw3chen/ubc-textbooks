<%@ Page Language="C#" MasterPageFile="~/SiteTemplate.Master" AutoEventWireup="true" CodeBehind="SellDetails.aspx.cs" Inherits="UBCTextbooks.SellDetails" %>
<%@ MasterType VirtualPath="~/SiteTemplate.Master" %>
<asp:Content id="selldetails" ContentPlaceHolderID="Content" runat="server">
    <div id="div_selldetails">
        <h1><asp:Literal id="lt_selldetails" Text="" runat="server" /></h1>
        <p><asp:Literal id="lt_selldetails_description" Text="" runat="server" /></p>
        <table id="tb_selldetails">
            <tr>
                <td class="label"><asp:Literal id="lt_condition" Text="" runat="server" /></td>
                <td><asp:DropDownList style="background-color:#F0F0F0;" id="ddl_ccondition" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_price" Text="" runat="server" /></td>
                <td><asp:TextBox id="txt_cprice" CssClass="textbox" runat="server" />
                    <asp:RequiredFieldValidator ID="req_cprice" runat="server" ErrorMessage="<%$ Resources: String, msg_requiredfield %>" ControlToValidate="txt_cprice" Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Literal id="lt_refprice" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:RegularExpressionValidator id="vld_cprice" runat="server" ErrorMessage="<%$ Resources: String, msg_pricenotvalid %>" ValidationExpression="^[0-9]{1,3}(\.[0-9]{1,2})?$" ControlToValidate="txt_cprice" Display="Dynamic" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_area" Text="" runat="server" /></td>
                <td><asp:DropDownList style="background-color:#F0F0F0;" id="ddl_clocation" runat="server" onchange="enableLtxt(this.value);" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_location" Text="" runat="server" /></td>
                <td><asp:TextBox id="txt_clocation" CssClass="textbox" Width="400" runat="server" /><asp:Hyperlink style="text-decoration:underline;color:blue;cursor:pointer;margin-left:5px;" id="hp_previewmap" Text="" CausesValidation="false" runat="server" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Literal id="lt_location_description" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td></td>
                <td><div id="map_canvas" style="width: 350px; height: 200px"></div></td>
            </tr>
            <tr>
                <td></td>
                <td style="color:Red;"><asp:Literal id="msg_mapinserterror" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td class="label"><asp:Literal id="lt_notes" Text="" runat="server" /></td>
                <td><asp:TextBox id="txt_cnotes" TextMode="MultiLine" Width="400" Height="50" runat="server" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Literal id="lt_notes_description" Text="" runat="server" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Button id="btn_confirm" CssClass="button" Text="" runat="server" 
                        onclick="btn_confirm_Click" /></td>
            </tr>
        </table>
    </div>
    <script src="http://maps.google.com/maps?file=api&amp;v=2&amp;sensor=false&amp;key=ABQIAAAAvqYYNYZ408xLN59l5OFVyBTfmLT69H07Hcj1PfelCpWB45oz6xRFsmuhpnumKWBkBzAx-bJEoJwHiw"
            type="text/javascript"></script>
    <script type="text/javascript">

        var map = null;
        var geocoder = null;

        initialize();
        enableLtxt('UBC');
        showAddress(document.getElementById('<%= ddl_clocation.ClientID %>').value);
        
        function enableLtxt(ddlvalue) {
            var txt_location = document.getElementById('<%= txt_clocation.ClientID %>');
            if (ddlvalue != 'UBC') {
                txt_location.disabled = false;
                txt_location.style.backgroundColor = 'white';
            }
            else {
                txt_location.disabled = true;
                txt_location.style.backgroundColor = '#F0F0F0';
                txt_location.value = "";
            }
        }
        function previewMap() {
            var ddlvalue = document.getElementById('<%= ddl_clocation.ClientID %>').value;
            var txtvalue = document.getElementById('<%= txt_clocation.ClientID %>').value;
            if (ddlvalue != 'UBC') {
                initialize();
                var address = txtvalue + ' ' + ddlvalue + ' BC Canada';
                showAddress(address);
            }
            else {
                initialize();
                showAddress(ddlvalue);
            }
        }
        
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
                                        if (!point) {
                                            document.getElementById('<%= msg_mapinserterror.ClientID %>').innerHTML = '<%# GetGlobalResourceObject("String", "msg_mapinserterror") %>';
                                        } else {
                                            map.setCenter(point, 13);
                                            var marker = new GMarker(point);
                                            map.addOverlay(marker);
                                        }
                                    }
                                );
            }
        }
    </script>
</asp:Content>
