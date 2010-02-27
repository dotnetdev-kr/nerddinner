<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.DateTime>" %>

<script src="/Scripts/jquery_ui_datepicker.js" type="text/javascript"></script>

     <%: Html.TextBox("", String.Format("{0:yyyy-MM-dd HH:mm}",Model)) %>
         <script type="text/javascript">
             $(function () {
                 $('#Dinner_EventDate').datetime({
                     userLang: 'en',
                     americanMode: true
                 });
             });
            </script>

<script src="/Scripts/timepicker_plug/timepicker.js" type="text/javascript"></script>
<link href="/Scripts/timepicker_plug/css/style.css" rel="stylesheet" type="text/css" />
<link href="/Scripts/smothness/jquery_ui_datepicker.css" rel="stylesheet" type="text/css" />