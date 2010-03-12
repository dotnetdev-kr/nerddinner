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
            return HostedBy.Equals(userName, StringComparison.InvariantCultureIgnoreCase);
        }

        public bool IsUserRegistered(string userName)
        {
            return RSVPs.Any(r => r.AttendeeName.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
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

        public string HostedBy { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Phone# is required")]
        public string ContactPhone { get; set; }
    }
}