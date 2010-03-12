using System;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;
using System.Xml.Linq;
using NerdDinner.Helpers;
using NerdDinner.Models;

namespace NerdDinner
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    public class Services : DataService<NerdDinnerEntities>
    {
        IDinnerRepository dinnerRepository;
        //
        // Dependency Injection enabled constructors

        public Services()
            : this(new DinnerRepository()) {
        }

        public Services(IDinnerRepository repository)
        {
            dinnerRepository = repository;
        }

        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            // We're exposing both Dinners and RSVPs for read
            config.SetEntitySetAccessRule("Dinners", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("RSVPs", EntitySetRights.AllRead);
            config.SetServiceOperationAccessRule("DinnersNearMe", ServiceOperationRights.AllRead);
            config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
            #if DEBUG
            config.UseVerboseErrors = true;
            #endif
        }

        [WebGet]
        public IQueryable<Dinner> FindUpcomingDinners()
        {
            return dinnerRepository.FindUpcomingDinners();
        }

        // http://localhost:60848/Services.svc/DinnersNearMe?placeOrZip='12345'
        [WebGet]
        public IQueryable<Dinner> DinnersNearMe(string placeOrZip)
        {
            if (String.IsNullOrEmpty(placeOrZip)) return null; ;

            string url = "http://ws.geonames.org/postalCodeSearch?{0}={1}&maxRows=1&style=SHORT";
            url = String.Format(url, placeOrZip.IsNumeric() ? "postalcode" : "placename", placeOrZip);

            var result = HttpContext.Current.Cache[placeOrZip] as XDocument;
            if (result == null)
            {
                result = XDocument.Load(url);
                HttpContext.Current.Cache.Insert(placeOrZip, result, null, DateTime.Now.AddDays(1), System.Web.Caching.Cache.NoSlidingExpiration);
            }

            var LatLong = (from x in result.Descendants("code")
                           select new
                           {
                               Lat = (float)x.Element("lat"),
                               Long = (float)x.Element("lng")
                           }).First();

            var dinners = dinnerRepository.
                            FindByLocation(LatLong.Lat, LatLong.Long).
                            OrderByDescending(p => p.EventDate);
            return dinners;
        }

    }
}
