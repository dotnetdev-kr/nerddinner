<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Find a Dinner
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">

<script src="http://dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2" type="text/javascript"></script>
<script src="/Scripts/Map.js?v=3" type="text/javascript"></script>

<h2>Find a Dinner</h2>

<div id="mapDivLeft">

    <div id="searchBox">
        <div class="search-text">Enter your location  or <%= Html.ActionLink("View All Upcoming Dinners", "Index", "Dinners") %>.</div>
        <%= Html.TextBox("Location") %>
        <input id="search" type="submit" value="Search" />
    </div>

<div id="theMap" style="width: 580px; height: 400px;"></div>
<%--    <div id="theMap"></div> --%>

</div>

<div id="mapDivRight">
    <div id="dinnerList"></div>
</div>

<script type="text/javascript">

    $(document).ready(function() {
        LoadMap();
        FindMostPopularDinners(10);
    });

    $("#search").click(ValidateAndFindDinners);

    $("#Location").keypress(function(evt) {
        if (evt.which == 13) {
            ValidateAndFindDinners();
        }
    });

    function ValidateAndFindDinners() {
        var where = jQuery.trim($("#Location").val());
        if (where.length < 1)
            return;

        FindDinnersGivenLocation(where);
    }

</script>

</asp:Content>
