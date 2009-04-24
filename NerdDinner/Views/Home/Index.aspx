<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    NerdDinner
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">

<script src="http://dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.2" type="text/javascript"></script>
<script src="/Scripts/MSAjaxHistoryBundle.js" type="text/javascript"></script>

<h2>Find a Dinner</h2>

<div id="mapDivLeft">

    <div id="searchBox">
        <div class="search-text">Enter your location  or <strong><%= Html.ActionLink("View All Upcoming Dinners", "Index", "Dinners") %></strong>.</div>
        <%= Html.TextBox("Location") %>
        <input id="search" type="submit" value="Search" />
    </div>

    <div id="theMap" style="width: 580px; height: 400px;"></div>

</div>

<h2>Popular Dinners</h2>
<div id="mapDivRight">
    <div id="dinnerList"></div>
</div>

<script type="text/javascript">
//<![CDATA[
    $(document).ready(function() {
        NerdDinner.LoadMap();

        Sys.Application.set_enableHistory(true);

        Sys.Application.add_navigate(OnNavigation);

        OnNavigation();
    });

    function OnNavigation(sender, args) {
        if (Sys.Application.get_stateString() === '') {
            $get('Location').value = '';
            NerdDinner.FindMostPopularDinners(8);
        }
        else {
            var where = Sys.Application._state.where;

            $get('Location').value = where;
            NerdDinner.FindDinnersGivenLocation(where);
        }
    }

    $("#search").click(ValidateAndFindDinners);

    $("#Location").keypress(function(evt) {
        if (evt.which == 13) {
            ValidateAndFindDinners();
        }
    });

    function ValidateAndFindDinners() {
        var where = $.trim($get('Location').value);
        
        if (where.length < 1)
            return;

        Sys.Application.addHistoryPoint({ 'where': where });

        NerdDinner.FindDinnersGivenLocation(where);
    }
//]]>
</script>

</asp:Content>
