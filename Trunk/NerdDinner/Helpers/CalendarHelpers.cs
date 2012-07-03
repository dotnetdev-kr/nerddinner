using System;
using NerdDinner.Models;
using DDay.iCal;

namespace NerdDinner.Helpers
{
    public static class CalendarHelpers
    {
        public static Event DinnerToEvent(Dinner dinner, iCalendar iCal)
        {
            string eventLink = "http://nrddnr.com/" + dinner.DinnerID;
            Event evt = iCal.Create<Event>();
            evt.Start = new iCalDateTime(dinner.EventDate);
            evt.Duration = new TimeSpan(3, 0, 0);
            evt.Location = dinner.Address;
            evt.Summary = String.Format("{0} with {1}", dinner.Description, dinner.HostedBy);
            evt.Contacts.Add(dinner.ContactPhone);
            evt.GeographicLocation = new GeographicLocation(dinner.Latitude, dinner.Longitude);
            evt.Url = new Uri(eventLink);
            evt.Description = eventLink;
            return evt;
        }
    }
}