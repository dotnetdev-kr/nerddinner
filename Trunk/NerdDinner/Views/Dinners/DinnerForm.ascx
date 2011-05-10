<%@ Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NerdDinner.Models.Dinner>" %>

<script src="<%=Url.Content("~/Scripts/MicrosoftAjax.js")%>" type="text/javascript"></script>
<script src="<%=Url.Content("~/Scripts/MicrosoftMvcAjax.js")%>" type="text/javascript"></script>
<script src="<%=Url.Content("~/Scripts/MicrosoftMvcValidation.js")%>" type="text/javascript"></script>

<% Html.EnableClientValidation(); %>
<%: Html.ValidationSummary("Please correct the errors and try again.") %>
    <% using (Html.BeginForm())
       { %>
    <fieldset>
        <div id="dinnerDiv">
            <%:Html.EditorForModel() %>
            <p>
                <input type="submit" value="Save" />
            </p>
        </div>
        <div id="mapDiv">
            <%: Html.EditorFor(m => m.Location) %>
        </div>
    </fieldset>
    <% } %>
    <script type="text/javascript">
//<![CDATA[
        $(document).ready(function () {
            NerdDinner.EnableMapMouseClickCallback();

            $("#Address").blur(function (evt) {
                //If it's time to look for an address, 
                // clear out the Lat and Lon
                $("#Latitude").val("0");
                $("#Longitude").val("0");
                var address = jQuery.trim($("#Address").val());
                if (address.length < 1)
                    return;
                NerdDinner.FindAddressOnMap(address);
            });
            var LL = new VELatLong(<%: Model.Location.Latitude %>,
                                   <%: Model.Location.Latitude %>);
            NerdDinner.LoadPin(LL, "Event", description);
        });
//]]>
    </script>

