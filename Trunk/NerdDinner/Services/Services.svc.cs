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
using NerdDinner.Services;

namespace NerdDinner
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    public class ODataServices : DataService<NerdDinnerEntities>
    {
        IDinnerRepository dinnerRepository;
        //
        // Dependency Injection enabled constructors

        public ODataServices()
            : this(new DinnerRepository()) {
        }

        public ODataServices(IDinnerRepository repository)
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

            LatLong location = GeolocationService.PlaceOrZipToLatLong(placeOrZip);

            var dinners = dinnerRepository.
                            FindByLocation(location.Lat, location.Long).
                            OrderByDescending(p => p.EventDate);
            return dinners;
        }

    }
}
