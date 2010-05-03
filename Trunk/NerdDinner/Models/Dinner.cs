using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web.Mvc;
using NerdDinner.Helpers;
using System.ComponentModel.DataAnnotations;

namespace NerdDinner.Models
{
    [Bind(Include = "Title,Description,EventDate,Address,Country,ContactPhone,Latitude,Longitude")]
    [MetadataType(typeof(Dinner_Validation))]
    public partial class Dinner
    {
        public bool IsHostedBy(string userName)
        {
            return String.Equals(HostedById ?? HostedBy, userName, StringComparison.Ordinal);
        }

        public bool IsUserRegistered(string userName)
        {
            return RSVPs.Any(r => r.AttendeeNameId == userName || (r.AttendeeNameId == null && r.AttendeeName == userName));
        }
    }

    public class Dinner_Validation
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(50, ErrorMessage = "Title may not be longer than 50 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(265, ErrorMessage = "Description may not be longer than 256 characters")]
        public string Description { get; set; }

        public string HostedById { get; set; }

        [StringLength(256, ErrorMessage = "Hosted By name may not be longer than 20 characters")]
        public string HostedBy { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(50, ErrorMessage = "Address may not be longer than 50 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(30, ErrorMessage = "Country may not be longer than 30 characters")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Contact phone is required")]
        [StringLength(20, ErrorMessage = "Contact phone may not be longer than 20 characters")]
        public string ContactPhone { get; set; }
    }
}