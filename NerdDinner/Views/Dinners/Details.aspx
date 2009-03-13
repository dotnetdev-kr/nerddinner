<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<NerdDinner.Models.Dinner>" MasterPageFile="~/Views/Shared/Site.Master"  %>

<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Html.Encode(Model.Title) %>
</asp:Content>

<asp:Content ID="details" ContentPlaceHolderID="MainContent" runat="server">

    <div id="dinnerDiv">

        <h2><%= Html.Encode(Model.Title) %></h2>
        <p>
            <strong>When:</strong> 
            <%= Model.EventDate.ToShortDateString() %> 

            <strong>@</strong>
            <%= Model.EventDate.ToShortTimeString() %>
        </p>
        <p>
            <strong>Where:</strong> 
            <%= Html.Encode(Model.Address) %>,
            <%= Html.Encode(Model.Country) %>
        </p>
         <p>
            <strong>Description:</strong> 
            <%= Html.Encode(Model.Description) %>
        </p>       
        <p>
            <strong>Organizer:</strong> 
            <%= Html.Encode(Model.HostedBy) %>
            (<%= Html.Encode(Model.ContactPhone) %>)
        </p>
        <% Html.RenderPartial("RSVPStatus"); %>
        <p id="whoscoming">
            <strong>Who's Coming:</strong>
            <%if (Model.RSVPs.Count == 0){%>
                  No one has registered.
            <% } %>
        </p>
        <%if(Model.RSVPs.Count > 0) {%>
        <ul class="attendees">
            <%foreach (var RSVP in Model.RSVPs){%>
              <li class="attendee"><%=Html.Encode(RSVP.AttendeeName.Replace("@"," at ")) %></li>    
            <% } %>
        </ul>
        <%} %>
        <% Html.RenderPartial("EditAndDeleteLinks"); %>    
        
    </div>
    
    <div id="mapDiv">
        <% Html.RenderPartial("map"); %>    
    </div>   
         
</asp:Content> 

