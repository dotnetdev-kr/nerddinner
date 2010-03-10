using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NerdDinner.Models;
using DDay.iCal;
using DDay.iCal.Components;
using DDay.iCal.Serialization;
using System.Web.Mvc;
using DDay.iCal.DataTypes;

namespace NerdDinner.Helpers
{
    public static class CalendarHelpers
    {
        public static string GetCalendarText(Dinner dinner)
        {
            string eventLink = "http://nrddnr.com/" + dinner.DinnerID;

            iCalendar iCal = new iCalendar();

            Event evt = iCal.Create<Event>();
            evt.Start = dinner.EventDate;
            evt.Duration = new TimeSpan(3, 0, 0); 
            evt.Location = dinner.Address;
            evt.Summary = "Nerd Dinner: " + dinner.Description;
            evt.Geo = new Geo(dinner.Latitude, dinner.Longitude);
            evt.Url = eventLink;
            evt.Description = eventLink;

            iCalendarSerializer serializer = new iCalendarSerializer(iCal);
            return serializer.SerializeToString();
        }
    }
}